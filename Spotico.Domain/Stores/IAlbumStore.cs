using Spotico.Domain.Models;

namespace Spotico.Domain.Stores;

public interface IAlbumStore
{
    /// <summary>
    /// Retrieves all albums from the database.
    /// </summary>
    /// <returns>The result contains a collection of albums.</returns>
    Task<IEnumerable<Album>> GetAsync();
    
    /// <summary>
    /// Retrieves an album by its identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the album.</param>
    /// <returns>The result contains the album if found; otherwise, a new album instance.</returns>
    Task<Album> GetByIdAsync(Guid id);
    
    /// <summary>
    /// Adds a new album to the database.
    /// </summary>
    /// <param name="album">The album to add.</param>
    Task AddAsync(Album album);
    
    /// <summary>
    /// Updates an existing album in the database.
    /// </summary>
    /// <param name="album">The album to update.</param>
    Task UpdateAsync(Album album);
    
    /// <summary>
    /// Deletes an album from the database by its identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the album to delete.</param>
    Task DeleteAsync(Guid id);
}