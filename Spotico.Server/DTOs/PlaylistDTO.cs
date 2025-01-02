namespace Spotico.Server.DTOs;

public class PlaylistDTO
{
    public string Title { get; set; }
    public string? Description { get; set; }
    public List<Guid>? TrackIds { get; set; }
    public Guid CreatedById { get; set; }
    public bool IsPublic { get; set; }
}