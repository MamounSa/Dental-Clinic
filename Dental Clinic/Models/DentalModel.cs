public class DentalModel
{
    public int Id { get; set; }
    public int ToothNumber { get; set; } // رقم السن (1-32)
    public string Condition { get; set; } // حالة السن (سليم، مسوس، مفقود...)

    public int PatientId { get; set; } // ربط السن بالمريض
    public Patient Patient { get; set; }

    public int MedicalRecordId { get; set; } // ربط السن بالسجل الطبي
    public MedicalRecord MedicalRecord { get; set; }

    public ICollection<DentalTreatment> Treatments { get; set; } = new List<DentalTreatment>(); // قائمة العلاجات لكل سن

}
