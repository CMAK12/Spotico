using Spotico.Core.Models;

namespace Spotico.Infrastructure.Interfaces;

public interface ITokenService
{
    string GenerateToken(User user);
}