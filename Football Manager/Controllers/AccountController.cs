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

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Send(SigninRequest command)
    {
        using var client = _factory.CreateClient("default");
        var request = await client.PostAsJsonAsync("users/login", command);

        if (!request.IsSuccessStatusCode)
        {
            _logger.LogError("Error occurred while logging in");
            return RedirectToAction("Signin");
        }

        var response = await request.Content.ReadFromJsonAsync<SigninResponse>();

        var tokenHandler = new JwtSecurityTokenHandler();
        if (tokenHandler.ReadToken(response?.Token) is not JwtSecurityToken jwt)
        {
            _logger.LogError("Login returned invalid jwt");
            return RedirectToAction("Signin");
        }

        var claims = jwt.Claims;
        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

        await HttpContext.SignInAsync(claimsPrincipal, new AuthenticationProperties
        {
            IsPersistent = true
        });
        
        return RedirectToAction("Index", "Home");
    }
}

public record SigninResponse(string Token);