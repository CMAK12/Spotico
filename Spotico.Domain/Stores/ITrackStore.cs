using Spotico.Domain.Models;

namespace Spotico.Domain.Stores;

public interface ITrackStore
{
    /// <summary>
    /// Retrieves all tracks from the database.
    /// </summary>
    /// <returns>The task result contains a collection of tracks.</returns>
    Task<IEnumerable<Track>> GetAsync();
    
    /// <summary>
    /// Retrieves a track by its identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the track.</param>
    /// <returns>The result contains the track if found; otherwise, a new track instance.</returns>
    Task<Track> GetByIdAsync(Guid id);
    
    /// <summary>
    /// Adds a new track to the database.
    /// </summary>
    /// <param name="track">The track to add.</param>
    Task AddAsync(Track track);
    
    /// <summary>
    /// Updates an existing track in the database.
    /// </summary>
    /// <param name="track">The track to update.</param>
    Task UpdateAsync(Track track);
    
    /// <summary>
    /// Deletes a track from the database by its identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the track to delete.</param>
    Task DeleteAsync(Guid id);
}