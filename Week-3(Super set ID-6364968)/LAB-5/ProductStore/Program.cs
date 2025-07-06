using System;
using System.Threading.Tasks;

namespace ProductStore
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                using var context = new StoreDbContext();
                
                // Ensure database is created and migrated
                Console.WriteLine("Ensuring database is created...");
                await context.Database.EnsureCreatedAsync();
                
                var service = new ProductService(context);

                Console.WriteLine("=== All Products ===");
                var products = await service.GetAllProductsAsync();
                foreach (var p in products)
                {
                    Console.WriteLine($"{p.Id} | {p.Name,-15} | {p.Price,10:C} | {p.LastUpdated:yyyy-MM-dd}");
                }

                Console.WriteLine("\n=== Finding Product by ID (1) ===");
                var product = await service.FindProductByIdAsync(1);
                Console.WriteLine($"Found: {product?.Name ?? "Not Found"}");

                Console.WriteLine("\n=== Finding Expensive Product (>50000) ===");
                var expensive = await service.FindExpensiveProductAsync(50000);
                Console.WriteLine($"Expensive: {expensive?.Name ?? "None found"}");
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }
                Console.ResetColor();
            }
        }
    }
}
