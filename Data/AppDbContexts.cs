using Microsoft.EntityFrameworkCore;
using RePurpose.Models;




namespace RePurpose.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Map models to database tables
        public DbSet<Product> Products { get; set; }
        public DbSet<Part> Parts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define the relationship: 1 Product → Many Parts
            modelBuilder.Entity<Product>()
                .HasMany(p => p.Parts)
                .WithOne(p => p.Product)
                .HasForeignKey(p => p.ProductID);
        }
    }
}