public interface IInvoiceValidator
{
    void ValidateForPayment(Invoice invoice, decimal newPaymentAmount);
}
