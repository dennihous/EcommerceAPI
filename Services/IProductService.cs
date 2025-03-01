using System.Collections.Generic;
using System.Threading.Tasks;
using EcommerceAPI.DTOs;

namespace EcommerceAPI.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GetAllProductsAsync();
        Task<ProductDTO> GetProductByIdAsync(int id);
        Task AddProductAsync(ProductDTO productDTO);
        Task UpdateProductAsync(int id, ProductDTO productDTO);
        Task DeleteProductAsync(int id);
    }
}