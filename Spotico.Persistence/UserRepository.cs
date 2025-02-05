using System.Text.Json;
using Spotico.Domain.Database;
using Spotico.Domain.Stores;
using Spotico.Domain.Models;
using StackExchange.Redis;

namespace Spotico.Persistence;
    
public class UserRepository : IUserStore
{
    private readonly SpoticoDbContext _db;
    private readonly IDatabase _redisDb;
    
    public UserRepository(
        SpoticoDbContext db, 
        IConnectionMultiplexer redis)
    {
        _db = db;
        _redisDb = redis.GetDatabase();
    }
    
    public async Task<User> GetByIdAsync(Guid id)
    {
        var cacheKey = $"user:{id}";
        
        var cachedUser = await _redisDb.StringGetAsync(cacheKey);
        if (!cachedUser.IsNullOrEmpty)
            return JsonSerializer.Deserialize<User>(cachedUser);
        
        var user = await _db.Users.FindAsync(id);
        
        if (user != null) 
            await _redisDb.StringSetAsync(
                cacheKey, 
                JsonSerializer.Serialize(user),
                TimeSpan.FromHours(3));
        
        return user;
    }
    
    public async Task AddAsync(User user)
    {
        await _db.Users.AddAsync(user);
        await _db.SaveChangesAsync();
    }
    
    public async Task UpdateAsync(User user)
    {
        _db.Users.Update(user);
        
        var cacheKey = $"user:{user.Id}";
        await _redisDb.KeyDeleteAsync(cacheKey);
        
        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var user = await GetByIdAsync(id);
        _db.Users.Remove(user);
        await _db.SaveChangesAsync();
    }
}