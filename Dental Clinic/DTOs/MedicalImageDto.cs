using System.ComponentModel.DataAnnotations;



/// <summary>
/// DTO used when creating a new medical image.
/// </summary>
public class CreateMedicalImageDto
{
    [Required]
    public string ImageUrl { get; set; }

    public string Description { get; set; }

    [Required]
    public int MedicalRecordId { get; set; }
}





/// <summary>
/// DTO used when updating a medical image.
/// </summary>
public class UpdateMedicalImageDto
{
    [Required]
    public int Id { get; set; }

    [Required]
    public string ImageUrl { get; set; }

    public string Description { get; set; }

    [Required]
    public int MedicalRecordId { get; set; }
}




/// <summary>
/// DTO representing a medical image.
/// </summary>
public class MedicalImageDto
{
    public int Id { get; set; }

    /// <summary>
    /// URL of the stored medical image.
    /// </summary>
    public string ImageUrl { get; set; }

    /// <summary>
    /// Description of the image.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Linked medical record ID.
    /// </summary>
    public int MedicalRecordId { get; set; }
}
