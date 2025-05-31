public class MedicalRecord
{
    public int Id { get; set; }
    public DateTime RecordDate { get; set; }
    public string Diagnosis { get; set; }
    public string Medications { get; set; }
    public string Notes { get; set; }

    public int PatientId { get; set; }
    public Patient Patient { get; set; }

    public ICollection<MedicalImage> Images { get; set; } = new List<MedicalImage>(); // ✅ إضافة الصور
}
