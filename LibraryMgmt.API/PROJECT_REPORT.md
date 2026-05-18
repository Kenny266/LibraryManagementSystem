# LibraryMgmt.API Project Report

## Current architecture

This repository is a library management API built with ASP.NET Core 10 and Entity Framework Core.

### Key modules
- `Controllers/` — thin HTTP controllers
- `Services/` — business logic and database coordination
- `DTOs/` — request/response models with validation
- `Models/` — domain entities for Books, Members, Loans, and ApplicationUser
- `Data/LibraryDbContext.cs` — EF Core context including identity support

## Implemented features

- Books CRUD via `BooksController` and `BooksService`
- Members CRUD via `MembersController` and `MembersService`
- Loans CRUD + borrow/return logic via `LoansController` and `LoansService`
- DTOs with validation attributes for request and response models
- JWT authentication with login and registration via `AuthController`
- Secure service layer separation: controllers only handle HTTP and validation, services handle database logic
- SQL relationships for `Member` → `Loan` and `Book` → `Loan`
- Swagger support in Development only

## Alignment with requested goals

### Built
- Members module with services, DTOs, and validation
- Business logic in service layer
- Authentication and JWT login
- Professional project documentation via this report

### Not built (left for future work)
- Multi-tenancy and tenant isolation
- Fully scoped role-based authorization beyond basic JWT auth
- Refresh token rotation or advanced security bootstraps

## Notes on the current implementation

- The app uses SQL Server via `DefaultConnection` in `appsettings.json`
- JWT settings are configured under `JwtSettings`
- `ApplicationUser` extends IdentityUser to support login and registration
- The project is now aligned with a professional backend pattern rather than the unrelated church/tenant scaffold described in the attached README

## Next recommended tasks

1. Add refresh token support
2. Add role-based authorization for librarians versus borrowers
3. Add request/response contracts for member-specific loan history
4. Add API versioning and documentation in `README.md`
