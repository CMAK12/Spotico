using Microsoft.AspNetCore.Http;
using Spotico.Infrastructure.Interfaces;

namespace Spotico.Infrastructure;

public class MediaService : IMediaService
{
    public async Task<string> UploadTrackAsync(IFormFile track)
    {
        string newTrackTitle = $"{Guid.NewGuid()}_{track.FileName}"; // Generate a new file name with a unique identifier
        await UploadFileAsync(track, "tracks", newTrackTitle); // Upload the track mp3 file to tracks
        return "/tracks/" + newTrackTitle;
    }
    
    public async Task<string> UploadImageAsync(IFormFile image)
    {
        if (image == null) return "/images/default.png"; // If no image is provided, return the default image
        string newImageTitle = $"{Guid.NewGuid()}_{image.FileName}"; // Generate a new file name with a unique identifier
        await UploadFileAsync(image, "images", newImageTitle); // Upload the image to images folder
        return "/images/" + newImageTitle;
    }
    
    public async Task DeleteFileAsync(string path)
    {
        var fullPath = Path.Combine(Directory.GetCurrentDirectory(), path.TrimStart('/'));
        if (File.Exists(fullPath)) File.Delete(fullPath);
    }

    private async Task UploadFileAsync(IFormFile file, string folder, string newFileName)
    {
        // Get the path to the directory where the images are stored
        var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", folder);
        // If the directory doesn't exist, create it
        if (!Directory.Exists(directoryPath)) Directory.CreateDirectory(directoryPath);
        // Get the full path to the file with the file name
        var fullPath = Path.Combine(directoryPath, newFileName);
        
        // Copy the file to the specified path
        using (var fileStream = new FileStream(fullPath, FileMode.Create))
        {
            await file.CopyToAsync(fileStream); // Ensures the file be written to the disk
        }
    }
}