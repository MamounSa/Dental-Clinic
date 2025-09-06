public enum PaymentType
{
    Free,
    Cash,
    Online,
    Insurance,
    Subscription
}

public enum PaymentStatus
{
    Paid,
    Pending,
    Failed
}
public class Payment
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public PaymentStatus Status { get; set; }
    public DateTime PaymentDate { get; set; }
    public int PatientId { get; set; }
    public Patient Patient { get; set; }
    public PaymentType PaymentType { get; set; }
    public int InvoiceId { get; set; }
    public Invoice Invoice { get; set; }
}