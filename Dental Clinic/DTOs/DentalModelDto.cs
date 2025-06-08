using System.ComponentModel.DataAnnotations;

/// <summary>
/// DTO يُستخدم لإنشاء سجل جديد لسن.
/// </summary>
public class CreateDentalModelDto
{
    /// <summary>
    /// رقم السن (بين 1 و 32).
    /// </summary>
    [Required(ErrorMessage = "رقم السن مطلوب")]
    [Range(1, 32, ErrorMessage = "رقم السن يجب أن يكون بين 1 و 32")]
    public int ToothNumber { get; set; }

    /// <summary>
    /// حالة السن (سليم، مسوس، مفقود...).
    /// </summary>
    [Required(ErrorMessage = "حالة السن مطلوبة")]
    [StringLength(50, ErrorMessage = "حالة السن يجب ألا تتجاوز 50 حرفًا")]
    public string Condition { get; set; }

    /// <summary>
    /// معرف المريض المرتبط بهذا السن.
    /// </summary>
    [Required(ErrorMessage = "معرف المريض مطلوب")]
    public int PatientId { get; set; }

    /// <summary>
    /// معرف السجل الطبي المرتبط بهذا السن.
    /// </summary>
    [Required(ErrorMessage = "معرف السجل الطبي مطلوب")]
    public int MedicalRecordId { get; set; }
}



/// <summary>
/// DTO لتحديث حالة سن موجود.
/// </summary>
public class UpdateDentalModelDto
{
    /// <summary>
    /// المعرف الفريد للسن المطلوب تحديثه.
    /// </summary>
    [Required(ErrorMessage = "معرف السن مطلوب")]
    public int Id { get; set; }

    /// <summary>
    /// الحالة الجديدة للسن (سليم، مسوس، إلخ).
    /// </summary>
    [Required(ErrorMessage = "الحالة مطلوبة")]
    [StringLength(50, ErrorMessage = "حالة السن يجب ألا تتجاوز 50 حرفًا")]
    public string Condition { get; set; }
}


/// <summary>
/// DTO يُستخدم لعرض تفاصيل السن.
/// </summary>
public class DentalModelDto
{
    /// <summary>
    /// معرف السن.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// رقم السن (1 - 32).
    /// </summary>
    public int ToothNumber { get; set; }

    /// <summary>
    /// حالة السن.
    /// </summary>
    public string Condition { get; set; }

    /// <summary>
    /// معرف المريض المرتبط.
    /// </summary>
    public int PatientId { get; set; }

    /// <summary>
    /// معرف السجل الطبي المرتبط.
    /// </summary>
    public int MedicalRecordId { get; set; }

    /// <summary>
    /// قائمة العلاجات المرتبطة بهذا السن.
    /// </summary>
   // public List<DentalTreatmentDto> Treatments { get; set; }
}