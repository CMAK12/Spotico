using Spotico.Core.Models;

namespace Spotico.Core.Stores;

public interface IUserStore
{
    Task<User> GetByIdAsync(Guid id);
    Task AddAsync(User user);
    Task DeleteAsync(Guid id);
}