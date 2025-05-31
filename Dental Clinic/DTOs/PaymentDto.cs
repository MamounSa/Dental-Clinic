public class PaymentDto
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public string Status { get; set; }
    public DateTime PaymentDate { get; set; }

    public int PatientId { get; set; } // ✅ الإدخال يكون عبر `PatientId`
    public string PatientName { get; set; } // ✅ الاسترجاع يظهر اسم المريض فقط

    public int PaymentMethodId { get; set; } // ✅ الإدخال يكون عبر `PaymentMethodId`
    public string PaymentMethodName { get; set; } // ✅ الاسترجاع يظهر اسم طريقة الدفع فقط
}
