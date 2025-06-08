public interface IMedicalImageService
{
    Task<int> AddAsync(CreateMedicalImageDto dto);
    Task<bool> UpdateAsync(UpdateMedicalImageDto dto);
    Task<bool> DeleteAsync(int id);
    Task<MedicalImageDto> GetByIdAsync(int id);
    Task<IEnumerable<MedicalImageDto>> GetAllAsync();
    Task<IEnumerable<MedicalImageDto>> GetByMedicalRecordIdAsync(int recordId);
}