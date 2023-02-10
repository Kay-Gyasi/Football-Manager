using Football_Manager.Authentication;
using Football_Manager.Users;

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
        services.AddPersistence(configuration)
            .RegisterProcessors()
            .AddAuthenticationService(configuration);
        return services;
    }

    private static IServiceCollection RegisterProcessors(this IServiceCollection services)
    {
        var assemblyTypes = typeof(DependencyInjection).Assembly
            .DefinedTypes;
        var processors = assemblyTypes.Where(type => type.IsClass
                                                     && type.CustomAttributes.Any(x
                                                         => x.AttributeType == typeof(ProcessorAttribute)));
        
        foreach (var typeInfo in processors)
        {
            services.AddScoped(typeInfo);
        }

        return services;
    }
}