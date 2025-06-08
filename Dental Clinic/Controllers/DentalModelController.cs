using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;

/// <summary>
/// إدارة بيانات الأسنان للمريض.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class DentalModelController : ControllerBase
{
    private readonly IDentalModelService _dentalModelService;

    public DentalModelController(IDentalModelService dentalModelService)
    {
        _dentalModelService = dentalModelService;
    }

    /// <summary>
    /// استرجاع جميع الأسنان.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<DentalModelDto>>> GetAll()
    {
        var result = await _dentalModelService.GetAllAsync();
        return Ok(result);
    }

    /// <summary>
    /// استرجاع سن واحد عبر المعرف.
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<DentalModelDto>> GetById(int id)
    {
        var result = await _dentalModelService.GetByIdAsync(id);
        if (result == null)
            return NotFound();

        return Ok(result);
    }

    /// <summary>
    /// إنشاء سن جديد.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<int>> Create([FromBody] CreateDentalModelDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var newId = await _dentalModelService.CreateAsync(dto);
        if (newId == null)
            return BadRequest("تعذر إنشاء السن (تأكد من صحة المريض أو السجل الطبي).");

        return CreatedAtAction(nameof(GetById), new { id = newId }, newId);
    }

    /// <summary>
    /// تعديل حالة سن موجود.
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateDentalModelDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (id != dto.Id)
            return BadRequest("رقم المعرف غير متطابق.");

        var updated = await _dentalModelService.UpdateAsync(dto);
        if (!updated)
            return NotFound();

        return NoContent();
    }

    /// <summary>
    /// حذف سن من السجل.
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _dentalModelService.DeleteAsync(id);
        if (!deleted)
            return NotFound();

        return NoContent();
    }
}