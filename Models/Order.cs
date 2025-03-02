using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceAPI.Models
{
    public class Order
    {
        public int OrderId { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal TotalAmount { get; set; }

        // Foreign key to Customer
        public int CustomerId { get; set; }

        // Navigation property to Customer
        public Customer Customer { get; set; }

        // Navigation property to OrderItems
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}