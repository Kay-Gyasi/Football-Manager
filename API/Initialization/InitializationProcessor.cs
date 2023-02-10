using Bogus;
using Football_Manager.Coaches;
using Football_Manager.Players;
using Football_Manager.Users;

namespace Football_Manager.Initialization;

[Processor]
public sealed class InitializationProcessor
{
    private readonly PlayerProcessor _playerProcessor;
    private readonly TeamProcessor _teamProcessor;
    private readonly CoachProcessor _coachProcessor;
    private readonly RoleManager<IdentityRole<int>> _roleManager;
    private List<TeamCommand>? _teams;
    private const int TeamSize = 5;

    public InitializationProcessor(PlayerProcessor playerProcessor,
        TeamProcessor teamProcessor, CoachProcessor coachProcessor,
        RoleManager<IdentityRole<int>> roleManager)
    {
        _playerProcessor = playerProcessor;
        _teamProcessor = teamProcessor;
        _coachProcessor = coachProcessor;
        _roleManager = roleManager;
        GetTeams();
    }

    public async Task Startup()
    {
        await SeedRoles();
        await SeedTeams();
        await SeedCoaches();
        await SeedPlayers();
    }
    
    private async Task SeedRoles()
    {
        if (_roleManager.Roles.Any()) return;
        await _roleManager.CreateAsync(new IdentityRole<int>
        {
            Name = UserType.Coach.ToString()
        });
        await _roleManager.CreateAsync(new IdentityRole<int>
        {
            Name = UserType.Player.ToString()
        });
        await _roleManager.CreateAsync(new IdentityRole<int>
        {
            Name = "admin"
        });
    }

    private async Task SeedTeams()
    {
        foreach (var team in _teams)
        {
            await _teamProcessor.UpsertAsync(team).ConfigureAwait(false);
        }
    }

    private async Task SeedPlayers()
    {
        foreach (var id in Enumerable.Range(1, TeamSize))
        {
            var players = GetPlayers(id);
            foreach (var player in players)
            {
                player.JerseyName = player.User?.LastName;
                await _playerProcessor.UpsertAsync(player).ConfigureAwait(false);
            }
        }
    }
    
    private async Task SeedCoaches()
    {
        foreach (var id in Enumerable.Range(1, TeamSize))
        {
            var coaches = GetCoach(id);
            foreach (var coach in coaches)
            {
                await _coachProcessor.UpsertAsync(coach).ConfigureAwait(false);
            }
        }
    }

    private void GetTeams()
    {
        _teams = new Faker<TeamCommand>()
            .Ignore(x => x.Id)
            .RuleFor(x => x.EstablishmentDate, f => f.Date.Past())
            .RuleFor(x => x.Name, (f, u) => 
                string.Join(" ", f.Random.Words(1), "FC"))
            .RuleFor(x => x.StadiumName, (f, u) => 
                string.Join(" ", f.Random.Words(1), "Stadium"))
            .Generate(TeamSize);
    }

    private static IEnumerable<PlayerCommand> GetPlayers(int teamId)
    {
        var userFaker = new Faker<UserCommand>()
            .RuleFor(x => x.FirstName, f => f.Name.FirstName())
            .RuleFor(x => x.LastName, f => f.Name.LastName())
            .RuleFor(x => x.Email, f => f.Internet.Email())
            .RuleFor(x => x.DateOfBirth, f => f.Date.Past(17))
            .RuleFor(x => x.PhoneNumber, f => f.Phone.PhoneNumber())
            .RuleFor(x => x.Password, _ => "Kaygyasi534$trey");
        return new Faker<PlayerCommand>()
            .Ignore(x => x.Id)
            .Ignore(x => x.JerseyName)
            .RuleFor(x => x.TeamId, _ => teamId)
            .RuleFor(x => x.User, () => userFaker)
            .RuleFor(x => x.JerseyNumber, f => f.Random.Number(1, 26))
            .Generate(26);
    }

    private static IEnumerable<CoachCommand> GetCoach(int teamId)
    {
        var userFaker = new Faker<UserCommand>()
            .RuleFor(x => x.FirstName, f => f.Name.FirstName())
            .RuleFor(x => x.LastName, f => f.Name.LastName())
            .RuleFor(x => x.Email, f => f.Internet.Email())
            .RuleFor(x => x.DateOfBirth, f => f.Date.Past(17))
            .RuleFor(x => x.PhoneNumber, f => f.Phone.PhoneNumber())
            .RuleFor(x => x.Password, _ => "Kaygyasi534$trey");
        return new Faker<CoachCommand>()
            .Ignore(x => x.Id)
            .RuleFor(x => x.TeamId, _ => teamId)
            .RuleFor(x => x.User, () => userFaker)
            .RuleFor(x => x.YearsOfExperience, f => f.Random.Number(30))
            .Generate(1);
    }
}