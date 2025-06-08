public class DentalModelService : IDentalModelService
{
    private readonly IDentalModelRepository _repository;
    private readonly IMapper _mapper;

    public DentalModelService(IDentalModelRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<DentalModelDto>> GetAllAsync()
    {
        var entities = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<DentalModelDto>>(entities);
    }

    public async Task<DentalModelDto> GetByIdAsync(int id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return _mapper.Map<DentalModelDto>(entity);
    }

    public async Task<int?> CreateAsync(CreateDentalModelDto dto)
    {
        var model = _mapper.Map<DentalModel>(dto);
        return await _repository.CreateAsync(model);
    }

    public async Task<bool> UpdateAsync(UpdateDentalModelDto dto)
    {
        var existing = await _repository.GetByIdAsync(dto.Id);
        if (existing == null) return false;

        existing.Condition = dto.Condition;
        return await _repository.UpdateAsync(existing);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _repository.DeleteAsync(id);
    }
}