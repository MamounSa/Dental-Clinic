public interface IPatientRepository
{
    Task<int> AddAsync(Patient patient);
    Task<bool> UpdateAsync(Patient patient);
    Task<bool> DeleteAsync(int id);
    Task<IEnumerable<Patient>> GetAllAsync();
    Task<Patient> GetByIdAsync(int id);
    Task<IEnumerable<Patient>> SearchByNameAsync(string name);
}