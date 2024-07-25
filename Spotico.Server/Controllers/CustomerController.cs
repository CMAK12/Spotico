using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Spotico.Core.Models;
using Spotico.Server.Repositories;
using Spotico.Server.Data;

namespace Spotico.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomerController : ControllerBase
{
    private readonly SpoticoDbContext _db;
    private readonly UserRepository _userRepository;
    
    public CustomerController(SpoticoDbContext db, UserRepository userRepository)
    {
        _db = db;
        _userRepository = userRepository;
    }
    
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var customers = await _db.Users.ToListAsync();
        
        return Ok(customers);
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