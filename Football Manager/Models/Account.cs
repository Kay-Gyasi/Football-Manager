namespace Football_Manager.Models;

public class SigninCommand
{
    [Required]
    public string Email { get; set; } = null!;

    [Required]
    public string Password { get; set; } = null!;
}