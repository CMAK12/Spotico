using Mapster;
using Microsoft.AspNetCore.Mvc;
using Spotico.Domain.Models;
using Spotico.Domain.Stores;
using Spotico.Server.DTOs;

namespace Spotico.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomerController : ControllerBase
{
    private readonly IUserStore _userRepository;
    
    public CustomerController(IUserStore userRepository)
    {
        _userRepository = userRepository;
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var customer = await _userRepository.GetByIdAsync(id);
        
        return customer != null ? Ok(customer) : NotFound();
    }
    
    [HttpPost]
    public async Task Post(UserDTO user)
    {
        var customer = user.Adapt<User>(); // Adapt is a Mapster method that maps properties from one object to another
        await _userRepository.AddAsync(customer);
    }
    
    [HttpDelete("{id}")]
    public async Task Delete(Guid id)
    {
        await _userRepository.DeleteAsync(id);
    }
}