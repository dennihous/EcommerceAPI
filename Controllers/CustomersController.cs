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
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly ILogger<CustomersController> _logger;

        public CustomersController(ICustomerService customerService, ILogger<CustomersController> logger)
        {
            _customerService = customerService;
            _logger = logger;
        }

        // GET: api/Customers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDTO>>> GetCustomers()
        {
            _logger.LogInformation("Fetching all customers.");
            var customers = await _customerService.GetAllCustomersAsync();
            return Ok(customers);
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDTO>> GetCustomer(int id)
        {
            _logger.LogInformation($"Fetching customer with ID {id}.");
            var customer = await _customerService.GetCustomerByIdAsync(id);

            if (customer == null)
            {
                _logger.LogWarning($"Customer with ID {id} not found.");
                return NotFound();
            }

            return Ok(customer);
        }

        // POST: api/Customers
        [HttpPost]
        public async Task<ActionResult<CustomerDTO>> PostCustomer(CustomerDTO customerDTO)
        {
            _logger.LogInformation("Creating a new customer.");
            await _customerService.AddCustomerAsync(customerDTO);
            return CreatedAtAction("GetCustomer", new { id = customerDTO.CustomerId }, customerDTO);
        }

        // PUT: api/Customers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(int id, CustomerDTO customerDTO)
        {
            _logger.LogInformation($"Updating customer with ID {id}.");
            if (id != customerDTO.CustomerId)
            {
                _logger.LogWarning("Customer ID mismatch.");
                return BadRequest();
            }

            try
            {
                await _customerService.UpdateCustomerAsync(id, customerDTO);
            }
            catch (KeyNotFoundException)
            {
                _logger.LogWarning($"Customer with ID {id} not found for update.");
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            _logger.LogInformation($"Deleting customer with ID {id}.");
            try
            {
                await _customerService.DeleteCustomerAsync(id);
            }
            catch (KeyNotFoundException)
            {
                _logger.LogWarning($"Customer with ID {id} not found for deletion.");
                return NotFound();
            }

            return NoContent();
        }
    }
}