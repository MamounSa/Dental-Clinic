public interface IDoctorRepository
{
    Task<int?> AddDoctorAsync(Doctor doctor);
    Task<bool> UpdateDoctorAsync(Doctor doctor);
    Task<bool> DeleteDoctorAsync(int id);
    Task<IEnumerable<Doctor>> GetAllDoctorsAsync();
    Task<Doctor> GetDoctorByIdAsync(int id);
    Task<IEnumerable<Doctor>> SearchByNameAsync(string name);
    Task<IEnumerable<Doctor>> SearchByEmailAsync(string email);
    Task<IEnumerable<Doctor>> SearchByPhoneAsync(string phoneNumber);
    Task<IEnumerable<Doctor>> GetDoctorsBySpecializationAsync(int specializationId);
}
