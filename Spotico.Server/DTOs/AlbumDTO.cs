namespace Spotico.Server.DTOs;

public class AlbumDTO
{
    public string Title { get; set; }
    public Guid ArtistId { get; set; }
    public IFormFile CoverImage { get; set; }
}