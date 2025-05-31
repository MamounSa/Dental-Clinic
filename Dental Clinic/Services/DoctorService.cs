public class DoctorService : IDoctorService
{
    private readonly IDoctorRepository _doctorRepository;
    private readonly IMapper _mapper;

    public DoctorService(IDoctorRepository doctorRepository, IMapper mapper)
    {
        _doctorRepository = doctorRepository;
        _mapper = mapper;
    }
    public async Task<int?> AddDoctorAsync(DoctorDto doctorDto)
    {
        var doctor = _mapper.Map<Doctor>(doctorDto);
        return await _doctorRepository.AddDoctorAsync(doctor);
    }

    public async Task<bool> UpdateDoctorAsync(DoctorDto doctorDto)
    {
        var doctor = _mapper.Map<Doctor>(doctorDto);
        return await _doctorRepository.UpdateDoctorAsync(doctor);
    }

    public async Task<bool> DeleteDoctorAsync(int id)
    {
        return await _doctorRepository.DeleteDoctorAsync(id);
    }

    public async Task<IEnumerable<DoctorDto>> GetAllDoctorsAsync()
    {
        var doctors = await _doctorRepository.GetAllDoctorsAsync();
        return _mapper.Map<IEnumerable<DoctorDto>>(doctors);
    }

    public async Task<DoctorDto> GetDoctorByIdAsync(int id)
    {
        var doctor = await _doctorRepository.GetDoctorByIdAsync(id);
        return _mapper.Map<DoctorDto>(doctor);
    }
    public async Task<IEnumerable<DoctorDto>> SearchByNameAsync(string name)
    {
        var doctors = await _doctorRepository.SearchByNameAsync(name);
        return _mapper.Map<IEnumerable<DoctorDto>>(doctors);
    }

    public async Task<IEnumerable<DoctorDto>> SearchByEmailAsync(string email)
    {
        var doctors = await _doctorRepository.SearchByEmailAsync(email);
        return _mapper.Map<IEnumerable<DoctorDto>>(doctors);
    }

    public async Task<IEnumerable<DoctorDto>> SearchByPhoneAsync(string phoneNumber)
    {
        var doctors = await _doctorRepository.SearchByPhoneAsync(phoneNumber);
        return _mapper.Map<IEnumerable<DoctorDto>>(doctors);
    }

    public async Task<IEnumerable<DoctorDto>> GetDoctorsBySpecializationAsync(int specializationId)
    {
        var doctors = await _doctorRepository.GetDoctorsBySpecializationAsync(specializationId);
        return _mapper.Map<IEnumerable<DoctorDto>>(doctors);
    }
}
