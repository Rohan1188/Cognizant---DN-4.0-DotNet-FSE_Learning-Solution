using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductStore
{
    public class ProductService
    {
        private readonly StoreDbContext _context;

        public ProductService(StoreDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetFilteredAndSortedProductsAsync()
        {
            try
            {
                return await _context.Products
                    .Where(p => p.Price > 1000)
                    .OrderByDescending(p => p.Price)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving products: {ex.Message}");
                return new List<Product>();
            }
        }

        public async Task<List<ProductDTO>> GetProductDTOsAsync()
        {
            try
            {
                return await _context.Products
                    .Select(p => new ProductDTO { Name = p.Name, Price = p.Price })
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving product DTOs: {ex.Message}");
                return new List<ProductDTO>();
            }
        }

        // Combined query example
        public async Task<List<ProductDTO>> GetFilteredProductDTOsAsync()
        {
            return await _context.Products
                .Where(p => p.Price > 1000)
                .Select(p => new ProductDTO { Name = p.Name, Price = p.Price })
                .OrderByDescending(p => p.Price)
                .ToListAsync();
        }
    }
}
