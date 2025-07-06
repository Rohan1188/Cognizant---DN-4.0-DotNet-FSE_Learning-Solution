using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace EfCoreMigrationsDemo
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("EF Core Migrations Demo");
            Console.WriteLine("1. Apply migrations");
            Console.WriteLine("2. Insert test data");
            Console.Write("Choose an option: ");
            
            var option = Console.ReadLine();

            using var db = new AppDbContext();
            
            if (option == "1")
            {
                Console.WriteLine("Applying migrations...");
                await db.Database.MigrateAsync();
                Console.WriteLine("Migrations applied successfully!");
            }
            else if (option == "2")
            {
                Console.WriteLine("Inserting test data...");
                
                var electronics = new Category { Name = "Electronics" };
                var groceries = new Category { Name = "Groceries" };
                
                await db.Categories.AddRangeAsync(electronics, groceries);
                await db.SaveChangesAsync();
                
                var laptop = new Product { Name = "Laptop", Price = 999.99m, CategoryId = 1 };
                var rice = new Product { Name = "Rice", Price = 12.50m, CategoryId = 2 };
                
                await db.Products.AddRangeAsync(laptop, rice);
                await db.SaveChangesAsync();
                
                Console.WriteLine("Test data inserted successfully!");
            }
            
            Console.WriteLine("\nCurrent database state:");
            Console.WriteLine("Categories:");
            foreach (var category in await db.Categories.ToListAsync())
            {
                Console.WriteLine($"- {category.Id}: {category.Name}");
            }
            
            Console.WriteLine("\nProducts:");
            foreach (var product in await db.Products.Include(p => p.Category).ToListAsync())
            {
                Console.WriteLine($"- {product.Id}: {product.Name} (${product.Price}) [{product.Category?.Name}]");
            }
        }
    }
}
