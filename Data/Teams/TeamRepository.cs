namespace Data.Teams;

public class TeamRepository : Repository<Team, int>, ITeamRepository
{
    public TeamRepository(AppDbContext context, ILogger<TeamRepository> logger) 
        : base(context, logger)
    {
    }
}

public interface ITeamRepository : IRepository<Team, int> {}