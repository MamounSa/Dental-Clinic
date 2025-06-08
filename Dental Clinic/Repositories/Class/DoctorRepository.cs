public class DoctorRepository : IDoctorRepository
{
    private readonly AppDbContext _context;

    public DoctorRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<int?> AddAsync(Doctor doctor)
    {
        var specializationExists = await _context.Specializations.AnyAsync(s => s.Id == doctor.SpecializationId);
        if (!specializationExists) return null;

        await _context.Doctors.AddAsync(doctor);
        await _context.SaveChangesAsync();
        return doctor.Id;
    }

    public async Task<bool> UpdateAsync(Doctor doctor)
    {
        var existing = await _context.Doctors.FindAsync(doctor.Id);
        if (existing == null) return false;

        _context.Entry(existing).CurrentValues.SetValues(doctor);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var doctor = await _context.Doctors.FindAsync(id);
        if (doctor == null) return false;

        _context.Doctors.Remove(doctor);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<Doctor>> GetAllAsync()
    {
        return await _context.Doctors.Include(d => d.Specialization).ToListAsync();
    }

    public async Task<Doctor> GetByIdAsync(int id)
    {
        return await _context.Doctors.Include(d => d.Specialization).FirstOrDefaultAsync(d => d.Id == id);
    }

    public async Task<IEnumerable<Doctor>> SearchByNameAsync(string name)
    {
        return await _context.Doctors
            .Include(d => d.Specialization)
            .Where(d => d.Name.Contains(name))
            .ToListAsync();
    }

    public async Task<IEnumerable<Doctor>> SearchBySpecializationAsync(int specializationId)
    {
        return await _context.Doctors
            .Include(d => d.Specialization)
            .Where(d => d.SpecializationId == specializationId)
            .ToListAsync();
    }
}