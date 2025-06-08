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
            .Where(a => a.Date >= from && a.Date <= to);

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
            var from = filter.From.Value.Date;
            var to = filter.To ?? from.AddDays(1).AddTicks(-1);
            query = query.Where(a => a.Date >= from && a.Date <= to);
        }
        else if (filter.To.HasValue)
        {
            query = query.Where(a => a.Date <= filter.To.Value);
        }

        return query;
    }

    private IQueryable<Appointment> ApplySorting(IQueryable<Appointment> query, AppointmentFilterDto filter)
    {
        return filter.SortBy?.ToLower() switch
        {
            "date" => filter.Desc ? query.OrderByDescending(a => a.Date) : query.OrderBy(a => a.Date),
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


}