using System;
using System.Linq;
using CustomerService.Implementation;
using CustomerService.Contracts;

namespace CustomerService.Host
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("WCF Customer Service Demo - .NET Framework 4.8");
                Console.WriteLine("================================================");
                Console.WriteLine();
                Console.WriteLine("Note: This demo shows the WCF service structure and data contracts.");
                Console.WriteLine("In .NET Framework 4.8, this would be hosted as a WCF service.");
                Console.WriteLine("In .NET Core/.NET 8.0, consider using gRPC or Web API for hosting.");
                Console.WriteLine();
                
                // Display some sample data information
                DisplaySampleDataInfo();
                
                Console.WriteLine();
                Console.WriteLine("Available Operations:");
                Console.WriteLine("- GetCustomer(int customerId)");
                Console.WriteLine("- GetAllCustomers()");
                Console.WriteLine("- GetCustomersByCity(string city)");
                Console.WriteLine("- CreateCustomer(Customer customer)");
                Console.WriteLine("- UpdateCustomer(Customer customer)");
                Console.WriteLine("- DeleteCustomer(int customerId)");
                Console.WriteLine("- SearchCustomers(string searchTerm)");
                Console.WriteLine("- GetCustomerCount()");
                Console.WriteLine();
                
                Console.WriteLine("Press 'T' to test the service operations, or any other key to exit...");
                var key = Console.ReadKey().KeyChar;
                
                if (key == 't' || key == 'T')
                {
                    TestService();
                    Console.WriteLine();
                    Console.WriteLine("Press any key to exit...");
                    Console.ReadKey();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }
                
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }
        }
        
        private static void DisplaySampleDataInfo()
        {
            try
            {
                var service = new CustomerService.Implementation.CustomerService();
                var customers = service.GetAllCustomers();
                
                Console.WriteLine($"Sample Data Loaded: {customers.Count} customers");
                Console.WriteLine("Sample customers include:");
                
                foreach (var customer in customers.Take(3))
                {
                    Console.WriteLine($"  - {customer.FirstName} {customer.LastName} from {customer.Address.City}, {customer.Address.State}");
                }
                
                if (customers.Count > 3)
                {
                    Console.WriteLine($"  ... and {customers.Count - 3} more customers");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error displaying sample data: {ex.Message}");
            }
        }
        
        private static void TestService()
        {
            Console.WriteLine();
            Console.WriteLine("Testing Service Operations:");
            Console.WriteLine("===========================");
            
            try
            {
                var service = new CustomerService.Implementation.CustomerService();
                
                // Test GetCustomerCount
                Console.WriteLine($"1. Customer Count: {service.GetCustomerCount()}");
                
                // Test GetCustomer
                var customer = service.GetCustomer(1);
                if (customer != null)
                {
                    Console.WriteLine($"2. Customer ID 1: {customer.FirstName} {customer.LastName} - {customer.Email}");
                }
                
                // Test GetCustomersByCity
                var nyCustomers = service.GetCustomersByCity("New York");
                Console.WriteLine($"3. Customers in New York: {nyCustomers.Count}");
                
                // Test SearchCustomers
                var searchResults = service.SearchCustomers("John");
                Console.WriteLine($"4. Search results for 'John': {searchResults.Count} customers found");
                
                // Test CreateCustomer
                var newCustomer = new Customer
                {
                    FirstName = "Test",
                    LastName = "User",
                    Email = "test.user@email.com",
                    Phone = "+1-555-TEST",
                    IsActive = true,
                    Address = new Address
                    {
                        Street = "123 Test St",
                        City = "Test City",
                        State = "TC",
                        ZipCode = "12345",
                        Country = "USA"
                    }
                };
                
                int newId = service.CreateCustomer(newCustomer);
                Console.WriteLine($"5. Created new customer with ID: {newId}");
                
                // Test updated count
                Console.WriteLine($"6. Updated Customer Count: {service.GetCustomerCount()}");
                
                Console.WriteLine();
                Console.WriteLine("All tests completed successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during testing: {ex.Message}");
            }
        }
    }
}