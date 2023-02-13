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
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string FirstName { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string LastName { get; set; }
    public string? Password { get; set; }
}