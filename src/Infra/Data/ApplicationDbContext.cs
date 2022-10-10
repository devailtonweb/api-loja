namespace AppStore.Infra.Data;

using AppStore.Domain.Products;
using Flunt.Notifications;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : IdentityDbContext<IdentityUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
  
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    //public DbSet<Tag> Tags { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        //pega o OnModelCreating da classe pai IdentityDbContext
        base.OnModelCreating(builder);

        builder.Ignore<Notification>();

        builder.Entity<Product>()
            .Property(p => p.Description).HasMaxLength(500);
        builder.Entity<Product>()
            .Property(p => p.Name).IsRequired();
        builder.Entity<Category>()
            .Property(c => c.Name).IsRequired();
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configuration) 
    {
        configuration.Properties<string>()
            .HaveMaxLength(100);
    }

}

