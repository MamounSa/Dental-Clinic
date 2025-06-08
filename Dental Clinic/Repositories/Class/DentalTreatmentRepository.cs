
public class DentalTreatmentRepository : IDentalTreatmentRepository
{
    private readonly AppDbContext _context;

    public DentalTreatmentRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<int> AddAsync(DentalTreatment treatment)
    {
        var DentalModels = await _context.DentalModels
            .AnyAsync(dt => dt.Id ==treatment.DentalModelId);
        if (DentalModels == false) return -1;
        await _context.DentalTreatments.AddAsync(treatment);
        await _context.SaveChangesAsync();
        return treatment.Id;
    }

    public async Task<bool> UpdateAsync(DentalTreatment treatment)
    {
       
        var existing = await _context.DentalTreatments.FindAsync(treatment.Id);
        if (existing == null) return false;
        _context.Entry(existing).CurrentValues.SetValues(treatment);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var existing = await _context.DentalTreatments.FindAsync(id);
        if (existing == null) return false;
        _context.DentalTreatments.Remove(existing);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<DentalTreatment> GetByIdAsync(int id)
    {
        return await _context.DentalTreatments
            .Include(dt => dt.Doctor)
            .Include(dt => dt.DentalModel)
            .FirstOrDefaultAsync(dt => dt.Id == id);
    }

    public async Task<IEnumerable<DentalTreatment>> GetAllAsync()
    {
        return await _context.DentalTreatments
            .Include(dt => dt.Doctor)
            .Include(dt => dt.DentalModel)
            .ToListAsync();
    }

    public async Task<IEnumerable<DentalTreatment>> GetByDentalModelIdAsync(int dentalModelId)
    {
        return await _context.DentalTreatments
            .Where(dt => dt.DentalModelId == dentalModelId)
            .Include(dt => dt.Doctor)
            .ToListAsync();
    }
}