using System.ComponentModel.DataAnnotations.Schema;

namespace Spotico.Domain.Models;

public class Track
{
    public Guid Id { get; set; }
    [Column(TypeName = "VARCHAR(100)")]
    public string Title { get; set; }
    public string? TrackPath { get; set; }
    public double Duration { get; set; }
    public int Views { get; set; }
    
    // Foreign keys
    public Guid AlbumId { get; set; }
    public Guid ArtistId { get; set; }

    // Navigation properties
    [ForeignKey(nameof(AlbumId))]
    public Album Album { get; set; }
    [ForeignKey(nameof(ArtistId))]
    public User Artist { get; set; }
}