using Microsoft.AspNetCore.Http;

namespace Spotico.Infrastructure.Interfaces;

public interface IMediaService
{
    /// <summary>
    /// Uploads the track mp3 to the "tracks" directory.
    /// </summary>
    /// <param name="track">The track file to be uploaded.</param>
    /// <returns>The relative path to the uploaded track file.</returns>
    Task<string> UploadTrackAsync(IFormFile track);
    
    /// <summary>
    /// Uploads the image file to the "images" directory.
    /// </summary>
    /// <param name="image">The image file to be uploaded.</param>
    /// <returns>The relative path to the uploaded image file.</returns>
    Task<string> UploadImageAsync(IFormFile image);
    
    /// <summary>Deletes the file from the directories.</summary>
    /// <param name="path">The full path to the file directory from DB.</param>
    Task DeleteFileAsync(string path);
}