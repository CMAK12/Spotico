namespace Spotico.Server.DTOs;

public record AlbumDTO(
    string Title,
    Guid ArtistId,
    IFormFile CoverImage
);