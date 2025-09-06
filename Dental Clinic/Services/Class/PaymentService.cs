
public class PaymentService : IPaymentService
{
    private readonly IPaymentRepository _repository;
    private readonly IInvoiceRepository _invoiceRepository;
    private readonly IInvoiceService _invoiceService;
    private readonly IInvoiceValidator _invoiceValidator;
    private readonly IMapper _mapper;
    private readonly AppDbContext _context;

    public PaymentService(
        IPaymentRepository repository,
        IInvoiceRepository invoiceRepository,
        IInvoiceService invoiceService,
        IInvoiceValidator invoiceValidator,
        IMapper mapper,
        AppDbContext context)
    {
        _repository = repository;
        _invoiceRepository = invoiceRepository;
        _invoiceService = invoiceService;
        _invoiceValidator = invoiceValidator;
        _mapper = mapper;
        _context = context;
    }

    public async Task<PaymentResultDto> CreateAsync(CreatePaymentDto dto)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            // 1️⃣ جلب الفاتورة المرتبطة
            var invoice = await _invoiceRepository.GetByIdAsync(dto.InvoiceId);
            if (invoice == null)
                throw new AppException("❌ الفاتورة غير موجودة.");

            // 2️⃣ تحقق من المبلغ وصلاحية الدفع
            _invoiceValidator.ValidateForPayment(invoice, dto.Amount);

            // 3️⃣ إنشاء الدفع
            var payment = _mapper.Map<Payment>(dto);
            payment.Status = PaymentStatus.Paid;
            payment.PaymentDate = DateTime.UtcNow;

            var paymentId = await _repository.AddAsync(payment);

            // 4️⃣ تحديث الفاتورة
            invoice.Payments.Add(payment);
            _invoiceService.UpdateInvoiceStatus(invoice);
            await _invoiceRepository.UpdateAsync(invoice);

            // 5️⃣ إنهاء الـ transaction
            await transaction.CommitAsync();

            // 6️⃣ رجوع النتيجة
            return new PaymentResultDto
            {
                PaymentId = paymentId,
                RemainingAmount = invoice.TotalAmount - invoice.Payments.Sum(p => p.Amount),
                InvoiceStatus = invoice.Status.ToString()
            };
        }
        catch
        {
            // ⚠️ إذا صار أي خطأ، نلغي العملية كلها
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task<PaymentDto> GetByIdAsync(int id)
    {
        var payment = await _repository.GetByIdAsync(id);
        return _mapper.Map<PaymentDto>(payment);
    }

    public async Task<IEnumerable<PaymentDto>> GetAllAsync()
    {
        var payments = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<PaymentDto>>(payments);
    }

    public async Task<IEnumerable<PaymentDto>> GetByInvoiceIdAsync(int invoiceId)
    {
        var payments = await _repository.GetByInvoiceIdAsync(invoiceId);
        return _mapper.Map<IEnumerable<PaymentDto>>(payments);
    }

    public async Task<bool> UpdateAsync(int id, UpdatePaymentDto dto)
    {
        var payment = await _repository.GetByIdAsync(id);
        if (payment == null) return false;

        // ⚠️ إذا تغير المبلغ لازم نتحقق من الفاتورة من جديد
        if (dto.Amount.HasValue && dto.Amount.Value != payment.Amount)
        {
            var invoice = await _invoiceRepository.GetByIdAsync(payment.InvoiceId);
            _invoiceValidator.ValidateForPayment(invoice, dto.Amount.Value - payment.Amount);
        }

        _mapper.Map(dto, payment);
        return await _repository.UpdateAsync(payment);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var payment = await _repository.GetByIdAsync(id);
        if (payment == null) return false;

        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var invoice = await _invoiceRepository.GetByIdAsync(payment.InvoiceId);

            // احذف الدفع
            var deleted = await _repository.DeleteAsync(payment);

            
// حدث حالة الفاتورة بعد الحذف
            _invoiceService.UpdateInvoiceStatus(invoice);
            await _invoiceRepository.UpdateAsync(invoice);

            await transaction.CommitAsync();
            return deleted;
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}