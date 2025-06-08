public interface IMedicalRecordRepository
{
    Task<MedicalRecord?> GetByIdAsync(int id);
    Task<IEnumerable<MedicalRecord>> GetAllAsync();
    Task<int> AddAsync(MedicalRecord record);
    Task<bool> UpdateAsync(MedicalRecord record);
    Task<bool> DeleteAsync(int id);
}