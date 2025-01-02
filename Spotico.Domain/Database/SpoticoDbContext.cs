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
        
        modelBuilder.Entity<User>().HasData(new User
        {
            Id = Guid.NewGuid(),
            Username = "user",
            Email = "user@gmail.com",
            Password = "user", // Hash this in production
            Bio = "This is the default user.",
            Role = nameof(UserRole.User)
        });
        modelBuilder.Entity<User>().HasData(new User
        {
            Id = Guid.NewGuid(),
            Username = "admin",
            Email = "admin@gmail.com",
            Password = "admin",
            Bio = "This is the admin user.",
            Role = nameof(UserRole.Admin)
        });
        modelBuilder.Entity<User>().HasData(new User
        {
            Id = Guid.NewGuid(),
            Username = "artist",
            Email = "artist@gmail.com",
            Password = "artist",
            Bio = "This is the artist.",
            Role = nameof(UserRole.Author)
        });
    }
}