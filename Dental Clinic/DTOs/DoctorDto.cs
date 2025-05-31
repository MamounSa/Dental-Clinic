public class DoctorDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Gender { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }

    //public int SpecializationId { get; set; }
    public string SpecializationName { get; set; } // علاقة الربط

    public string SpecializationId { get; set; }
    public string LicenseNumber { get; set; }
}
