using System.ComponentModel.DataAnnotations;

namespace EcommerceAPI.DTOs
{
    public class CustomerDTO
    {
        public int CustomerId { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}