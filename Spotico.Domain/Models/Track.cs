namespace Spotico.Core.Models;

public class Track
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Artist { get; set; }
    public string Album { get; set; }
    // Temporary without a song file
    public float Duration { get; set; }
    public string Cover { get; set; }
}