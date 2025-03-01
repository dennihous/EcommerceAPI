using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcommerceAPI.Models;
using EcommerceAPI.DTOs;
using Microsoft.EntityFrameworkCore;

namespace EcommerceAPI.Services
{
    public class ProductService : IProductService
    {
        private readonly EcommerceContext _context;

        public ProductService(EcommerceContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductDTO>> GetAllProductsAsync()
        {
            return await _context.Products
                .Select(p => new ProductDTO
                {
                    ProductId = p.ProductId,
                    Name = p.Name,
                    Price = p.Price,
                    Description = p.Description
                })
                .ToListAsync();
        }

        public async Task<ProductDTO> GetProductByIdAsync(int id)
        {
            var product = await _context.Products
                .Where(p => p.ProductId == id)
                .Select(p => new ProductDTO
                {
                    ProductId = p.ProductId,
                    Name = p.Name,
                    Price = p.Price,
                    Description = p.Description
                })
                .FirstOrDefaultAsync();

            return product;
        }

        public async Task AddProductAsync(ProductDTO productDTO)
        {
            var product = new Product
            {
                Name = productDTO.Name,
                Price = productDTO.Price,
                Description = productDTO.Description
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProductAsync(int id, ProductDTO productDTO)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                throw new KeyNotFoundException("Product not found.");
            }

            product.Name = productDTO.Name;
            product.Price = productDTO.Price;
            product.Description = productDTO.Description;

            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                throw new KeyNotFoundException("Product not found.");
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
    }
}