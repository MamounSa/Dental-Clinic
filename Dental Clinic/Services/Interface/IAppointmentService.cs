public interface IAppointmentService
{
    Task<IEnumerable<AppointmentDto>> GetAllAsync();
    Task<AppointmentDto> GetByIdAsync(int id);
    Task<IEnumerable<AppointmentDto>> GetByDoctorIdAsync(int doctorId);
    Task<IEnumerable<AppointmentDto>> GetByPatientIdAsync(int patientId);
    Task<int> AddAsync(CreateAppointmentDto dto);
    Task<bool> UpdateAsync(UpdateAppointmentDto dto);
    Task<bool> DeleteAsync(int id);

    Task<AppointmentsReportDto> GetAppointmentsReportAsync(DateTime from, DateTime to, int? doctorId);
    Task<WeeklyCalendarDto> GetWeeklyCalendarAsync(DateTime anyDateInWeek, int? doctorId = null);
    Task<IEnumerable<AppointmentDto>> FilterAppointmentsAsync(AppointmentFilterDto filter);
}