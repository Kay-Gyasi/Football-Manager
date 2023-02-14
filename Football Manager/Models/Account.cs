using Microsoft.AspNetCore.Identity;
using System.ComponentModel;

namespace Football_Manager.Models;

public class SigninRequest
{
    [Required]
    public string Email { get; set; } = null!;

    [Required]
    public string Password { get; set; } = null!;
}

public class UserCommand
{
    [DisplayName("User Name")]
    public string? UserName { get; set; }

    [DisplayName("Email Address")]
    [Required]
    public string? Email { get; set; }

    [DisplayName("Phone Number")]
    [Required]
    public string? PhoneNumber { get; set; }

    [DisplayName("First Name")]
    [Required]
    public string FirstName { get; set; }

    [DisplayName("Date of Birth")]
    public DateTime? DateOfBirth { get; set; }

    [DisplayName("Last Name")]
    [Required]
    public string LastName { get; set; }
    public string? Password { get; set; }

    public static explicit operator UserCommand(UserDto dto)
    {
        return new UserCommand()
        {
            UserName = dto.UserName,
            Email = dto.Email,
            PhoneNumber = dto.PhoneNumber,
            FirstName = dto.FirstName,
            DateOfBirth = dto.DateOfBirth,
            LastName = dto.LastName
        };
    }
}