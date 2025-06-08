public interface ISpecializationRepository
{
    Task<IEnumerable<Specialization>> GetAllAsync();
    Task<Specialization?> GetByIdAsync(int id);
    Task<int> AddAsync(Specialization specialization);
    Task<bool> UpdateAsync(Specialization specialization);
    Task<bool> DeleteAsync(int id);
}