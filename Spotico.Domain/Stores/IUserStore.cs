using Spotico.Core.Models;

namespace Spotico.Core.Stores;

public interface IUserStore
{
    Task<User> GetById(Guid id);

    Task Add(User user);
    
    Task Delete(Guid id);
}