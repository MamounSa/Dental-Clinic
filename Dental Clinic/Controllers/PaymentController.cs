using Microsoft.AspNetCore.Mvc;

namespace YourNamespace.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _service;

        public PaymentController(IPaymentService service)
        {
            _service = service;
        }

        /// <summary>
        /// Get all payments.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var payments = await _service.GetAllAsync();
            return Ok(payments);
        }

        /// <summary>
        /// Get a payment by its ID.
        /// </summary>
        /// <param name="id">Payment ID</param>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var payment = await _service.GetByIdAsync(id);
            return payment == null ? NotFound() : Ok(payment);
        }

        /// <summary>
        /// Get payments for a specific patient.
        /// </summary>
        /// <param name="patientId">Patient ID</param>
        [HttpGet("bypatient/{patientId}")]
        public async Task<IActionResult> GetByPatientId(int patientId)
        {
            var payments = await _service.GetByPatientIdAsync(patientId);
            return Ok(payments);
        }

        /// <summary>
        /// Create a new payment record.
        /// </summary>
        /// <param name="dto">Payment data</param>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePaymentDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var id = await _service.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id }, null);
        }

        /// <summary>
        /// Update an existing payment.
        /// </summary>
        /// <param name="dto">Updated payment data</param>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdatePaymentDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var success = await _service.UpdateAsync(dto);
            return success ? NoContent() : NotFound();
        }

        /// <summary>
        /// Delete a payment by ID.
        /// </summary>
        /// <param name="id">Payment ID</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);
            return success ? NoContent() : NotFound();
        }
    }
}