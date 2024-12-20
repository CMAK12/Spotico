namespace Spotico.Core.Models;

public class Playlist
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public List<Track>? Tracks { get; set; }
    public string CreatedBy { get; set; }
    public bool IsPublic { get; set; }
}