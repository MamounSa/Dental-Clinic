

[ApiController]
[Route("api/[controller]")]
public class MedicalRecordController : ControllerBase
{
    private readonly IMedicalRecordService _service;

    public MedicalRecordController(IMedicalRecordService service)
    {
        _service = service;
    }

    /// <summary>
    /// Get all medical records.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _service.GetAllAsync());
    }

    /// <summary>
    /// Get a medical record by its ID.
    /// </summary>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _service.GetByIdAsync(id);
        return result == null ? NotFound() : Ok(result);
    }

    /// <summary>
    /// Create a new medical record.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateMedicalRecordDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var id = await _service.AddAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id }, null);
    }

    /// <summary>
    /// Update an existing medical record.
    /// </summary>
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateMedicalRecordDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var success = await _service.UpdateAsync(dto);
        return success ? NoContent() : NotFound();
    }

    /// <summary>
    /// Delete a medical record.
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _service.DeleteAsync(id);
        return success ? NoContent() : NotFound();
    }
}