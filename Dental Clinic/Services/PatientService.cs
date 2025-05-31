public class PatientService : IPatientService
{
    private readonly IPatientRepository _patientRepository;
    private readonly IMapper _mapper;

    public PatientService(IPatientRepository patientRepository, IMapper mapper)
    {
        _patientRepository = patientRepository;
        _mapper = mapper;
    }

    public async Task<int?> AddPatientAsync(PatientDto patientDto)
    {
        var patient = _mapper.Map<Patient>(patientDto);
        return await _patientRepository.AddPatientAsync(patient);
    }

    public async Task<bool> UpdatePatientAsync(PatientDto patientDto)
    {
        var patient = _mapper.Map<Patient>(patientDto);
        return await _patientRepository.UpdatePatientAsync(patient);
    }

    public async Task<bool> DeletePatientAsync(int id)
    {
        return await _patientRepository.DeletePatientAsync(id);
    }

    public async Task<IEnumerable<PatientDto>> GetAllPatientsAsync()
    {
        var patients = await _patientRepository.GetAllPatientsAsync();
        return _mapper.Map<IEnumerable<PatientDto>>(patients);
    }

    public async Task<PatientDto> GetPatientByIdAsync(int id)
    {
        var patient = await _patientRepository.GetPatientByIdAsync(id);
        return _mapper.Map<PatientDto>(patient);
    }

    public async Task<IEnumerable<PatientDto>> SearchByNameAsync(string name)
    {
        var patients = await _patientRepository.SearchByNameAsync(name);
        return _mapper.Map<IEnumerable<PatientDto>>(patients);
    }

    public async Task<IEnumerable<PatientDto>> SearchByEmailAsync(string email)
    {
        var patients = await _patientRepository.SearchByEmailAsync(email);
        return _mapper.Map<IEnumerable<PatientDto>>(patients);
    }

    public async Task<IEnumerable<PatientDto>> SearchByPhoneAsync(string phoneNumber)
    {
        var patients = await _patientRepository.SearchByPhoneAsync(phoneNumber);
        return _mapper.Map<IEnumerable<PatientDto>>(patients);
    }
}
