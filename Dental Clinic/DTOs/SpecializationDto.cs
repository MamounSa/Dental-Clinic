using System.ComponentModel.DataAnnotations;




    /// <summary>
    /// DTO used when updating a specialization.
    /// </summary>
    public class UpdateSpecializationDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }
    }

    /// <summary>
    /// DTO for displaying specialization information.
    /// </summary>
    public class SpecializationDto
    {
        public int Id { get; set; }

        /// <summary>
        /// Name of the specialization.
        /// </summary>
        public string Name { get; set; }
    }



    /// <summary>
    /// DTO used when creating a new specialization.
    /// </summary>
    public class CreateSpecializationDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
    }
