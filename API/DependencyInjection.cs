using Football_Manager.Authentication;

namespace Football_Manager;

public static class DependencyInjection
{
    public static IServiceCollection RegisterServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddControllers()
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });
        services.AddEndpointsApiExplorer();
        services.AddPersistence(configuration)
            .RegisterProcessors()
            .AddAuthenticationService(configuration)
            .AddAuthorizationPolicies()
            .AddSwaggerConfig();
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