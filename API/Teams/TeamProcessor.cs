using Data.Lookup;
using Football_Manager.Coaches;
using Football_Manager.Players;

namespace Football_Manager.Teams;

[Processor]
public sealed class TeamProcessor
{
    private readonly ITeamRepository _teamRepository;
    private readonly ILogger<TeamProcessor> _logger;
    private readonly UserManager<User> _userManager;
    private readonly ICoachRepository _coachRepository;
    private readonly IPlayerRepository _playerRepository;

    public TeamProcessor(ITeamRepository teamRepository, 
        ILogger<TeamProcessor> logger, UserManager<User> userManager,
        ICoachRepository coachRepository, IPlayerRepository playerRepository)
    {
        _teamRepository = teamRepository;
        _logger = logger;
        _userManager = userManager;
        _coachRepository = coachRepository;
        _playerRepository = playerRepository;
    }

    public async Task<OneOf<int, InvalidIdException>> UpsertAsync(TeamCommand command)
    {
        var isNew = command.Id is null or 0;
        Team? team;

        if (isNew)
        {
            team = Team.Create(command.Name)
                .HasStadium(command.StadiumName)
                .WasEstablishedOn(command.EstablishmentDate);
            await _teamRepository.AddAsync(team, true);
            return team.Id;
        }

        team = await _teamRepository.FindByIdAsync(command.Id ?? 0);
        if (team is null) return new InvalidIdException();
        team.HasName(command.Name)
            .HasStadium(command.StadiumName)
            .WasEstablishedOn(command.EstablishmentDate);
        await _teamRepository.UpdateAsync(team, true);
        return team.Id;
    }

    public async Task<TeamDto?> GetAsync(int id)
    {
        return (TeamDto) await _teamRepository.FindByIdAsync(id);
    }
    
    public async Task<CoachDto?> GetCoachAsync(string userId)
    {
        var userType = (await _userManager.FindByIdAsync(userId))?.Type;
        var id = userType == UserType.Coach ? 
            (await _coachRepository.FindByIdAsync(Convert.ToInt32(userId)))?.TeamId 
            : (await _playerRepository.FindByIdAsync(Convert.ToInt32(userId)))?.TeamId;
        return (CoachDto) await _teamRepository.GetCoachAsync(id ?? 0);
    }
    
    public async Task<PaginatedList<TeamPageDto>> GetPageAsync(PaginatedCommand query)
    {
        var paginatedList = await _teamRepository.GetPageAsync(query);
        return TeamPageDto.ToPageDto(paginatedList);
    }
    
    public async Task<PaginatedList<PlayerPageDto>> GetPlayersPageAsync(PaginatedCommand query, string userId)
    {
        var userType = (await _userManager.FindByIdAsync(userId))?.Type;
        var id = userType == UserType.Coach ? 
            (await _coachRepository.FindByIdAsync(Convert.ToInt32(userId)))?.TeamId 
            : (await _playerRepository.FindByIdAsync(Convert.ToInt32(userId)))?.TeamId;

        var paginatedList = await Task.Run(() => _teamRepository.GetPlayers(query, id ?? 0));
        return PlayerPageDto.ToPageDto(paginatedList);
    }

    public async Task<List<Lookup>> GetLookup()
    {
        return await _teamRepository.GetLookup();
    }

    public async Task DeleteAsync(int id)
    {
        var team = await _teamRepository.FindByIdAsync(id);
        if (team is null) throw new InvalidIdException();
        await _teamRepository.SoftDeleteAsync(team, true);
    }
}