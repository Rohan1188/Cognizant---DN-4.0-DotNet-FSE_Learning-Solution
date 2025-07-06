using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace RetailInventory
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Console.WriteLine("Retail Inventory System with EF Core 8.0");
            Console.WriteLine("----------------------------------------");

            // Initialize the database
            using var db = new RetailDbContext();
            await db.Database.EnsureCreatedAsync();

            // Add sample data if database is empty
            if (!db.Products.Any())
            {
                await SeedDatabase(db);
                Console.WriteLine("Database seeded with sample data.");
            }

            while (true)
            {
                Console.WriteLine("\nMenu:");
                Console.WriteLine("1. List all products");
                Console.WriteLine("2. Add new product");
                Console.WriteLine("3. Update product stock");
                Console.WriteLine("4. Delete product");
                Console.WriteLine("5. View products by category");
                Console.WriteLine("6. View product details (with JSON metadata)");
                Console.WriteLine("7. Exit");
                Console.Write("Select an option: ");

                if (!int.TryParse(Console.ReadLine(), out var choice))
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                    continue;
                }

                try
                {
                    switch (choice)
                    {
                        case 1:
                            await ListAllProducts(db);
                            break;
                        case 2:
                            await AddNewProduct(db);
                            break;
                        case 3:
                            await UpdateProductStock(db);
                            break;
                        case 4:
                            await DeleteProduct(db);
                            break;
                        case 5:
                            await ViewProductsByCategory(db);
                            break;
                        case 6:
                            await ViewProductDetails(db);
                            break;
                        case 7:
                            return;
                        default:
                            Console.WriteLine("Invalid option. Please try again.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }

        private static async Task ListAllProducts(RetailDbContext db)
        {
            Console.WriteLine("\nAll Products:");
            var products = await db.Products
                .Include(p => p.Category)
                .OrderBy(p => p.Name)
                .ToListAsync();

            foreach (var product in products)
            {
                Console.WriteLine($"{product.Id}: {product.Name} - {product.Price:C} ({product.StockLevel} in stock)");
                Console.WriteLine($"   Category: {product.Category?.Name}");
            }
        }

        private static async Task AddNewProduct(RetailDbContext db)
        {
            Console.Write("Enter product name: ");
            var name = Console.ReadLine() ?? string.Empty;

            Console.Write("Enter product price: ");
            if (!decimal.TryParse(Console.ReadLine(), out var price))
            {
                Console.WriteLine("Invalid price.");
                return;
            }

            Console.Write("Enter initial stock level: ");
            if (!int.TryParse(Console.ReadLine(), out var stockLevel))
            {
                Console.WriteLine("Invalid stock level.");
                return;
            }

            // List available categories
            var categories = await db.Categories.ToListAsync();
            Console.WriteLine("Available Categories:");
            foreach (var categoryItem in categories)
            {
                Console.WriteLine($"{categoryItem.Id}: {categoryItem.Name}");
            }

            Console.Write("Enter category ID (0 to skip): ");
            if (!int.TryParse(Console.ReadLine(), out var categoryId))
            {
                Console.WriteLine("Invalid category ID.");
                return;
            }

            var selectedCategory = categoryId > 0 ? await db.Categories.FindAsync(categoryId) : null;

            Console.Write("Enter additional metadata as JSON (or press Enter to skip): ");
            var metadataJson = Console.ReadLine();
            Dictionary<string, object>? metadata = null;

            if (!string.IsNullOrWhiteSpace(metadataJson))
            {
                try
                {
                    metadata = JsonSerializer.Deserialize<Dictionary<string, object>>(metadataJson);
                }
                catch
                {
                    Console.WriteLine("Invalid JSON format. Metadata will not be saved.");
                }
            }

            var product = new Product
            {
                Name = name,
                Price = price,
                StockLevel = stockLevel,
                Category = selectedCategory,
                Metadata = metadata
            };

            db.Products.Add(product);
            await db.SaveChangesAsync();
            Console.WriteLine($"Product '{name}' added successfully with ID: {product.Id}");
        }

        private static async Task UpdateProductStock(RetailDbContext db)
        {
            Console.Write("Enter product ID to update: ");
            if (!int.TryParse(Console.ReadLine(), out var productId))
            {
                Console.WriteLine("Invalid product ID.");
                return;
            }

            var product = await db.Products.FindAsync(productId);
            if (product == null)
            {
                Console.WriteLine("Product not found.");
                return;
            }

            Console.Write($"Current stock level: {product.StockLevel}. Enter new stock level: ");
            if (!int.TryParse(Console.ReadLine(), out var newStockLevel))
            {
                Console.WriteLine("Invalid stock level.");
                return;
            }

            product.StockLevel = newStockLevel;
            await db.SaveChangesAsync();
            Console.WriteLine("Stock level updated successfully.");
        }

        private static async Task DeleteProduct(RetailDbContext db)
        {
            Console.Write("Enter product ID to delete: ");
            if (!int.TryParse(Console.ReadLine(), out var productId))
            {
                Console.WriteLine("Invalid product ID.");
                return;
            }

            var product = await db.Products.FindAsync(productId);
            if (product == null)
            {
                Console.WriteLine("Product not found.");
                return;
            }

            db.Products.Remove(product);
            await db.SaveChangesAsync();
            Console.WriteLine($"Product '{product.Name}' deleted successfully.");
        }

        private static async Task ViewProductsByCategory(RetailDbContext db)
        {
            var categories = await db.Categories.ToListAsync();
            Console.WriteLine("Available Categories:");
            foreach (var cat in categories)
            {
                Console.WriteLine($"{cat.Id}: {cat.Name}");
            }

            Console.Write("Enter category ID: ");
            if (!int.TryParse(Console.ReadLine(), out var categoryId))
            {
                Console.WriteLine("Invalid category ID.");
                return;
            }

            var products = await db.Products
                .Where(p => p.Category != null && p.Category.Id == categoryId)
                .Include(p => p.Category)
                .ToListAsync();

            if (!products.Any())
            {
                Console.WriteLine("No products found in this category.");
                return;
            }

            Console.WriteLine($"\nProducts in category '{categories.First(c => c.Id == categoryId).Name}':");
            foreach (var product in products)
            {
                Console.WriteLine($"{product.Id}: {product.Name} - {product.Price:C} ({product.StockLevel} in stock)");
            }
        }

        private static async Task ViewProductDetails(RetailDbContext db)
        {
            Console.Write("Enter product ID: ");
            if (!int.TryParse(Console.ReadLine(), out var productId))
            {
                Console.WriteLine("Invalid product ID.");
                return;
            }

            var product = await db.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == productId);

            if (product == null)
            {
                Console.WriteLine("Product not found.");
                return;
            }

            Console.WriteLine($"\nProduct Details for '{product.Name}':");
            Console.WriteLine($"ID: {product.Id}");
            Console.WriteLine($"Price: {product.Price:C}");
            Console.WriteLine($"Stock Level: {product.StockLevel}");
            Console.WriteLine($"Category: {product.Category?.Name ?? "None"}");

            if (product.Metadata != null)
            {
                Console.WriteLine("Additional Metadata:");
                foreach (var (key, value) in product.Metadata)
                {
                    Console.WriteLine($"  {key}: {value}");
                }
            }
        }

        private static async Task SeedDatabase(RetailDbContext db)
        {
            // Add categories
            var categories = new List<Category>
            {
                new() { Name = "Electronics" },
                new() { Name = "Clothing" },
                new() { Name = "Groceries" },
                new() { Name = "Home Goods" }
            };
            await db.Categories.AddRangeAsync(categories);
            await db.SaveChangesAsync();

            // Add products
            var products = new List<Product>
            {
                new() {
                    Name = "Smartphone",
                    Price = 599.99m,
                    StockLevel = 50,
                    Category = categories[0],
                    Metadata = new Dictionary<string, object>
                    {
                        { "Brand", "Samsung" },
                        { "Model", "Galaxy S23" },
                        { "Color", "Black" }
                    }
                },
                new() {
                    Name = "Laptop",
                    Price = 1299.99m,
                    StockLevel = 30,
                    Category = categories[0],
                    Metadata = new Dictionary<string, object>
                    {
                        { "Brand", "Apple" },
                        { "Model", "MacBook Pro" },
                        { "Processor", "M2" }
                    }
                },
                new() {
                    Name = "T-Shirt",
                    Price = 19.99m,
                    StockLevel = 100,
                    Category = categories[1],
                    Metadata = new Dictionary<string, object>
                    {
                        { "Size", "M" },
                        { "Color", "Blue" },
                        { "Material", "Cotton" }
                    }
                },
                new() {
                    Name = "Milk",
                    Price = 3.49m,
                    StockLevel = 200,
                    Category = categories[2]
                },
                new() {
                    Name = "Coffee Table",
                    Price = 149.99m,
                    StockLevel = 15,
                    Category = categories[3],
                    Metadata = new Dictionary<string, object>
                    {
                        { "Material", "Wood" },
                        { "Dimensions", "120x60x45 cm" }
                    }
                }
            };
            await db.Products.AddRangeAsync(products);
            await db.SaveChangesAsync();
        }
    }

    public class RetailDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Using SQL Server LocalDB for this demo
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=RetailInventory;Trusted_Connection=True;");
            
            // Enable sensitive data logging for debugging (remove in production)
            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the Product entity
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Name).IsRequired().HasMaxLength(100);
                entity.Property(p => p.Price).HasColumnType("decimal(18,2)");
                entity.Property(p => p.StockLevel).HasDefaultValue(0);
                
                // Configure JSON column for Metadata (EF Core 8.0 feature)
                entity.Property(p => p.Metadata)
                    .HasColumnType("nvarchar(max)")
                    .HasConversion(
                        v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
                        v => JsonSerializer.Deserialize<Dictionary<string, object>>(v, (JsonSerializerOptions?)null)
                    );

                // Relationship with Category
                entity.HasOne(p => p.Category)
                    .WithMany()
                    .HasForeignKey(p => p.CategoryId)
                    .IsRequired(false)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            // Configure the Category entity
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Name).IsRequired().HasMaxLength(50);
            });
        }
    }

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int StockLevel { get; set; }
        public int? CategoryId { get; set; }
        public Category? Category { get; set; }
        public Dictionary<string, object>? Metadata { get; set; }
    }

    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
