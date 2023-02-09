namespace Data.Coaches;

public class CoachRepository : Repository<Coach, int>, ICoachRepository
{
    public CoachRepository(AppDbContext context, ILogger<CoachRepository> logger) 
        : base(context, logger)
    {
    }
}

public interface ICoachRepository : IRepository<Coach, int> {}