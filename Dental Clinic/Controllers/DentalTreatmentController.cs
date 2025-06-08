
[ApiController]
[Route("api/[controller]")]
public class DentalTreatmentController : ControllerBase
{
    private readonly IDentalTreatmentService _service;

    public DentalTreatmentController(IDentalTreatmentService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _service.GetAllAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _service.GetByIdAsync(id);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpGet("bydentalmodel/{dentalModelId}")]
    public async Task<IActionResult> GetByDentalModelId(int dentalModelId)
    {
        var result = await _service.GetByDentalModelIdAsync(dentalModelId);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateDentalTreatmentDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var id = await _service.AddAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id }, null);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateDentalTreatmentDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var updated = await _service.UpdateAsync(dto);
        if (!updated) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _service.DeleteAsync(id);
        if (!deleted) return NotFound();
        return NoContent();
    }
}