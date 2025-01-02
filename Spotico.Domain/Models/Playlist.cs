using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Spotico.Domain.Models;

public class Playlist
{
    public Guid Id { get; set; }
    [Column(TypeName = "VARCHAR(100)")]
    public string Title { get; set; }
    [Column(TypeName = "VARCHAR(600)")]
    public string? Description { get; set; }
    public List<Guid>? TrackIds { get; set; }
    public bool IsPublic { get; set; }
    
    // Foreign keys
    public Guid CreatedById { get; set; }
    
    // Navigation properties
    [ForeignKey(nameof(CreatedById))]
    public User CreatedBy { get; set; }
}