[Route("api/[controller]")]
[ApiController]
public class PatientController : ControllerBase
{
    private readonly IPatientService _patientService;

    public PatientController(IPatientService patientService)
    {
        _patientService = patientService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PatientDto>>> GetAllPatients()
    {
        var patients = await _patientService.GetAllPatientsAsync();
        if (!patients.Any())
            return NotFound();

        return Ok(patients);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PatientDto>> GetPatientById(int id)
    {
        var patient = await _patientService.GetPatientByIdAsync(id);
        if (patient == null)
            return NotFound();
        return Ok(patient);
    }

    [HttpPost("add")]

    public async Task<ActionResult<int?>> AddPatient([FromBody] PatientDto patientDto)
    {
        var patientId = await _patientService.AddPatientAsync(patientDto);
        if (patientId == null)
            return BadRequest("Failed to add patient.");

        return CreatedAtAction(nameof(GetPatientById), new { id = patientId }, patientId);
    }

    [HttpPut("update/{id}")]
    public async Task<ActionResult<bool>> UpdatePatient(int id, [FromBody] PatientDto patientDto)
    {
        if (id != patientDto.Id)
            return BadRequest();

        var result = await _patientService.UpdatePatientAsync(patientDto);
        if (!result)
            return NotFound();

        return Ok(result);
    }

    [HttpDelete("delete/{id}")]
    public async Task<ActionResult<bool>> DeletePatient(int id)
    {
        var result = await _patientService.DeletePatientAsync(id);
        if (!result)
            return NotFound();

        return Ok(result);
    }

    // 🟢 البحث عن المرضى حسب الاسم
    [HttpGet("search/byName")]
    public async Task<ActionResult<IEnumerable<PatientDto>>> SearchByName([FromQuery] string name)
    {
        var patients = await _patientService.SearchByNameAsync(name);
        if (!patients.Any())
            return NotFound();

        return Ok(patients);
    }

    // 🟢 البحث عن المرضى حسب البريد الإلكتروني
    [HttpGet("search/byEmail")]
    public async Task<ActionResult<IEnumerable<PatientDto>>> SearchByEmail([FromQuery] string email)
    {
        var patients = await _patientService.SearchByEmailAsync(email);
        if (!patients.Any())
            return NotFound();

        return Ok(patients);
    }

    // 🟢 البحث عن المرضى حسب رقم الهاتف
    [HttpGet("search/byPhone")]
    public async Task<ActionResult<IEnumerable<PatientDto>>> SearchByPhone([FromQuery] string phoneNumber)
    {
        var patients = await _patientService.SearchByPhoneAsync(phoneNumber);
        if (!patients.Any())
            return NotFound();

        return Ok(patients);
    }
}


