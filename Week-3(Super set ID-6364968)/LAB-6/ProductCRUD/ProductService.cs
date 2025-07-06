using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductCRUD
{
    public class ProductService
    {
        private readonly StoreDbContext _context;

        public ProductService(StoreDbContext context) => _context = context;

        // CRUD Operations
        public async Task<List<Product>> GetAllProducts() => 
            await _context.Products.ToListAsync();

        public async Task<Product?> GetProductByName(string name) =>
            await _context.Products.FirstOrDefaultAsync(p => p.Name == name);

        public async Task UpdateProductPrice(string productName, decimal newPrice)
        {
            var product = await GetProductByName(productName);
            if (product != null)
            {
                product.Price = newPrice;
                product.LastUpdated = DateTime.UtcNow;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteProduct(string productName)
        {
            var product = await GetProductByName(productName);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }
    }
}
