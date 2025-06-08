public interface IPaymentMethodService
{
    Task<IEnumerable<PaymentMethodDto>> GetAllAsync();
    Task<PaymentMethodDto?> GetByIdAsync(int id);
    Task<int> AddAsync(CreatePaymentMethodDto dto);
    Task<bool> UpdateAsync(UpdatePaymentMethodDto dto);
    Task<bool> DeleteAsync(int id);
}