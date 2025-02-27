using System.ComponentModel.DataAnnotations;

namespace EcommerceAPI.Models
{
    public class Review
    {
        public int ReviewId { get; set; }

        [Required]
        public string Comment { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; }

        // Foreign key to Product
        public int ProductId { get; set; }
        public Product Product { get; set; } // Navigation property to Product

        // Foreign key to Customer
        public int CustomerId { get; set; }
        public Customer Customer { get; set; } // Navigation property to Customer
    }
}