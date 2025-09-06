public class InvoiceRepository : IInvoiceRepository
{
    private readonly AppDbContext _context;

    public InvoiceRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<int> AddAsync(Invoice invoice)
    {
        _context.Invoices.Add(invoice);
        await _context.SaveChangesAsync();
        return invoice.Id;
    }

    public async Task<bool> UpdateAsync(Invoice invoice)
    {
        _context.Invoices.Update(invoice);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var invoice = await _context.Invoices
            .Include(i => i.Payments)
            .FirstOrDefaultAsync(i => i.Id == id);

        if (invoice == null) return false;
        if (invoice.Payments != null && invoice.Payments.Any())
            throw new AppException("❌ لا يمكن حذف الفاتورة لأنها تحتوي على مدفوعات.");

        _context.Invoices.Remove(invoice);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<Invoice> GetByIdAsync(int id)
    {
        return await _context.Invoices
            .Include(i => i.Patient)
            .Include(i => i.Payments)
            .FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task<IEnumerable<Invoice>> GetAllAsync()
    {
        return await _context.Invoices
            .Include(i => i.Patient)
            .Include(i => i.Payments)
            .ToListAsync();
    }

    public async Task<IEnumerable<Invoice>> FilterInvoicesAsync(InvoiceFilterDto filter)
    {
        var query = _context.Invoices
            .Include(i => i.Patient)
            .Include(i => i.Payments)
            .AsQueryable();

        query = ApplyFiltering(query, filter);
        query = ApplySorting(query, filter);
        query = ApplyPagination(query, filter);

        return await query.ToListAsync();
    }

    private IQueryable<Invoice> ApplyFiltering(IQueryable<Invoice> query, InvoiceFilterDto filter)
    {
        if (filter.PatientId.HasValue)
            query = query.Where(i => i.PatientId == filter.PatientId.Value);

        if (filter.AppointmentId.HasValue)
            query = query.Where(i => i.AppointmentId == filter.AppointmentId.Value);

        if (!string.IsNullOrWhiteSpace(filter.Status) &&
            Enum.TryParse<InvoiceStatus>(filter.Status, out var status))
            query = query.Where(i => i.Status == status);

        if (filter.FromDate.HasValue)
        {
            var from = filter.FromDate.Value;
            var to = filter.ToDate ?? from.AddDays(1).AddTicks(-1);
            query = query.Where(i => i.IssueDate >= from && i.IssueDate <= to);
        }
        else if (filter.ToDate.HasValue)
        {
            query = query.Where(i => i.IssueDate <= filter.ToDate.Value);
        }

        return query;
    }

    private IQueryable<Invoice> ApplySorting(IQueryable<Invoice> query, InvoiceFilterDto filter)
    {
        return filter.SortBy?.ToLower() switch
        {
            "date" => filter.Desc ? query.OrderByDescending(i => i.IssueDate) : query.OrderBy(i => i.IssueDate),
            "amount" => filter.Desc ? query.OrderByDescending(i => i.TotalAmount) : query.OrderBy(i => i.TotalAmount),
            "status" => filter.Desc ? query.OrderByDescending(i => i.Status) : query.OrderBy(i => i.Status),
            _ => query.OrderBy(i => i.Id),
        };
    }

    private IQueryable<Invoice> ApplyPagination(IQueryable<Invoice> query, InvoiceFilterDto filter)
    {
        return query.Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize);
    }
}