using Data.Helpers;

namespace Data.Teams;

public class TeamRepository : Repository<Team, int>, ITeamRepository
{
    public TeamRepository(AppDbContext context, ILogger<TeamRepository> logger) 
        : base(context, logger)
    {
    }

    public async Task<List<Lookup.Lookup>> GetLookup()
    {
        return await GetBaseQuery()
            .Select(x => new Lookup.Lookup(x.Id, x.Name))
            .ToListAsync();
    }

    public async Task<Coach?> GetCoachAsync(int id)
    {
        return await Context.Coaches
            .Include(x => x.Team)
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.TeamId == id);
    }

    public PaginatedList<Player> GetPlayers(PaginatedCommand command, int id)
    {
        return PaginatedList<Player>.Create(Context.Players
                .Include(x => x.Team)
                .Include(x => x.User)
                .Where(x => x.TeamId == id), 
            command.PageNumber, command.PageSize);
    }
}

public interface ITeamRepository : IRepository<Team, int>
{
    Task<List<Lookup.Lookup>> GetLookup();
    Task<Coach?> GetCoachAsync(int id);
    PaginatedList<Player> GetPlayers(PaginatedCommand command, int id);
}