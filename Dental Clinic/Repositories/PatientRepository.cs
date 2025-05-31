public class PatientRepository : IPatientRepository
{
    private readonly AppDbContext _context;

    public PatientRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<int?> AddPatientAsync(Patient patient)
    {
        await _context.Patients.AddAsync(patient);
        await _context.SaveChangesAsync();
        return patient.Id > 0 ? patient.Id : null;
    }

    public async Task<bool> UpdatePatientAsync(Patient patient)
    {
        var existingPatient = await _context.Patients.FindAsync(patient.Id);
        if (existingPatient == null) return false;

        _context.Entry(existingPatient).CurrentValues.SetValues(patient);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeletePatientAsync(int id)
    {
        var patient = await _context.Patients.FindAsync(id);
        if (patient == null) return false;

        _context.Patients.Remove(patient);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<Patient>> GetAllPatientsAsync()
    {
        return await _context.Patients.ToListAsync();
    }

    public async Task<Patient> GetPatientByIdAsync(int id)
    {
        return await _context.Patients.FindAsync(id);
    }

    public async Task<IEnumerable<Patient>> SearchByNameAsync(string name)
    {
        return await _context.Patients.Where(p => p.Name.StartsWith(name)).ToListAsync();
    }

    public async Task<IEnumerable<Patient>> SearchByEmailAsync(string email)
    {
        return await _context.Patients.Where(p => p.Email.StartsWith(email)).ToListAsync();
    }

    public async Task<IEnumerable<Patient>> SearchByPhoneAsync(string phoneNumber)
    {
        return await _context.Patients.Where(p => p.PhoneNumber.StartsWith(phoneNumber)).ToListAsync();
    }
}
