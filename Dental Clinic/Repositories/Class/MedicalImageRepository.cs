public class MedicalImageRepository : IMedicalImageRepository
{
    private readonly AppDbContext _context;

    public MedicalImageRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<int> AddAsync(MedicalImage image)
    {
        await _context.MedicalImages.AddAsync(image);
        await _context.SaveChangesAsync();
        return image.Id;
    }

    public async Task<bool> UpdateAsync(MedicalImage image)
    {
        var existing = await _context.MedicalImages.FindAsync(image.Id);
        if (existing == null)
            return false;

        _context.Entry(existing).CurrentValues.SetValues(image);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var image = await _context.MedicalImages.FindAsync(id);
        if (image == null)
            return false;

        _context.MedicalImages.Remove(image);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<MedicalImage> GetByIdAsync(int id)
    {
        return await _context.MedicalImages
            .Include(i => i.MedicalRecord)
            .ThenInclude(r => r.Patient)
            .FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task<IEnumerable<MedicalImage>> GetAllAsync()
    {
        return await _context.MedicalImages
            .Include(i => i.MedicalRecord)
            .ThenInclude(r => r.Patient)
            .ToListAsync();
    }

    public async Task<IEnumerable<MedicalImage>> GetByMedicalRecordIdAsync(int medicalRecordId)
    {
        return await _context.MedicalImages
            .Where(i => i.MedicalRecordId == medicalRecordId)
            .ToListAsync();
    }
}