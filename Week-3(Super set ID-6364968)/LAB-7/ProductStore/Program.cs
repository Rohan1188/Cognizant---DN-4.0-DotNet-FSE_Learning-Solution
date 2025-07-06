using System;
using System.Threading.Tasks;

namespace ProductStore
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using var context = new StoreDbContext();
            var service = new ProductService(context);

            try
            {
                // Ensure database exists and has test data
                context.Database.EnsureCreated();

                Console.WriteLine("1. Filtered and Sorted Products (Price > 1000):");
                var filteredProducts = await service.GetFilteredAndSortedProductsAsync();
                foreach (var product in filteredProducts)
                {
                    Console.WriteLine($"{product.Name} - ${product.Price}");
                }

                Console.WriteLine("\n2. Product DTOs:");
                var productDTOs = await service.GetProductDTOsAsync();
                foreach (var dto in productDTOs)
                {
                    Console.WriteLine($"{dto.Name} - ${dto.Price}");
                }

                Console.WriteLine("\n3. Combined Query (Filtered DTOs):");
                var filteredDTOs = await service.GetFilteredProductDTOsAsync();
                foreach (var dto in filteredDTOs)
                {
                    Console.WriteLine($"{dto.Name} - ${dto.Price}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}
