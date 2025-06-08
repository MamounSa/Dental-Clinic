public class PatientService : IPatientService
{
    private readonly IPatientRepository _repository;
    private readonly IMapper _mapper;

    public PatientService(IPatientRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<int> AddAsync(CreatePatientDto dto)
    {
        var patient = _mapper.Map<Patient>(dto);
        return await _repository.AddAsync(patient);
    }

    public async Task<bool> UpdateAsync(UpdatePatientDto dto)
    {
        var patient = _mapper.Map<Patient>(dto);
        return await _repository.UpdateAsync(patient);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _repository.DeleteAsync(id);
    }

    public async Task<IEnumerable<PatientDto>> GetAllAsync()
    {
        var patients = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<PatientDto>>(patients);
    }

    public async Task<PatientDto> GetByIdAsync(int id)
    {
        var patient = await _repository.GetByIdAsync(id);
        return _mapper.Map<PatientDto>(patient);
    }

    public async Task<IEnumerable<PatientDto>> SearchByNameAsync(string name)
    {
        var patients = await _repository.SearchByNameAsync(name);
        return _mapper.Map<IEnumerable<PatientDto>>(patients);
    }
}