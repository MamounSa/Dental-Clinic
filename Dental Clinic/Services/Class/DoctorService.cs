public class DoctorService : IDoctorService
{
    private readonly IDoctorRepository _repo;
    private readonly IMapper _mapper;

    public DoctorService(IDoctorRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<int?> AddAsync(CreateDoctorDto dto)
    {
        var doctor = _mapper.Map<Doctor>(dto);
        return await _repo.AddAsync(doctor);
    }

    public async Task<bool> UpdateAsync(UpdateDoctorDto dto)
    {
        var doctor = _mapper.Map<Doctor>(dto);
        return await _repo.UpdateAsync(doctor);
    }

    public async Task<bool> DeleteAsync(int id) => await _repo.DeleteAsync(id);

    public async Task<IEnumerable<DoctorDto>> GetAllAsync()
    {
        var list = await _repo.GetAllAsync();
        return _mapper.Map<IEnumerable<DoctorDto>>(list);
    }

    public async Task<DoctorDto> GetByIdAsync(int id)
    {
        var doctor = await _repo.GetByIdAsync(id);
        return _mapper.Map<DoctorDto>(doctor);
    }

    public async Task<IEnumerable<DoctorDto>> SearchByNameAsync(string name)
    {
        var list = await _repo.SearchByNameAsync(name);
        return _mapper.Map<IEnumerable<DoctorDto>>(list);
    }

    public async Task<IEnumerable<DoctorDto>> GetBySpecializationAsync(int specializationId)
    {
        var list = await _repo.SearchBySpecializationAsync(specializationId);
        return _mapper.Map<IEnumerable<DoctorDto>>(list);
    }
}