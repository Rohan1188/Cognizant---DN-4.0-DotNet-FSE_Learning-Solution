using Microsoft.EntityFrameworkCore;
using RetailStore.Data;
using RetailStore.Models;

var db = new AppDbContext();

// Ensure database is created and seeded
db.Database.EnsureCreated();

// Query and display products
var products = db.Products.Include(p => p.Category).ToList();
Console.WriteLine("Products in the store:");
foreach (var product in products)
{
    Console.WriteLine($"{product.Name} - ${product.Price} ({product.Category.Name})");
}
