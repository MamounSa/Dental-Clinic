public class InvoiceService : IInvoiceService
{
    private readonly IInvoiceRepository _repository;
    private readonly IMapper _mapper;

    public InvoiceService(IInvoiceRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<int> CreateAsync(CreateInvoiceDto dto)
    {
        var invoice = _mapper.Map<Invoice>(dto);
        invoice.Status = InvoiceStatus.Pending;
        return await _repository.AddAsync(invoice);
    }

    public async Task<bool> UpdateAsync(int id, UpdateInvoiceDto dto)
    {
        var invoice = await _repository.GetByIdAsync(id);
        if (invoice == null) return false;

        _mapper.Map(dto, invoice);
        return await _repository.UpdateAsync(invoice);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _repository.DeleteAsync(id);
    }

    public async Task<InvoiceDto> GetByIdAsync(int id)
    {
        var invoice = await _repository.GetByIdAsync(id);
        return _mapper.Map<InvoiceDto>(invoice);
    }

    public async Task<IEnumerable<InvoiceDto>> GetAllAsync()
    {
        var invoices = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<InvoiceDto>>(invoices);
    }

    public async Task<decimal> GetRemainingBalanceAsync(int invoiceId)
    {
        var invoice = await _repository.GetByIdAsync(invoiceId);
        if (invoice == null) throw new AppException("❌ الفاتورة غير موجودة.");
        return invoice.Remaining;
    }

    public async Task<InvoiceSummaryDto> GetInvoiceSummaryAsync(int invoiceId)
    {
        var invoice = await _repository.GetByIdAsync(invoiceId);
        if (invoice == null) return null;

        return new InvoiceSummaryDto
        {
            InvoiceId = invoice.Id,
            TotalAmount = invoice.TotalAmount,
            TotalPaid = invoice.TotalPaid,
            Remaining = invoice.Remaining,
            IsPaid = invoice.IsPaid,
            IsPartiallyPaid = invoice.IsPartiallyPaid,
            PercentagePaid = invoice.PercentagePaid
        };
    }

    public void UpdateInvoiceStatus(Invoice invoice)
    {
        if (invoice.TotalPaid == 0)
            invoice.Status = InvoiceStatus.Pending;
        else if (invoice.TotalPaid < invoice.TotalAmount)
            invoice.Status = InvoiceStatus.PartiallyPaid;
        else
            invoice.Status = InvoiceStatus.Paid;
    }

    public async Task<IEnumerable<Invoice>> FilterInvoicesAsync(InvoiceFilterDto filter)
    {
        return await _repository.FilterInvoicesAsync(filter);
    }
}