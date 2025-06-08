public class PatientRepository : IPatientRepository
{
    private readonly AppDbContext _context;

    public PatientRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<int> AddAsync(Patient patient)
    {
        await _context.Patients.AddAsync(patient);
        await _context.SaveChangesAsync();
        return patient.Id;
    }

    public async Task<bool> UpdateAsync(Patient patient)
    {
        var existing = await _context.Patients.FindAsync(patient.Id);
        if (existing == null) return false;

        _context.Entry(existing).CurrentValues.SetValues(patient);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var patient = await _context.Patients.FindAsync(id);
        if (patient == null) return false;

        _context.Patients.Remove(patient);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<Patient>> GetAllAsync()
    {
        return await _context.Patients.ToListAsync();
    }

    public async Task<Patient> GetByIdAsync(int id)
    {
        return await _context.Patients.FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Patient>> SearchByNameAsync(string name)
    {
        return await _context.Patients
            .Where(p => p.Name.Contains(name))
            .ToListAsync();
    }
}