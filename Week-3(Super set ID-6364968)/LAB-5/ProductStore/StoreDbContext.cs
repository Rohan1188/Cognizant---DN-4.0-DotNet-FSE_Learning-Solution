using Microsoft.EntityFrameworkCore;

namespace ProductStore
{
    public class StoreDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=ProductStore;Trusted_Connection=True;")
                .LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(p => p.Price)
                    .HasColumnType("decimal(18,2)");
                
                entity.Property(p => p.LastUpdated)
                    .HasColumnType("datetime2");
            });

            var seedDate = new DateTime(2023, 1, 1);
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Laptop", Description = "High performance laptop", Price = 1500.99m, LastUpdated = seedDate },
                new Product { Id = 2, Name = "Smartphone", Description = "Latest model smartphone", Price = 899.99m, LastUpdated = seedDate },
                new Product { Id = 3, Name = "Headphones", Description = "Noise cancelling", Price = 349.99m, LastUpdated = seedDate },
                new Product { Id = 4, Name = "Gaming Console", Description = "Next-gen gaming console", Price = 499.99m, LastUpdated = seedDate },
                new Product { Id = 5, Name = "4K TV", Description = "Ultra HD Smart TV", Price = 1200.00m, LastUpdated = seedDate }
            );
        }
    }
}
