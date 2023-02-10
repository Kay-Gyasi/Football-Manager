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

        return _tokenService.GenerateToken(user);
    }
}

public record LoginCommand(string Email, string Password);