public class MedicalRecordDto
{
    public int Id { get; set; }
    public DateTime RecordDate { get; set; }
    public string Diagnosis { get; set; }
    public string Medications { get; set; }
    public string Notes { get; set; }

    public int PatientId { get; set; }
    public string PatientName { get; set; }

    public List<MedicalImageDto> Images { get; set; } = new List<MedicalImageDto>(); // ✅ دعم الصور
}
