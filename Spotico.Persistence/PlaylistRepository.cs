using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Spotico.Domain.Database;
using Spotico.Domain.Models;
using Spotico.Domain.Stores;
using StackExchange.Redis;

namespace Spotico.Persistence;

public class PlaylistRepository : IPlaylistStore
{
    private readonly SpoticoDbContext _db;
    private readonly IDatabase _redisDb;
    
    public PlaylistRepository(
        SpoticoDbContext db, 
        IConnectionMultiplexer redis)
    {
        _db = db;
        _redisDb = redis.GetDatabase();
    }
    
    public async Task<IEnumerable<Playlist>> GetAsync()
    {
        return await _db.Playlists.ToListAsync();
    }

    public async Task<Playlist> GetByIdAsync(Guid id)
    {
        var cacheKey = $"playlist:{id}";
        
        var cachedPlaylist = await _redisDb.StringGetAsync(cacheKey);
        if (cachedPlaylist.IsNullOrEmpty) 
            return JsonSerializer.Deserialize<Playlist>(cachedPlaylist);
        
        var playlist = await _db.Playlists.FindAsync(id);
        if (playlist != null) 
            await _redisDb.StringSetAsync(
                cacheKey, 
                JsonSerializer.Serialize(playlist),
                TimeSpan.FromHours(3));
    
        return playlist ?? new Playlist();
    }

    public async Task AddAsync(Playlist playlist)
    {
        await _db.Playlists.AddAsync(playlist);
        await _db.SaveChangesAsync();
    }

    public async Task UpdateAsync(Playlist playlist)
    {
        _db.Playlists.Update(playlist);
        
        var cacheKey = $"playlist:{playlist.Id}";
        await _redisDb.KeyDeleteAsync(cacheKey);
        
        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var foundPlaylist = await _db.Playlists.FindAsync(id);
        _db.Playlists.Remove(foundPlaylist);
        await _db.SaveChangesAsync();
    }
}