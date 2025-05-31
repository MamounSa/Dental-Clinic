public class MedicalImage
{
    public int Id { get; set; }
    public string ImageUrl { get; set; } // ✅ رابط الصورة المخزنة
    public string Description { get; set; } // ✅ وصف الصورة

    public int MedicalRecordId { get; set; }
    public MedicalRecord MedicalRecord { get; set; }
}
