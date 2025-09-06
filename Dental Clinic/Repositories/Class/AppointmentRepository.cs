public class AppointmentRepository : IAppointmentRepository
{
    private readonly AppDbContext _context;

    public AppointmentRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Appointment>> GetAllAsync()
    {
        return await _context.Appointments.ToListAsync();
    }

    public async Task<Appointment> GetByIdAsync(int id)
    {
        return await _context.Appointments.FindAsync(id);
    }

    public async Task<IEnumerable<Appointment>> GetByDoctorIdAsync(int doctorId)
    {
        return await _context.Appointments
            .Where(a => a.DoctorId == doctorId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Appointment>> GetByPatientIdAsync(int patientId)
    {
        return await _context.Appointments
            .Where(a => a.PatientId == patientId)
            .ToListAsync();
    }

    public async Task<int> AddAsync(Appointment appointment)
    {
        await _context.Appointments.AddAsync(appointment);
        await _context.SaveChangesAsync();
        return appointment.Id;
    }

    public async Task<bool> UpdateAsync(Appointment appointment)
    {
        var existing = await _context.Appointments.FindAsync(appointment.Id);
        if (existing == null) return false;

        _context.Entry(existing).CurrentValues.SetValues(appointment);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var appointment = await _context.Appointments.FindAsync(id);
        if (appointment == null) return false;

        _context.Appointments.Remove(appointment);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<Appointment>> GetAppointmentsInRangeAsync(DateTime from, DateTime to, int? doctorId)
    {
        var query = _context.Appointments
            .Include(a => a.Doctor)
            .Where(a => a.Start >= from && a.Start <= to);

        if (doctorId.HasValue)
            query = query.Where(a => a.DoctorId == doctorId);

        return await query.ToListAsync();
    }

    public async Task<IEnumerable<Appointment>> FilterAppointmentsAsync(AppointmentFilterDto filter)
    {
        var query = _context.Appointments.AsQueryable();

        query = ApplyFiltering(query, filter);
        query = ApplySorting(query, filter);
        query = ApplyPagination(query, filter);

        return await query.ToListAsync();
    }

    private IQueryable<Appointment> ApplyFiltering(IQueryable<Appointment> query, AppointmentFilterDto filter)
    {
        if (filter.DoctorId.HasValue)
            query = query.Where(a => a.DoctorId == filter.DoctorId.Value);

        if (filter.PatientId.HasValue)
            query = query.Where(a => a.PatientId == filter.PatientId.Value);

        if (!string.IsNullOrEmpty(filter.Status))
            query = query.Where(a => a.Status == filter.Status);

        if (filter.From.HasValue)
        {
            var from = filter.From.Value;
            var to = filter.To ?? from.Date.AddDays(1).AddTicks(-1);
            query = query.Where(a => a.Start >= from && a.Start <= to);
        }
        else if (filter.To.HasValue)
        {
            query = query.Where(a => a.Start <= filter.To.Value);
        }

        if (filter.FromTime.HasValue)
        {
            query = filter.ToTime.HasValue ? 
                query.Where(a =>a.Start.TimeOfDay >= filter.FromTime.Value &&
                a.Start.TimeOfDay <= filter.ToTime.Value)
                :
                query = query.Where(a => a.Start.TimeOfDay >= filter.FromTime.Value);

        }
        else if (filter.ToTime.HasValue)
        {
            query = query.Where(a => a.Start.TimeOfDay <= filter.ToTime.Value);
        }

        return query;
    }


    private IQueryable<Appointment> ApplySorting(IQueryable<Appointment> query, AppointmentFilterDto filter)
    {
        return filter.SortBy?.ToLower() switch
        {
            "date" => filter.Desc ? query.OrderByDescending(a => a.Start) : query.OrderBy(a => a.Start),
            "status" => filter.Desc ? query.OrderByDescending(a => a.Status) : query.OrderBy(a => a.Status),
            "doctor" => filter.Desc ? query.OrderByDescending(a => a.DoctorId) : query.OrderBy(a => a.DoctorId),
            "patient" => filter.Desc ? query.OrderByDescending(a => a.PatientId) : query.OrderBy(a => a.PatientId),
            _ => query.OrderBy(a => a.Id),
        };
    }

    private IQueryable<Appointment> ApplyPagination(IQueryable<Appointment> query, AppointmentFilterDto filter)
    {
        return query
            .Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize);
    }

    public async Task<bool> HasConflictAsync(Appointment appointment)
    {
        return await _context.Appointments.AnyAsync(a =>
            a.Id !=appointment.Id &&
            (a.DoctorId == appointment.DoctorId) && a.Status=="مؤكد" &&
            a.Start < appointment.End &&
            a.End > appointment.Start
        );
    }
    public async Task<bool> UpdateAttendanceStatusAsync(int appointmentId, AttendanceStatus status)
    {
        var appointment = await _context.Appointments.FindAsync(appointmentId);
        if (appointment == null) return false;

        appointment.AttendanceStatus = status;

        switch (status)
        {
            case AttendanceStatus.CheckedIn:
                appointment.CheckInTime = DateTime.Now;
                break;
            case AttendanceStatus.CheckedOut:
                appointment.CheckOutTime = DateTime.Now;
                break;
            case AttendanceStatus.NoShow:
                appointment.CheckInTime = null;
                appointment.CheckOutTime = null;
                break;
        }

        await _context.SaveChangesAsync();
        return true;
    }


}