using Microsoft.AspNetCore.Mvc;
using Spotico.Core.Database;
using Spotico.Core.Models;
using Spotico.Infrastructure;
using Spotico.Infrastructure.Interfaces;
using Spotico.Server.DTOs;

namespace Spotico.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthorizationController : ControllerBase
{
    private readonly SpoticoDbContext _db;
    private readonly IJwtProvider _jwtProvider;
    
    public AuthorizationController(SpoticoDbContext db, IJwtProvider jwtProvider)
    {
        _db = db;
         _jwtProvider = jwtProvider;
    }
    
    [HttpPost]
    public async Task<IActionResult> Login(LoginDTO request)
    {
        var user = ValidateUserCredentials(request);
        
        if (user == null) return Unauthorized();
        
        var token = _jwtProvider.GenerateToken(user);
        
        return Ok(new { AccessToken = token });
    }
     
    private User ValidateUserCredentials(LoginDTO form)
    {
        var user = _db.Users
            .SingleOrDefault(u => u.Username == form.Username && u.Password == form.Password);
        return user;
    }
}