using System;
using System.Collections.Generic;
using System.Linq;
using CustomerService.Contracts;

namespace CustomerService.Data
{
    public class CustomerRepository
    {
        private static List<Customer> _customers;
        private static int _nextId = 1;

        static CustomerRepository()
        {
            InitializeSampleData();
        }

        private static void InitializeSampleData()
        {
            _customers = new List<Customer>
            {
                new Customer
                {
                    CustomerId = _nextId++,
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "john.doe@email.com",
                    Phone = "+1-555-0123",
                    DateCreated = DateTime.Now.AddMonths(-6),
                    IsActive = true,
                    Address = new Address
                    {
                        Street = "123 Main St",
                        City = "New York",
                        State = "NY",
                        ZipCode = "10001",
                        Country = "USA"
                    }
                },
                new Customer
                {
                    CustomerId = _nextId++,
                    FirstName = "Jane",
                    LastName = "Smith",
                    Email = "jane.smith@email.com",
                    Phone = "+1-555-0456",
                    DateCreated = DateTime.Now.AddMonths(-3),
                    IsActive = true,
                    Address = new Address
                    {
                        Street = "456 Oak Ave",
                        City = "Los Angeles",
                        State = "CA",
                        ZipCode = "90210",
                        Country = "USA"
                    }
                },
                new Customer
                {
                    CustomerId = _nextId++,
                    FirstName = "Robert",
                    LastName = "Johnson",
                    Email = "robert.johnson@email.com",
                    Phone = "+1-555-0789",
                    DateCreated = DateTime.Now.AddMonths(-1),
                    IsActive = true,
                    Address = new Address
                    {
                        Street = "789 Pine St",
                        City = "Chicago",
                        State = "IL",
                        ZipCode = "60601",
                        Country = "USA"
                    }
                },
                new Customer
                {
                    CustomerId = _nextId++,
                    FirstName = "Emily",
                    LastName = "Brown",
                    Email = "emily.brown@email.com",
                    Phone = "+1-555-0321",
                    DateCreated = DateTime.Now.AddDays(-15),
                    IsActive = true,
                    Address = new Address
                    {
                        Street = "321 Elm St",
                        City = "Houston",
                        State = "TX",
                        ZipCode = "77001",
                        Country = "USA"
                    }
                },
                new Customer
                {
                    CustomerId = _nextId++,
                    FirstName = "Michael",
                    LastName = "Davis",
                    Email = "michael.davis@email.com",
                    Phone = "+1-555-0654",
                    DateCreated = DateTime.Now.AddDays(-7),
                    IsActive = false,
                    Address = new Address
                    {
                        Street = "654 Cedar Ave",
                        City = "Phoenix",
                        State = "AZ",
                        ZipCode = "85001",
                        Country = "USA"
                    }
                }
            };
        }

        public List<Customer> GetAllCustomers()
        {
            return _customers.ToList();
        }

        public Customer GetCustomer(int customerId)
        {
            return _customers.FirstOrDefault(c => c.CustomerId == customerId);
        }

        public List<Customer> GetCustomersByCity(string city)
        {
            return _customers.Where(c => c.Address.City.Equals(city, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public int CreateCustomer(Customer customer)
        {
            if (customer == null) return -1;
            
            customer.CustomerId = _nextId++;
            customer.DateCreated = DateTime.Now;
            _customers.Add(customer);
            return customer.CustomerId;
        }

        public bool UpdateCustomer(Customer customer)
        {
            if (customer == null) return false;
            
            var existingCustomer = _customers.FirstOrDefault(c => c.CustomerId == customer.CustomerId);
            if (existingCustomer == null) return false;

            existingCustomer.FirstName = customer.FirstName;
            existingCustomer.LastName = customer.LastName;
            existingCustomer.Email = customer.Email;
            existingCustomer.Phone = customer.Phone;
            existingCustomer.IsActive = customer.IsActive;
            existingCustomer.Address = customer.Address;
            
            return true;
        }

        public bool DeleteCustomer(int customerId)
        {
            var customer = _customers.FirstOrDefault(c => c.CustomerId == customerId);
            if (customer == null) return false;
            
            return _customers.Remove(customer);
        }

        public List<Customer> SearchCustomers(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm)) return new List<Customer>();
            
            searchTerm = searchTerm.ToLower();
            return _customers.Where(c => 
                c.FirstName.ToLower().Contains(searchTerm) ||
                c.LastName.ToLower().Contains(searchTerm) ||
                c.Email.ToLower().Contains(searchTerm) ||
                c.Address.City.ToLower().Contains(searchTerm) ||
                c.Address.State.ToLower().Contains(searchTerm)
            ).ToList();
        }

        public int GetCustomerCount()
        {
            return _customers.Count;
        }
    }
}