using Microsoft.AspNetCore.Authentication;

namespace Football_Manager.Controllers;

[Authorize]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IHttpClientFactory _factory;
    private PlayerDto? _player;
    private CoachDto? _coach;
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

            _coach = await request.Content.ReadFromJsonAsync<CoachDto>();
            return View(new UserModel{Coach = _coach});
        }

        request = await client
            .GetAsync($"players/getByType/{UserId}");
        if (!request.IsSuccessStatusCode) return View();

        _player = await request.Content.ReadFromJsonAsync<PlayerDto>();
        return View(new UserModel{Player = _player});
    }

    public async Task<IActionResult> EditPlayer()
    {
        using var client = _factory.CreateClient("default");
        var request = await client.GetAsync($"players/getByType/{UserId}");
        if (!request.IsSuccessStatusCode) return View(new UserCommandModel{Player = (PlayerCommand)_player});

        _player = await request.Content.ReadFromJsonAsync<PlayerDto>();
        return View(new UserCommandModel{Player = (PlayerCommand)_player});
    }
    
    public async Task<IActionResult> EditCoach()
    {
        using var client = _factory.CreateClient("default");
        var request = await client.GetAsync($"coaches/getByType/{UserId}");
        if (!request.IsSuccessStatusCode) return View((CoachCommand)_coach);

        _coach = await request.Content.ReadFromJsonAsync<CoachDto>();
        return View((CoachCommand)_coach);
    }
    
    // TODO:: Work on teams lookup
    public async Task<IActionResult> SaveProfileChanges(UserCommandModel command)
    {
        using var client = _factory.CreateClient("default");
        HttpResponseMessage response;
        if(Role == "Player")
        {
            response = await client.PostAsJsonAsync("players/save", command.Player);
        }
        else
        {
            response = await client.PostAsJsonAsync("coaches/save", command.Coach);
        }

        return RedirectToAction(response.IsSuccessStatusCode ? "Index" : "EditPlayer");
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