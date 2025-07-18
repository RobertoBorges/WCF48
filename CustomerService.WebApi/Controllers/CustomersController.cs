using Microsoft.AspNetCore.Mvc;
using CustomerService.WebApi.Models;
using CustomerService.WebApi.Services;
using System.ComponentModel.DataAnnotations;

namespace CustomerService.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly ILogger<CustomersController> _logger;

        public CustomersController(ICustomerService customerService, ILogger<CustomersController> logger)
        {
            _customerService = customerService;
            _logger = logger;
        }

        /// <summary>
        /// Get all customers
        /// </summary>
        /// <returns>List of all customers</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetAllCustomers()
        {
            try
            {
                var customers = await _customerService.GetAllCustomersAsync();
                return Ok(customers);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all customers");
                return StatusCode(500, "An error occurred while retrieving customers");
            }
        }

        /// <summary>
        /// Get a customer by ID
        /// </summary>
        /// <param name="id">Customer ID</param>
        /// <returns>Customer details</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest("Customer ID must be greater than 0");

                var customer = await _customerService.GetCustomerAsync(id);
                if (customer == null)
                    return NotFound($"Customer with ID {id} not found");

                return Ok(customer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving customer {CustomerId}", id);
                return StatusCode(500, "An error occurred while retrieving the customer");
            }
        }

        /// <summary>
        /// Get customers by city
        /// </summary>
        /// <param name="city">City name</param>
        /// <returns>List of customers in the specified city</returns>
        [HttpGet("by-city/{city}")]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomersByCity(string city)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(city))
                    return BadRequest("City cannot be null or empty");

                var customers = await _customerService.GetCustomersByCityAsync(city);
                return Ok(customers);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving customers by city {City}", city);
                return StatusCode(500, "An error occurred while retrieving customers by city");
            }
        }

        /// <summary>
        /// Search customers by name, email, city, or state
        /// </summary>
        /// <param name="searchTerm">Search term</param>
        /// <returns>List of matching customers</returns>
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Customer>>> SearchCustomers([FromQuery] string searchTerm)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(searchTerm))
                    return BadRequest("Search term cannot be null or empty");

                var customers = await _customerService.SearchCustomersAsync(searchTerm);
                return Ok(customers);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error searching customers with term {SearchTerm}", searchTerm);
                return StatusCode(500, "An error occurred while searching customers");
            }
        }

        /// <summary>
        /// Get total customer count
        /// </summary>
        /// <returns>Total number of customers</returns>
        [HttpGet("count")]
        public async Task<ActionResult<int>> GetCustomerCount()
        {
            try
            {
                var count = await _customerService.GetCustomerCountAsync();
                return Ok(count);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving customer count");
                return StatusCode(500, "An error occurred while retrieving customer count");
            }
        }

        /// <summary>
        /// Create a new customer
        /// </summary>
        /// <param name="customer">Customer to create</param>
        /// <returns>Created customer with ID</returns>
        [HttpPost]
        public async Task<ActionResult<Customer>> CreateCustomer([FromBody] Customer customer)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var customerId = await _customerService.CreateCustomerAsync(customer);
                customer.CustomerId = customerId;

                return CreatedAtAction(nameof(GetCustomer), new { id = customerId }, customer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating customer");
                return StatusCode(500, "An error occurred while creating the customer");
            }
        }

        /// <summary>
        /// Update an existing customer
        /// </summary>
        /// <param name="id">Customer ID</param>
        /// <param name="customer">Updated customer data</param>
        /// <returns>Updated customer</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<Customer>> UpdateCustomer(int id, [FromBody] Customer customer)
        {
            try
            {
                if (id <= 0)
                    return BadRequest("Customer ID must be greater than 0");

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (id != customer.CustomerId)
                    return BadRequest("Customer ID in URL does not match customer ID in body");

                var success = await _customerService.UpdateCustomerAsync(customer);
                if (!success)
                    return NotFound($"Customer with ID {id} not found");

                return Ok(customer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating customer {CustomerId}", id);
                return StatusCode(500, "An error occurred while updating the customer");
            }
        }

        /// <summary>
        /// Delete a customer
        /// </summary>
        /// <param name="id">Customer ID</param>
        /// <returns>Success status</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCustomer(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest("Customer ID must be greater than 0");

                var success = await _customerService.DeleteCustomerAsync(id);
                if (!success)
                    return NotFound($"Customer with ID {id} not found");

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting customer {CustomerId}", id);
                return StatusCode(500, "An error occurred while deleting the customer");
            }
        }
    }
}