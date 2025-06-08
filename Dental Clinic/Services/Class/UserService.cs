using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;



public class UserService : IUserService
{
    private readonly IUserRepository _repository;
    private readonly IMapper _mapper;
    private readonly JwtSettings _jwtSettings;

    public UserService(IUserRepository repository, IMapper mapper, IOptions<JwtSettings> jwtOptions)
    {
        _repository = repository;
        _mapper = mapper;
        _jwtSettings = jwtOptions.Value;
    }

    public async Task<IEnumerable<ReadUserDto>> GetAllAsync()
    {
        var users = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<ReadUserDto>>(users);
    }

    public async Task<ReadUserDto?> GetByIdAsync(int id)
    {
        var user = await _repository.GetByIdAsync(id);
        return user is null ? null : _mapper.Map<ReadUserDto>(user);
    }

    public async Task<int> AddAsync(CreateUserDto dto)
    {
        CreatePasswordHash(dto.Password, out var hash, out var salt);
        var user = _mapper.Map<User>(dto);
        user.PasswordHash = hash;
        user.PasswordSalt = salt;
        await _repository.AddAsync(user);
        await _repository.SaveChangesAsync();
        return user.Id;
    }

    public async Task<bool> UpdateAsync(UpdateUserDto dto)
    {
        var user = await _repository.GetByIdAsync(dto.Id);
        if (user is null) return false;

        user.Username = dto.Username;
        user.Role = dto.Role;
        user.DateofBirth = dto.DateofBirth;

        if (!string.IsNullOrWhiteSpace(dto.Password))
        {
            CreatePasswordHash(dto.Password, out var hash, out var salt);
            user.PasswordHash = hash;
            user.PasswordSalt = salt;
        }

        await _repository.UpdateAsync(user);
        await _repository.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var user = await _repository.GetByIdAsync(id);
        if (user is null) return false;
        await _repository.DeleteAsync(user);
        await _repository.SaveChangesAsync();
        return true;
    }

    public async Task<AuthResponseDto?> LoginAsync(LoginDto dto)
    {
        var user = await _repository.GetByUsernameAsync(dto.Username);
        if (user == null || !VerifyPassword(dto.Password, user.PasswordHash, user.PasswordSalt))
            return null;

        var accessToken = GenerateJwtToken(user);
        var refreshToken = GenerateRefreshToken();

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
        await _repository.UpdateAsync(user);
        await _repository.SaveChangesAsync();

        return new AuthResponseDto
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };
    }

    public async Task<AuthResponseDto?> RegisterAsync(RegisterDto dto)
    {
        if (await _repository.GetByUsernameAsync(dto.Username) != null)
            return null;

        CreatePasswordHash(dto.Password, out byte[] hash, out byte[] salt);
        
        var refreshToken = GenerateRefreshToken();
        var user = new User
        {
            Username = dto.Username,
            PasswordHash = hash,
            PasswordSalt = salt,
            Role = dto.Role,
            DateofBirth = dto.DateofBirth,
            RefreshToken = refreshToken,
            RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7)
        };
        var accessToken = GenerateJwtToken(user);
        await _repository.AddAsync(user);
        await _repository.SaveChangesAsync();
       
        return new AuthResponseDto
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };
    }

    public async Task<AuthResponseDto?> RefreshTokenAsync(string refreshToken)
    {
        // ابحث عن المستخدم الذي يملك هذا الـ refreshToken
        var user = await _repository.GetByRefreshTokenAsync(refreshToken);
        if (user == null || user.RefreshTokenExpiryTime < DateTime.UtcNow)
            return null; // التوكين غير صالح أو منتهي

        // أنشئ توكين جديد
        var newAccessToken = GenerateJwtToken(user);
        var newRefreshToken = GenerateRefreshToken();

        // حدث بيانات التوكين في قاعدة البيانات
        user.RefreshToken = newRefreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

        await _repository.UpdateAsync(user);
        await _repository.SaveChangesAsync();

        return new AuthResponseDto
        {
            AccessToken = newAccessToken,
            RefreshToken = newRefreshToken
        };
    }

    private string GenerateJwtToken(User user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Name, user.Username),
            new(ClaimTypes.Role, user.Role),
            new("DateofBirth", user.DateofBirth.ToString("yyyy-MM-dd"))
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddHours(4),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    

    private string GenerateRefreshToken()
    {

        var randomBytes = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomBytes);
        return Convert.ToBase64String(randomBytes);
    }

    private void CreatePasswordHash(string password, out byte[] hash, out byte[] salt)
    {
        using var hmac = new HMACSHA512();
        salt = hmac.Key;
        hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
    }

    private bool VerifyPassword(string password, byte[] hash, byte[] salt)
    {
        using var hmac = new HMACSHA512(salt);
        var computed = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        return computed.SequenceEqual(hash);
    }

    
}