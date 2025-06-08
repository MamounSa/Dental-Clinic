// Services/IUserService.cs

public interface IUserService
{
    Task<IEnumerable<ReadUserDto>> GetAllAsync();
    Task<ReadUserDto?> GetByIdAsync(int id);
    Task<int> AddAsync(CreateUserDto dto);
    Task<bool> UpdateAsync(UpdateUserDto dto);
    Task<bool> DeleteAsync(int id);

    Task<AuthResponseDto?> LoginAsync(LoginDto dto); // تم التعديل هنا
    public  Task<AuthResponseDto?> RegisterAsync(RegisterDto dto); // تم الإضافة هنا

    public Task<AuthResponseDto?> RefreshTokenAsync(string refreshToken);
}