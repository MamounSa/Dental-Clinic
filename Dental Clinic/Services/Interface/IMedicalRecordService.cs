public interface IMedicalRecordService
{
    Task<MedicalRecordDto?> GetByIdAsync(int id);
    Task<IEnumerable<MedicalRecordDto>> GetAllAsync();
    Task<int> AddAsync(CreateMedicalRecordDto dto);
    Task<bool> UpdateAsync(UpdateMedicalRecordDto dto);
    Task<bool> DeleteAsync(int id);
}