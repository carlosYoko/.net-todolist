# TodoList (Clean Architecture) .NET Core

This repository contains a small TodoList built using a clean architecture in C# with the .NET Core framework. The project is configured and implements the following packages:

- **AutoMapper.Extensions.Microsoft.DependencyInjection**
- **FluentValidation**
- **Microsoft.AspNetCore.OpenApi**
- **Microsoft.EntityFrameworkCore.SqlServer**

## Clean Architecture
The project follows the principles of clean architecture, separating responsibilities
into layers:

- **Presentation Layer:** Contains API controllers and view models.
- **Application Layer:** Implements business logic and utilizes infrastructure services.
- **Domain Layer:** Defines entities and repository contracts.
- **Infrastructure Layer:** Contains concrete implementations of application services, such as database access.
