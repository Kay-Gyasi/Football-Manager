using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Football_Manager.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Football_Manager.Authentication;

public static class AuthenticationExtensions
{
    public static IServiceCollection AddAuthenticationService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(opts =>
            {
                opts.MapInboundClaims = true;
                opts.SaveToken = false;
                opts.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Authentication:Schemes:Bearer:ValidIssuer"],
                    ValidAudience = configuration["Authentication:Schemes:Bearer:ValidAudiences:0"],
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                        .GetBytes(configuration["Authentication:Schemes:Bearer:SigningKeys:0:Value"] ?? "")),
                    RoleClaimType = ClaimTypes.Role,
                    NameClaimType = JwtRegisteredClaimNames.Sub
                };
            });
        services.AddScoped<ITokenService, TokenService>();
        return services;
    }
}