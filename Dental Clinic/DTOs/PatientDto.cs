using System.ComponentModel.DataAnnotations;


/// <summary>
/// تفاصيل المريض.
/// </summary>
public class PatientDto
{
    public int Id { get; set; }

    /// <summary>
    /// الاسم الكامل للمريض.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// تاريخ الميلاد.
    /// </summary>
    public DateTime DateOfBirth { get; set; }

    /// <summary>
    /// الجنس (ذكر/أنثى).
    /// </summary>
    public string Gender { get; set; }

    /// <summary>
    /// رقم الهاتف.
    /// </summary>
    public string PhoneNumber { get; set; }

    /// <summary>
    /// البريد الإلكتروني.
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// العنوان.
    /// </summary>
    public string Address { get; set; }

    /// <summary>
    /// الحالة الصحية العامة.
    /// </summary>
    public string HealthStatus { get; set; }
}



/// <summary>
/// نموذج إنشاء مريض جديد.
/// </summary>
public class CreatePatientDto
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; }

    [Required]
    public DateTime DateOfBirth { get; set; }

    [Required]
    public string Gender { get; set; }

    [Required]
    [Phone]
    public string PhoneNumber { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    public string Address { get; set; }

    public string HealthStatus { get; set; }
}


/// <summary>
/// نموذج تعديل معلومات مريض.
/// </summary>
public class UpdatePatientDto
{
    [Required]
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; }

    [Required]
    public DateTime DateOfBirth { get; set; }

    [Required]
    public string Gender { get; set; }

    [Required]
    [Phone]
    public string PhoneNumber { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    public string Address { get; set; }

    public string HealthStatus { get; set; }
}