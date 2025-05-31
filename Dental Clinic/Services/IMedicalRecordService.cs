public interface IMedicalRecordService
{
    Task<int?> AddMedicalRecordAsync(MedicalRecordDto medicalRecordDto);
    Task<bool> UpdateMedicalRecordAsync(MedicalRecordDto medicalRecordDto);
    Task<bool> DeleteMedicalRecordAsync(int id);
    Task<IEnumerable<MedicalRecordDto>> GetAllMedicalRecordsAsync();
    Task<MedicalRecordDto> GetMedicalRecordByIdAsync(int id);
    Task<IEnumerable<MedicalRecordDto>> GetMedicalRecordsByPatientIdAsync(int patientId);

    Task<IEnumerable<MedicalRecordDto>> SearchByPatientNameAsync(string patientName);

}
