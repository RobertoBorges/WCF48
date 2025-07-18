using System;
using System.Collections.Generic;
using CustomerService.Contracts;
using CustomerService.Data;

namespace CustomerService.Implementation
{
    // Note: In .NET Core/.NET 8.0, server-side WCF hosting is not supported
    // This implementation shows the service contract structure for .NET Framework 4.8
    // For actual hosting in .NET Core, consider using gRPC or Web API
    public class CustomerService : ICustomerService
    {
        private readonly CustomerRepository _repository;

        public CustomerService()
        {
            _repository = new CustomerRepository();
        }

        public Customer GetCustomer(int customerId)
        {
            try
            {
                return _repository.GetCustomer(customerId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving customer: {ex.Message}");
            }
        }

        public List<Customer> GetAllCustomers()
        {
            try
            {
                return _repository.GetAllCustomers();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving customers: {ex.Message}");
            }
        }

        public List<Customer> GetCustomersByCity(string city)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(city))
                    throw new ArgumentException("City cannot be null or empty", nameof(city));
                
                return _repository.GetCustomersByCity(city);
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving customers by city: {ex.Message}");
            }
        }

        public int CreateCustomer(Customer customer)
        {
            try
            {
                if (customer == null)
                    throw new ArgumentNullException(nameof(customer), "Customer cannot be null");
                
                ValidateCustomer(customer);
                return _repository.CreateCustomer(customer);
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error creating customer: {ex.Message}");
            }
        }

        public bool UpdateCustomer(Customer customer)
        {
            try
            {
                if (customer == null)
                    throw new ArgumentNullException(nameof(customer), "Customer cannot be null");
                
                if (customer.CustomerId <= 0)
                    throw new ArgumentException("Customer ID must be greater than 0", nameof(customer));
                
                ValidateCustomer(customer);
                return _repository.UpdateCustomer(customer);
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating customer: {ex.Message}");
            }
        }

        public bool DeleteCustomer(int customerId)
        {
            try
            {
                if (customerId <= 0)
                    throw new ArgumentException("Customer ID must be greater than 0", nameof(customerId));
                
                return _repository.DeleteCustomer(customerId);
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting customer: {ex.Message}");
            }
        }

        public List<Customer> SearchCustomers(string searchTerm)
        {
            try
            {
                return _repository.SearchCustomers(searchTerm);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error searching customers: {ex.Message}");
            }
        }

        public int GetCustomerCount()
        {
            try
            {
                return _repository.GetCustomerCount();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting customer count: {ex.Message}");
            }
        }

        private void ValidateCustomer(Customer customer)
        {
            if (string.IsNullOrWhiteSpace(customer.FirstName))
                throw new ArgumentException("First name is required", nameof(customer));
            
            if (string.IsNullOrWhiteSpace(customer.LastName))
                throw new ArgumentException("Last name is required", nameof(customer));
            
            if (string.IsNullOrWhiteSpace(customer.Email))
                throw new ArgumentException("Email is required", nameof(customer));
            
            if (!IsValidEmail(customer.Email))
                throw new ArgumentException("Invalid email format", nameof(customer));
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}