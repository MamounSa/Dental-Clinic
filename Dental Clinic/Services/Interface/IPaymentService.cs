public interface IPaymentService
{
    Task<int> AddAsync(CreatePaymentDto dto);
    Task<bool> UpdateAsync(UpdatePaymentDto dto);
    Task<bool> DeleteAsync(int id);
    Task<PaymentDto> GetByIdAsync(int id);
    Task<IEnumerable<PaymentDto>> GetAllAsync();
    Task<IEnumerable<PaymentDto>> GetByPatientIdAsync(int patientId);
}