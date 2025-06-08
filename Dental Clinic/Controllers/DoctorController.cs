using Microsoft.AspNetCore.Mvc;

namespace YourNamespace.Controllers
{
    /// <summary>
    /// Controller for managing doctor-related operations.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _service;

        public DoctorController(IDoctorService service)
        {
            _service = service;
        }

        /// <summary>
        /// Gets all doctors.
        /// </summary>
        /// <returns>List of doctors.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

        /// <summary>
        /// Gets a doctor by ID.
        /// </summary>
        /// <param name="id">Doctor ID.</param>
        /// <returns>Doctor details if found, otherwise 404.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var doctor = await _service.GetByIdAsync(id);
            return doctor == null ? NotFound() : Ok(doctor);
        }

        /// <summary>
        /// Searches for doctors by name.
        /// </summary>
        /// <param name="name">Doctor's name or part of it.</param>
        /// <returns>List of matching doctors.</returns>
        [HttpGet("search")]
        public async Task<IActionResult> SearchByName([FromQuery] string name)
        {
            var result = await _service.SearchByNameAsync(name);
            return Ok(result);
        }

        /// <summary>
        /// Gets doctors by specialization ID.
        /// </summary>
        /// <param name="id">Specialization ID.</param>
        /// <returns>List of doctors with the given specialization.</returns>
        [HttpGet("byspecialization/{id}")]
        public async Task<IActionResult> GetBySpecialization(int id)
        {
            var result = await _service.GetBySpecializationAsync(id);
            return Ok(result);
        }

        /// <summary>
        /// Creates a new doctor.
        /// </summary>
        /// <param name="dto">Doctor creation data.</param>
        /// <returns>Created doctor location.</returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateDoctorDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var id = await _service.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id }, null);
        }

        /// <summary>
        /// Updates an existing doctor.
        /// </summary>
        /// <param name="dto">Updated doctor data.</param>
        /// <returns>No content if successful, otherwise 404.</returns>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateDoctorDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var success = await _service.UpdateAsync(dto);
            return success ? NoContent() : NotFound();
        }

        /// <summary>
        /// Deletes a doctor by ID.
        /// </summary>
        /// <param name="id">Doctor ID.</param>
        /// <returns>No content if deleted, otherwise 404.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);
            return success ? NoContent() : NotFound();
        }
    }
}