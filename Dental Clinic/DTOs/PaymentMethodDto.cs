    /// <summary>
    /// DTO used when creating a payment method.
    /// </summary>
    public class CreatePaymentMethodDto
    {
        [Required]
        public string MethodName { get; set; }
    }

    /// <summary>
    /// DTO used when updating a payment method.
    /// </summary>
    public class UpdatePaymentMethodDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string MethodName { get; set; }
    }


    /// <summary>
    /// DTO for displaying payment method information.
    /// </summary>
    public class PaymentMethodDto
    {
        public int Id { get; set; }
        public string MethodName { get; set; }
    }
