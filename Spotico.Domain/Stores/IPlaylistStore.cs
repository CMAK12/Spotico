using Spotico.Core.Models;

namespace Spotico.Core.Stores;

public interface IPlaylistStore
{
    Task<List<Playlist>> GetAsync();
    Task<Playlist> GetByIdAsync(Guid id);
    Task AddAsync(Playlist playlist);
    Task UpdateAsync(Playlist playlist);
    Task DeleteAsync(Guid id);
}