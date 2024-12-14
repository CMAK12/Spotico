using Mapster;
using Microsoft.AspNetCore.Mvc;
using Spotico.Core.Database;
using Spotico.Core.Models;
using Spotico.Core.Stores;
using Spotico.Persistence;
using Spotico.Server.DTOs;

namespace Spotico.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomerController : ControllerBase
{
    private readonly IUserStore _userRepository;
    
    public CustomerController(SpoticoDbContext db, IUserStore userRepository)
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
    public async Task Post(UserDTO user)
    {
        var customer = user.Adapt<User>(); // Adapt is a Mapster method that maps properties from one object to another
        await _userRepository.Add(customer);
    }
    
    [HttpDelete("{id}")]
    public async Task Delete(Guid id)
    {
        await _userRepository.Delete(id);
    }
}