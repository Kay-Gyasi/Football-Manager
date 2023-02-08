namespace Football_Manager;

public static class DependencyInjection
{
    public static IServiceCollection RegisterServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddSwaggerGen();
        services.AddPersistence(configuration);
        return services;
    }
}