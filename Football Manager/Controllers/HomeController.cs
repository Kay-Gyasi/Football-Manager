namespace Football_Manager.Controllers;

[Authorize]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IHttpClientFactory _factory;
    private string UserId => User?.FindFirst("userid")?.Value ?? "";
    private IEnumerable<Claim>? Roles => User?.FindAll("role");

    public HomeController(ILogger<HomeController> logger, IHttpClientFactory factory)
    {
        _logger = logger;
        _factory = factory;
    }

    public async Task<IActionResult> Index()
    {
        using var client = GetClient();
        HttpResponseMessage request;
        if (Roles!.Any(x => x.Value == "Coach"))
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

    public async Task<IActionResult> EditPlayer()
    {
        using var client = GetClient();
        var requestTask = client.GetAsync($"players/getByType/{UserId}");
        var teamsRequestTask = client.GetAsync("Teams/GetLookup");

        await Task.WhenAll(requestTask, teamsRequestTask);

        var request = await requestTask;
        var teamsRequest = await teamsRequestTask;
        if (!request.IsSuccessStatusCode) return View();

        var player = await request.Content.ReadFromJsonAsync<PlayerDto>();
        if(!teamsRequest.IsSuccessStatusCode)
            return View(new UserCommandModel{Player = (PlayerCommand) player});
        
        return View(new UserCommandModel
        {
            Player = (PlayerCommand) player,
            Teams = await teamsRequest.Content.ReadFromJsonAsync<List<Lookup>>()
        });
    }

    [AdminAuthorize]
    public async Task<IActionResult> CreatePlayer()
    {
        using var client = GetClient();
        var request = await client.GetAsync("Teams/GetLookup");
        
        if (!request.IsSuccessStatusCode) return View();

        if(!request.IsSuccessStatusCode)
            return View();
        
        return View(new UserCommandModel
        {
            Teams = await request.Content.ReadFromJsonAsync<List<Lookup>>()
        });
    }
    
    public async Task<IActionResult> SaveProfileChanges(UserCommandModel command)
    {
        using var client = GetClient();
        HttpResponseMessage response;
        if(Roles!.Any(x => x.Value == "Player"))
        {
            if (command.Player?.Id is not null or 0)
            {
                response = await client.PostAsJsonAsync("players/update", command.Player);   
            }
            else
            {
                response = await client.PostAsJsonAsync("players/save", command.Player);
            }
        }
        else
        {
            if (command.Coach?.Id is not null or 0)
            {
                response = await client.PostAsJsonAsync("coaches/update", command.Coach);   
            }
            else
            {
                response = await client.PostAsJsonAsync("coaches/save", command.Coach);
            }
        }

        return RedirectToAction(response.IsSuccessStatusCode ? "Index" : "EditPlayer");
    }

    public async Task<IActionResult> Team()
    {
        using var client = GetClient();
        var coachTask = client.GetAsync("Teams/GetCoach");
        var playersTask = client.PostAsJsonAsync("Teams/GetPlayersPage", PaginatedQuery.Build());

        await Task.WhenAll(coachTask, playersTask);

        var coachRequest = await coachTask;
        var playersRequest = await playersTask;

        if (!coachRequest.IsSuccessStatusCode || !playersRequest.IsSuccessStatusCode)
            return View();

        return View(new TeamModel()
        {
            Coach = await coachRequest.Content.ReadFromJsonAsync<CoachDto>(),
            Players = await playersRequest.Content.ReadFromJsonAsync<PaginatedList<PlayerPageDto>>()
        });
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

    private HttpClient GetClient()
    {
        var client = _factory.CreateClient("default");
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
            User.FindFirst("access_token")?.Value.Replace("Bearer ", ""));
        return client;
    }
}