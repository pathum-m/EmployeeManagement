
# Employee Management System

## Overview
This is a .NET 8 based Employee Management System. The solution consists of multiple projects:
- `EmployeeManagement.WebAPI`: The main web API project.
- `EmployeeManagement.Application`: Contains application logic and command handlers.
- `EmployeeManagement.Infrastructure`: Contains data access logic and database migrations.
- `EmployeeManagement.Domain`: Contains domain entities and value objects.

## Prerequisites
- .NET 8 SDK
- SQL Server
- Docker (optional, for containerized deployment)

## Setup Instructions

### 1. Clone the Repository

### 2. Configure the Database
Update the connection string in `appsettings.json` located in the `EmployeeManagement.WebAPI` project to point to your SQL Server instance.
```json
{
  "ConnectionStrings": {
	"DefaultConnection": "Server=localhost;Database=EmployeeManagement;Trusted_Connection=True;"
  }
}
```

### 3. Apply Migrations
Navigate to the `EmployeeManagement.WebAPI` project directory and run the following commands to apply the database migrations:
# cd EmployeeManagement.WebAPI dotnet ef database update


### 4. Run the Application
You can run the application using the following command:
# dotnet run --project EmployeeManagement.WebAPI Or , you can run the application using Visual Studio or Visual Studio Code. (IIS Express)


The application will start and be accessible at `https://localhost:5001` (or `http://localhost:5000`).

### 5. Using Docker (Optional)
To run the application in a Docker container, ensure Docker is installed and running on your machine. Then, build and run the Docker image:
# docker build -t employee-management . docker run -d -p 5000:80 employee-management


## Additional Information

### Logging
The application uses Serilog for logging. Configuration for Serilog can be found in the `appsettings.json` file.

### API Documentation
Swagger is used for API documentation. When running in development mode, you can access the Swagger UI at `https://localhost:5001/swagger` (or `http://localhost:5000/swagger`).


-- EFCore commands
Add-Migration
Bundle-Migration
Drop-Database
Get-DbContext
Get-Migration
Optimize-DbContext
Remove-Migration
Scaffold-DbContext
Script-Migration
Update-Database