using System.Collections.Generic;
using System.Threading.Tasks;

public interface IPaymentService
{
    Task<PaymentDto> GetByIdAsync(int id);
    Task<IEnumerable<PaymentDto>> GetAllAsync();
    Task<IEnumerable<PaymentDto>> GetByInvoiceIdAsync(int invoiceId);
    Task<PaymentResultDto> CreateAsync(CreatePaymentDto dto);
    Task<bool> UpdateAsync(int id, UpdatePaymentDto dto);
    Task<bool> DeleteAsync(int id);
}