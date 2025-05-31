public interface ISpecializationService
{
    Task<IEnumerable<SpecializationDto>> GetAllSpecializationsAsync();
    Task<SpecializationDto> GetSpecializationByIdAsync(int id);
    Task<int?> AddSpecializationAsync(SpecializationDto specializationDto);
    Task<bool> UpdateSpecializationAsync(SpecializationDto specializationDto);
    Task<bool> DeleteSpecializationAsync(int id);
    Task<bool> DoesSpecializationExistAsync(int id);
}
