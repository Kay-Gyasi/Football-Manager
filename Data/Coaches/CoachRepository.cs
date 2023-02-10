namespace Data.Coaches;

public class CoachRepository : Repository<Coach, int>, ICoachRepository
{
    public CoachRepository(AppDbContext context, ILogger<CoachRepository> logger) 
        : base(context, logger)
    {
    }

    protected override IQueryable<Coach> GetBaseQuery()
    {
        return base.GetBaseQuery()
            .Include(x => x.Team)
            .Include(x => x.User);
    }
}

public interface ICoachRepository : IRepository<Coach, int> {}