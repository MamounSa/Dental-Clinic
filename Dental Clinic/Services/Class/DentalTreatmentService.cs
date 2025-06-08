public class DentalTreatmentService : IDentalTreatmentService
{
    private readonly IDentalTreatmentRepository _repository;
    private readonly IMapper _mapper;

    public DentalTreatmentService(IDentalTreatmentRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<int> AddAsync(CreateDentalTreatmentDto dto)
    {
        var entity = _mapper.Map<DentalTreatment>(dto);
        return await _repository.AddAsync(entity);
    }

    public async Task<bool> UpdateAsync(UpdateDentalTreatmentDto dto)
    {
        var entity = _mapper.Map<DentalTreatment>(dto);
        return await _repository.UpdateAsync(entity);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _repository.DeleteAsync(id);
    }

    public async Task<DentalTreatmentDto> GetByIdAsync(int id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return _mapper.Map<DentalTreatmentDto>(entity);
    }

    public async Task<IEnumerable<DentalTreatmentDto>> GetAllAsync()
    {
        var list = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<DentalTreatmentDto>>(list);
    }

    public async Task<IEnumerable<DentalTreatmentDto>> GetByDentalModelIdAsync(int dentalModelId)
    {
        var list = await _repository.GetByDentalModelIdAsync(dentalModelId);
        return _mapper.Map<IEnumerable<DentalTreatmentDto>>(list);
    }
}