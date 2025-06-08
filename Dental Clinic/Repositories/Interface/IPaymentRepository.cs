public interface IPaymentRepository
{
    Task<int> AddAsync(Payment payment);
    Task<bool> UpdateAsync(Payment payment);
    Task<bool> DeleteAsync(int id);
    Task<Payment> GetByIdAsync(int id);
    Task<IEnumerable<Payment>> GetAllAsync();
    Task<IEnumerable<Payment>> GetByPatientIdAsync(int patientId);
}