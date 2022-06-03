using Lab4.Data.Models;

namespace Lab4.Data;

using Microsoft.EntityFrameworkCore;
using Lab4.Data.Models;




public class EducationContext:DbContext
{
    public DbSet<Seller> Sellers { get; set; }
    public DbSet<CarPart> CarParts { get; set; }
    public DbSet<Customer> Customers { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder); 
        optionsBuilder.UseNpgsql(@"Host=localhost; Port=5432; Database=education;Username=education; Password=password")
            .UseSnakeCaseNamingConvention()
            .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole())).EnableSensitiveDataLogging();
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>().Property(cu => cu.ID).ValueGeneratedOnAdd();
        modelBuilder.Entity<CarPart>().Property(cp => cp.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<Seller>().Property(sl => sl.ID).ValueGeneratedOnAdd();
        //modelBuilder.Entity<Customer>().HasMany(cu => cu.CСParts).WithMany(cp => cp.Customers);
        modelBuilder.Entity<Seller>().HasMany(sl => sl.SСParts).WithMany(cp => cp.Sellers);

    }

}