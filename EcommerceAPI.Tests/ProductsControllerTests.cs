using Xunit;
using EcommerceAPI.Controllers;
using EcommerceAPI.Services;
using EcommerceAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using EcommerceAPI.DTOs;
using Microsoft.Extensions.Logging;

namespace EcommerceAPI.Tests
{
    public class ProductsControllerTests
    {
        [Fact]
        public async Task GetProducts_ReturnsListOfProducts()
        {
            var mockService = new Mock<IProductService>();
            var mockLogger = new Mock<ILogger<ProductsController>>();

            mockService.Setup(service => service.GetAllProductsAsync())
                .ReturnsAsync(new List<ProductDTO>
                {
                    new ProductDTO { ProductId = 1, Name = "Laptop", Price = 1200.00M, Description = "A high-end gaming laptop" },
                    new ProductDTO { ProductId = 2, Name = "Smartphone", Price = 800.00M, Description = "A flagship smartphone" }
                });

            var controller = new ProductsController(mockService.Object, mockLogger.Object);

            var result = await controller.GetProducts();

            var actionResult = Assert.IsType<ActionResult<IEnumerable<ProductDTO>>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var products = Assert.IsType<List<ProductDTO>>(okResult.Value);
            Assert.Equal(2, products.Count);
        }
    }
}