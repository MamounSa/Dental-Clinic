using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Controller for managing patient-related operations.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class PatientController : ControllerBase
{
    private readonly IPatientService _service;

    public PatientController(IPatientService service)
    {
        _service = service;
    }

    /// <summary>
    /// Gets all patients.
    /// </summary>
    /// <returns>List of patients.</returns>
    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

    /// <summary>
    /// Gets a patient by ID.
    /// </summary>
    /// <param name="id">Patient ID.</param>
    /// <returns>Patient details if found; otherwise, 404 Not Found.</returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var patient = await _service.GetByIdAsync(id);
        return patient == null ? NotFound() : Ok(patient);
    }

    /// <summary>
    /// Searches patients by name.
    /// </summary>
    /// <param name="name">Full or partial patient name.</param>
    /// <returns>List of matching patients.</returns>
    [HttpGet("search")]
    public async Task<IActionResult> SearchByName([FromQuery] string name)
    {
        var result = await _service.SearchByNameAsync(name);
        return Ok(result);
    }

    /// <summary>
    /// Creates a new patient record.
    /// </summary>
    /// <param name="dto">Patient creation data.</param>
    /// <returns>201 Created with patient ID if successful.</returns>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePatientDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var id = await _service.AddAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id }, null);
    }

    /// <summary>
    /// Updates an existing patient record.
    /// </summary>
    /// <param name="dto">Updated patient data.</param>
    /// <returns>NoContent (204) if successful; NotFound (404) if not found.</returns>
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdatePatientDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var success = await _service.UpdateAsync(dto);
        return success ? NoContent() : NotFound();
    }

    /// <summary>
    /// Deletes a patient by ID.
    /// </summary>
    /// <param name="id">Patient ID.</param>
    /// <returns>NoContent (204) if deleted; NotFound (404) if not found.</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _service.DeleteAsync(id);
        return success ? NoContent() : NotFound();
    }
}