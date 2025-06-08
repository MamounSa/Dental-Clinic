public class PaymentService : IPaymentService
{
    private readonly IPaymentRepository _repository;
    private readonly IMapper _mapper;

    public PaymentService(IPaymentRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<int> AddAsync(CreatePaymentDto dto)
    {
        var entity = _mapper.Map<Payment>(dto);
        return await _repository.AddAsync(entity);
    }

    public async Task<bool> UpdateAsync(UpdatePaymentDto dto)
    {
        var entity = _mapper.Map<Payment>(dto);
        return await _repository.UpdateAsync(entity);
    }

    public async Task<bool> DeleteAsync(int id) => await _repository.DeleteAsync(id);

    public async Task<PaymentDto> GetByIdAsync(int id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return _mapper.Map<PaymentDto>(entity);
    }

    public async Task<IEnumerable<PaymentDto>> GetAllAsync()
    {
        var list = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<PaymentDto>>(list);
    }

    public async Task<IEnumerable<PaymentDto>> GetByPatientIdAsync(int patientId)
    {
        var list = await _repository.GetByPatientIdAsync(patientId);
        return _mapper.Map<IEnumerable<PaymentDto>>(list);
    }
}