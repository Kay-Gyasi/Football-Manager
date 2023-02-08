namespace Data;

public class AppDbContext : IdentityDbContext<User, IdentityRole<int>, int>
{
    public AppDbContext() { }
    public AppDbContext(DbContextOptions options) : base(options) { }

    public DbSet<Team> Teams { get; set; }
    public DbSet<Coach> Coaches { get; set; }
    public DbSet<Player> Players { get; set; }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entry in ChangeTracker.Entries()
                     .Where(x => x.State is EntityState.Added or EntityState.Modified))
        {
            if (entry.Entity is Entity<int> entity)
            {
                entity.Audit.WasUpdatedBy("admin");
            }
        }
        
        foreach (var entry in ChangeTracker.Entries()
                     .Where(x => x.State is EntityState.Deleted))
        {
            if (entry.Entity is not Entity<int> entity) continue;
            entity.Audit.WasUpdatedBy("admin");
            entity.Audit.Delete();
        }

        return base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(ConnectionStrings.Development);
        }
    }
}