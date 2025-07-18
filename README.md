# WCF48 Demo - Customer Service

A complete WCF (Windows Communication Foundation) demo application built with .NET Framework 4.8 featuring a Customer service endpoint with sample data.

## Project Structure

- **CustomerService**: WCF Service Library containing the service contracts, data contracts, and service implementation
- **CustomerService.Host**: Console application to host and test the WCF service

## Features

### Customer Service Operations

The `ICustomerService` interface provides the following operations:

- `GetCustomer(int customerId)` - Retrieve a customer by ID
- `GetAllCustomers()` - Get all customers  
- `GetCustomersByCity(string city)` - Filter customers by city
- `CreateCustomer(Customer customer)` - Add a new customer
- `UpdateCustomer(Customer customer)` - Update existing customer
- `DeleteCustomer(int customerId)` - Remove a customer
- `SearchCustomers(string searchTerm)` - Search customers by name, email, city, or state
- `GetCustomerCount()` - Get total number of customers

### Sample Data

The service comes preloaded with 5 sample customers from different US cities:

1. **John Doe** - New York, NY
2. **Jane Smith** - Los Angeles, CA  
3. **Robert Johnson** - Chicago, IL
4. **Emily Brown** - Houston, TX
5. **Michael Davis** - Phoenix, AZ (Inactive)

## Getting Started

### Prerequisites

- .NET Framework 4.8
- Visual Studio 2019 or later (recommended)

### Building the Solution

1. Open `WCF48Demo.sln` in Visual Studio
2. Build the solution (Build → Build Solution)

### Running the Service

1. Set `CustomerService.Host` as the startup project
2. Run the application (F5 or Ctrl+F5)
3. The service will start and display:
   - Service endpoint URL: `http://localhost:8080/CustomerService`
   - WSDL location: `http://localhost:8080/CustomerService?wsdl`
   - Sample data information
   - Available operations

### Testing the Service

When the host application starts, press 'T' to run automated tests that demonstrate:

- Getting customer count
- Retrieving a specific customer
- Filtering customers by city
- Searching customers
- Creating a new customer

## Service Configuration

The service is configured with:

- **Binding**: WSHttpBinding (WS-* standards support)
- **Behavior**: Metadata publishing enabled, exception details included
- **Address**: `http://localhost:8080/CustomerService`
- **Metadata Exchange**: Available at `/mex` endpoint

## Data Contracts

### Customer
- `CustomerId` (int) - Unique identifier
- `FirstName` (string) - Customer's first name
- `LastName` (string) - Customer's last name  
- `Email` (string) - Email address
- `Phone` (string) - Phone number
- `DateCreated` (DateTime) - Account creation date
- `IsActive` (bool) - Account status
- `Address` (Address) - Customer address

### Address
- `Street` (string) - Street address
- `City` (string) - City name
- `State` (string) - State/Province
- `ZipCode` (string) - Postal code
- `Country` (string) - Country name

## Architecture

The solution follows WCF best practices:

- **Separation of Concerns**: Contracts, implementation, and data access are separated
- **Service Contracts**: Define the service interface with `[ServiceContract]` and `[OperationContract]`
- **Data Contracts**: Define data structures with `[DataContract]` and `[DataMember]`
- **Error Handling**: Proper exception handling with `FaultException`
- **Validation**: Input validation for all service operations
- **Repository Pattern**: Data access abstracted through `CustomerRepository`

## Development Notes

- Service uses `InstanceContextMode.PerCall` for stateless operation
- In-memory data storage for demonstration purposes
- Comprehensive error handling and validation
- WSDL metadata publishing enabled for client generation