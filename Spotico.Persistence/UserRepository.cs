using Microsoft.EntityFrameworkCore;
using Spotico.Domain.Database;
using Spotico.Domain.Stores;
using Spotico.Domain.Models;

namespace Spotico.Persistence;
    
public class UserRepository : IUserStore
{
    private readonly SpoticoDbContext _db;
    
    public UserRepository(SpoticoDbContext db)
    {
        _db = db;
    }
    
    public async Task<User> GetByIdAsync(Guid id)
    {
        var identifiedUser = await _db.Users.FindAsync(id);
        
        return identifiedUser ?? new User();
    }
    
    public async Task AddAsync(User user)
    {
        await _db.Users.AddAsync(user);
        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var user = await GetByIdAsync(id);
        _db.Users.Remove(user);
        await _db.SaveChangesAsync();
    }
}