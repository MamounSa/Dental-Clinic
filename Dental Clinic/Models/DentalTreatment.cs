public class DentalTreatment
{
    public int Id { get; set; }
    public string TreatmentType { get; set; } // نوع العلاج (حشوة، خلع، تقويم...)
    public DateTime TreatmentDate { get; set; } // تاريخ العلاج
    public string Notes { get; set; } // ملاحظات إضافية

    public int DoctorId { get; set; } // الطبيب الذي قام بالإجراء
    public Doctor Doctor { get; set; }

    public int DentalModelId { get; set; } // ربط العلاج بالسن المحدد
    public DentalModel DentalModel { get; set; }
}
