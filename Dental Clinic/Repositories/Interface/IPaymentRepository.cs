using System.Collections.Generic;
using System.Threading.Tasks;

public interface IPaymentRepository
{
    Task<Payment> GetByIdAsync(int id);
    Task<IEnumerable<Payment>> GetByInvoiceIdAsync(int invoiceId);
    Task<IEnumerable<Payment>> GetAllAsync();
    Task<int> AddAsync(Payment payment);
    Task<bool> UpdateAsync(Payment payment);
    Task<bool> DeleteAsync(Payment payment);
}