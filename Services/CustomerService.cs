using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcommerceAPI.Models;
using EcommerceAPI.DTOs;
using Microsoft.EntityFrameworkCore;

namespace EcommerceAPI.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly EcommerceContext _context;

        public CustomerService(EcommerceContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CustomerDTO>> GetAllCustomersAsync()
        {
            return await _context.Customers
                .Select(c => new CustomerDTO
                {
                    CustomerId = c.CustomerId,
                    Name = c.Name,
                    Email = c.Email
                })
                .ToListAsync();
        }

        public async Task<CustomerDTO> GetCustomerByIdAsync(int id)
        {
            var customer = await _context.Customers
                .Where(c => c.CustomerId == id)
                .Select(c => new CustomerDTO
                {
                    CustomerId = c.CustomerId,
                    Name = c.Name,
                    Email = c.Email
                })
                .FirstOrDefaultAsync();

            if (customer == null)
            {
                throw new KeyNotFoundException("Customer not found.");
            }

            return customer;
        }

        public async Task AddCustomerAsync(CustomerDTO customerDTO)
        {
            var customer = new Customer
            {
                Name = customerDTO.Name,
                Email = customerDTO.Email
            };

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCustomerAsync(int id, CustomerDTO customerDTO)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                throw new KeyNotFoundException("Customer not found.");
            }

            customer.Name = customerDTO.Name;
            customer.Email = customerDTO.Email;

            _context.Entry(customer).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCustomerAsync(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                throw new KeyNotFoundException("Customer not found.");
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
        }
    }
}