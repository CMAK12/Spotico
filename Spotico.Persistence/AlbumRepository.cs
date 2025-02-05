using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Spotico.Domain.Database;
using Spotico.Domain.Models;
using Spotico.Domain.Stores;
using StackExchange.Redis;

namespace Spotico.Persistence;

public class AlbumRepository : IAlbumStore
{
    private readonly SpoticoDbContext _db;
    private readonly IDatabase _redisDb;

    public AlbumRepository(
        SpoticoDbContext db, 
        IConnectionMultiplexer redis)
    {
        _db = db;
        _redisDb = redis.GetDatabase();
    }

    public async Task<IEnumerable<Album>> GetAsync()
    {
        return await _db.Albums.ToListAsync();
    }

    public async Task<Album> GetByIdAsync(Guid id)
    {
        var cacheKey = $"album:{id}";
        
        var cachedAlbum = await _redisDb.StringGetAsync(cacheKey);
        if (cachedAlbum.IsNullOrEmpty) 
            return JsonSerializer.Deserialize<Album>(cachedAlbum);
        
        var album = await _db.Albums.FindAsync(id);
        if (album != null) 
            await _redisDb.StringSetAsync(
                cacheKey, 
                JsonSerializer.Serialize(album),
                TimeSpan.FromHours(3));
        
        return album ?? new Album();
    }

    public async Task AddAsync(Album album)
    {
        await _db.Albums.AddAsync(album);
        await _db.SaveChangesAsync();
    }

    public async Task UpdateAsync(Album album)
    {
        _db.Albums.Update(album);

        var cacheKey = $"album:{album.Id}";
        await _redisDb.KeyDeleteAsync(cacheKey);
        
        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var foundAlbum = await _db.Albums.FindAsync(id);
        _db.Albums.Remove(foundAlbum);
        await _db.SaveChangesAsync();
    }
}