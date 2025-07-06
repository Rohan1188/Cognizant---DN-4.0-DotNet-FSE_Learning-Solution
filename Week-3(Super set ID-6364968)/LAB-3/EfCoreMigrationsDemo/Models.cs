using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace EfCoreMigrationsDemo
{
    public class Category
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
    }

    public class Product
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
        
        [Range(0, 1000000)]
        public decimal Price { get; set; }
        
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }

    public class AppDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=EfCoreMigrationsDemoDB;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(p => p.Name).HasMaxLength(200);
                entity.HasIndex(p => p.Name);
            });
            
            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(c => c.Name).HasMaxLength(100);
                entity.HasIndex(c => c.Name).IsUnique();
            });
        }
    }
}
