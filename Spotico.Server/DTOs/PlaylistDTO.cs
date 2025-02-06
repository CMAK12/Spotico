namespace Spotico.Server.DTOs;

public class PlaylistDTO
{
    public string Title;
    public string? Description;
    public List<Guid>? TrackIds;
    public Guid CreatedById;
    public bool IsPublic;
};