public class Payment
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public string Status { get; set; } // ✅ حالة الدفع: "مدفوع"، "غير مدفوع"، "معلق"
    public DateTime PaymentDate { get; set; }

    public int PatientId { get; set; }
    public Patient Patient { get; set; }

    public int PaymentMethodId { get; set; }
    public PaymentMethod PaymentMethod { get; set; } // ✅ العلاقة مع `PaymentMethod`
}
