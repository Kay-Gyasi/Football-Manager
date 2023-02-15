namespace Football_Manager.Users;

[Processor]
public class UserProcessor
{
    private readonly ITokenService _tokenService;
    private readonly UserManager<User> _userManager;

    public UserProcessor(ITokenService tokenService, UserManager<User> userManager)
    {
        _tokenService = tokenService;
        _userManager = userManager;
    }

    public async Task<OneOf<AuthToken, InvalidLoginException>> LoginAsync(LoginCommand command)
    {
        var user = await _userManager.FindByEmailAsync(command.Email);

        if (user is null || !await _userManager.CheckPasswordAsync(user, command.Password))
        {
            return new InvalidLoginException();
        }

        return await _tokenService.GenerateToken(user);
    }

    public async Task<bool> MakeAdmin(int id)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());
        if (user is null) return false;
        
        var isAdmin = await _userManager.IsInRoleAsync(user, "admin");
        if (!isAdmin)
        {
            await _userManager.AddToRoleAsync(user, "admin");
        }
        return true;
    }
}

public record LoginCommand(string Email, string Password);