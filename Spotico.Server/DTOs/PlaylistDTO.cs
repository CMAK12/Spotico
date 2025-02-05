namespace Spotico.Server.DTOs;

public class PlaylistDTO(
    string Title, 
    string? Description, 
    List<Guid>? TrackIds, 
    Guid CreatedById, 
    bool IsPublic
);