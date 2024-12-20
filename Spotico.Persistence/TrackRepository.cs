using Microsoft.EntityFrameworkCore;
using Spotico.Core.Database;
using Spotico.Core.Models;
using Spotico.Core.Stores;

namespace Spotico.Persistence;

public class TrackRepository : ITrackStore
{
    private readonly SpoticoDbContext _db;
    
    public TrackRepository(SpoticoDbContext db)
    {
        _db = db;
    }
    
    public Task<List<Track>> GetAsync()
    {
        return _db.Tracks.ToListAsync();
    }

    public async Task<Track> GetByIdAsync(Guid id)
    {
        var identifiedTrack = await _db.Tracks.FindAsync(id);

        return identifiedTrack ?? new Track();
    }

    public async Task AddAsync(Track track)
    {
        await _db.Tracks.AddAsync(track);
        await _db.SaveChangesAsync();
    }

    public async Task UpdateAsync(Track track)
    {
        _db.Tracks.Update(track);
        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var foundTrack = await _db.Tracks.FindAsync(id);
        _db.Tracks.Remove(foundTrack);
        await _db.SaveChangesAsync();
    }
}