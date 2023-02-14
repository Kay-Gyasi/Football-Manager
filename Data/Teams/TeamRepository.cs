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
}

public interface ITeamRepository : IRepository<Team, int>
{
    Task<List<Lookup.Lookup>> GetLookup();
}