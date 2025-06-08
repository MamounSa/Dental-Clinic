public class DentalModelRepository : IDentalModelRepository
{
    private readonly AppDbContext _context;

    public DentalModelRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<DentalModel>> GetAllAsync()
    {
        return await _context.DentalModels
            .Include(m => m.Treatments)
            .ToListAsync();
    }

    public async Task<DentalModel> GetByIdAsync(int id)
    {
        return await _context.DentalModels
            .Include(m => m.Treatments)
            .FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<int?> CreateAsync(DentalModel model)
    {
        // تحقق من وجود المريض والسجل الطبي
        var patientExists = await _context.Patients.AnyAsync(p => p.Id == model.PatientId);
        var recordExists = await _context.MedicalRecords.AnyAsync(r => r.Id == model.MedicalRecordId);

        if (!patientExists || !recordExists)
            return null;

        await _context.DentalModels.AddAsync(model);
        await _context.SaveChangesAsync();
        return model.Id;
    }

    public async Task<bool> UpdateAsync(DentalModel model)
    {
        var existing = await _context.DentalModels.FindAsync(model.Id);
        if (existing == null) return false;

        _context.Entry(existing).CurrentValues.SetValues(model);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var model = await _context.DentalModels.FindAsync(id);
        if (model == null) return false;

        _context.DentalModels.Remove(model);
        await _context.SaveChangesAsync();
        return true;
    }
}