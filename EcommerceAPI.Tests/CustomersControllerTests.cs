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
    public class CustomersControllerTests
    {
        private readonly Mock<ICustomerService> _mockCustomerService;
        private readonly Mock<ILogger<CustomersController>> _mockLogger;
        private readonly CustomersController _controller;

        public CustomersControllerTests()
        {
            _mockCustomerService = new Mock<ICustomerService>();
            _mockLogger = new Mock<ILogger<CustomersController>>();
            _controller = new CustomersController(_mockCustomerService.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task GetCustomers_ReturnsListOfCustomers()
        {
            var customers = new List<CustomerDTO>
            {
                new CustomerDTO { CustomerId = 1, Name = "John Doe", Email = "john.doe@example.com" },
                new CustomerDTO { CustomerId = 2, Name = "Jane Smith", Email = "jane.smith@example.com" }
            };
            _mockCustomerService.Setup(service => service.GetAllCustomersAsync()).ReturnsAsync(customers);

            var result = await _controller.GetCustomers();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedCustomers = Assert.IsType<List<CustomerDTO>>(okResult.Value);
            Assert.Equal(2, returnedCustomers.Count);
        }

        [Fact]
        public async Task GetCustomer_ReturnsCustomer_WhenCustomerExists()
        {
            var customer = new CustomerDTO { CustomerId = 1, Name = "John Doe", Email = "john.doe@example.com" };
            _mockCustomerService.Setup(service => service.GetCustomerByIdAsync(1)).ReturnsAsync(customer);

            var result = await _controller.GetCustomer(1);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedCustomer = Assert.IsType<CustomerDTO>(okResult.Value);
            Assert.Equal(1, returnedCustomer.CustomerId);
        }

        [Fact]
        public async Task GetCustomer_ReturnsNotFound_WhenCustomerDoesNotExist()
        {
            _mockCustomerService.Setup(service => service.GetCustomerByIdAsync(1)).ReturnsAsync((CustomerDTO)null);

            var result = await _controller.GetCustomer(1);

            Assert.IsType<NotFoundResult>(result.Result);
        }
    }
}