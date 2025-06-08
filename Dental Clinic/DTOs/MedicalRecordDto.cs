using System.ComponentModel.DataAnnotations;


    /// <summary>
    /// DTO used to create a new medical record.
    /// </summary>
    public class CreateMedicalRecordDto
    {
        [Required]
        public int PatientId { get; set; }

        public string Diagnosis { get; set; }

        public string TreatmentPlan { get; set; }
    }

    /// <summary>
    /// DTO used to update an existing medical record.
    /// </summary>
    public class UpdateMedicalRecordDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int PatientId { get; set; }

        public string Diagnosis { get; set; }

        public string TreatmentPlan { get; set; }
    }

    /// <summary>
    /// DTO used to return medical record data.
    /// </summary>
    public class MedicalRecordDto
    {
        public int Id { get; set; }

        public int PatientId { get; set; }

        public string Diagnosis { get; set; }

        public string TreatmentPlan { get; set; }

        public string PatientName { get; set; }
    }
