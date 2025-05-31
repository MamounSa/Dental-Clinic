public interface IPatientService
{
    Task<int?> AddPatientAsync(PatientDto patientDto);
    Task<bool> UpdatePatientAsync(PatientDto patientDto);
    Task<bool> DeletePatientAsync(int id);
    Task<IEnumerable<PatientDto>> GetAllPatientsAsync();
    Task<PatientDto> GetPatientByIdAsync(int id);
    Task<IEnumerable<PatientDto>> SearchByNameAsync(string name);
    Task<IEnumerable<PatientDto>> SearchByEmailAsync(string email);
    Task<IEnumerable<PatientDto>> SearchByPhoneAsync(string phoneNumber);
}
