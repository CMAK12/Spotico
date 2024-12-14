using Spotico.Core.Models;

namespace Spotico.Infrastructure.Interfaces;

public interface IJwtProvider
{
    string GenerateToken(User user);
}