using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AuthWebApi.Models.Data;

public class AuthDbContext : IdentityDbContext<PspUser>
{
    public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
    { }
    
    public DbSet<ClientEntity> Clients { get; set; }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<ClientEntity>().HasKey(m => m.ClientId);
        base.OnModelCreating(builder);
    }
}