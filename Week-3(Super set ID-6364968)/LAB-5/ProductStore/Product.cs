using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductStore
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string Name { get; set; } = string.Empty;
        
        [Column(TypeName = "nvarchar(200)")]
        public string? Description { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        
        public bool IsDiscontinued { get; set; } = false;
        
        [Column(TypeName = "datetime2")]
        public DateTime LastUpdated { get; set; }
    }
}
