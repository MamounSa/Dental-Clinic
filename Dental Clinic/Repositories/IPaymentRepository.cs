public interface IPaymentRepository
{
    Task<int?> AddPaymentAsync(Payment payment);
    Task<bool> UpdatePaymentAsync(Payment payment);
    Task<bool> DeletePaymentAsync(int id);
    Task<IEnumerable<Payment>> GetAllPaymentsAsync();
    Task<Payment> GetPaymentByIdAsync(int id);

    Task<IEnumerable<Payment>> SearchByStatusAsync(string status);
}
