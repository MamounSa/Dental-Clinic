// Repositories/IUserRepository.cs

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllAsync();
    Task<User?> GetByIdAsync(int id);
    Task<User?> GetByUsernameAsync(string username);
    Task AddAsync(User user);
    Task UpdateAsync(User user);
    Task DeleteAsync(User user);
    Task SaveChangesAsync();

    Task<User?> GetByRefreshTokenAsync(string refreshToken);
}