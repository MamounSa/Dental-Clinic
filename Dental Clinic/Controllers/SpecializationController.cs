[Route("api/specializations")]
[ApiController]
public class SpecializationController : ControllerBase
{
    private readonly ISpecializationService _specializationService;

    public SpecializationController(ISpecializationService specializationService)
    {
        _specializationService = specializationService;
    }

    [HttpGet("all")]
    public async Task<ActionResult<IEnumerable<SpecializationDto>>> GetAllSpecializations()
    {
        var specializations = await _specializationService.GetAllSpecializationsAsync();
        if (!specializations.Any()) return NotFound();
        return Ok(specializations);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SpecializationDto>> GetSpecializationById(int id)
    {
        var specialization = await _specializationService.GetSpecializationByIdAsync(id);
        if (specialization == null) return NotFound();
        return Ok(specialization);
    }

    [HttpPost("add")]
    public async Task<ActionResult<int?>> AddSpecialization([FromBody] SpecializationDto specializationDto)
    {
        var specializationId = await _specializationService.AddSpecializationAsync(specializationDto);
        if (specializationId == null) return BadRequest("Failed to add specialization.");
        return CreatedAtAction(nameof(GetSpecializationById), new { id = specializationId }, specializationId);
    }

    [HttpPut("update/{id}")]
    public async Task<ActionResult<bool>> UpdateSpecialization(int id, [FromBody] SpecializationDto specializationDto)
    {
        if (id != specializationDto.Id) return BadRequest();
        var result = await _specializationService.UpdateSpecializationAsync(specializationDto);
        if (!result) return NotFound();
        return Ok(result);
    }

    [HttpDelete("delete/{id}")]
    public async Task<ActionResult<bool>> DeleteSpecialization(int id)
    {
        var result = await _specializationService.DeleteSpecializationAsync(id);
        if (!result) return NotFound();
        return Ok(result);
    }
}
