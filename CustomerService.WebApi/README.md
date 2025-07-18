# Customer Service Web API (.NET 8.0)

A modern REST API for managing customers, converted from WCF to .NET 8.0 with Swagger/OpenAPI support.

## Features

- **REST API Endpoints**: Full CRUD operations for customer management
- **Swagger UI**: Interactive API documentation and testing interface  
- **.NET 8.0**: Latest version of .NET with performance improvements
- **Async/Await**: Modern asynchronous programming patterns
- **Model Validation**: Built-in validation using Data Annotations
- **Comprehensive Error Handling**: Proper HTTP status codes and error responses

## API Endpoints

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

## Running the API

1. Navigate to the Web API directory:
   ```bash
   cd CustomerService.WebApi
   ```

2. Run the application:
   ```bash
   dotnet run
   ```

3. Open Swagger UI in your browser:
   ```
   http://localhost:5114
   ```

## Sample Data

The API comes with 5 pre-loaded sample customers:
- John Doe (New York, NY)
- Jane Smith (Los Angeles, CA)  
- Robert Johnson (Chicago, IL)
- Emily Brown (Houston, TX)
- Michael Davis (Phoenix, AZ) - Inactive

## Testing the API

### Using Swagger UI
1. Navigate to `http://localhost:5114` 
2. Click on any endpoint to expand it
3. Click "Try it out" to test the endpoint
4. Fill in required parameters and click "Execute"

### Using curl
```bash
# Get all customers
curl -X GET "http://localhost:5114/api/customers"

# Get customer by ID
curl -X GET "http://localhost:5114/api/customers/1"

# Get customer count
curl -X GET "http://localhost:5114/api/customers/count"

# Search customers
curl -X GET "http://localhost:5114/api/customers/search?searchTerm=John"

# Create new customer
curl -X POST "http://localhost:5114/api/customers" \
  -H "Content-Type: application/json" \
  -d '{
    "firstName": "Test",
    "lastName": "User", 
    "email": "test@example.com",
    "phone": "+1-555-TEST",
    "isActive": true,
    "address": {
      "street": "123 Test St",
      "city": "Test City",
      "state": "TC",
      "zipCode": "12345",
      "country": "USA"
    }
  }'
```

## Project Structure

```
CustomerService.WebApi/
├── Controllers/
│   └── CustomersController.cs    # REST API controller
├── Models/
│   └── Customer.cs               # Data models with validation
├── Services/
│   └── CustomerService.cs        # Business logic service
├── Program.cs                    # Application configuration
└── CustomerService.WebApi.csproj # Project file
```

## Key Differences from WCF Version

| Aspect | WCF (.NET Framework) | Web API (.NET 8.0) |
|--------|---------------------|---------------------|
| **Protocol** | SOAP/WS-* | REST/HTTP |
| **Serialization** | DataContract | JSON (System.Text.Json) |
| **Attributes** | [ServiceContract], [DataContract] | [ApiController], [HttpGet] |
| **Documentation** | WSDL | Swagger/OpenAPI |
| **Testing** | WCF Test Client | Swagger UI + curl |
| **Performance** | Heavy SOAP envelope | Lightweight JSON |
| **Platform** | Windows only | Cross-platform |

## Built With

- [.NET 8.0](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [ASP.NET Core Web API](https://docs.microsoft.com/en-us/aspnet/core/web-api/)
- [Swashbuckle.AspNetCore](https://github.com/domaindrivendev/Swashbuckle.AspNetCore) (Swagger)
- [System.ComponentModel.DataAnnotations](https://docs.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations) (Validation)