public class CreatePaymentDto
{
    public decimal Amount { get; set; }
    public int PatientId { get; set; }
    public int InvoiceId { get; set; }
    public PaymentType PaymentType { get; set; }
}

public class UpdatePaymentDto
{
    public decimal? Amount { get; set; }
    public PaymentStatus? Status { get; set; }
    public DateTime? PaymentDate { get; set; }
    public PaymentType? PaymentType { get; set; }
}

public class PaymentDto
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public PaymentStatus Status { get; set; }
    public DateTime PaymentDate { get; set; }
    public int PatientId { get; set; }
    public int InvoiceId { get; set; }
    public PaymentType PaymentType { get; set; }
}

public class PaymentResultDto
{
    public int PaymentId { get; set; }
    public decimal RemainingAmount { get; set; }
    public string InvoiceStatus { get; set; }
}