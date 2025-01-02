using Spotico.Domain.Models;

namespace Spotico.Infrastructure.Interfaces;

public interface ITokenService
{
    /// <summary>
    /// Generates a JWT token for the specified user.
    /// </summary>
    /// <param name="user">The user for whom the token is being generated.</param>
    /// <returns>A JWT token as a string.</returns>
    string GenerateToken(User user);
}