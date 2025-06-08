using System.ComponentModel.DataAnnotations;


    /// <summary>
    /// DTO used to return dental treatment data.
    /// </summary>
    public class DentalTreatmentDto
    {
        public int Id { get; set; }

        public string TreatmentType { get; set; }

        public DateTime TreatmentDate { get; set; }

        public string Notes { get; set; }

        public int DoctorId { get; set; }

        public int DentalModelId { get; set; }
    }

    /// <summary>
    /// DTO used to create a new dental treatment.
    /// </summary>
    public class CreateDentalTreatmentDto
    {
        [Required]
        public string TreatmentType { get; set; }

        [Required]
        public DateTime TreatmentDate { get; set; }

        public string Notes { get; set; }

        [Required]
        public int DoctorId { get; set; }

        [Required]
        public int DentalModelId { get; set; }
    }

    /// <summary>
    /// DTO used to update an existing dental treatment.
    /// </summary>
    public class UpdateDentalTreatmentDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string TreatmentType { get; set; }

        [Required]
        public DateTime TreatmentDate { get; set; }

        public string Notes { get; set; }

        [Required]
        public int DoctorId { get; set; }

        [Required]
        public int DentalModelId { get; set; }
    }
