public enum InvoiceStatus
{
    Pending,
    Paid,
    PartiallyPaid,
    Cancelled
}

public class Invoice
{
    public int Id { get; set; }
    public int PatientId { get; set; }
    public Patient Patient { get; set; }
    public decimal TotalAmount { get; set; }
    public DateTime IssueDate { get; set; }
    public DateTime DueDate { get; set; }
    public ICollection<Payment> Payments { get; set; }

    public decimal TotalPaid => Payments?.Sum(p => p.Amount) ?? 0;
    public decimal Remaining => TotalAmount - TotalPaid;
    public bool IsPaid => TotalPaid >= TotalAmount;
    public bool IsPartiallyPaid => TotalPaid > 0 && TotalPaid < TotalAmount;
    public decimal PercentagePaid => TotalAmount == 0 ? 0 : Math.Round(TotalPaid / TotalAmount * 100, 2);

    public int? AppointmentId { get; set; }
    public Appointment Appointment { get; set; }
    public InvoiceStatus Status { get; set; }
}