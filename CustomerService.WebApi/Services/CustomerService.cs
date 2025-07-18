using CustomerService.WebApi.Models;

namespace CustomerService.WebApi.Services
{
    public interface ICustomerService
    {
        Task<Customer?> GetCustomerAsync(int customerId);
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
        Task<IEnumerable<Customer>> GetCustomersByCityAsync(string city);
        Task<int> CreateCustomerAsync(Customer customer);
        Task<bool> UpdateCustomerAsync(Customer customer);
        Task<bool> DeleteCustomerAsync(int customerId);
        Task<IEnumerable<Customer>> SearchCustomersAsync(string searchTerm);
        Task<int> GetCustomerCountAsync();
    }

    public class CustomerService : ICustomerService
    {
        private static List<Customer> _customers = new();
        private static int _nextId = 1;
        private static readonly object _lock = new object();

        static CustomerService()
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

        public Task<Customer?> GetCustomerAsync(int customerId)
        {
            lock (_lock)
            {
                var customer = _customers.FirstOrDefault(c => c.CustomerId == customerId);
                return Task.FromResult(customer);
            }
        }

        public Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            lock (_lock)
            {
                return Task.FromResult<IEnumerable<Customer>>(_customers.ToList());
            }
        }

        public Task<IEnumerable<Customer>> GetCustomersByCityAsync(string city)
        {
            lock (_lock)
            {
                var customers = _customers.Where(c => c.Address.City.Equals(city, StringComparison.OrdinalIgnoreCase)).ToList();
                return Task.FromResult<IEnumerable<Customer>>(customers);
            }
        }

        public Task<int> CreateCustomerAsync(Customer customer)
        {
            lock (_lock)
            {
                customer.CustomerId = _nextId++;
                customer.DateCreated = DateTime.Now;
                _customers.Add(customer);
                return Task.FromResult(customer.CustomerId);
            }
        }

        public Task<bool> UpdateCustomerAsync(Customer customer)
        {
            lock (_lock)
            {
                var existingCustomer = _customers.FirstOrDefault(c => c.CustomerId == customer.CustomerId);
                if (existingCustomer == null) return Task.FromResult(false);

                existingCustomer.FirstName = customer.FirstName;
                existingCustomer.LastName = customer.LastName;
                existingCustomer.Email = customer.Email;
                existingCustomer.Phone = customer.Phone;
                existingCustomer.IsActive = customer.IsActive;
                existingCustomer.Address = customer.Address;

                return Task.FromResult(true);
            }
        }

        public Task<bool> DeleteCustomerAsync(int customerId)
        {
            lock (_lock)
            {
                var customer = _customers.FirstOrDefault(c => c.CustomerId == customerId);
                if (customer == null) return Task.FromResult(false);

                return Task.FromResult(_customers.Remove(customer));
            }
        }

        public Task<IEnumerable<Customer>> SearchCustomersAsync(string searchTerm)
        {
            lock (_lock)
            {
                if (string.IsNullOrWhiteSpace(searchTerm)) 
                    return Task.FromResult<IEnumerable<Customer>>(new List<Customer>());

                searchTerm = searchTerm.ToLower();
                var customers = _customers.Where(c =>
                    c.FirstName.ToLower().Contains(searchTerm) ||
                    c.LastName.ToLower().Contains(searchTerm) ||
                    c.Email.ToLower().Contains(searchTerm) ||
                    c.Address.City.ToLower().Contains(searchTerm) ||
                    c.Address.State.ToLower().Contains(searchTerm)
                ).ToList();

                return Task.FromResult<IEnumerable<Customer>>(customers);
            }
        }

        public Task<int> GetCustomerCountAsync()
        {
            lock (_lock)
            {
                return Task.FromResult(_customers.Count);
            }
        }
    }
}