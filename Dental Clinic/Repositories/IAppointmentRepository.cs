public interface IAppointmentRepository
{
    Task<int?> AddAppointmentAsync(Appointment appointment);
    Task<bool> UpdateAppointmentAsync(Appointment appointment);
    Task<bool> DeleteAppointmentAsync(int id);
    Task<IEnumerable<Appointment>> GetAllAppointmentsAsync();
    Task<Appointment> GetAppointmentByIdAsync(int id);
    Task<IEnumerable<Appointment>> GetAppointmentsByDoctorIdAsync(int doctorId);
    Task<IEnumerable<Appointment>> GetAppointmentsByPatientIdAsync(int patientId);

    Task<IEnumerable<Appointment>> SearchByDateAsync(DateTime date);
}
