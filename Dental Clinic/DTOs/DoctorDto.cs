using System.ComponentModel.DataAnnotations;

/// <summary>
/// Doctor DTO for response and shared view.
/// </summary>
public class DoctorDto
{
    public int Id { get; set; }

    /// <summary>Doctor full name.</summary>
    public string Name { get; set; }

    public DateTime DateOfBirth { get; set; }

    public string Gender { get; set; }

    public string PhoneNumber { get; set; }

    public string Email { get; set; }

    public string Address { get; set; }

    public int SpecializationId { get; set; }

    public string LicenseNumber { get; set; }
}

/// <summary>DTO used when creating a new doctor.</summary>
public class CreateDoctorDto
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

    [Required]
    public int SpecializationId { get; set; }

    [Required]
    public string LicenseNumber { get; set; }
}


/// <summary>
/// DTO used when updating a doctor.
/// </summary>
public class UpdateDoctorDto
{
    /// <summary>Doctor ID to be updated.</summary>
    [Required(ErrorMessage = "Doctor ID is required.")]
    public int Id { get; set; }

    /// <summary>Doctor full name.</summary>
    [Required]
    [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
    public string Name { get; set; }

    /// <summary>Date of birth.</summary>
    [Required]
    public DateTime DateOfBirth { get; set; }

    /// <summary>Gender of the doctor (Male/Female).</summary>
    [Required]
    public string Gender { get; set; }

    /// <summary>Phone number in valid format.</summary>
    [Required]
    [Phone]
    public string PhoneNumber { get; set; }

    /// <summary>Email address of the doctor.</summary>
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    /// <summary>Physical address of the doctor.</summary>
    public string Address { get; set; }

    /// <summary>Specialization ID the doctor is assigned to.</summary>
    [Required]
    public int SpecializationId { get; set; }

    /// <summary>License number for medical practice.</summary>
    [Required]
    public string LicenseNumber { get; set; }
}