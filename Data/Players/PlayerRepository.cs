namespace Data.Players;

public class PlayerRepository : Repository<Player, int>, IPlayerRepository
{
    public PlayerRepository(AppDbContext context, ILogger<PlayerRepository> logger) 
        : base(context, logger)
    {
    }

    public async Task<Player?> GetByTypeId(int id)
    {
        return await GetBaseQuery().FirstOrDefaultAsync(x => x.UserId == id);
    }

    protected override IQueryable<Player> GetBaseQuery()
    {
        return base.GetBaseQuery()
            .Include(x => x.Team)
            .Include(x => x.User);
    }
}

public interface IPlayerRepository : IRepository<Player, int>
{
    Task<Player?> GetByTypeId(int id);
}