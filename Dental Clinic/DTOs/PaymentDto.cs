

    /// <summary>
    /// DTO used when creating a payment.
    /// </summary>
    public class CreatePaymentDto
    {
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0.")]
        public decimal Amount { get; set; }

        [Required]
        [StringLength(20)]
        public string Status { get; set; }

        [Required]
        public DateTime PaymentDate { get; set; }

        [Required]
        public int PatientId { get; set; }

        [Required]
        public int PaymentMethodId { get; set; }
    }



    /// <summary>
    /// DTO used when updating a payment.
    /// </summary>
    public class UpdatePaymentDto : CreatePaymentDto
    {
        [Required]
        public int Id { get; set; }
    }



    /// <summary>
    /// DTO used for reading/displaying payment details.
    /// </summary>
    public class PaymentDto
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
        public DateTime PaymentDate { get; set; }
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public int PaymentMethodId { get; set; }
        public string PaymentMethodName { get; set; }
    }
