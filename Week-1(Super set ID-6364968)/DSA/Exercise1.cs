using System;
using System.Collections.Generic;
using System.Linq;

namespace InventoryManagementSystem
{
    // Step 1: Understanding the Problem
    /*
     * Data structures and algorithms are crucial for inventory management because:
     * 1. They enable efficient storage and retrieval of large amounts of product data
     * 2. They optimize operations like searching, sorting, and updating inventory
     * 3. They help maintain data integrity and organization
     * 
     * Suitable data structures:
     * - Dictionary/HashMap: O(1) average case for lookup, insert, delete
     * - List/ArrayList: O(n) for search, O(1) for access by index
     * - SortedDictionary: Maintains sorted order with O(log n) operations
     */

    // Step 2: Product Class Definition
    public class Product
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public DateTime LastUpdated { get; set; }

        public Product(string id, string name, int quantity, decimal price, string category)
        {
            ProductId = id;
            ProductName = name;
            Quantity = quantity;
            Price = price;
            Category = category;
            LastUpdated = DateTime.Now;
        }

        public override string ToString()
        {
            return $"{ProductId,-10} {ProductName,-20} {Quantity,8} {Price,10:C} {Category,-15} {LastUpdated:yyyy-MM-dd}";
        }
    }

    // Step 3: Inventory Management System
    public class InventoryManager
    {
        // Using Dictionary for O(1) average case operations
        private Dictionary<string, Product> products;

        public InventoryManager()
        {
            products = new Dictionary<string, Product>();
        }

        // Add product with O(1) average time complexity
        public bool AddProduct(Product product)
        {
            if (products.ContainsKey(product.ProductId))
            {
                return false; // Product already exists
            }
            products.Add(product.ProductId, product);
            return true;
        }

        // Update product with O(1) average time complexity
        public bool UpdateProduct(string productId, string name = null, int? quantity = null, decimal? price = null, string category = null)
        {
            if (!products.ContainsKey(productId))
            {
                return false;
            }

            var product = products[productId];
            if (name != null) product.ProductName = name;
            if (quantity.HasValue) product.Quantity = quantity.Value;
            if (price.HasValue) product.Price = price.Value;
            if (category != null) product.Category = category;
            product.LastUpdated = DateTime.Now;

            return true;
        }

        // Delete product with O(1) average time complexity
        public bool DeleteProduct(string productId)
        {
            return products.Remove(productId);
        }

        // Get product with O(1) average time complexity
        public Product GetProduct(string productId)
        {
            return products.TryGetValue(productId, out var product) ? product : null;
        }

        // Get all products with O(n) time complexity
        public List<Product> GetAllProducts()
        {
            return products.Values.ToList();
        }

        // Search products by name with O(n) time complexity
        public List<Product> SearchByName(string name)
        {
            return products.Values
                .Where(p => p.ProductName.Contains(name, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        // Get low stock products with O(n) time complexity
        public List<Product> GetLowStockProducts(int threshold = 10)
        {
            return products.Values.Where(p => p.Quantity < threshold).ToList();
        }

        // Get products by category with O(n) time complexity
        public Dictionary<string, List<Product>> GetProductsByCategory()
        {
            return products.Values
                .GroupBy(p => p.Category)
                .ToDictionary(g => g.Key, g => g.ToList());
        }
    }

    // Step 4: Analysis and Optimization
    /*
     * Time Complexity Analysis:
     * - AddProduct: O(1) average case (Dictionary.Add)
     * - UpdateProduct: O(1) average case (Dictionary access)
     * - DeleteProduct: O(1) average case (Dictionary.Remove)
     * - GetProduct: O(1) average case (Dictionary lookup)
     * - GetAllProducts: O(n) (converting to list)
     * - SearchByName: O(n) (linear search)
     * - GetLowStockProducts: O(n) (filtering)
     * 
     * Optimizations:
     * 1. Using Dictionary provides fast lookups by product ID
     * 2. For name searches, we could maintain a separate index (Dictionary<string, List<Product>>)
     * 3. For category grouping, we could maintain a pre-computed dictionary
     * 4. For large inventories, consider database storage with proper indexing
     */

    class Program
    {
        static void Main(string[] args)
        {
            var inventory = new InventoryManager();

            // Sample data
            inventory.AddProduct(new Product("P1001", "Laptop", 15, 999.99m, "Electronics"));
            inventory.AddProduct(new Product("P1002", "Smartphone", 8, 699.99m, "Electronics"));
            inventory.AddProduct(new Product("P2001", "Desk Chair", 25, 149.99m, "Furniture"));
            inventory.AddProduct(new Product("P3001", "Notebook", 5, 4.99m, "Stationery"));
            inventory.AddProduct(new Product("P3002", "Pen Set", 12, 9.99m, "Stationery"));

            Console.WriteLine("=== Inventory Management System ===");
            Console.WriteLine("1. View All Products");
            Console.WriteLine("2. Add New Product");
            Console.WriteLine("3. Update Product");
            Console.WriteLine("4. Delete Product");
            Console.WriteLine("5. View Low Stock Items");
            Console.WriteLine("6. Search Products");
            Console.WriteLine("7. Exit");

            while (true)
            {
                Console.Write("\nEnter your choice: ");
                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        DisplayProducts(inventory.GetAllProducts());
                        break;
                    case 2:
                        AddProductMenu(inventory);
                        break;
                    case 3:
                        UpdateProductMenu(inventory);
                        break;
                    case 4:
                        DeleteProductMenu(inventory);
                        break;
                    case 5:
                        DisplayProducts(inventory.GetLowStockProducts());
                        break;
                    case 6:
                        SearchProductsMenu(inventory);
                        break;
                    case 7:
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        static void DisplayProducts(List<Product> products)
        {
            if (products.Count == 0)
            {
                Console.WriteLine("No products found.");
                return;
            }

            Console.WriteLine("\nID         Name                 Quantity      Price       Category      Last Updated");
            Console.WriteLine("--------------------------------------------------------------------------");
            foreach (var product in products)
            {
                Console.WriteLine(product);
            }
        }

        static void AddProductMenu(InventoryManager inventory)
        {
            Console.WriteLine("\n=== Add New Product ===");
            Console.Write("Enter Product ID: ");
            string id = Console.ReadLine();
            Console.Write("Enter Product Name: ");
            string name = Console.ReadLine();
            Console.Write("Enter Quantity: ");
            int quantity = int.Parse(Console.ReadLine());
            Console.Write("Enter Price: ");
            decimal price = decimal.Parse(Console.ReadLine());
            Console.Write("Enter Category: ");
            string category = Console.ReadLine();

            if (inventory.AddProduct(new Product(id, name, quantity, price, category)))
            {
                Console.WriteLine("Product added successfully.");
            }
            else
            {
                Console.WriteLine("Product with this ID already exists.");
            }
        }

        static void UpdateProductMenu(InventoryManager inventory)
        {
            Console.WriteLine("\n=== Update Product ===");
            Console.Write("Enter Product ID to update: ");
            string id = Console.ReadLine();

            var product = inventory.GetProduct(id);
            if (product == null)
            {
                Console.WriteLine("Product not found.");
                return;
            }

            Console.WriteLine("Current product details:");
            Console.WriteLine(product);

            Console.Write("Enter new Name (leave blank to keep current): ");
            string name = Console.ReadLine();
            Console.Write("Enter new Quantity (leave blank to keep current): ");
            string quantityStr = Console.ReadLine();
            int? quantity = string.IsNullOrEmpty(quantityStr) ? null : (int?)int.Parse(quantityStr);
            Console.Write("Enter new Price (leave blank to keep current): ");
            string priceStr = Console.ReadLine();
            decimal? price = string.IsNullOrEmpty(priceStr) ? null : (decimal?)decimal.Parse(priceStr);
            Console.Write("Enter new Category (leave blank to keep current): ");
            string category = Console.ReadLine();

            if (inventory.UpdateProduct(id, name, quantity, price, category))
            {
                Console.WriteLine("Product updated successfully.");
            }
            else
            {
                Console.WriteLine("Failed to update product.");
            }
        }

        static void DeleteProductMenu(InventoryManager inventory)
        {
            Console.WriteLine("\n=== Delete Product ===");
            Console.Write("Enter Product ID to delete: ");
            string id = Console.ReadLine();

            if (inventory.DeleteProduct(id))
            {
                Console.WriteLine("Product deleted successfully.");
            }
            else
            {
                Console.WriteLine("Product not found.");
            }
        }

        static void SearchProductsMenu(InventoryManager inventory)
        {
            Console.WriteLine("\n=== Search Products ===");
            Console.Write("Enter product name to search: ");
            string name = Console.ReadLine();

            var results = inventory.SearchByName(name);
            DisplayProducts(results);
        }
    }
}
