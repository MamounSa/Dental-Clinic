public class MedicalRecordService : IMedicalRecordService
{
    private readonly IMedicalRecordRepository _repository;
    private readonly IMapper _mapper;

    public MedicalRecordService(IMedicalRecordRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<MedicalRecordDto?> GetByIdAsync(int id)
    {
        var record = await _repository.GetByIdAsync(id);
        return record == null ? null : _mapper.Map<MedicalRecordDto>(record);
    }

    public async Task<IEnumerable<MedicalRecordDto>> GetAllAsync()
    {
        var records = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<MedicalRecordDto>>(records);
    }

    public async Task<int> AddAsync(CreateMedicalRecordDto dto)
    {
        var record = _mapper.Map<MedicalRecord>(dto);
        return await _repository.AddAsync(record);
    }

    public async Task<bool> UpdateAsync(UpdateMedicalRecordDto dto)
    {
        var record = _mapper.Map<MedicalRecord>(dto);
        return await _repository.UpdateAsync(record);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _repository.DeleteAsync(id);
    }
}