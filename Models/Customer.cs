using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace EcommerceAPI.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        [JsonIgnore]
        public List<Order>? Orders { get; set; } = new List<Order>();

        public List<Review>? Reviews { get; set; }
    }
}