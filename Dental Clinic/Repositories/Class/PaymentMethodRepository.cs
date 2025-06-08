
    public class PaymentMethodRepository : IPaymentMethodRepository
    {
        private readonly AppDbContext _context;

        public PaymentMethodRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PaymentMethod>> GetAllAsync()
        {
            return await _context.PaymentMethods.ToListAsync();
        }

        public async Task<PaymentMethod?> GetByIdAsync(int id)
        {
            return await _context.PaymentMethods.FindAsync(id);
        }

        public async Task<int> AddAsync(PaymentMethod method)
        {
            await _context.PaymentMethods.AddAsync(method);
            await _context.SaveChangesAsync();
            return method.Id;
        }

        public async Task<bool> UpdateAsync(PaymentMethod method)
        {
            var existing = await _context.PaymentMethods.FindAsync(method.Id);
            if (existing == null) return false;

            existing.MethodName = method.MethodName;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var method = await _context.PaymentMethods.FindAsync(id);
            if (method == null) return false;

            _context.PaymentMethods.Remove(method);
            await _context.SaveChangesAsync();
            return true;
        }
    }
