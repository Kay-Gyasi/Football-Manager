using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Football_Manager.Users;

public class TokenService : ITokenService
{
    private readonly string _issuer;
    private readonly SigningCredentials _jwtSigningCredentials;
    private readonly Claim[] _audiences;

    public TokenService(IAuthenticationConfigurationProvider authenticationConfigurationProvider)
    {
        var bearerSection = authenticationConfigurationProvider
            .GetSchemeConfiguration(JwtBearerDefaults.AuthenticationScheme);
        var section = bearerSection.GetSection("SigningKeys:0");
        
        _issuer = bearerSection["ValidIssuer"] ?? throw new InvalidOperationException("Issuer is not specified");
        var signingKey = section["Value"] ?? throw new InvalidOperationException("Signing key is not specified");

        var signingKeyBytes = Encoding.UTF8.GetBytes(signingKey);

        _jwtSigningCredentials = new SigningCredentials(new SymmetricSecurityKey(signingKeyBytes),
            SecurityAlgorithms.HmacSha256Signature);

        _audiences = bearerSection.GetSection("ValidAudiences").GetChildren()
            .Where(s => !string.IsNullOrEmpty(s.Value))
            .Select(s => new Claim(JwtRegisteredClaimNames.Aud, s.Value!))
            .ToArray();
    }


    public AuthToken GenerateToken(User user)
    {
        var identity = new ClaimsIdentity(JwtBearerDefaults.AuthenticationScheme);
        
        identity.AddClaims(new []
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName ?? ""),
            new Claim(ClaimTypes.Role, user.Type.ToString()),
            new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName),
            new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName),
            new Claim(JwtRegisteredClaimNames.Email, user.Email ?? ""),
            new Claim(ClaimTypes.MobilePhone, user.PhoneNumber ?? ""),
        });
        
        var id = Guid.NewGuid().ToString().GetHashCode().ToString("x", CultureInfo.InvariantCulture);
        identity.AddClaim(new Claim(JwtRegisteredClaimNames.Jti, id));

        identity.AddClaims(_audiences);
        
        var handler = new JwtSecurityTokenHandler();

        var jwtToken = handler.CreateJwtSecurityToken(
            _issuer,
            audience: null,
            identity,
            notBefore: DateTime.UtcNow,
            expires: DateTime.UtcNow.AddMinutes(30),
            issuedAt: DateTime.UtcNow,
            _jwtSigningCredentials);

        return new AuthToken(handler.WriteToken(jwtToken));
    }
}

public interface ITokenService
{
    AuthToken GenerateToken(User user);
}

public record AuthToken(string Token);