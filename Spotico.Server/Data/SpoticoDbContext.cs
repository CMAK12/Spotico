using Microsoft.EntityFrameworkCore;
using Spotico.Core.Models;

namespace Spotico.Server.Data;

public class SpoticoDbContext : DbContext
{
    public SpoticoDbContext(DbContextOptions<SpoticoDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }
    
    public DbSet<User> Users { get; set; }
}