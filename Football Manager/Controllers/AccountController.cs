namespace Football_Manager.Controllers;

public class AccountController : Controller
{
    private readonly ILogger<AccountController> _logger;
    private readonly IHttpClientFactory _factory;

    public AccountController(ILogger<AccountController> logger, 
        IHttpClientFactory factory)
    {
        _logger = logger;
        _factory = factory;
    }

    public IActionResult Signin()
    {
        return View();
    }
    
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();
        return RedirectToAction("Signin");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Send(SigninRequest command)
    {
        var response = await GetLoginResponseAsync(command);
        if (response is null) return RedirectToAction("Signin");

        var tokenHandler = new JwtSecurityTokenHandler();
        if (tokenHandler.ReadToken(response?.Token) is not JwtSecurityToken jwt)
        {
            _logger.LogError("Login returned invalid jwt");
            return RedirectToAction("Signin");
        }

        var claims = jwt.Claims;
        claims = claims.Append(new Claim("access_token", response?.Token ?? ""));
        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

        await HttpContext.SignInAsync(claimsPrincipal, new AuthenticationProperties
        {
            IsPersistent = true
        });
        
        return RedirectToAction("Index", "Home");
    }

    private async Task<SigninResponse?> GetLoginResponseAsync(SigninRequest command)
    {
        using var client = _factory.CreateClient("default");
        var request = await client.PostAsJsonAsync("users/login", command);

        if (request.IsSuccessStatusCode) return await request.Content.ReadFromJsonAsync<SigninResponse>();
        _logger.LogError("Error occurred while logging in");
        return null;
    }
}

public record SigninResponse(string Token);