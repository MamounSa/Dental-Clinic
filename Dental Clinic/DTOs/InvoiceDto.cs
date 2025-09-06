public class CreateInvoiceDto
{
    public int PatientId { get; set; }
    public decimal TotalAmount { get; set; }
    public DateTime IssueDate { get; set; }
    public DateTime DueDate { get; set; }
}

public class UpdateInvoiceDto
{
    public int PatientId { get; set; }
    public decimal TotalAmount { get; set; }
    public DateTime IssueDate { get; set; }
    public DateTime DueDate { get; set; }
}

public class InvoiceDto
{
    public int Id { get; set; }
    public int PatientId { get; set; }
    public decimal TotalAmount { get; set; }
    public DateTime IssueDate { get; set; }
    public DateTime DueDate { get; set; }
    public InvoiceStatus Status { get; set; }
}

public class InvoiceSummaryDto
{
    public int InvoiceId { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal TotalPaid { get; set; }
    public decimal Remaining { get; set; }
    public bool IsPaid { get; set; }
    public bool IsPartiallyPaid { get; set; }
    public decimal PercentagePaid { get; set; }
}

public class InvoiceFilterDto
{
    public int? PatientId { get; set; }
    public int? AppointmentId { get; set; }
    public string? Status { get; set; }
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
    public string? SortBy { get; set; }
    public bool Desc { get; set; } = false;
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}