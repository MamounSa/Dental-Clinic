public interface ISpecializationRepository
{
    Task<IEnumerable<Specialization>> GetAllSpecializationsAsync();
    Task<Specialization> GetSpecializationByIdAsync(int id);
    Task<int?> AddSpecializationAsync(Specialization specialization);
    Task<bool> UpdateSpecializationAsync(Specialization specialization);
    Task<bool> DeleteSpecializationAsync(int id);
    Task<bool> DoesSpecializationExistAsync(int id);
}
