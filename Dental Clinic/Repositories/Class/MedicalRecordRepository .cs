public class MedicalRecordRepository : IMedicalRecordRepository
{
    private readonly AppDbContext _context;

    public MedicalRecordRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<MedicalRecord?> GetByIdAsync(int id)
    {
        return await _context.MedicalRecords
            .Include(r => r.Patient)
            //.Include(r => r.MedicalImages)
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<IEnumerable<MedicalRecord>> GetAllAsync()
    {
        return await _context.MedicalRecords
            .Include(r => r.Patient)
            //.Include(r => r.MedicalImages)
            .ToListAsync();
    }

    public async Task<int> AddAsync(MedicalRecord record)
    {
        await _context.MedicalRecords.AddAsync(record);
        await _context.SaveChangesAsync();
        return record.Id;
    }

    public async Task<bool> UpdateAsync(MedicalRecord record)
    {
        var existing = await _context.MedicalRecords.FindAsync(record.Id);
        if (existing == null) return false;

        _context.Entry(existing).CurrentValues.SetValues(record);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var record = await _context.MedicalRecords.FindAsync(id);
        if (record == null) return false;

        _context.MedicalRecords.Remove(record);
        await _context.SaveChangesAsync();
        return true;
    }
}