# Employee Management System - C# Learning Project

## Project Overview

A simple console-based CRUD (Create, Read, Update, Delete) application for managing employee records. Built incrementally to learn C# and database concepts.

## Learning Approach

**IMPORTANT:** This is a hands-on learning project. I will write all the code myself. Copilot should only answer questions and provide guidance - NOT write code for me.

## Version History

### ✅ Version 0 (Completed)
**Goal:** Basic console CRUD app with in-memory storage

**What was learned:**
- Console I/O and menu systems
- Basic C# syntax (variables, loops, switch statements)
- Lists and collections
- User input validation

**Implementation:**
- Simple `Employee` class with Id, Name, Position, Salary
- In-memory `List<Employee>` storage
- Basic CRUD operations via console menu

**Tag:** `v0`

---

### ✅ Version 1 (Completed)
**Goal:** Connect to PostgreSQL database using Entity Framework Core (EF Core)

**What was learned:**
- EF Core basics (DbContext, DbSet, migrations)
- Database connections and connection strings
- NuGet package management
- Code-first database design
- C# attributes (`[Key]`)
- `using` statement and IDisposable pattern
- Working with DateTime (CreatedAt, UpdatedAt timestamps)

**Technologies added:**
- Npgsql.EntityFrameworkCore.PostgreSQL
- Microsoft.EntityFrameworkCore.Design
- Microsoft.EntityFrameworkCore.Tools

**Implementation:**
- Created `AppDbContext` class extending `DbContext`
- Simplified `Employee` model (Id, Name, Age, CreatedAt, UpdatedAt)
- Set up Postgres database (`ems_db`)
- Implemented EF Core migrations
- Full CRUD operations persisting to database
- Proper error handling (checking if records exist before update/delete)

**Tag:** `v1`

---

### 📋 Version 2 (Planned)
**Goal:** Refactor to separate concerns - move database logic out of Program.cs

**What you'll learn:**
- Separation of concerns principle
- Service/Repository pattern
- Code organization and maintainability
- Making code reusable

**Tasks:**
1. Create a `Services/EmployeeService.cs` class
2. Move all database operations from `Program.cs` into the service:
   - `List<Employee> GetAllEmployees()`
   - `void AddEmployee(string name, int age)`
   - `bool UpdateEmployee(int id, string name, int age)`
   - `bool DeleteEmployee(int id)`
   - `Employee? GetEmployeeById(int id)` (optional helper)
3. Update `Program.cs` to use the service instead of directly using DbContext
4. Test that everything still works

**Benefits:**
- `Program.cs` focuses only on UI/menu logic
- Database code is in one reusable place
- Easier to test and maintain
- Could swap database implementation without changing UI
- Service could be reused in a web API, desktop app, etc.

**Example structure after refactoring:**
```
EMS/
├── Models/
│   └── Employee.cs
├── Data/
│   └── AppDbContext.cs
├── Services/
│   └── EmployeeService.cs    ← New!
├── Program.cs                 ← Simplified
└── EMS.csproj
```

**Tag:** `v2` (when complete)

---

## Project Complete!

This covers the core learning goals:
- ✅ Basic C# console programming
- ✅ Database integration with EF Core
- ✅ Clean code architecture patterns

**What you've built:**
A working CRUD application with database persistence and clean separation of concerns - the foundation for any data-driven application!
3. Create a new .NET console application
4. Implement Phase 1 (models without database)
5. Test and verify before moving forward

## Resources to Keep Handy

**C# Documentation:**
- Microsoft C# Programming Guide
- Npgsql documentation
- PostgreSQL documentation

**Concepts to Research:**
- SOLID principles (especially Single Responsibility)
- Dependency Injection (for future enhancement)
- Unit testing (for future enhancement)
- Entity Framework Core (alternative to raw SQL)
