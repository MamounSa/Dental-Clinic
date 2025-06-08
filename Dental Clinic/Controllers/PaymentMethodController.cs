
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentMethodController : ControllerBase
    {
        private readonly IPaymentMethodService _service;

        public PaymentMethodController(IPaymentMethodService service)
        {
            _service = service;
        }

        /// <summary>
        /// Get all payment methods.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok(await _service.GetAllAsync());

        /// <summary>
        /// Get payment method by ID.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            return result == null ? NotFound() : Ok(result);
        }

        /// <summary>
        /// Create a new payment method.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePaymentMethodDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var id = await _service.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id }, null);
        }

        /// <summary>
        /// Update a payment method.
        /// </summary>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdatePaymentMethodDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var success = await _service.UpdateAsync(dto);
            return success ? NoContent() : NotFound();
        }

        /// <summary>
        /// Delete a payment method.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);
            return success ? NoContent() : NotFound();
        }
    }
