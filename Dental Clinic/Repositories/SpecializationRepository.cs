public class SpecializationRepository : ISpecializationRepository
{
    private readonly AppDbContext _context;

    public SpecializationRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Specialization>> GetAllSpecializationsAsync()
    {
        return await _context.Specializations.ToListAsync();
    }

    public async Task<Specialization> GetSpecializationByIdAsync(int id)
    {
        return await _context.Specializations.FindAsync(id);
    }

    public async Task<int?> AddSpecializationAsync(Specialization specialization)
    {
        await _context.Specializations.AddAsync(specialization);
        await _context.SaveChangesAsync();
        return specialization.Id;
    }

    public async Task<bool> UpdateSpecializationAsync(Specialization specialization)
    {
        var existingSpecialization = await _context.Specializations.FindAsync(specialization.Id);
        if (existingSpecialization == null) return false;

        _context.Entry(existingSpecialization).CurrentValues.SetValues(specialization);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteSpecializationAsync(int id)
    {
        var specialization = await _context.Specializations.FindAsync(id);
        if (specialization == null) return false;

        _context.Specializations.Remove(specialization);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DoesSpecializationExistAsync(int id)
    {
        return await _context.Specializations.AnyAsync(s => s.Id == id);
    }
}
