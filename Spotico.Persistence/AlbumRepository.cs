using Microsoft.EntityFrameworkCore;
using Spotico.Domain.Database;
using Spotico.Domain.Models;
using Spotico.Domain.Stores;

namespace Spotico.Persistence;

public class AlbumRepository : IAlbumStore
{
    private readonly SpoticoDbContext _db;

    public AlbumRepository(SpoticoDbContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<Album>> GetAsync()
    {
        return await _db.Albums.ToListAsync();
    }

    public async Task<Album> GetByIdAsync(Guid id)
    {
        var identifiedAlbum = await _db.Albums.FindAsync(id);
        return identifiedAlbum ?? new Album();
    }

    public async Task AddAsync(Album album)
    {
        await _db.Albums.AddAsync(album);
        await _db.SaveChangesAsync();
    }

    public async Task UpdateAsync(Album album)
    {
        _db.Albums.Update(album);
        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var foundAlbum = await _db.Albums.FindAsync(id);
        _db.Albums.Remove(foundAlbum);
        await _db.SaveChangesAsync();
    }
}