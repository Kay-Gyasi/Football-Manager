using System.Security.Claims;

namespace Football_Manager.Extensions;

public static class AuthorizationExtensions
{
    public static IServiceCollection AddAuthorizationPolicies(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy("AdminOnly", opts =>
            {
                opts.RequireAuthenticatedUser();
                opts.RequireAssertion(context => context.User.FindAll(ClaimTypes.Role)
                    .Any(x => x.Value == "admin"));
            });
        });
        
        services.AddAuthorization(options =>
        {
            options.AddPolicy("Player", opts =>
            {
                opts.RequireAuthenticatedUser();
                opts.RequireAssertion(context => context.User.FindAll(ClaimTypes.Role)
                    .Any(x => x.Value is "admin" or "Player"));
            });
        });
        
        services.AddAuthorization(options =>
        {
            options.AddPolicy("Coach", opts =>
            {
                opts.RequireAuthenticatedUser();
                opts.RequireAssertion(context => context.User.FindAll(ClaimTypes.Role)
                    .Any(x => x.Value is "admin" or "Coach"));
            });
        });

        return services;
    }
}