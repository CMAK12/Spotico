using Spotico.Domain.Models;

namespace Spotico.Domain.Stores;

public interface IPlaylistStore
{
    /// <summary>
    /// Retrieves all playlists from the database.
    /// </summary>
    /// <returns>The result contains a collection of playlists.</returns>
    Task<IEnumerable<Playlist>> GetAsync();
    
    /// <summary>
    /// Retrieves a playlist by its identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the playlist.</param>
    /// <returns>The result contains the playlist if found; otherwise, a new playlist instance.</returns>
    Task<Playlist> GetByIdAsync(Guid id);
    
    /// <summary>
    /// Adds a new playlist to the database.
    /// </summary>
    /// <param name="playlist">The playlist to add.</param>
    Task AddAsync(Playlist playlist);
    
    /// <summary>
    /// Updates an existing playlist in the database.
    /// </summary>
    /// <param name="playlist">The playlist to update.</param>
    Task UpdateAsync(Playlist playlist);
    
    /// <summary>
    /// Deletes a playlist from the database by its identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the playlist to delete.</param>
    Task DeleteAsync(Guid id);
}