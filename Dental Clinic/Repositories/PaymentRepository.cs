public class PaymentRepository : IPaymentRepository
{
    private readonly AppDbContext _context;

    public PaymentRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<int?> AddPaymentAsync(Payment payment)
    {
        await _context.Payments.AddAsync(payment);
        await _context.SaveChangesAsync();
        return payment.Id;
    }

    public async Task<bool> UpdatePaymentAsync(Payment payment)
    {
        var existingPayment = await _context.Payments.FindAsync(payment.Id);
        if (existingPayment == null) return false;

        _context.Entry(existingPayment).CurrentValues.SetValues(payment);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeletePaymentAsync(int id)
    {
        var payment = await _context.Payments.FindAsync(id);
        if (payment == null) return false;

        _context.Payments.Remove(payment);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<Payment>> GetAllPaymentsAsync()
    {
        return await _context.Payments.Include(p => p.Patient).Include(p => p.PaymentMethod).ToListAsync();
    }

    public async Task<Payment> GetPaymentByIdAsync(int id)
    {
        return await _context.Payments.Include(p => p.Patient).Include(p => p.PaymentMethod).FirstOrDefaultAsync(p => p.Id == id);
    }
    public async Task<IEnumerable<Payment>> SearchByStatusAsync(string status)
    {
        return await _context.Payments
            .Include(p => p.Patient)
            .Include(p => p.PaymentMethod)
            .Where(p => p.Status == status)
            .ToListAsync();
    }

}
