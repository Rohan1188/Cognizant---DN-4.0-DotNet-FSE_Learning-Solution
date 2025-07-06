using Microsoft.EntityFrameworkCore;

namespace ProductStore
{
    public class StoreDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=ProductStore;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed test data with descriptions
            modelBuilder.Entity<Product>().HasData(
                new Product { 
                    Id = 1, 
                    Name = "Premium Laptop", 
                    Description = "High performance business laptop",
                    Price = 2500.99m,
                    CategoryId = 1,
                    CreatedDate = DateTime.Now.AddDays(-30),
                    InStock = true
                },
                new Product { 
                    Id = 2, 
                    Name = "Wireless Headphones", 
                    Description = "Noise-cancelling Bluetooth headphones",
                    Price = 349.99m,
                    CategoryId = 2,
                    CreatedDate = DateTime.Now.AddDays(-15),
                    InStock = true 
                },
                new Product { 
                    Id = 3, 
                    Name = "4K Smart TV", 
                    Description = "55-inch Ultra HD Smart TV",
                    Price = 1899.99m,
                    CategoryId = 3,
                    CreatedDate = DateTime.Now.AddDays(-60),
                    InStock = true 
                },
                new Product { 
                    Id = 4, 
                    Name = "Gaming Console", 
                    Description = "Latest generation gaming console",
                    Price = 499.99m,
                    CategoryId = 4,
                    CreatedDate = DateTime.Now.AddDays(-7),
                    InStock = true 
                },
                new Product { 
                    Id = 5, 
                    Name = "High-End Smartphone", 
                    Description = "Flagship smartphone with advanced camera",
                    Price = 1299.99m,
                    CategoryId = 5,
                    CreatedDate = DateTime.Now.AddDays(-3),
                    InStock = false 
                }
            );
        }
    }
}
