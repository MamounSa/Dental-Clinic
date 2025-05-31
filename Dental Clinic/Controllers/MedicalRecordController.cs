[Route("api/medicalRecords")]
[ApiController]
public class MedicalRecordController : ControllerBase
{
    private readonly IMedicalRecordService _medicalRecordService;

    public MedicalRecordController(IMedicalRecordService medicalRecordService)
    {
        _medicalRecordService = medicalRecordService;
    }

    [HttpGet("all")]
    public async Task<ActionResult<IEnumerable<MedicalRecordDto>>> GetAllMedicalRecords()
    {
        var records = await _medicalRecordService.GetAllMedicalRecordsAsync();
        if (!records.Any()) return NotFound();
        return Ok(records);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MedicalRecordDto>> GetMedicalRecordById(int id)
    {
        var record = await _medicalRecordService.GetMedicalRecordByIdAsync(id);
        if (record == null) return NotFound();
        return Ok(record);
    }

    [HttpPost("add")]
    public async Task<ActionResult<int?>> AddMedicalRecord([FromBody] MedicalRecordDto medicalRecordDto)
    {
        var recordId = await _medicalRecordService.AddMedicalRecordAsync(medicalRecordDto);
        if (recordId == null) return BadRequest("Failed to add medical record.");
        return CreatedAtAction(nameof(GetMedicalRecordById), new { id = recordId }, recordId);
    }

    [HttpPut("update/{id}")]
    public async Task<ActionResult<bool>> UpdateMedicalRecord(int id, [FromBody] MedicalRecordDto medicalRecordDto)
    {
        if (id != medicalRecordDto.Id) return BadRequest();
        var result = await _medicalRecordService.UpdateMedicalRecordAsync(medicalRecordDto);
        if (!result) return NotFound();
        return Ok(result);
    }

    [HttpDelete("delete/{id}")]
    public async Task<ActionResult<bool>> DeleteMedicalRecord(int id)
    {
        var result = await _medicalRecordService.DeleteMedicalRecordAsync(id);
        if (!result) return NotFound();
        return Ok(result);
    }

    [HttpGet("search/byPatientName")]
    public async Task<ActionResult<IEnumerable<MedicalRecordDto>>> SearchByPatientName([FromQuery] string patientName)
    {
        var records = await _medicalRecordService.SearchByPatientNameAsync(patientName);
        if (!records.Any()) return NotFound();
        return Ok(records);
    }

}
