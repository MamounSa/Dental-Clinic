[ApiController]
[Route("api/[controller]")]
public class PaymentController : ControllerBase
{
    private readonly IPaymentService _paymentService;

    public PaymentController(IPaymentService paymentService)
    { 
        _paymentService = paymentService;
    }

    // GET: api/payment
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var payments = await _paymentService.GetAllAsync();
        return Ok(payments);
    }

    // GET: api/payment/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var payment = await _paymentService.GetByIdAsync(id);
        if (payment == null)
            return NotFound();

        return Ok(payment);
    }

    // GET: api/payment/invoice/3
    [HttpGet("invoice/{invoiceId}")]
    public async Task<IActionResult> GetByInvoiceId(int invoiceId)
    {
        var payments = await _paymentService.GetByInvoiceIdAsync(invoiceId);
        return Ok(payments);
    }

    // POST: api/payment
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePaymentDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var paymentResult = await _paymentService.CreateAsync(dto);

        if (paymentResult == null)
            return StatusCode(500, "حدث خطأ أثناء إنشاء الدفع.");

        // بيرجع 201 Created وفيه رابط للدفع الجديد + تفاصيل الدفع
        return CreatedAtAction(nameof(GetById), new { id = paymentResult.PaymentId }, paymentResult);
    }

    // PUT: api/payment/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdatePaymentDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var success = await _paymentService.UpdateAsync(id, dto);
        if (!success)
            return NotFound();

        return NoContent();
    }

    // DELETE: api/payment/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _paymentService.DeleteAsync(id);
        if (!success)
            return NotFound();

        return NoContent();
    }


    
}