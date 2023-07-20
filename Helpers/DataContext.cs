using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Project.WebApi.Entities.Models;

namespace Project.WebApi.Helpers;

public class DataContext : IdentityDbContext<User>
{
    public DataContext(DbContextOptions options)
    : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new RoleConfiguration());
    }

    public DbSet<Article> Articles { get; set; }

}