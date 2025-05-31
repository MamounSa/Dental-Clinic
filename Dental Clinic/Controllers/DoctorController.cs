[Route("api/doctors")]
[ApiController]
public class DoctorController : ControllerBase
{
    private readonly IDoctorService _doctorService;

    public DoctorController(IDoctorService doctorService)
    {
        _doctorService = doctorService;
    }

    [HttpGet("all")]
    public async Task<ActionResult<IEnumerable<DoctorDto>>> GetAllDoctors()
    {
        var doctors = await _doctorService.GetAllDoctorsAsync();
        if (!doctors.Any()) return NotFound();
        return Ok(doctors);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<DoctorDto>> GetDoctorById(int id)
    {
        var doctor = await _doctorService.GetDoctorByIdAsync(id);
        if (doctor == null) return NotFound();
        return Ok(doctor);
    }

    [HttpPost("add")]
    public async Task<ActionResult<int?>> AddDoctor([FromBody] DoctorDto doctorDto)
    {
        var doctorId = await _doctorService.AddDoctorAsync(doctorDto);
        if (doctorId == null) return BadRequest("Failed to add doctor.");
        return CreatedAtAction(nameof(GetDoctorById), new { id = doctorId }, doctorId);
    }

    [HttpPut("update/{id}")]
    public async Task<ActionResult<bool>> UpdateDoctor(int id, [FromBody] DoctorDto doctorDto)
    {
        if (id != doctorDto.Id) return BadRequest();
        var result = await _doctorService.UpdateDoctorAsync(doctorDto);
        if (!result) return NotFound();
        return Ok(result);
    }

    [HttpDelete("delete/{id}")]
    public async Task<ActionResult<bool>> DeleteDoctor(int id)
    {
        var result = await _doctorService.DeleteDoctorAsync(id);
        if (!result) return NotFound();
        return Ok(result);
    }
    [HttpGet("search/byName")]
    public async Task<ActionResult<IEnumerable<DoctorDto>>> SearchByName([FromQuery] string name)
    {
        var doctors = await _doctorService.SearchByNameAsync(name);
        if (!doctors.Any()) return NotFound();
        return Ok(doctors);
    }

    [HttpGet("search/byEmail")]
    public async Task<ActionResult<IEnumerable<DoctorDto>>> SearchByEmail([FromQuery] string email)
    {
        var doctors = await _doctorService.SearchByEmailAsync(email);
        if (!doctors.Any()) return NotFound();
        return Ok(doctors);
    }

    [HttpGet("search/byPhone")]
    public async Task<ActionResult<IEnumerable<DoctorDto>>> SearchByPhone([FromQuery] string phoneNumber)
    {
        var doctors = await _doctorService.SearchByPhoneAsync(phoneNumber);
        if (!doctors.Any()) return NotFound();
        return Ok(doctors);
    }

    [HttpGet("search/bySpecialization")]
    public async Task<ActionResult<IEnumerable<DoctorDto>>> GetDoctorsBySpecialization([FromQuery] int specializationId)
    {
        var doctors = await _doctorService.GetDoctorsBySpecializationAsync(specializationId);
        if (!doctors.Any()) return NotFound();
        return Ok(doctors);
    }

}
