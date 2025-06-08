public interface IDentalModelRepository
{
    Task<IEnumerable<DentalModel>> GetAllAsync();
    Task<DentalModel> GetByIdAsync(int id);
    Task<int?> CreateAsync(DentalModel model);
    Task<bool> UpdateAsync(DentalModel model);
    Task<bool> DeleteAsync(int id);
}