using System.Collections.Generic;

namespace RetailStore.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string? Name { get; set; } // Make Name nullable
        public List<Product> Products { get; set; } = new List<Product>();
    }

    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; } // Make Name nullable
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; } // Make Category nullable
    }
}
