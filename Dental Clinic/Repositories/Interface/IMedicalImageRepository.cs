public interface IMedicalImageRepository
{
    Task<int> AddAsync(MedicalImage image);
    Task<bool> UpdateAsync(MedicalImage image);
    Task<bool> DeleteAsync(int id);
    Task<MedicalImage> GetByIdAsync(int id);
    Task<IEnumerable<MedicalImage>> GetAllAsync();
    Task<IEnumerable<MedicalImage>> GetByMedicalRecordIdAsync(int medicalRecordId);
}