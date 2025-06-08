public interface IDoctorService
{
    Task<int?> AddAsync(CreateDoctorDto dto);
    Task<bool> UpdateAsync(UpdateDoctorDto dto);
    Task<bool> DeleteAsync(int id);
    Task<IEnumerable<DoctorDto>> GetAllAsync();
    Task<DoctorDto> GetByIdAsync(int id);
    Task<IEnumerable<DoctorDto>> SearchByNameAsync(string name);
    Task<IEnumerable<DoctorDto>> GetBySpecializationAsync(int specializationId);
}