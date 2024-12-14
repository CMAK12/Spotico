using Microsoft.AspNetCore.Mvc;
using Spotico.Core.Database;
using Spotico.Core.Models;
using Spotico.Core.Stores;
using Spotico.Persistence;

namespace Spotico.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomerController : ControllerBase
{
    private readonly IUserStore _userRepository;
    
    public CustomerController(SpoticoDbContext db, UserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var customer = await _userRepository.GetById(id);
        
        return customer != null ? Ok(customer) : NotFound();
    }
    
    [HttpPost]
    public async Task Post(User user)
    {
        await _userRepository.Add(user);
    }
    
    [HttpDelete("{id}")]
    public async Task Delete(Guid id)
    {
        await _userRepository.Delete(id);
    }
}