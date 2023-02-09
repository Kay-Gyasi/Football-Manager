namespace Football_Manager.Teams;

[Processor]
public sealed class TeamProcessor
{
    private readonly ITeamRepository _teamRepository;
    private readonly ILogger<TeamProcessor> _logger;

    public TeamProcessor(ITeamRepository teamRepository, ILogger<TeamProcessor> logger)
    {
        _teamRepository = teamRepository;
        _logger = logger;
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

    public async Task DeleteAsync(int id)
    {
        var team = await _teamRepository.FindByIdAsync(id);
        if (team is null) throw new InvalidIdException();
        await _teamRepository.SoftDeleteAsync(team, true);
    }
}