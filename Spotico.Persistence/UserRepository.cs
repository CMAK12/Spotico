using Spotico.Core.Stores;
using Spotico.Core.Models;

namespace Spotico.Persistence;

public class UserRepository : IUserStore
{
    public Task<User> GetById(Guid id)
    {
        throw new NotImplementedException();
    }
    
    public Task Add(User user)
    {
        throw new NotImplementedException();
    }
}