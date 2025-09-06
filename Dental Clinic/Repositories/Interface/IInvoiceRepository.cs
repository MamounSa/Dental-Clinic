using System.Collections.Generic;
using System.Threading.Tasks;

public interface IInvoiceRepository
{
    Task<int> AddAsync(Invoice invoice);
    Task<bool> UpdateAsync(Invoice invoice);
    Task<bool> DeleteAsync(int id);
    Task<Invoice> GetByIdAsync(int id);
    Task<IEnumerable<Invoice>> GetAllAsync();
    //Task<bool> IsPaidAsync(int invoiceId);
   // Task<decimal> GetRemainingBalanceAsync(int invoiceId);
    Task<IEnumerable<Invoice>> FilterInvoicesAsync(InvoiceFilterDto filter);
}