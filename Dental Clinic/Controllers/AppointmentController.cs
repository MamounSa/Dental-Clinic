using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("api/[controller]")]
public class AppointmentController : ControllerBase
{
    private readonly IAppointmentService _service;

    public AppointmentController(IAppointmentService service)
    {
        _service = service;
    }

    /// <summary>
    /// الحصول على جميع المواعيد.
    /// </summary>
    [HttpGet]

    public async Task<IActionResult> GetAll()
    {
        var result = await _service.GetAllAsync();
        return Ok(result);
    }

    /// <summary>
    /// الحصول على موعد بواسطة المعرّف.
    /// </summary>


    [Authorize]
    [HttpGet("{id}")]
    [Authorize(Policy = "AtLeast18")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _service.GetByIdAsync(id);
        if (result == null) return NotFound();
        return Ok(result);
    }

    /// <summary>
    /// الحصول على مواعيد حسب معرف الطبيب.
    /// </summary>
    [HttpGet("bydoctor/{doctorId}")]
    public async Task<IActionResult> GetByDoctorId(int doctorId)
    {
        var result = await _service.GetByDoctorIdAsync(doctorId);
        return Ok(result);
    }

    /// <summary>
    /// الحصول على مواعيد حسب معرف المريض.
    /// </summary>
    [HttpGet("bypatient/{patientId}")]
    public async Task<IActionResult> GetByPatientId(int patientId)
    {
        var result = await _service.GetByPatientIdAsync(patientId);
        return Ok(result);
    }

    /// <summary>
    /// إنشاء موعد جديد.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateAppointmentDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var id = await _service.AddAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id }, null);
    }

    /// <summary>
    /// تعديل موعد.
    /// </summary>
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateAppointmentDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var success = await _service.UpdateAsync(dto);
        return success ? NoContent() : NotFound();
    }

    /// <summary>
    /// حذف موعد.
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _service.DeleteAsync(id);
        return success ? NoContent() : NotFound();
    }


    [HttpGet("appointments-summary")]
    public async Task<IActionResult> GetAppointmentsReport([FromQuery] DateTime from, [FromQuery] DateTime to, [FromQuery] int? doctorId)
    {
        var report = await _service.GetAppointmentsReportAsync(from, to, doctorId);
        return Ok(report);
    }

    [HttpGet("weekly-calendar")]
    public async Task<IActionResult> GetWeeklyCalendar([FromQuery] DateTime date, [FromQuery] int? doctorId)
    {
        // date هو أي يوم داخل الأسبوع اللي بدك تجيب تقويمه
        var calendar = await _service.GetWeeklyCalendarAsync(date, doctorId);
        return Ok(calendar);
    }

    [HttpGet("filter")]
    public async Task<IActionResult> Filter([FromBody] AppointmentFilterDto filter)
    {
        var data = await _service.FilterAppointmentsAsync(filter);
        return Ok(data);
    }
}
