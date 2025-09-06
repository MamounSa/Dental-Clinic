using System.Collections.Generic;
using System.Threading.Tasks;

public interface IInvoiceService
{
    Task<int> CreateAsync(CreateInvoiceDto dto);
    Task<bool> UpdateAsync(int id, UpdateInvoiceDto dto);
    Task<bool> DeleteAsync(int id);
    Task<InvoiceDto> GetByIdAsync(int id);
    Task<IEnumerable<InvoiceDto>> GetAllAsync();
   // Task<bool> IsPaidAsync(int invoiceId);
    Task<decimal> GetRemainingBalanceAsync(int invoiceId);
    Task<InvoiceSummaryDto> GetInvoiceSummaryAsync(int invoiceId);
    void UpdateInvoiceStatus(Invoice invoice);
    Task<IEnumerable<Invoice>> FilterInvoicesAsync(InvoiceFilterDto filter);
}