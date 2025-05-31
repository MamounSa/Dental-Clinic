public class DoctorRepository : IDoctorRepository
{
    private readonly AppDbContext _context;

    public DoctorRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<int?> AddDoctorAsync(Doctor doctor)
    {
        // 🔹 التحقق من صحة `SpecializationId`
        var specializationExists = await _context.Specializations.AnyAsync(s => s.Id == doctor.SpecializationId);
        if (!specializationExists)
            return null; // ❌ رفض الإدخال إذا كان الاختصاص غير موجود

        await _context.Doctors.AddAsync(doctor);
        await _context.SaveChangesAsync();
        return doctor.Id;
    }

    public async Task<bool> UpdateDoctorAsync(Doctor doctor)
    {
        var existingDoctor = await _context.Doctors.FindAsync(doctor.Id);
        if (existingDoctor == null) return false;

        _context.Entry(existingDoctor).CurrentValues.SetValues(doctor);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteDoctorAsync(int id)
    {
        var doctor = await _context.Doctors.FindAsync(id);
        if (doctor == null) return false;

        _context.Doctors.Remove(doctor);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<Doctor>> GetAllDoctorsAsync()
    {
        return await _context.Doctors.Include(d => d.Specialization).ToListAsync();
    }

    public async Task<Doctor> GetDoctorByIdAsync(int id)
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

    public async Task<IEnumerable<Doctor>> SearchByEmailAsync(string email)
    {
        return await _context.Doctors
            .Include(d => d.Specialization)
            .Where(d => d.Email.Contains(email))
            .ToListAsync();
    }

    public async Task<IEnumerable<Doctor>> SearchByPhoneAsync(string phoneNumber)
    {
        return await _context.Doctors
            .Include(d => d.Specialization)
            .Where(d => d.PhoneNumber.Contains(phoneNumber))
            .ToListAsync();
    }

    public async Task<IEnumerable<Doctor>> GetDoctorsBySpecializationAsync(int specializationId)
    {
        return await _context.Doctors
            .Include(d => d.Specialization)
            .Where(d => d.SpecializationId == specializationId)
            .ToListAsync();
    }
}
