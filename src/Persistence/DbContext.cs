


namespace App.Persistence;

using App.Domain.Entities;
using Microsoft.EntityFrameworkCore;

class ApplicationDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Suffix> suffixes { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderLine> OrderLines { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder.UseInMemoryDatabase("appddd")
            .EnableSensitiveDataLogging();    

        //optionsBuilder.UseSqlServer("Server=localhost;Database=appddd;User Id=sa;Password=Password123;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        HiLoConfig.Configure(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }

}