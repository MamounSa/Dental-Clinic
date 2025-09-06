
public class Patient
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Gender { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
    
    public string HealthStatus { get; set; }

    public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public ICollection<DentalModel> DentalModels { get; set; } = new List<DentalModel>();

    public ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();


    public MedicalRecord MedicalRecords { get;  set; }
}


