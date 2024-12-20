using Microsoft.AspNetCore.Mvc;
using Spotico.Core.Database;
using Spotico.Core.Models;
using Spotico.Infrastructure.Interfaces;
using Spotico.Server.DTOs;

namespace Spotico.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthorizationController : ControllerBase
{
    private readonly SpoticoDbContext _db;
    private readonly ITokenService _tokenService;
    
    public AuthorizationController(SpoticoDbContext db, ITokenService tokenService)
    {
        _db = db;
         _tokenService = tokenService;
    }
    
    [HttpPost]
    public async Task<IActionResult> Login(LoginDTO request)
    {
        var user = ValidateUserCredentials(request);
        
        if (user == null) return Unauthorized();
        
        var token = _tokenService.GenerateToken(user);
        
        return Ok(new { access_token = token });
    }
     
    private User ValidateUserCredentials(LoginDTO form)
    {
        var user = _db.Users
            .SingleOrDefault(u => u.Email == form.Email && u.Password == form.Password);
        return user;
    }
}