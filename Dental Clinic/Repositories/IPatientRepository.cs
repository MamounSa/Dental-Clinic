public interface IPatientRepository
{
    Task<int?> AddPatientAsync(Patient patient);
    Task<bool> UpdatePatientAsync(Patient patient);
    Task<bool> DeletePatientAsync(int id);
    Task<IEnumerable<Patient>> GetAllPatientsAsync();
    Task<Patient> GetPatientByIdAsync(int id);
    Task<IEnumerable<Patient>> SearchByNameAsync(string name);
    Task<IEnumerable<Patient>> SearchByEmailAsync(string email);
    Task<IEnumerable<Patient>> SearchByPhoneAsync(string phoneNumber);
}
