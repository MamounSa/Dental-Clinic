public interface IDoctorService
{
    Task<int?> AddDoctorAsync(DoctorDto doctorDto);
    Task<bool> UpdateDoctorAsync(DoctorDto doctorDto);
    Task<bool> DeleteDoctorAsync(int id);
    Task<IEnumerable<DoctorDto>> GetAllDoctorsAsync();
    Task<DoctorDto> GetDoctorByIdAsync(int id);
    Task<IEnumerable<DoctorDto>> SearchByNameAsync(string name);
    Task<IEnumerable<DoctorDto>> SearchByEmailAsync(string email);
    Task<IEnumerable<DoctorDto>> SearchByPhoneAsync(string phoneNumber);
    Task<IEnumerable<DoctorDto>> GetDoctorsBySpecializationAsync(int specializationId);
}
