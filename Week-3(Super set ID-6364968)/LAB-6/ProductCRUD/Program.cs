using System;
using System.Threading.Tasks;

namespace ProductCRUD
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using var context = new StoreDbContext();
            context.Database.EnsureCreated();
            
            var service = new ProductService(context);

            Console.WriteLine("=== Initial Products ===");
            await DisplayProducts(service);

            // Update operation
            Console.WriteLine("\nUpdating Laptop price to 70000...");
            await service.UpdateProductPrice("Laptop", 70000);

            // Delete operation
            Console.WriteLine("\nDeleting Headphones...");
            await service.DeleteProduct("Headphones");

            Console.WriteLine("\n=== Updated Products ===");
            await DisplayProducts(service);
        }

        static async Task DisplayProducts(ProductService service)
        {
            var products = await service.GetAllProducts();
            foreach (var p in products)
            {
                Console.WriteLine($"{p.Id} | {p.Name,-10} | {p.Price,10:C} | Discontinued: {p.IsDiscontinued}");
            }
        }
    }
}
