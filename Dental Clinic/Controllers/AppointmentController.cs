using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic;

[Route("api/appointments")]
[ApiController]
[Authorize]
public class AppointmentController : ControllerBase
{
    private readonly IAppointmentService _appointmentService;
    private readonly IOptionsMonitor<AppSettings> _appSettingsMonitor;
    
    public AppointmentController(IAppointmentService appointmentService, IOptionsMonitor<AppSettings> appSettingsMonitor)
    {
        _appointmentService = appointmentService;
        _appSettingsMonitor = appSettingsMonitor;
        var value=_appSettingsMonitor.CurrentValue;
       /* _appSettingsMonitor.OnChange(newSettings =>
        {
            Console.WriteLine($"Monitor: Settings changed! New MaxItemsPerPage: {newSettings.MaxItemsPerPage}");
        });*/

        
    }
    [HttpGet("geyyyyyt")]
    public  IActionResult Get()
    {
        
        Thread.Sleep(8000);
        return Ok(new
        {
            
            Monitor =_appSettingsMonitor.CurrentValue,
        });
    }
    [HttpGet("all")]
    public async Task<ActionResult<IEnumerable<AppointmentDto>>> GetAllAppointments()
    {
        var user = User.Identity.Name;
        var appointments = await _appointmentService.GetAllAppointmentsAsync();
        if (!appointments.Any()) return NotFound();
        return Ok(appointments);
    }

    [HttpGet("Getbyid")]
    public async Task<ActionResult<AppointmentDto>> GetAppointmentById([FromHeader(Name = "Accept-Language")]int id)
    {
        var appointment = await _appointmentService.GetAppointmentByIdAsync(id);
        if (appointment == null) return NotFound();
        return Ok(appointment);
    }

    [HttpPost("add")]
    public async Task<ActionResult<int?>> AddAppointment([FromBody] AppointmentDto Appointment1)
    {
        var appointmentId = await _appointmentService.AddAppointmentAsync(Appointment1);
        if (appointmentId == null) return BadRequest("Failed to add appointment.");
        return CreatedAtAction(nameof(GetAppointmentById), new { id = appointmentId }, appointmentId);
    }

    [HttpPut("update/{id}")]
    public async Task<ActionResult<bool>> UpdateAppointment(int id, [FromBody] AppointmentDto appointmentDto)
    {
        if (id != appointmentDto.Id) return BadRequest();
        var result = await _appointmentService.UpdateAppointmentAsync(appointmentDto);
        if (!result) return NotFound();
        return Ok(result);
    }

    [HttpDelete("delete/{id}")]
    public async Task<ActionResult<bool>> DeleteAppointment(int id)
    {
        var result = await _appointmentService.DeleteAppointmentAsync(id);
        if (!result) return NotFound();
        return Ok(result);
    }
}
