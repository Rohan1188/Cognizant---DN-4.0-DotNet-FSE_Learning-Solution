using System;
using System.Diagnostics;
using System.Linq;

class Program
{
    // Step 1: Understanding Asymptotic Notation
    /*
     * Big O Notation:
     * - Describes the upper bound of an algorithm's time complexity
     * - Helps analyze how runtime scales with input size
     * - Focuses on worst-case scenario for conservative analysis
     * 
     * Search Scenarios:
     * - Best case (Ω): Element is found immediately (O(1) for both)
     * - Average case (Θ): Element is found in the middle (O(n) for linear, O(log n) for binary)
     * - Worst case (O): Element not found (O(n) for linear, O(log n) for binary)
     */
     
    // Step 2: Product class setup
    class Product : IComparable<Product>
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }

        public Product(int id, string name, string category, decimal price)
        {
            ProductId = id;
            ProductName = name;
            Category = category;
            Price = price;
        }

        public int CompareTo(Product other)
        {
            return ProductId.CompareTo(other.ProductId);
        }

        public override string ToString()
        {
            return $"ID: {ProductId}, Name: {ProductName}, Category: {Category}, Price: {Price:C}";
        }
    }

    // Step 3: Search implementations
    static Product LinearSearch(Product[] products, int productId)
    {
        foreach (var product in products)
        {
            if (product.ProductId == productId)
                return product;
        }
        return null;
    }

    static Product BinarySearch(Product[] sortedProducts, int productId)
    {
        int left = 0;
        int right = sortedProducts.Length - 1;

        while (left <= right)
        {
            int mid = left + (right - left) / 2;

            if (sortedProducts[mid].ProductId == productId)
                return sortedProducts[mid];

            if (sortedProducts[mid].ProductId < productId)
                left = mid + 1;
            else
                right = mid - 1;
        }

        return null;
    }

    // Helper method to generate random products
    static Product[] GenerateProducts(int count)
    {
        var random = new Random();
        var categories = new[] { "Electronics", "Clothing", "Home", "Books", "Toys" };
        var products = new Product[count];

        for (int i = 0; i < count; i++)
        {
            products[i] = new Product(
                i + 1, 
                $"Product {i + 1}", 
                categories[random.Next(categories.Length)], 
                (decimal)(random.NextDouble() * 100)
            );
        }

        return products;
    }

    static void Main(string[] args)
    {
        // Create and sort product arrays
        int productCount = 10000;
        Product[] products = GenerateProducts(productCount);
        Product[] sortedProducts = products.OrderBy(p => p.ProductId).ToArray();

        Console.WriteLine($"Generated {productCount} products for testing search algorithms");
        Console.WriteLine();

        // Test searching for existing and non-existing products
        int existingId = productCount / 2;
        int nonExistingId = productCount + 1;

        // Measure linear search performance
        var stopwatch = Stopwatch.StartNew();
        var result = LinearSearch(products, existingId);
        stopwatch.Stop();
        Console.WriteLine($"Linear search (found): {result}");
        Console.WriteLine($"Time taken: {stopwatch.ElapsedTicks} ticks");

        stopwatch.Restart();
        LinearSearch(products, nonExistingId);
        stopwatch.Stop();
        Console.WriteLine($"Linear search (not found) Time taken: {stopwatch.ElapsedTicks} ticks");
        Console.WriteLine();

        // Measure binary search performance
        stopwatch.Restart();
        result = BinarySearch(sortedProducts, existingId);
        stopwatch.Stop();
        Console.WriteLine($"Binary search (found): {result}");
        Console.WriteLine($"Time taken: {stopwatch.ElapsedTicks} ticks");

        stopwatch.Restart();
        BinarySearch(sortedProducts, nonExistingId);
        stopwatch.Stop();
        Console.WriteLine($"Binary search (not found) Time taken: {stopwatch.ElapsedTicks} ticks");
        Console.WriteLine();

        // Step 4: Analysis comparison
        Console.WriteLine("Algorithm Analysis:");
        Console.WriteLine("-------------------");
        Console.WriteLine("Linear Search:");
        Console.WriteLine("- Time Complexity: O(n) - scales linearly with input size");
        Console.WriteLine("- Best for: Unsorted data, small datasets, simple implementation");
        Console.WriteLine("- Advantages: Simple to implement, no sorting required");
        Console.WriteLine();

        Console.WriteLine("Binary Search:");
        Console.WriteLine("- Time Complexity: O(log n) - logarithmic scaling");
        Console.WriteLine("- Best for: Large sorted datasets");
        Console.WriteLine("- Advantages: Much faster for large datasets");
        Console.WriteLine("- Requirements: Data must be sorted (O(n log n) sorting cost)");
        Console.WriteLine();

        Console.WriteLine("Recommendation:");
        Console.WriteLine("For an e-commerce platform with thousands of products that change infrequently:");
        Console.WriteLine("- Sort products by ID during product updates");
        Console.WriteLine("- Use binary search for product lookups");
        Console.WriteLine("- Maintain both approaches for different use cases");
    }
}
