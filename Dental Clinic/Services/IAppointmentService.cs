public interface IAppointmentService
{
    Task<int?> AddAppointmentAsync(AppointmentDto appointmentDto);
    Task<bool> UpdateAppointmentAsync(AppointmentDto appointmentDto);
    Task<bool> DeleteAppointmentAsync(int id);
    Task<IEnumerable<AppointmentDto>> GetAllAppointmentsAsync();
    Task<AppointmentDto> GetAppointmentByIdAsync(int id);
    Task<IEnumerable<AppointmentDto>> GetAppointmentsByDoctorIdAsync(int doctorId);
    Task<IEnumerable<AppointmentDto>> GetAppointmentsByPatientIdAsync(int patientId);

    Task<IEnumerable<AppointmentDto>> SearchByDateAsync(DateTime date);
}
