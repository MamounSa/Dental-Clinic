public interface IPatientService
{
    Task<int> AddAsync(CreatePatientDto dto);
    Task<bool> UpdateAsync(UpdatePatientDto dto);
    Task<bool> DeleteAsync(int id);
    Task<IEnumerable<PatientDto>> GetAllAsync();
    Task<PatientDto> GetByIdAsync(int id);
    Task<IEnumerable<PatientDto>> SearchByNameAsync(string name);
}