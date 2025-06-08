using Microsoft.EntityFrameworkCore;

    public class SpecializationRepository : ISpecializationRepository
    {
        private readonly AppDbContext _context;

        public SpecializationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Specialization>> GetAllAsync()
        {
            return await _context.Specializations.ToListAsync();
        }

        public async Task<Specialization?> GetByIdAsync(int id)
        {
            return await _context.Specializations.FindAsync(id);
        }

        public async Task<int> AddAsync(Specialization specialization)
        {
           
            await _context.Specializations.AddAsync(specialization);
            await _context.SaveChangesAsync();
            return specialization.Id;
        }

        public async Task<bool> UpdateAsync(Specialization specialization)
        {
            var existing = await _context.Specializations.FindAsync(specialization.Id);
            if (existing == null) return false;

            existing.Name = specialization.Name;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var spec = await _context.Specializations.FindAsync(id);
            if (spec == null) return false;

            _context.Specializations.Remove(spec);
            await _context.SaveChangesAsync();
            return true;
        }
    }
