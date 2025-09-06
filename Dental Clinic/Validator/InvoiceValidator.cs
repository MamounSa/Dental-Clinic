public class InvoiceValidator : IInvoiceValidator
{
    public void ValidateForPayment(Invoice invoice,decimal newPaymentAmount)
    {
        if (invoice == null)
            throw new AppException("الفاتورة غير موجودة");

        if (invoice.Status == InvoiceStatus.Cancelled)
            throw new AppException("لا يمكن الدفع لفاتورة ملغاة");

        var totalPaid = invoice.Payments?.Sum(p => p.Amount) ?? 0;
        if (totalPaid >= invoice.TotalAmount)
            throw new AppException("تم دفع هذه الفاتورة بالكامل مسبقاً");

        var paidSoFar = invoice.Payments?.Sum(p => p.Amount) ?? 0;
        var remaining = invoice.TotalAmount - paidSoFar;

        if (newPaymentAmount > remaining)
            throw new AppException($"المبلغ المدفوع ({newPaymentAmount}) أكبر من المبلغ المتبقي ({remaining}).");
    }
}

