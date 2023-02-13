namespace Football_Manager.Controllers;

[Authorize]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IHttpClientFactory _factory;
    private string UserId => User?.FindFirst(JwtRegisteredClaimNames.NameId)?.Value ?? "";
    private string Role => User?.FindFirst("role")?.Value ?? "";

    public HomeController(ILogger<HomeController> logger, IHttpClientFactory factory)
    {
        _logger = logger;
        _factory = factory;
    }

    public async Task<IActionResult> Index()
    {
        using var client = _factory.CreateClient("default");
        HttpResponseMessage request;
        if (Role == "Coach")
        {
            request = await client
                .GetAsync($"coaches/getByType/{UserId}");
            if (!request.IsSuccessStatusCode) return View();

            var coach = await request.Content.ReadFromJsonAsync<CoachDto>();
            return View(new UserModel{Coach = coach});
        }

        request = await client
            .GetAsync($"players/getByType/{UserId}");
        if (!request.IsSuccessStatusCode) return View();

        var player = await request.Content.ReadFromJsonAsync<PlayerDto>();
        return View(new UserModel{Player = player});
    }

    public IActionResult EditProfile()
    {
        return View();
    }
    
    public IActionResult SaveProfileChanges(PlayerCommand command)
    {   
        var isParsed = int.TryParse(UserId, out var id);
        if(!isParsed) return RedirectToAction("EditProfile");

        command.UserId = id;
        return RedirectToAction("EditProfile");
    }
    
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
    }
}