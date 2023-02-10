namespace Data.Players;

public class PlayerRepository : Repository<Player, int>, IPlayerRepository
{
    public PlayerRepository(AppDbContext context, ILogger<PlayerRepository> logger) 
        : base(context, logger)
    {
    }

    protected override IQueryable<Player> GetBaseQuery()
    {
        return base.GetBaseQuery()
            .Include(x => x.Team)
            .Include(x => x.User);
    }
}

public interface IPlayerRepository : IRepository<Player, int> {}