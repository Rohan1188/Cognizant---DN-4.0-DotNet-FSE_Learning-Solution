using Microsoft.EntityFrameworkCore;

namespace ProductCRUD
{
    public class StoreDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=ProductCRUD;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Laptop", Description = "High performance laptop", Price = 1500.99m },
                new Product { Id = 2, Name = "Smartphone", Description = "Latest model smartphone", Price = 899.99m },
                new Product { Id = 3, Name = "Headphones", Description = "Noise cancelling", Price = 349.99m }
            );
        }
    }
}
