# Product API with JWT Authentication and CORS

This is a basic CRUD API for managing products using .NET 8 and SQL Server, with JWT authentication and CORS implementation.

## Features
- **CRUD Operations:** Create, read, update, and delete products.
- **JWT-based Authentication:** Secure your API with JSON Web Tokens.
- **CORS Support:** Configure Cross-Origin Resource Sharing for both development and production environments.
- **Entity Framework Core:** Simplified database operations using EF Core.
- **ASP.NET Core Identity:** Manage user authentication and authorization.

## Technologies Used
- **.NET 8**
- **SQL Server**
- **Entity Framework Core**
- **ASP.NET Core Identity**
- **JWT Authentication**
- **CORS**

## Getting Started
- **Prerequisites**
- **.NET 9 SDK**
- **SQL Server**


## Installation
- **1 - Clone the repository:**
   ```bash
   git clone https://github.com/dvdalves/jwt-authentication-api.git

- **2 - Update the appsettings.json file with your SQL Server connection string and JWT settings::**
   ```bash
   {
      "ConnectionStrings": {
        "DefaultConnection": "Your SQL Server connection string here"
  },
      "JwtSettings": {
        "Secret": "YourSecretKey",
        "ExpirationHours": 1,
        "Issuer": "YourIssuer",
        "Audience": "https://localhost"
      }
  }

- **3 - Run the migrations to create the database:**
  ```bash
  dotnet ef database update

- **4 - Run the application:**
  ```bash
  dotnet run

**License**
<br>
This project is licensed under the [MIT License](https://opensource.org/licenses/MIT).
