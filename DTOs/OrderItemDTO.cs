using System.ComponentModel.DataAnnotations;

namespace EcommerceAPI.DTOs
{
    public class OrderItemDTO
    {
        public int OrderItemId { get; set; }

        [Required]
        public int ProductId { get; set; } // Foreign key to Product

        [Required]
        [Range(1, 100)]
        public int Quantity { get; set; }

        [Required]
        [Range(0.01, 10000)]
        public decimal UnitPrice { get; set; }
    }
}