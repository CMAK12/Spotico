using System.ComponentModel.DataAnnotations.Schema;
using Spotico.Domain.Common.Enums;

namespace Spotico.Domain.Models;

public class User
{
    public Guid Id { get; set; }
    [Column(TypeName = "VARCHAR(100)")]
    public string Username { get; set; }
    [Column(TypeName = "VARCHAR(320)")]
    public string Email { get; set; }
    [Column(TypeName = "VARCHAR(600)")]
    public string? Bio { get; set; }
    public string Password { get; set; }
    public string Role { get; set; } = nameof(UserRole.User);
}