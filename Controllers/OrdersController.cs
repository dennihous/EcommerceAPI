using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EcommerceAPI.Models;
using EcommerceAPI.DTOs;
using EcommerceAPI.Services;
using Microsoft.Extensions.Logging;

namespace EcommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly ILogger<OrdersController> _logger;

        public OrdersController(IOrderService orderService, ILogger<OrdersController> logger)
        {
            _orderService = orderService;
            _logger = logger;
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetOrders()
        {
            _logger.LogInformation("Fetching all orders.");
            var orders = await _orderService.GetAllOrdersAsync();
            return Ok(orders);
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDTO>> GetOrder(int id)
        {
            _logger.LogInformation($"Fetching order with ID {id}.");
            var order = await _orderService.GetOrderByIdAsync(id);

            if (order == null)
            {
                _logger.LogWarning($"Order with ID {id} not found.");
                return NotFound();
            }

            return Ok(order);
        }

        // POST: api/Orders
        [HttpPost]
        public async Task<ActionResult<OrderDTO>> PostOrder(OrderDTO orderDTO)
        {
            _logger.LogInformation("Creating a new order.");
            await _orderService.AddOrderAsync(orderDTO);
            return CreatedAtAction("GetOrder", new { id = orderDTO.OrderId }, orderDTO);
        }

        // PUT: api/Orders/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, OrderDTO orderDTO)
        {
            _logger.LogInformation($"Updating order with ID {id}.");
            if (id != orderDTO.OrderId)
            {
                _logger.LogWarning("Order ID mismatch.");
                return BadRequest();
            }

            try
            {
                await _orderService.UpdateOrderAsync(id, orderDTO);
            }
            catch (KeyNotFoundException)
            {
                _logger.LogWarning($"Order with ID {id} not found for update.");
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            _logger.LogInformation($"Deleting order with ID {id}.");
            try
            {
                await _orderService.DeleteOrderAsync(id);
            }
            catch (KeyNotFoundException)
            {
                _logger.LogWarning($"Order with ID {id} not found for deletion.");
                return NotFound();
            }

            return NoContent();
        }
    }
}