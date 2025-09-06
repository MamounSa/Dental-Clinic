public class PaymentRepository : IPaymentRepository
{
    private readonly AppDbContext _context;

    public PaymentRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Payment> GetByIdAsync(int id) =>
        await _context.Payments.Include(p => p.Invoice).FirstOrDefaultAsync(p => p.Id == id);

    public async Task<IEnumerable<Payment>> GetByInvoiceIdAsync(int invoiceId) =>
        await _context.Payments.Where(p => p.InvoiceId == invoiceId).ToListAsync();

    public async Task<IEnumerable<Payment>> GetAllAsync() =>
        await _context.Payments.Include(p => p.Invoice).ToListAsync();

    public async Task<int> AddAsync(Payment payment)
    {
        await _context.Payments.AddAsync(payment);
        await _context.SaveChangesAsync();
        return payment.Id;
    }

    public async Task<bool> UpdateAsync(Payment payment)
    {
        _context.Payments.Update(payment);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAsync(Payment payment)
    {
        _context.Payments.Remove(payment);
        return await _context.SaveChangesAsync() > 0;
    }
}