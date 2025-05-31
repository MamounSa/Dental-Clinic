public class SpecializationService : ISpecializationService
{
    private readonly ISpecializationRepository _specializationRepository;
    private readonly IMapper _mapper;

    public SpecializationService(ISpecializationRepository specializationRepository, IMapper mapper)
    {
        _specializationRepository = specializationRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<SpecializationDto>> GetAllSpecializationsAsync()
    {
        var specializations = await _specializationRepository.GetAllSpecializationsAsync();
        return _mapper.Map<IEnumerable<SpecializationDto>>(specializations);
    }

    public async Task<SpecializationDto> GetSpecializationByIdAsync(int id)
    {
        var specialization = await _specializationRepository.GetSpecializationByIdAsync(id);
        return _mapper.Map<SpecializationDto>(specialization);
    }

    public async Task<int?> AddSpecializationAsync(SpecializationDto specializationDto)
    {
        var specialization = _mapper.Map<Specialization>(specializationDto);
        return await _specializationRepository.AddSpecializationAsync(specialization);
    }

    public async Task<bool> UpdateSpecializationAsync(SpecializationDto specializationDto)
    {
        var specialization = _mapper.Map<Specialization>(specializationDto);
        return await _specializationRepository.UpdateSpecializationAsync(specialization);
    }

    public async Task<bool> DeleteSpecializationAsync(int id)
    {
        return await _specializationRepository.DeleteSpecializationAsync(id);
    }

    public async Task<bool> DoesSpecializationExistAsync(int id)
    {
        return await _specializationRepository.DoesSpecializationExistAsync(id);
    }
}
