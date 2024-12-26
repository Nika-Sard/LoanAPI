# LoanAPI Documentation

## Overview

LoanAPI is a web API designed for managing loans, users, and authentication. It provides a robust and secure way to handle operations related to users and loans, such as adding, updating, deleting, and retrieving data. This documentation covers OpenAPI specifications and a detailed README file to guide users on how to work with the application.

---

## README

# LoanAPI

### **Overview**

LoanAPI is a RESTful API for managing loans and user accounts. It includes secure user authentication, user management, and loan operations.

### **Key Features**

- User registration and authentication.
- Loan creation, update, and deletion.
- Secure token-based authentication using JWT.
- Middleware for exception handling.

### **Getting Started**

#### Prerequisites

- .NET 6.0 SDK
- Microsoft SQL Server

#### Installation

1. Clone the repository:

   ```bash
   git clone https://github.com/your-repository/LoanAPI.git
   ```

2. Navigate to the project directory:

   ```bash
   cd LoanAPI
   ```

3. Install dependencies:

   ```bash
   dotnet restore
   ```

4. Set up the database:

   - Configure the connection string in `appsettings.json`.
   - Run migrations:
     ```bash
     dotnet ef database update
     ```

5. Start the application:

   ```bash
   dotnet run
   ```

The API will be available at `http://localhost:5000`.

---

### **Endpoints**

#### **User Endpoints**

- `POST /api/users`
  - Adds a new user.
  - **Body**:
    ```json
    {
      "username": "string",
      "password": "string",
      "firstName": "string",
      "lastName": "string",
      "age": "integer",
      "salary": "number",
      "isBlocked": "boolean",
      "role": "string"
    }
    ```

#### **Loan Endpoints**

- `POST /api/loans`
  - Creates a loan for a user.
  - **Body**:
    ```json
    {
      "userId": 1,
      "loanType": "Home",
      "amount": 50000,
      "currency": "USD",
      "startDate": "2024-01-01",
      "endDate": "2025-01-01",
      "status": "Pending"
    }
    ```

#### **Authentication**

- Generate a token using `POST /api/authenticate`.
- Use the token as a `Bearer` token in the `Authorization` header for protected endpoints.

---

### **Error Handling**

The API uses middleware to handle exceptions and return appropriate status codes:

- `401 Unauthorized`: Invalid authentication.
- `404 Not Found`: Resource not found.
- `400 Bad Request`: Invalid input or operation.
- `500 Internal Server Error`: Unexpected server error.

---

### **Development**

- Run tests:
  ```bash
  dotnet test
  ```
- Use Swagger at `https://localhost:7238/swagger/index.html` for API testing and documentation.

---

