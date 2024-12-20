using Spotico.Core.Models;

namespace Spotico.Core.Stores;

public interface ITrackStore
{
    Task<List<Track>> GetAsync();
    Task<Track> GetByIdAsync(Guid id);
    Task AddAsync(Track track);
    Task UpdateAsync(Track track);
    Task DeleteAsync(Guid id);
}