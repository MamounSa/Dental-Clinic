public class MedicalImageService : IMedicalImageService
{
    private readonly IMedicalImageRepository _repository;
    private readonly IMapper _mapper;

    public MedicalImageService(IMedicalImageRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<int> AddAsync(CreateMedicalImageDto dto)
    {
        var image = _mapper.Map<MedicalImage>(dto);
        return await _repository.AddAsync(image);
    }

    public async Task<bool> UpdateAsync(UpdateMedicalImageDto dto)
    {
        var image = _mapper.Map<MedicalImage>(dto);
        return await _repository.UpdateAsync(image);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _repository.DeleteAsync(id);
    }

    public async Task<MedicalImageDto> GetByIdAsync(int id)
    {
        var image = await _repository.GetByIdAsync(id);
        return image == null ? null : _mapper.Map<MedicalImageDto>(image);
    }

    public async Task<IEnumerable<MedicalImageDto>> GetAllAsync()
    {
        var images = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<MedicalImageDto>>(images);
    }

    public async Task<IEnumerable<MedicalImageDto>> GetByMedicalRecordIdAsync(int recordId)
    {
        var images = await _repository.GetByMedicalRecordIdAsync(recordId);
        return _mapper.Map<IEnumerable<MedicalImageDto>>(images);
    }
}