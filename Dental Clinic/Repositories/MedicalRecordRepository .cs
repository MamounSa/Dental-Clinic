public class MedicalRecordRepository : IMedicalRecordRepository
{
    private readonly AppDbContext _context;

    public MedicalRecordRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<int?> AddMedicalRecordAsync(MedicalRecord medicalRecord)
    {
        await _context.MedicalRecords.AddAsync(medicalRecord);
        
        await _context.SaveChangesAsync();
        return medicalRecord.Id;
    }

    public async Task<bool> UpdateMedicalRecordAsync(MedicalRecord medicalRecord)
    {
        var existingRecord = await _context.MedicalRecords.FindAsync(medicalRecord.Id);
        if (existingRecord == null) return false;

        _context.Entry(existingRecord).CurrentValues.SetValues(medicalRecord);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteMedicalRecordAsync(int id)
    {
        var medicalRecord = await _context.MedicalRecords.FindAsync(id);
        if (medicalRecord == null) return false;

        _context.MedicalRecords.Remove(medicalRecord);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<MedicalRecord>> GetAllMedicalRecordsAsync()
    {
        return await _context.MedicalRecords
            .Include(mr => mr.Patient)
            .Include(mr => mr.Images).ToListAsync();
    }

    public async Task<MedicalRecord> GetMedicalRecordByIdAsync(int id)
    {
        return await _context.MedicalRecords
            .Include(mr => mr.Patient)
            .Include(mr=>mr.Images)
            .FirstOrDefaultAsync(mr => mr.Id == id);
    }

    public async Task<IEnumerable<MedicalRecord>> GetMedicalRecordsByPatientIdAsync(int patientId)
    {
        return await _context.MedicalRecords
            .Include(mr => mr.Patient)
            .Include(mr => mr.Images)
            .Where(mr => mr.PatientId == patientId).ToListAsync();
    }

    public async Task<IEnumerable<MedicalRecord>> SearchByPatientNameAsync(string patientName)
    {
        return await _context.MedicalRecords
            .Include(mr => mr.Patient)
            .Include(mr => mr.Images)
            .Where(mr => mr.Patient.Name.Contains(patientName))
            .ToListAsync();
    }

    public async Task<bool> AddMedicalImagesAsync(IEnumerable<MedicalImage> images)
    {
        await _context.MedicalImages.AddRangeAsync(images);
        await _context.SaveChangesAsync();
        return true;
    }

}
