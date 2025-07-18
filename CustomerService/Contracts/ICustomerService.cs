using System;
using System.Collections.Generic;
using System.ServiceModel;
using CustomerService.Contracts;

namespace CustomerService.Contracts
{
    [ServiceContract]
    public interface ICustomerService
    {
        [OperationContract]
        Customer GetCustomer(int customerId);

        [OperationContract]
        List<Customer> GetAllCustomers();

        [OperationContract]
        List<Customer> GetCustomersByCity(string city);

        [OperationContract]
        int CreateCustomer(Customer customer);

        [OperationContract]
        bool UpdateCustomer(Customer customer);

        [OperationContract]
        bool DeleteCustomer(int customerId);

        [OperationContract]
        List<Customer> SearchCustomers(string searchTerm);

        [OperationContract]
        int GetCustomerCount();
    }
}