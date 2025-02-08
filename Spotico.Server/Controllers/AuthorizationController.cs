using Microsoft.AspNetCore.Mvc;
using Spotico.Domain.Database;
using Spotico.Domain.Models;
using Spotico.Infrastructure.Interfaces;
using Spotico.Server.DTOs;

namespace Spotico.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthorizationController : ControllerBase
{
    private readonly SpoticoDbContext _db;
    private readonly ITokenService _tokenService;
    private readonly IPasswordEncryptor _encryptor;
    
    public AuthorizationController(
        SpoticoDbContext db, 
        ITokenService tokenService,
        IPasswordEncryptor encryptor)
    {
        _db = db;
        _tokenService = tokenService;
        _encryptor = encryptor;
    }
    
    [HttpPost]
    public async Task<IActionResult> Login(LoginDTO request)
    {
        var user = ValidateUserCredentials(request);
        var isMatched = _encryptor.Verify(request.Password, user.Password);
        
        if (user == null && isMatched) return Unauthorized();
        
        var token = _tokenService.GenerateToken(user);
        
        return Ok(new { access_token = token });
    }
     
    [NonAction]
    private User ValidateUserCredentials(LoginDTO form)
    {
        var user = _db.Users
            .SingleOrDefault(u => u.Email == form.Email);
        return user;
    }
}