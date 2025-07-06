using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductStore
{
    public class ProductService
    {
        private readonly StoreDbContext _context;

        public ProductService(StoreDbContext context) => _context = context;

        // Retrieve all products
        public async Task<List<Product>> GetAllProductsAsync() =>
            await _context.Products.ToListAsync();

        // Find product by ID
        public async Task<Product?> FindProductByIdAsync(int id) =>
            await _context.Products.FindAsync(id);

        // Find the first product that meets a condition
        public async Task<Product?> FindExpensiveProductAsync(decimal priceThreshold) =>
            await _context.Products.FirstOrDefaultAsync(p => p.Price > priceThreshold);
    }
}
