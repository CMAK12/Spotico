namespace Spotico.Infrastructure.Interfaces;

public interface IPasswordEncryptor
{
    string Generate(string password);
    bool Verify(string password, string hashedPassword);
}