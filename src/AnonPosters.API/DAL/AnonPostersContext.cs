using AnonPosters.API.Models;
using Microsoft.EntityFrameworkCore;

namespace AnonPosters.API.DAL;

public class AnonPostersContext : DbContext
{
    public DbSet<Post> Post { get; set; }
    
    public DbSet<User> User { get; set; }

    public AnonPostersContext(DbContextOptions<AnonPostersContext> options) : base(options)
    {
    }
    
    
}