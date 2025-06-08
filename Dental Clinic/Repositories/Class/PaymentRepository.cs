public class PaymentRepository : IPaymentRepository
{
    private readonly AppDbContext _context;

    public PaymentRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<int> AddAsync(Payment payment)
    {
        await _context.Payments.AddAsync(payment);
        await _context.SaveChangesAsync();
        return payment.Id;
    }

    public async Task<bool> UpdateAsync(Payment payment)
    {
        var existing = await _context.Payments.FindAsync(payment.Id);
        if (existing == null) return false;

        _context.Entry(existing).CurrentValues.SetValues(payment);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var payment = await _context.Payments.FindAsync(id);
        if (payment == null) return false;

        _context.Payments.Remove(payment);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<Payment> GetByIdAsync(int id)
    {
        return await _context.Payments
            .Include(p => p.Patient)
            .Include(p => p.PaymentMethod)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Payment>> GetAllAsync()
    {
        return await _context.Payments
            .Include(p => p.Patient)
            .Include(p => p.PaymentMethod)
            .ToListAsync();
    }

    public async Task<IEnumerable<Payment>> GetByPatientIdAsync(int patientId)
    {
        return await _context.Payments
            .Include(p => p.PaymentMethod)
            .Where(p => p.PatientId == patientId)
            .ToListAsync();
    }
}