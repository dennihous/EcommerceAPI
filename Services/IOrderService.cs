using System.Collections.Generic;
using System.Threading.Tasks;
using EcommerceAPI.DTOs;

namespace EcommerceAPI.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDTO>> GetAllOrdersAsync();
        Task<OrderDTO> GetOrderByIdAsync(int id);
        Task AddOrderAsync(OrderDTO orderDTO);
        Task UpdateOrderAsync(int id, OrderDTO orderDTO);
        Task DeleteOrderAsync(int id);
    }
}