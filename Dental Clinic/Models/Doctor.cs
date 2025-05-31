public class Doctor
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Gender { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
    public int SpecializationId { get; set; }

    public Specialization Specialization { get; set; } // علاقة الربط
    public string LicenseNumber { get; set; }

    public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}
