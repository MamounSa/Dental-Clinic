public interface IDentalModelService
{
    Task<IEnumerable<DentalModelDto>> GetAllAsync();
    Task<DentalModelDto> GetByIdAsync(int id);
    Task<int?> CreateAsync(CreateDentalModelDto dto);
    Task<bool> UpdateAsync(UpdateDentalModelDto dto);
    Task<bool> DeleteAsync(int id);
}