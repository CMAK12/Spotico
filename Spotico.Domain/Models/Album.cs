using System.ComponentModel.DataAnnotations.Schema;

namespace Spotico.Domain.Models;

public class Album
{
    public Guid Id { get; set; }
    [Column(TypeName = "VARCHAR(100)")]
    public string Title { get; set; }
    public Guid ArtistId { get; set; }
    public List<Track> Tracks { get; set; }
    public string? CoverPath { get; set; }
}