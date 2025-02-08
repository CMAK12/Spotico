using Microsoft.EntityFrameworkCore;
using Spotico.Domain.Common.Enums;
using Spotico.Domain.Models;

namespace Spotico.Domain.Database;

public class SpoticoDbContext : DbContext
{
    public SpoticoDbContext(DbContextOptions<SpoticoDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }
    
    public DbSet<User> Users { get; set; }
    public DbSet<Playlist> Playlists { get; set; }
    public DbSet<Album> Albums { get; set; }
    public DbSet<Track> Tracks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // Define fixed GUIDs for seeded users
        var userId = Guid.NewGuid();
        var adminId = Guid.NewGuid();
        var artistId = Guid.NewGuid();
        
        // Define fixed GUIDs for seeded albums
        var throughTheDisasterAlbumId = Guid.NewGuid();
        
        // Define fixed GUIDs for seeded tracks
        var throughTheDisasterId = Guid.NewGuid();
        var welcomeAndGoodbyeId = Guid.NewGuid();
        
        modelBuilder.Entity<User>().HasData(
            new User {
                Id = userId,
                Username = "user",
                Email = "user@gmail.com",
                Password = "$2a$11$/KFp1kzp86HDW4Sbb1QeXOrmHLs2oNy2JwPFGY.To82L2/Ww1rnBS", // Encrypted "user"
                Bio = "This is the default user.",
                Role = nameof(UserRole.User)
            },
            new User {
                Id = adminId,
                Username = "admin",
                Email = "admin@gmail.com",
                Password = "$2a$11$nCLlbQ45OMfL.uuEtmucLOd74FNUNoS6jEUks6gPbzTzn52F4EDpC", // Encrypted "admin"
                Bio = "This is the admin user.",
                Role = nameof(UserRole.Admin)
            },
            new User {
                Id = artistId,
                Username = "artist",
                Email = "artist@gmail.com",
                Password = "$2a$11$SAmCUGu.oVf5XkCsU1WRO.AzGh6C07y5Jth4xa.ywfCOzMu8vDxJO", // Encrypted "artist"
                Bio = "This is the artist.",
                Role = nameof(UserRole.Author)
            }
        );

        modelBuilder.Entity<Track>().HasData(
            new Track
            {
                Id = throughTheDisasterId,
                Title = "Through The Disaster",
                TrackPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/tracks/1.mp3"),
                Duration = 196.3,
                Views = 51445,
                AlbumId = throughTheDisasterAlbumId,
                ArtistId = artistId
            },
            new Track
            {
                Id = welcomeAndGoodbyeId,
                Title = "welcome and goodbye",
                TrackPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/tracks/2.mp3"),
                Duration = 141.5,
                Views = 1425272,
                AlbumId = throughTheDisasterAlbumId,
                ArtistId = artistId
            }
        );

        modelBuilder.Entity<Album>().HasData(
            new Album
            {
                Id = throughTheDisasterAlbumId,
                Title = "Through The Disaster",
                CoverPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/default.png"),
                TrackIds = new List<Guid> { throughTheDisasterId, welcomeAndGoodbyeId },
                CreatedById = artistId
            }
        );

        modelBuilder.Entity<Playlist>().HasData(
            new Playlist
            {
                Id = Guid.NewGuid(),
                Title = "Kurtka Cobain",
                Description = "The best of Nirvana",
                TrackIds = new List<Guid> { throughTheDisasterId, welcomeAndGoodbyeId },
                IsPublic = false,
                CreatedById = adminId
            }
        );
    }
}