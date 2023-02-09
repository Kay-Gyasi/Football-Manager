namespace Data.Players;

public class PlayerRepository : Repository<Player, int>, IPlayerRepository
{
    public PlayerRepository(AppDbContext context, ILogger<PlayerRepository> logger) 
        : base(context, logger)
    {
        _dbSet.Include(x => x.Team);
    }
}

public interface IPlayerRepository : IRepository<Player, int> {}