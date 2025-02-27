using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace EcommerceAPI.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }

        [JsonIgnore]
        public List<OrderItem>? OrderItems { get; set; }

        public List<Review>? Reviews { get; set; } // Add this line
    }
}