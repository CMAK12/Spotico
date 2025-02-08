namespace Spotico.Server.DTOs;

public record UserDTO(
    string Username,
    string Email,
    string Password
);