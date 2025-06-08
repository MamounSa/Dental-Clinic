
    /// <summary>
    /// API لإدارة المستخدمين في النظام.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        /// <summary>
        /// جلب كل المستخدمين.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _service.GetAllAsync();
            return Ok(users);
        }

        /// <summary>
        /// جلب مستخدم عبر المعرف.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _service.GetByIdAsync(id);
            return user is null ? NotFound() : Ok(user);
        }

        /// <summary>
        /// إنشاء مستخدم جديد.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var id = await _service.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id }, null);
        }

        /// <summary>
        /// تعديل مستخدم موجود.
        /// </summary>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateUserDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var success = await _service.UpdateAsync(dto);
            return success ? NoContent() : NotFound();
        }

        /// <summary>
        /// حذف مستخدم عبر المعرف.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);
            return success ? NoContent() : NotFound();
        }
    }
