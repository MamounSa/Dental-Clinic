public interface IPaymentService
{
    Task<int?> AddPaymentAsync(PaymentDto paymentDto);
    Task<bool> UpdatePaymentAsync(PaymentDto paymentDto);
    Task<bool> DeletePaymentAsync(int id);
    Task<IEnumerable<PaymentDto>> GetAllPaymentsAsync();
    Task<PaymentDto> GetPaymentByIdAsync(int id);

    Task<IEnumerable<PaymentDto>> SearchByStatusAsync(string status);
}
