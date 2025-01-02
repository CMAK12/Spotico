namespace Spotico.Server.DTOs;

public class TrackDTO
{
    public string Title { get; set; }
    public Guid ArtistId { get; set; }
    public IFormFile File { get; set; }
    public string Duration { get; set; }
    public Guid AlbumId { get; set; }
}