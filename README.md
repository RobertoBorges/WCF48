# WCF48 Demo - Customer Service

A complete customer service solution showing migration from WCF (.NET Framework 4.8) to modern REST API (.NET 8.0).

## Project Structure

- **CustomerService**: Original WCF Service Library (.NET Framework 4.8)
- **CustomerService.Host**: Console application to host the WCF service (.NET Framework 4.8)
- **CustomerService.WebApi**: Modern REST API with Swagger (.NET 8.0) ⭐ **NEW**

## 🆕 Web API (.NET 8.0) - **RECOMMENDED**

The new **CustomerService.WebApi** project provides a modern REST API with:

- ✅ REST endpoints instead of SOAP
- ✅ Swagger/OpenAPI documentation  
- ✅ JSON serialization
- ✅ Cross-platform compatibility
- ✅ Better performance
- ✅ Interactive API testing

### Quick Start (Web API)

1. Navigate to the Web API directory:
   ```bash
   cd CustomerService.WebApi
   ```

2. Run the application:
   ```bash
   dotnet run
   ```

3. Open Swagger UI: `http://localhost:5114`

![Swagger UI](https://github.com/user-attachments/assets/51499dcd-7d3c-48a9-890e-ec66375e0a27)

### API Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/customers` | Get all customers |
| GET | `/api/customers/{id}` | Get customer by ID |
| GET | `/api/customers/by-city/{city}` | Get customers by city |
| GET | `/api/customers/search?searchTerm={term}` | Search customers |
| GET | `/api/customers/count` | Get total customer count |
| POST | `/api/customers` | Create new customer |
| PUT | `/api/customers/{id}` | Update existing customer |
| DELETE | `/api/customers/{id}` | Delete customer |

## 📊 Migration Comparison

| Aspect | WCF (.NET Framework) | Web API (.NET 8.0) |
|--------|---------------------|---------------------|
| **Protocol** | SOAP/WS-* | REST/HTTP |
| **Data Format** | XML | JSON |
| **Documentation** | WSDL | Swagger/OpenAPI |
| **Testing** | WCF Test Client | Swagger UI + curl |
| **Performance** | Heavy SOAP envelope | Lightweight JSON |
| **Platform** | Windows only | Cross-platform |
| **Modern Features** | Limited | Async/await, DI, etc. |

---

## Legacy WCF Version (.NET Framework 4.8)

> **Note**: The WCF version is maintained for reference, but the Web API version is recommended for new development.

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