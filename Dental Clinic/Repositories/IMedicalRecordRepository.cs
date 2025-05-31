public interface IMedicalRecordRepository
{
    Task<int?> AddMedicalRecordAsync(MedicalRecord medicalRecord);
    Task<bool> UpdateMedicalRecordAsync(MedicalRecord medicalRecord);
    Task<bool> DeleteMedicalRecordAsync(int id);
    Task<IEnumerable<MedicalRecord>> GetAllMedicalRecordsAsync();
    Task<MedicalRecord> GetMedicalRecordByIdAsync(int id);
    Task<IEnumerable<MedicalRecord>> GetMedicalRecordsByPatientIdAsync(int patientId);

    Task<IEnumerable<MedicalRecord>> SearchByPatientNameAsync(string patientName);

    Task<bool> AddMedicalImagesAsync(IEnumerable<MedicalImage> images); // ✅ إضافة الصور عبر `Repository`
}
