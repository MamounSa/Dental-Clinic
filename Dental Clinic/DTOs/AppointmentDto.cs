public class AppointmentDto
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string Status { get; set; }

    public int DoctorId { get; set; } // الإدخال يكون عبر `DoctorId`
    public string DoctorName { get; set; } // الاسترجاع يظهر اسم الطبيب فقط

    public int PatientId { get; set; } // الإدخال يكون عبر `PatientId`
    public string PatientName { get; set; } // الاسترجاع يظهر اسم المريض فقط
}
