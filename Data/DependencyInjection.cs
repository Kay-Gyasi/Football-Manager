namespace Data;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("Default"));
            options.EnableSensitiveDataLogging();
        })
            .InstallRepositories();
        services.AddIdentityCore<User>()
            .AddRoles<IdentityRole<int>>()
            .AddEntityFrameworkStores<AppDbContext>();
        return services;
    }

    private static IServiceCollection InstallRepositories(this IServiceCollection services)
    {
        services.AddScoped<ICoachRepository, CoachRepository>()
            .AddScoped<IPlayerRepository, PlayerRepository>()
            .AddScoped<ITeamRepository, TeamRepository>();
        return services;
    } 
}