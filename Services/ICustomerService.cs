using System.Collections.Generic;
using System.Threading.Tasks;
using EcommerceAPI.DTOs;

namespace EcommerceAPI.Services
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerDTO>> GetAllCustomersAsync();
        Task<CustomerDTO> GetCustomerByIdAsync(int id);
        Task AddCustomerAsync(CustomerDTO customerDTO);
        Task UpdateCustomerAsync(int id, CustomerDTO customerDTO);
        Task DeleteCustomerAsync(int id);
    }
}