using Microsoft.EntityFrameworkCore;
using Spotico.Core.Database;
using Spotico.Core.Models;
using Spotico.Core.Stores;

namespace Spotico.Persistence;

public class PlaylistRepository : IPlaylistStore
{
    private readonly SpoticoDbContext _db;
    
    public PlaylistRepository(SpoticoDbContext db)
    {
        _db = db;
    }
    
    public async Task<List<Playlist>> GetAsync()
    {
        return await _db.Playlists.ToListAsync();
    }

    public async Task<Playlist> GetByIdAsync(Guid id)
    {
        var identifiedPlaylist = await _db.Playlists.FindAsync(id);
        
        return identifiedPlaylist ?? new Playlist();
    }

    public async Task AddAsync(Playlist playlist)
    {
        await _db.Playlists.AddAsync(playlist);
        await _db.SaveChangesAsync();
    }

    public async Task UpdateAsync(Playlist playlist)
    {
        _db.Playlists.Update(playlist);
        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var foundPlaylist = await _db.Playlists.FindAsync(id);
        _db.Playlists.Remove(foundPlaylist);
        await _db.SaveChangesAsync();
    }
}