using System.ComponentModel.DataAnnotations;

namespace EcommerceAPI.DTOs
{
    public class ProductDTO
    {
        public int ProductId { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; }

        [Range(0.01, 10000)]
        public decimal Price { get; set; }

        [StringLength(500)]
        public string Description { get; set; }
    }
}