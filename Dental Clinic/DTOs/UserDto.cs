// DTOs/UserDtos.cs
using System.ComponentModel.DataAnnotations;


/// <summary>DTO used when creating a user.</summary>
public class CreateUserDto
{
    [Required]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }

    public string Role { get; set; } = "User";
    public DateTime DateofBirth { get; set; }
}

/// <summary>DTO used when updating a user.</summary>
public class UpdateUserDto
{
    [Required]
    public int Id { get; set; }

    [Required]
    public string Username { get; set; }

    public string Password { get; set; }

    public DateTime DateofBirth { get; set; }
    public string Role { get; set; } = "User";
}

/// <summary>DTO for returning user info (without password fields).</summary>
public class ReadUserDto
{
    public int Id { get; set; }

    public string Username { get; set; }

    public string Role { get; set; }

    public DateTime DateofBirth { get; set; }
}

/// <summary>
/// DTOs/User/LoginDto.cs
/// </summary>
public class LoginDto
{
    public string Username { get; set; }
    public string Password { get; set; }
}

/// <summary>
/// DTOs/User/RegisterDto.cs
/// </summary>
public class RegisterDto
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string Role { get; set; } = "User"; // Admin / Doctor / Assistant etc.

    public DateTime DateofBirth { get; set; }
}


