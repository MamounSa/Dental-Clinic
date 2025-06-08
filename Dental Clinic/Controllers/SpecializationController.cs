
    [ApiController]
    [Route("api/[controller]")]
    public class SpecializationController : ControllerBase
    {
        private readonly ISpecializationService _service;

        public SpecializationController(ISpecializationService service)
        {
            _service = service;
        }

        /// <summary>
        /// Get all specializations.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok(await _service.GetAllAsync());

        /// <summary>
        /// Get specialization by ID.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            return result == null ? NotFound() : Ok(result);
        }

        /// <summary>
        /// Create new specialization.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSpecializationDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var id = await _service.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id }, null);
        }

        /// <summary>
        /// Update existing specialization.
        /// </summary>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateSpecializationDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var success = await _service.UpdateAsync(dto);
            return success ? NoContent() : NotFound();
        }

        /// <summary>
        /// Delete specialization by ID.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);
            return success ? NoContent() : NotFound();
        }
    }
