using Xunit;
using EcommerceAPI.Controllers;
using EcommerceAPI.Services;
using EcommerceAPI.DTOs;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace EcommerceAPI.Tests
{
    public class OrdersControllerTests
    {
        private readonly Mock<IOrderService> _mockOrderService;
        private readonly Mock<ILogger<OrdersController>> _mockLogger;
        private readonly OrdersController _controller;

        public OrdersControllerTests()
        {
            _mockOrderService = new Mock<IOrderService>();
            _mockLogger = new Mock<ILogger<OrdersController>>();
            _controller = new OrdersController(_mockOrderService.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task GetOrders_ReturnsListOfOrders()
        {
            var orders = new List<OrderDTO>
            {
                new OrderDTO { OrderId = 1, OrderDate = DateTime.UtcNow, TotalAmount = 100.00M },
                new OrderDTO { OrderId = 2, OrderDate = DateTime.UtcNow, TotalAmount = 200.00M }
            };
            _mockOrderService.Setup(service => service.GetAllOrdersAsync()).ReturnsAsync(orders);

            var result = await _controller.GetOrders();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedOrders = Assert.IsType<List<OrderDTO>>(okResult.Value);
            Assert.Equal(2, returnedOrders.Count);
        }

        [Fact]
        public async Task GetOrder_ReturnsOrder_WhenOrderExists()
        {
            var order = new OrderDTO { OrderId = 1, OrderDate = DateTime.UtcNow, TotalAmount = 100.00M };
            _mockOrderService.Setup(service => service.GetOrderByIdAsync(1)).ReturnsAsync(order);

            var result = await _controller.GetOrder(1);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedOrder = Assert.IsType<OrderDTO>(okResult.Value);
            Assert.Equal(1, returnedOrder.OrderId);
        }

        [Fact]
        public async Task GetOrder_ReturnsNotFound_WhenOrderDoesNotExist()
        {
            _mockOrderService.Setup(service => service.GetOrderByIdAsync(1)).ReturnsAsync((OrderDTO)null);

            var result = await _controller.GetOrder(1);

            Assert.IsType<NotFoundResult>(result.Result);
        }
    }
}