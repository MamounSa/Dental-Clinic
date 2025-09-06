public class AppointmentService : IAppointmentService
{
    private readonly IAppointmentRepository _repo;
    private readonly IMapper _mapper;

    public AppointmentService(IAppointmentRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<IEnumerable<AppointmentDto>> GetAllAsync()
    {
        var data = await _repo.GetAllAsync();
        return _mapper.Map<IEnumerable<AppointmentDto>>(data);
    }

    public async Task<AppointmentDto> GetByIdAsync(int id)
    {
        var entity = await _repo.GetByIdAsync(id);
        return _mapper.Map<AppointmentDto>(entity);
    }

    public async Task<IEnumerable<AppointmentDto>> GetByDoctorIdAsync(int doctorId)
    {
        var data = await _repo.GetByDoctorIdAsync(doctorId);
        return _mapper.Map<IEnumerable<AppointmentDto>>(data);
    }

    public async Task<IEnumerable<AppointmentDto>> GetByPatientIdAsync(int patientId)
    {
        var data = await _repo.GetByPatientIdAsync(patientId);
        return _mapper.Map<IEnumerable<AppointmentDto>>(data);
    }

    public async Task<int> AddAsync(CreateAppointmentDto dto)
    {
        
        var entity = _mapper.Map<Appointment>(dto);
        var hasConflict = await _repo.HasConflictAsync(entity);
        if (hasConflict)
            return -1;
        return await _repo.AddAsync(entity);
    }

    public async Task<bool> UpdateAsync(UpdateAppointmentDto dto)
    {
        var entity = _mapper.Map<Appointment>(dto);
        var hasConflict = await _repo.HasConflictAsync(entity);
        if (hasConflict)
            return false;
        return await _repo.UpdateAsync(entity);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _repo.DeleteAsync(id);
    }

    public async Task<AppointmentsReportDto> GetAppointmentsReportAsync(DateTime from, DateTime to, int? doctorId)
    {
        var appointments = await _repo.GetAppointmentsInRangeAsync(from, to, doctorId);

        return new AppointmentsReportDto
        {
            TotalAppointments = appointments.Count,
            Completed = appointments.Count(a => a.Status == "تم"),
            Cancelled = appointments.Count(a => a.Status == "ملغى"),
            NoShows = appointments.Count(a => a.Status == "لم يحضر"),
            DoctorName = doctorId.HasValue ? appointments.FirstOrDefault()?.Doctor?.Name : null,
            DateRange = $"{from:yyyy-MM-dd} to {to:yyyy-MM-dd}"
        };
    }

    public async Task<WeeklyCalendarDto> GetWeeklyCalendarAsync(DateTime anyDateInWeek, int? doctorId = null)
    {
        // نحدد بداية الأسبوع بالإعتماد على التاريخ المدخل
        DateTime weekStart = GetStartOfWeek(anyDateInWeek, DayOfWeek.Monday);
        DateTime weekEnd = weekStart.AddDays(6);

        var appointments = await _repo.GetAppointmentsInRangeAsync(weekStart, weekEnd, doctorId);

        var grouped = appointments
            .GroupBy(a => a.Start)
            .ToDictionary(g => g.Key, g => _mapper.Map<List<AppointmentDto>>(g.ToList()));

        return new WeeklyCalendarDto
        {
            WeekStart = weekStart,
            WeekEnd = weekEnd,
            AppointmentsByDay = grouped
        };
    }

    private DateTime GetStartOfWeek(DateTime date, DayOfWeek startOfWeek)
    {
        int diff = (7 + (date.DayOfWeek - startOfWeek)) % 7;
        return date.AddDays(-diff).Date;
    }

    public async Task<IEnumerable<AppointmentDto>> FilterAppointmentsAsync(AppointmentFilterDto filter)
    {
        var data = await _repo.FilterAppointmentsAsync(filter);
        
        return _mapper.Map<IEnumerable<AppointmentDto>>(data);
    }

    public async Task<bool> UpdateAttendanceStatusAsync(UpdateAttendanceDto dto)
    {
        return await _repo.UpdateAttendanceStatusAsync(dto.AppointmentId, dto.AttendanceStatus);
    }




}