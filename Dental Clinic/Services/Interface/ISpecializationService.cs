public interface ISpecializationService
{
    Task<IEnumerable<SpecializationDto>> GetAllAsync();
    Task<SpecializationDto?> GetByIdAsync(int id);
    Task<int> AddAsync(CreateSpecializationDto dto);
    Task<bool> UpdateAsync(UpdateSpecializationDto dto);
    Task<bool> DeleteAsync(int id);
}