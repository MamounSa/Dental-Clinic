using System.Security;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; }

    public byte[] PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set; }

    public string Role { get; set; } = "User";
    public DateTime DateofBirth { get; set; }

    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiryTime { get; set; }

    
}
public class UserPermission
{

    public int PermissionId { get; set; }

    public int UserId { get; set; }
    public User User { get; set; }


    //public Permission Permission { get; set; }
}
public enum Permission
{
    ViewProducts = 4,
    ManageUsers = 2,
    EditOrders = 3,
    DeleteProducts = 1
}
public class UserLogin
{
    public string Username { get; set; }
    public string Password { get; set; }
}
public class AuthResponseDto
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
}
public class RefreshRequestDto
{
    public string Username { get; set; }
    public string RefreshToken { get; set; }
}


