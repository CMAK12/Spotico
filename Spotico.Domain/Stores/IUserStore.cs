using Spotico.Domain.Models;

namespace Spotico.Domain.Stores;

public interface IUserStore
{
    /// <summary>Asynchronously retrieves a user by their ID</summary>
    /// <param name="id">The unique identificator of customers</param>
    /// <returns>Returns specific customer</returns>
    Task<User> GetByIdAsync(Guid id);
    
    /// <summary>Adds a user into the database</summary>
    /// <param name="user">User object for adding</param>
    Task AddAsync(User user);
    
    /// <summary>Updates a user in the database</summary>
    /// <param name="user">User object for updating</param>
    Task UpdateAsync(User user);
    
    /// <summary>Removes a user from the database</summary>
    /// <param name="id">The unique identificator of customers</param>
    Task DeleteAsync(Guid id);
}