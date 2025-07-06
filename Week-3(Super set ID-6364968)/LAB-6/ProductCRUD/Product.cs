using System;
using System.ComponentModel.DataAnnotations;

namespace ProductCRUD
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; } = string.Empty;
        
        public string? Description { get; set; }
        
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        
        public bool IsDiscontinued { get; set; }
        
        public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
    }
}
