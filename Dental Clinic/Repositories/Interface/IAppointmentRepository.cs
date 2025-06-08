public interface IAppointmentRepository
{
    Task<IEnumerable<Appointment>> GetAllAsync();
    Task<Appointment> GetByIdAsync(int id);
    Task<IEnumerable<Appointment>> GetByDoctorIdAsync(int doctorId);
    Task<IEnumerable<Appointment>> GetByPatientIdAsync(int patientId);
    Task<int> AddAsync(Appointment appointment);
    Task<bool> UpdateAsync(Appointment appointment);
    Task<bool> DeleteAsync(int id);

    Task<List<Appointment>> GetAppointmentsInRangeAsync(DateTime from, DateTime to, int? doctorId);

    Task<IEnumerable<Appointment>> FilterAppointmentsAsync(AppointmentFilterDto filter);

   
}