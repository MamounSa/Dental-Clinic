[Route("api/payments")]
[ApiController]
public class PaymentController : ControllerBase
{
    private readonly IPaymentService _paymentService;

    public PaymentController(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    [HttpGet("all")]
    public async Task<ActionResult<IEnumerable<PaymentDto>>> GetAllPayments()
    {
        var payments = await _paymentService.GetAllPaymentsAsync();
        if (!payments.Any()) return NotFound();
        return Ok(payments);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PaymentDto>> GetPaymentById(int id)
    {
        var payment = await _paymentService.GetPaymentByIdAsync(id);
        if (payment == null) return NotFound();
        return Ok(payment);
    }

    [HttpPost("add")]
    public async Task<ActionResult<int?>> AddPayment([FromBody] PaymentDto paymentDto)
    {
        var paymentId = await _paymentService.AddPaymentAsync(paymentDto);
        if (paymentId == null) return BadRequest("Failed to add payment.");
        return CreatedAtAction(nameof(GetPaymentById), new { id = paymentId }, paymentId);
    }

    [HttpPut("update/{id}")]
    public async Task<ActionResult<bool>> UpdatePayment(int id, [FromBody] PaymentDto paymentDto)
    {
        if (id != paymentDto.Id) return BadRequest();
        var result = await _paymentService.UpdatePaymentAsync(paymentDto);
        if (!result) return NotFound();
        return Ok(result);
    }

    [HttpDelete("delete/{id}")]
    public async Task<ActionResult<bool>> DeletePayment(int id)
    {
        var result = await _paymentService.DeletePaymentAsync(id);
        if (!result) return NotFound();
        return Ok(result);
    }

    [HttpGet("search/byStatus")]
    public async Task<ActionResult<IEnumerable<PaymentDto>>> SearchByStatus([FromQuery] string status)
    {
        var payments = await _paymentService.SearchByStatusAsync(status);
        if (!payments.Any()) return NotFound();
        return Ok(payments);
    }

}
