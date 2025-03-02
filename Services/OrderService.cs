using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcommerceAPI.Models;
using EcommerceAPI.DTOs;
using Microsoft.EntityFrameworkCore;

namespace EcommerceAPI.Services
{
    public class OrderService : IOrderService
    {
        private readonly EcommerceContext _context;

        public OrderService(EcommerceContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<OrderDTO>> GetAllOrdersAsync()
        {
            return await _context.Orders
                .Include(o => o.OrderItems) // Include OrderItems
                .Select(o => new OrderDTO
                {
                    OrderId = o.OrderId,
                    OrderDate = o.OrderDate,
                    TotalAmount = o.TotalAmount,
                    CustomerId = o.CustomerId,
                    OrderItems = o.OrderItems.Select(oi => new OrderItemDTO
                    {
                        OrderItemId = oi.OrderItemId,
                        ProductId = oi.ProductId,
                        Quantity = oi.Quantity,
                        UnitPrice = oi.Product.Price // Assuming UnitPrice is the product price
                    }).ToList()
                })
                .ToListAsync();
        }

        public async Task<OrderDTO> GetOrderByIdAsync(int id)
        {
            var order = await _context.Orders
                .Include(o => o.OrderItems) // Include OrderItems
                .Where(o => o.OrderId == id)
                .Select(o => new OrderDTO
                {
                    OrderId = o.OrderId,
                    OrderDate = o.OrderDate,
                    TotalAmount = o.TotalAmount,
                    CustomerId = o.CustomerId,
                    OrderItems = o.OrderItems.Select(oi => new OrderItemDTO
                    {
                        OrderItemId = oi.OrderItemId,
                        ProductId = oi.ProductId,
                        Quantity = oi.Quantity,
                        UnitPrice = oi.Product.Price // Assuming UnitPrice is the product price
                    }).ToList()
                })
                .FirstOrDefaultAsync();

            if (order == null)
            {
                throw new KeyNotFoundException("Order not found.");
            }

            return order;
        }

        public async Task AddOrderAsync(OrderDTO orderDTO)
        {
            var order = new Order
            {
                OrderDate = orderDTO.OrderDate,
                TotalAmount = orderDTO.TotalAmount,
                CustomerId = orderDTO.CustomerId,
                OrderItems = orderDTO.OrderItems.Select(oi => new OrderItem
                {
                    ProductId = oi.ProductId,
                    Quantity = oi.Quantity,
                    UnitPrice = oi.UnitPrice
                }).ToList()
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateOrderAsync(int id, OrderDTO orderDTO)
        {
            var order = await _context.Orders
                .Include(o => o.OrderItems) // Include OrderItems
                .FirstOrDefaultAsync(o => o.OrderId == id);

            if (order == null)
            {
                throw new KeyNotFoundException("Order not found.");
            }

            order.OrderDate = orderDTO.OrderDate;
            order.TotalAmount = orderDTO.TotalAmount;
            order.CustomerId = orderDTO.CustomerId;

            // Update OrderItems
            order.OrderItems.Clear();
            foreach (var oiDto in orderDTO.OrderItems)
            {
                order.OrderItems.Add(new OrderItem
                {
                    ProductId = oiDto.ProductId,
                    Quantity = oiDto.Quantity,
                    UnitPrice = oiDto.UnitPrice
                });
            }

            _context.Entry(order).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteOrderAsync(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                throw new KeyNotFoundException("Order not found.");
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
        }
    }
}