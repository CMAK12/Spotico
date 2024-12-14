using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Spotico.Core.Common.Enums;
using Spotico.Core.Models;
using Spotico.Core.Database;
using Spotico.Infrastructure.Interfaces;

namespace Spotico.Infrastructure;

public class JwtProvider : IJwtProvider
{
    private readonly SpoticoDbContext _db;
    private readonly IOptions<AuthOptions> _options;
    
    public JwtProvider(SpoticoDbContext db, IOptions<AuthOptions> options)
    {   
        _db = db;
        _options = options;
    }
    
    public string GenerateToken(User user)
    {
        var authParams = _options.Value;

        var securityKey = authParams.GetSymmetricSecurityKey();
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>()
        {
            new Claim(JwtRegisteredClaimNames.Name, user.Username),
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(ClaimTypes.Role, nameof(UserRole.User))
        };

        var token = new JwtSecurityToken(
            authParams.Issuer,
            authParams.Audience,
            claims,
            expires: DateTime.Now.AddSeconds(authParams.TokenLifetime),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}