using System.Collections.Generic;

namespace EcommerceAPI.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }

        public List<OrderItem>? OrderItems { get; set; }
    }
}