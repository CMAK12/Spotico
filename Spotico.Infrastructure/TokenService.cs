using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Spotico.Domain.Common.Enums;
using Spotico.Domain.Models;
using Spotico.Infrastructure.Configuration;
using Spotico.Infrastructure.Interfaces;

namespace Spotico.Infrastructure;

public class TokenService : ITokenService
{
    private readonly IOptions<AuthOptions> _options;
    
    public TokenService(IOptions<AuthOptions> options)
    {   
        _options = options;
    }
    
    public string GenerateToken(User user)
    {
        var authParams = _options.Value; // Get the auth options

        // Get the symmetric security key from the auth options
        var securityKey = authParams.GetSymmetricSecurityKey();
        // Create the signing credentials
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        // Create the claims for the token
        var claims = new List<Claim>()
        {
            new Claim(JwtRegisteredClaimNames.Name, user.Username),
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(ClaimTypes.Role, nameof(UserRole.User))
        };

        // Create the JWT token with data
        var token = new JwtSecurityToken(
            authParams.Issuer,
            authParams.Audience,
            claims,
            expires: DateTime.Now.AddSeconds(authParams.TokenLifetime),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}