public class MedicalRecordService : IMedicalRecordService
{
    private readonly IMedicalRecordRepository _medicalRecordRepository;
    private readonly IMapper _mapper;

    public MedicalRecordService(IMedicalRecordRepository medicalRecordRepository, IMapper mapper)
    {
        _medicalRecordRepository = medicalRecordRepository;
        _mapper = mapper;
    }

    

    public async Task<bool> UpdateMedicalRecordAsync(MedicalRecordDto medicalRecordDto)
    {
        var medicalRecord = _mapper.Map<MedicalRecord>(medicalRecordDto);
        return await _medicalRecordRepository.UpdateMedicalRecordAsync(medicalRecord);
    }

    public async Task<bool> DeleteMedicalRecordAsync(int id)
    {
        return await _medicalRecordRepository.DeleteMedicalRecordAsync(id);
    }

    public async Task<IEnumerable<MedicalRecordDto>> GetAllMedicalRecordsAsync()
    {
        var records = await _medicalRecordRepository.GetAllMedicalRecordsAsync();
        return _mapper.Map<IEnumerable<MedicalRecordDto>>(records);
    }

    public async Task<MedicalRecordDto> GetMedicalRecordByIdAsync(int id)
    {
        var record = await _medicalRecordRepository.GetMedicalRecordByIdAsync(id);
        return _mapper.Map<MedicalRecordDto>(record);
    }

    public async Task<IEnumerable<MedicalRecordDto>> GetMedicalRecordsByPatientIdAsync(int patientId)
    {
        var records = await _medicalRecordRepository.GetMedicalRecordsByPatientIdAsync(patientId);
        return _mapper.Map<IEnumerable<MedicalRecordDto>>(records);
    }

    public async Task<IEnumerable<MedicalRecordDto>> SearchByPatientNameAsync(string patientName)
    {
        var records = await _medicalRecordRepository.SearchByPatientNameAsync(patientName);
        return _mapper.Map<IEnumerable<MedicalRecordDto>>(records);
    }

    public async Task<int?> AddMedicalRecordAsync(MedicalRecordDto medicalRecordDto)
    {
        var medicalRecord = _mapper.Map<MedicalRecord>(medicalRecordDto);

        // ✅ حفظ السجل الطبي عبر `Repository`
        var recordId = await _medicalRecordRepository.AddMedicalRecordAsync(medicalRecord);
        if (recordId == null) return null;

        // ✅ تحويل الصور باستخدام `Mapper`
        var images = _mapper.Map<IEnumerable<MedicalImage>>(medicalRecordDto.Images);
        foreach (var image in images)
        {
            image.MedicalRecordId = recordId.Value; // ✅ تعيين `MedicalRecordId` للسجل الجديد
        }

        // ✅ إضافة الصور عبر `Repository`
        await _medicalRecordRepository.AddMedicalImagesAsync(images);

        return recordId;
    }





}
