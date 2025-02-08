using Spotico.Infrastructure.Interfaces;

namespace Spotico.Infrastructure;

public class PasswordEncryptor : IPasswordEncryptor
{
    public string Generate(string password) =>
        BCrypt.Net.BCrypt.HashPassword(password);

    public bool Verify(string password, string hashedPassword) =>
        BCrypt.Net.BCrypt.Verify(password, hashedPassword);
}