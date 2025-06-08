public interface IDoctorRepository
{
    Task<int?> AddAsync(Doctor doctor);
    Task<bool> UpdateAsync(Doctor doctor);
    Task<bool> DeleteAsync(int id);
    Task<IEnumerable<Doctor>> GetAllAsync();
    Task<Doctor> GetByIdAsync(int id);
    Task<IEnumerable<Doctor>> SearchByNameAsync(string name);
    Task<IEnumerable<Doctor>> SearchBySpecializationAsync(int specializationId);
}