// Program.cs
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

// Define the models
public class Category
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
}

public class Product
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    public decimal Price { get; set; }
    
    public int CategoryId { get; set; }
    public Category Category { get; set; }
}

// DbContext
public class AppDbContext : DbContext
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Use SQL Server LocalDB - change connection string as needed
        optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=StoreDb;Trusted_Connection=True;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Optional: Configure relationships
        modelBuilder.Entity<Product>()
            .HasOne(p => p.Category)
            .WithMany()
            .HasForeignKey(p => p.CategoryId);
    }
}

public class Program
{
    public static async Task Main(string[] args)
    {
        // Create database if it doesn't exist
        using (var context = new AppDbContext())
        {
            await context.Database.EnsureCreatedAsync();
            
            // Insert initial data
            var electronics = new Category { Name = "Electronics" };
            var groceries = new Category { Name = "Groceries" };
            await context.Categories.AddRangeAsync(electronics, groceries);
            
            var product1 = new Product { Name = "Laptop", Price = 75000, Category = electronics };
            var product2 = new Product { Name = "Rice Bag", Price = 1200, Category = groceries };
            await context.Products.AddRangeAsync(product1, product2);
            
            await context.SaveChangesAsync();
            
            Console.WriteLine("Initial data inserted successfully!");
            
            // Display the inserted data
            Console.WriteLine("\nCategories:");
            foreach (var category in await context.Categories.ToListAsync())
            {
                Console.WriteLine($"{category.Id}: {category.Name}");
            }
            
            Console.WriteLine("\nProducts:");
            foreach (var product in await context.Products.Include(p => p.Category).ToListAsync())
            {
                Console.WriteLine($"{product.Id}: {product.Name} - ${product.Price} (Category: {product.Category.Name})");
            }
        }
    }
}
