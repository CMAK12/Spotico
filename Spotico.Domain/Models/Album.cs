using System.ComponentModel.DataAnnotations.Schema;

namespace Spotico.Domain.Models;

public class Album
{
    public Guid Id { get; set; }
    [Column(TypeName = "VARCHAR(100)")]
    public string Title { get; set; }
    public string? CoverPath { get; set; }
    public List<Guid>? TrackIds { get; set; }
    
    // Foreign keys
    public Guid CreatedById { get; set; }
    
    // Navigation properties
    [ForeignKey(nameof(CreatedById))]
    public User CreatedBy { get; set; }
}