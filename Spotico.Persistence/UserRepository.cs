using Microsoft.EntityFrameworkCore;
using Spotico.Core.Database;
using Spotico.Core.Stores;
using Spotico.Core.Models;

namespace Spotico.Persistence;
    
public class UserRepository : IUserStore
{
    private readonly SpoticoDbContext _db;
    
    public UserRepository(SpoticoDbContext db)
    {
        _db = db;
    }
    
    /// <summary>Asynchronously retrieves a user by their ID</summary>
    public async Task<User> GetByIdAsync(Guid id)
    {
        var identifiedUser = await _db.Users.SingleOrDefaultAsync(u => u.Id == id);
        
        return identifiedUser ?? new User();
    }
    
    /// <summary>Adds a user into the database</summary>
    public async Task AddAsync(User user)
    {
        await _db.Users.AddAsync(user);
        await _db.SaveChangesAsync();
    }

    /// <summary>Removes a user from the database</summary>
    public async Task DeleteAsync(Guid id)
    {
        var user = await GetByIdAsync(id);
        _db.Users.Remove(user);
        await _db.SaveChangesAsync();
    }
}