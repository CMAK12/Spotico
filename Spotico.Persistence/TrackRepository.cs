using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Spotico.Domain.Database;
using Spotico.Domain.Models;
using Spotico.Domain.Stores;
using StackExchange.Redis;

namespace Spotico.Persistence;

public class TrackRepository : ITrackStore
{
    private readonly SpoticoDbContext _db;
    private readonly IDatabase _redisDb;
    
    public TrackRepository(
        SpoticoDbContext db,
        IConnectionMultiplexer redis)
    {
        _db = db;
        _redisDb = redis.GetDatabase();
    }
    
    public async Task<IEnumerable<Track>> GetAsync()
    {
        return await _db.Tracks.ToListAsync();
    }

    public async Task<Track> GetByIdAsync(Guid id)
    {
        var cacheKey = $"track:{id}";
        
        var cachedTrack = await _redisDb.StringGetAsync(cacheKey);
        if (cachedTrack.IsNullOrEmpty) 
            return JsonSerializer.Deserialize<Track>(cachedTrack);
        
        var track = await _db.Tracks.FindAsync(id);
        if (track != null) 
            await _redisDb.StringSetAsync(
                cacheKey, 
                JsonSerializer.Serialize(track),
                TimeSpan.FromHours(3));
        
        return track ?? new Track();
    }

    public async Task AddAsync(Track track)
    {
        await _db.Tracks.AddAsync(track);
        await _db.SaveChangesAsync();
    }

    public async Task UpdateAsync(Track track)
    {
        _db.Tracks.Update(track);
        
        var cacheKey = $"track:{track.Id}";
        await _redisDb.KeyDeleteAsync(cacheKey);
        
        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var foundTrack = await _db.Tracks.FindAsync(id);
        _db.Tracks.Remove(foundTrack);
        await _db.SaveChangesAsync();
    }
}