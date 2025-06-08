public interface IPaymentMethodRepository
{
    Task<IEnumerable<PaymentMethod>> GetAllAsync();
    Task<PaymentMethod?> GetByIdAsync(int id);
    Task<int> AddAsync(PaymentMethod method);
    Task<bool> UpdateAsync(PaymentMethod method);
    Task<bool> DeleteAsync(int id);
}