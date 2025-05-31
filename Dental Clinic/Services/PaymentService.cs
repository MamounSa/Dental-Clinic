public class PaymentService : IPaymentService
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly IMapper _mapper;

    public PaymentService(IPaymentRepository paymentRepository, IMapper mapper)
    {
        _paymentRepository = paymentRepository;
        _mapper = mapper;
    }

    public async Task<int?> AddPaymentAsync(PaymentDto paymentDto)
    {
        var payment = _mapper.Map<Payment>(paymentDto);
        return await _paymentRepository.AddPaymentAsync(payment);
    }

    public async Task<bool> UpdatePaymentAsync(PaymentDto paymentDto)
    {
        var payment = _mapper.Map<Payment>(paymentDto);
        return await _paymentRepository.UpdatePaymentAsync(payment);
    }

    public async Task<bool> DeletePaymentAsync(int id)
    {
        return await _paymentRepository.DeletePaymentAsync(id);
    }

    public async Task<IEnumerable<PaymentDto>> GetAllPaymentsAsync()
    {
        var payments = await _paymentRepository.GetAllPaymentsAsync();
        return _mapper.Map<IEnumerable<PaymentDto>>(payments);
    }

    public async Task<PaymentDto> GetPaymentByIdAsync(int id)
    {
        var payment = await _paymentRepository.GetPaymentByIdAsync(id);
        return _mapper.Map<PaymentDto>(payment);
    }

    public async Task<IEnumerable<PaymentDto>> SearchByStatusAsync(string status)
    {
        var payments = await _paymentRepository.SearchByStatusAsync(status);
        return _mapper.Map<IEnumerable<PaymentDto>>(payments);
    }

}
