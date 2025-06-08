using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("api/[controller]")]

public class AuthController : ControllerBase
{
    private readonly IUserService _service;
    public AuthController(IUserService service) => _service = service;

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        var authResponse = await _service.LoginAsync(dto);
        if (authResponse == null)
            return Unauthorized("البيانات غير صحيحة");

        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTime.UtcNow.AddDays(7)
        };

        Response.Cookies.Append("refreshToken", authResponse.RefreshToken, cookieOptions);

        return Ok(new { accessToken = authResponse.AccessToken });
    }


    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh()
    {
        var refreshToken = Request.Cookies["refreshToken"];
        if (string.IsNullOrEmpty(refreshToken))
            return Unauthorized("لا يوجد Refresh Token");

        
        var result = await _service.RefreshTokenAsync(refreshToken);
        if (result == null)
            return Unauthorized("توكن غير صالح");

        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTime.UtcNow.AddDays(7)
        };

        Response.Cookies.Append("refreshToken", result.RefreshToken, cookieOptions);

        return Ok(new { accessToken = result.AccessToken });
    }
}