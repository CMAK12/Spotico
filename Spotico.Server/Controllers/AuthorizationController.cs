using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Spotico.Core.Models;
using Spotico.Server.Configurations;
using Spotico.Server.Data;
using Spotico.Server.Services;

namespace Spotico.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthorizationController : ControllerBase
{
    private readonly SpoticoDbContext _db;
    private readonly TokenService _tokenService;
    
    public AuthorizationController(SpoticoDbContext db, TokenService tokenService)
    {
        _db = db;
         _tokenService = tokenService;
    }
    
    [HttpPost]
    public async Task<IActionResult> Login(LoginForm request)
    {
        var user = ValidateUserCredentials(request);
        
        if (user == null) return Unauthorized();
        
        var token = _tokenService.GenerateToken(user);
        
        return Ok(new { AccessToken = token });
    }
     
    private User ValidateUserCredentials(LoginForm form)
    {
        var user = _db.Users
            .SingleOrDefault(u => u.Username == form.Username && u.Password == form.Password);
        return user;
    }
}