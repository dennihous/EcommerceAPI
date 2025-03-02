using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EcommerceAPI.DTOs
{
    public class OrderDTO
    {
        public int OrderId { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        [Range(0.01, 100000)]
        public decimal TotalAmount { get; set; }

        public int CustomerId { get; set; } // Foreign key to Customer
        public List<OrderItemDTO> OrderItems { get; set; } // List of OrderItemDTOs
    }
}