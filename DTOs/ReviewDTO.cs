using System.ComponentModel.DataAnnotations;

namespace EcommerceAPI.DTOs
{
    public class ReviewDTO
    {
        public int ReviewId { get; set; }

        [Required]
        [StringLength(500)]
        public string Comment { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; }

        public int ProductId { get; set; }
        public int CustomerId { get; set; }
    }
}