public class Appointment
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string Status { get; set; } // مثل "مؤكد"، "ملغي"، "مكتمل"

    public int DoctorId { get; set; }
    public Doctor Doctor { get; set; }

    public int PatientId { get; set; }
    public Patient Patient { get; set; }
}
