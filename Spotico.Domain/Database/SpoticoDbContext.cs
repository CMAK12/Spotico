using Microsoft.EntityFrameworkCore;
using Spotico.Core.Models;

namespace Spotico.Core.Database;

public class SpoticoDbContext : DbContext
{
    public SpoticoDbContext(DbContextOptions<SpoticoDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }
    
    public DbSet<User> Users { get; set; }
    public DbSet<Playlist> Playlists { get; set; }
    public DbSet<Track> Tracks { get; set; }
}