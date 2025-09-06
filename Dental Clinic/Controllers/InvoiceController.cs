using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

[ApiController]
[Route("api/[controller]")]
public class InvoiceController : ControllerBase
{
    private readonly IInvoiceService _service;

    public InvoiceController(IInvoiceService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateInvoiceDto dto)
    {
        var id = await _service.CreateAsync(dto);
        return Ok(new { Id = id });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateInvoiceDto dto)
    {
        var result = await _service.UpdateAsync(id, dto);
        return result ? Ok() : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _service.DeleteAsync(id);
        return result ? Ok() : NotFound();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var invoice = await _service.GetByIdAsync(id);
        return invoice == null ? NotFound() : Ok(invoice);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var invoices = await _service.GetAllAsync();
        return Ok(invoices);
    }

    /*[HttpGet("{id}/is-paid")]
    public async Task<IActionResult> IsPaid(int id)
    {
        var isPaid = await _service.IsPaidAsync(id);
        return Ok(new { IsPaid = isPaid });
    }*/

    [HttpGet("{id}/balance")]
    public async Task<IActionResult> GetRemainingBalance(int id)
    {
        var balance = await _service.GetRemainingBalanceAsync(id);
        return Ok(new { InvoiceId = id, RemainingBalance = balance });
    }

    [HttpGet("{id}/summary")]
    public async Task<IActionResult> GetInvoiceSummary(int id)
    {
        var summary = await _service.GetInvoiceSummaryAsync(id);
        if (summary == null)
            return NotFound();

        return Ok(summary);
    }

    [HttpPost("filter")]
    public async Task<IActionResult> Filter([FromBody] InvoiceFilterDto filter)
    {
        
        var result = await _service.FilterInvoicesAsync(filter);
        return Ok();
    }
}