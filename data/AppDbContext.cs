using Microsoft.EntityFrameworkCore;

namespace authentication;

public class AppDbContext:DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
    {
        
    }

    public DbSet<User> Users {get;set;}
    public DbSet<Roles> Roles {get;set;}

}
