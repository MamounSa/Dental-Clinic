


    /// <summary>
    /// Controller for managing medical images.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class MedicalImageController : ControllerBase
    {
        private readonly IMedicalImageService _service;

        public MedicalImageController(IMedicalImageService service)
        {
            _service = service;
        }

        /// <summary>
        /// Gets all medical images.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

        /// <summary>
        /// Gets a specific medical image by ID.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _service.GetByIdAsync(id);
            return item == null ? NotFound() : Ok(item);
        }

        /// <summary>
        /// Creates a new medical image.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateMedicalImageDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var id = await _service.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id }, null);
        }

        /// <summary>
        /// Updates an existing medical image.
        /// </summary>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateMedicalImageDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var success = await _service.UpdateAsync(dto);
            return success ? NoContent() : NotFound();
        }

        /// <summary>
        /// Deletes a medical image by ID.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);
            return success ? NoContent() : NotFound();
        }
    }
