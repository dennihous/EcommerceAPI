using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EcommerceAPI.Models;
using EcommerceAPI.Services;
using EcommerceAPI.DTOs;
using Microsoft.Extensions.Logging;

namespace EcommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IProductService productService, ILogger<ProductsController> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProducts()
        {
            _logger.LogInformation("Fetching all products.");
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetProduct(int id)
        {
            _logger.LogInformation($"Fetching product with ID {id}.");
            var product = await _productService.GetProductByIdAsync(id);

            if (product == null)
            {
                _logger.LogWarning($"Product with ID {id} not found.");
                return NotFound();
            }

            return Ok(product);
        }

        // POST: api/Products
        [HttpPost]
        public async Task<ActionResult<ProductDTO>> PostProduct(ProductDTO productDTO)
        {
            _logger.LogInformation("Creating a new product.");
            await _productService.AddProductAsync(productDTO);
            return CreatedAtAction("GetProduct", new { id = productDTO.ProductId }, productDTO);
        }

        // PUT: api/Products/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, ProductDTO productDTO)
        {
            _logger.LogInformation($"Updating product with ID {id}.");
            if (id != productDTO.ProductId)
            {
                _logger.LogWarning("Product ID mismatch.");
                return BadRequest();
            }

            try
            {
                await _productService.UpdateProductAsync(id, productDTO);
            }
            catch (KeyNotFoundException)
            {
                _logger.LogWarning($"Product with ID {id} not found for update.");
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            _logger.LogInformation($"Deleting product with ID {id}.");
            try
            {
                await _productService.DeleteProductAsync(id);
            }
            catch (KeyNotFoundException)
            {
                _logger.LogWarning($"Product with ID {id} not found for deletion.");
                return NotFound();
            }

            return NoContent();
        }
    }
}