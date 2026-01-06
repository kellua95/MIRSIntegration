### MIRS Project Architecture & Usage Guide

Welcome to the MIRS project. This template is built using **Clean Architecture** principles, designed for scalability, maintainability, and clear separation of concerns. This guide explains the structure, patterns, and how to extend the project.

---

### 1. Solution Structure

The solution is divided into several layers, each with a specific responsibility:

*   **`MIRS.Core`**: The innermost layer. Contains base interfaces (`IUnitOfWork`, `IGenericRepository`), base entity models, and shared infrastructure like the `TransactionMiddleware`. It has no dependencies on other layers.
*   **`MIRS.Domain`**: Contains business logic, domain models (Entities), enums, and constants (like `Roles`).
*   **`MIRS.Persistence`**: Handles database communication using EF Core. Includes the `ApplicationContext`, repository implementations, and data seeders.
*   **`MIRS.Application`**: The "brain" of the application. Contains application interfaces, services (e.g., `IssueService`, `AuthService`), DTOs, and mapping profiles.
*   **`MIRS.Startup`**: A shared infrastructure helper that coordinates database migrations and data seeding during startup.
*   **`MIRS.Api`**: The REST API interface.
*   **`MIRS.Web`**: The Razor Pages user interface.

---

### 2. Key Patterns & Components

#### **Unit of Work & Generic Repository**
We use the **Unit of Work (UoW)** pattern to coordinate operations across multiple repositories.
*   **Usage**: Inject `IUnitOfWork` into your services. Access repositories via `_unitOfWork.Repository<TEntity>()`.
*   **Transactions**: You don't need to call `SaveChangesAsync` manually in most cases.

#### **Automatic Transactions (Middleware)**
The `TransactionMiddleware` (located in `MIRS.Application/Middleware`) automatically calls `UnitOfWork.CompleteAsync()` at the end of every successful "Write" request (POST, PUT, DELETE). 
*   If your service method completes without an error and the HTTP status is 2xx or 302, the changes are committed to the database.
*   If an exception occurs, the transaction is implicitly rolled back.

#### **Base Services (ABP-Style)**
All services should inherit from `DomainService` or `ApplicationService`. These base classes provide lazy-loaded access to common dependencies like `Logger`, `Mapper`, and `UnitOfWork`.
```csharp
public class MyService : ApplicationService, IMyService {
    public MyService(IServiceProvider sp) : base(sp) { }
    
    public async Task DoWork() {
        // No need to inject Mapper or UnitOfWork in constructor
        var entity = Mapper.Map<Entity>(dto);
        await UnitOfWork.Repository<Entity>().AddAsync(entity);
    }
}
```

#### **Specifications**
For complex queries (filtering, paging, sorting), use the **Specification Pattern**.
*   Define a spec in `MIRS.Domain/Specifications`.
*   Pass it to repository methods like `GetListBySpecAsync(spec)`.

---

### 3. Authentication & Authorization

#### **Identity Integration**
The project uses ASP.NET Core Identity.
*   **API**: Uses **JWT Tokens**. Tokens include user claims and roles.
*   **Web**: Uses **Cookie Authentication**. The session is synchronized with the JWT token lifetime (1 hour by default).

#### **Roles**
Available roles are defined in `MIRS.Domain.Constants.Roles`: `SuperAdmin`, `Admin`, `Employee`, `Citizen`.

---

### 4. Data Seeding & Migrations

The `MIRS.Startup` project handles database readiness.
*   **Automatic Migrations**: On every start, the system checks for and applies pending EF migrations.
*   **Data Seeders**: Implement `IDataSeeder` in the Persistence layer. They will be automatically picked up and run.
    *   `IdentityDataSeeder`: Creates mandatory roles.
    *   `UsersDataSeeder`: Creates the default `superadmin@mirs.com` (Password: `Pas$word1`).

---

### 5. How to Add a New Feature (Workflow)

1.  **Domain**: Add your Entity in `MIRS.Domain/Models` (must inherit from `BaseEntity`).
2.  **Persistence**: Add a `DbSet` in `ApplicationContext` and configuration in `Configurations/`.
3.  **Application (DTOs)**: Create DTOs in `MIRS.Application/DTOs`.
4.  **Application (Service)**: 
    *   Create an interface in `Interfaces/`.
    *   Create an implementation in `Services/` (inheriting from `ApplicationService`).
    *   Add AutoMapper rules in `Mappings/MappingProfile.cs`.
    *   Register the service in `DIRegistration/ApplicationServiceRegistry.cs`.
5.  **UI**: Create a Controller in `MIRS.Api` or a Razor Page in `MIRS.Web`.
    *   In Web, create a **ViewModel** in `MIRS.Web/ViewModels` and map it to your DTO in `WebMappingProfile.cs`.

---

### 6. Configuration

Connection strings and JWT settings are managed in `appsettings.json`. Ensure the `Token:Key` is at least 64 characters long for production.
