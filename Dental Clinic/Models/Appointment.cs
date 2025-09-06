public enum AttendanceStatus
{
    InWaiting,
    CheckedIn,
    CheckedOut,
    NoShow
}

public class Appointment
{
    public int Id { get; set; }
    public DateTime Start { get; set; }  // بداية الموعد
    public DateTime End { get; set; }    // نهاية الموعدsq
    public string Status { get; set; } // مثل "مؤكد"، "ملغي"، "مكتمل"

    public int DoctorId { get; set; }
    public Doctor Doctor { get; set; }

    public int PatientId { get; set; }
    public Patient Patient { get; set; }

    public AttendanceStatus? AttendanceStatus { get; set; }
    public DateTime? CheckInTime { get; set; }
    public DateTime? CheckOutTime { get; set; }

    // ربط مع الفاتورة (اختياري)
    public int? InvoiceId { get; set; }
    public Invoice Invoice { get; set; }
}
