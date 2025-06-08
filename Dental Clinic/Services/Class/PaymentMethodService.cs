
    public class PaymentMethodService : IPaymentMethodService
    {
        private readonly IPaymentMethodRepository _repository;
        private readonly IMapper _mapper;

        public PaymentMethodService(IPaymentMethodRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PaymentMethodDto>> GetAllAsync()
        {
            var methods = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<PaymentMethodDto>>(methods);
        }

        public async Task<PaymentMethodDto?> GetByIdAsync(int id)
        {
            var method = await _repository.GetByIdAsync(id);
            return method == null ? null : _mapper.Map<PaymentMethodDto>(method);
        }

        public async Task<int> AddAsync(CreatePaymentMethodDto dto)
        {
            var entity = _mapper.Map<PaymentMethod>(dto);
            return await _repository.AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(UpdatePaymentMethodDto dto)
        {
            var entity = _mapper.Map<PaymentMethod>(dto);
            return await _repository.UpdateAsync(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
