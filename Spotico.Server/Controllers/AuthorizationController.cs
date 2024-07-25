using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Spotico.Core.Models;
using Spotico.Server.Configurations;
using Spotico.Server.Data;

namespace Spotico.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthorizationController : ControllerBase
{
    private readonly SpoticoDbContext _db;
    private readonly IOptions<AuthOptions> _options;
    
    public AuthorizationController(SpoticoDbContext db, IOptions<AuthOptions> options)
    {   
        _db = db;
        _options = options;
    }
    
    [HttpPost]
    public async Task<IActionResult> Login(LoginForm request)
    {
        var user = Authenticate(request.Username, request.Password);
        
        if (user == null) return Unauthorized();
        
        var token = GenerateToken(user);
        
        return Ok(new { AccessToken = token });
    }
     
    private User Authenticate(string username, string password)
    {
        var user = _db.Users.FirstOrDefault(u => u.Username == username && u.Password == password);
        return user;
    }

    private string GenerateToken(User user)
    {
        var authParams = _options.Value;

        var securityKey = authParams.GetSymmetricSecurityKey();
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>()
        {
            new Claim(JwtRegisteredClaimNames.Name, user.Username),
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(ClaimTypes.Role, UserRole.User)
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