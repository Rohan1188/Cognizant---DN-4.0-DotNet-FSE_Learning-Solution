using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductStore
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; } = string.Empty;  // Initialize with empty string
        
        public string? Description { get; set; }  // Make nullable
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        
        public int CategoryId { get; set; }
        
        public DateTime CreatedDate { get; set; }
        
        public bool InStock { get; set; }
    }
}
