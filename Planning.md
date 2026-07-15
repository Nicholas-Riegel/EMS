# Employee Management System - C# Learning Project

## Project Overview

A console-based CRUD (Create, Read, Update, Delete) application for managing employee records, connected to a PostgreSQL database. This hands-on project will teach you essential C# concepts through practical implementation.

## Learning Approach

**IMPORTANT:** This is a hands-on learning project. I will write all the code myself. Copilot should only answer questions in the chat and provide guidance - NOT write code for me.

## Learning Objectives

### Core C# Concepts You'll Master

**Object-Oriented Programming (OOP)**
- Classes and objects
- Encapsulation (properties, access modifiers)
- Inheritance (base classes, derived classes)
- Polymorphism (virtual methods, method overriding)
- Abstraction (abstract classes, interfaces)

**Data Management**
- Working with PostgreSQL using Npgsql
- ADO.NET (ActiveX Data Objects for .NET) fundamentals
- Database connection management
- SQL query execution
- Parameterized queries (SQL injection prevention)

**Modern C# Features**
- LINQ (Language Integrated Query) for data manipulation
- Async/await for database operations
- Exception handling and error management
- String interpolation and formatting
- Collections (List, Dictionary)

**Software Design**
- Repository pattern
- Separation of concerns
- Dependency management
- Console UI design patterns

## Technologies & Tools

**Required:**
- .NET 8.0 SDK (latest Long-Term Support version)
- PostgreSQL database
- Npgsql - PostgreSQL data provider for .NET
- Code editor (VS Code, Visual Studio, or Rider)

**Optional:**
- Dapper (lightweight ORM - Object-Relational Mapper) for simplified data access
- Entity Framework Core (full-featured ORM) - for future enhancement

## Project Structure

```
EMS/
├── Models/
│   ├── Employee.cs          # Employee entity class
│   ├── Department.cs        # Department entity class
│   └── IEntity.cs          # Base interface (demonstrates abstraction)
├── Data/
│   ├── DatabaseContext.cs   # Database connection management
│   └── IRepository.cs      # Repository interface
├── Repositories/
│   ├── EmployeeRepository.cs
│   └── DepartmentRepository.cs
├── Services/
│   ├── EmployeeService.cs   # Business logic layer
│   └── ValidationService.cs
├── UI/
│   ├── ConsoleUI.cs        # Menu and display logic
│   └── InputHelper.cs      # User input handling
├── Program.cs              # Entry point
└── appsettings.json       # Configuration (connection strings)
```

## Database Schema

**employees table:**
- id (SERIAL PRIMARY KEY)
- first_name (VARCHAR)
- last_name (VARCHAR)
- email (VARCHAR UNIQUE)
- department_id (INTEGER FOREIGN KEY)
- hire_date (DATE)
- salary (DECIMAL)
- is_active (BOOLEAN)

**departments table:**
- id (SERIAL PRIMARY KEY)
- name (VARCHAR UNIQUE)
- description (TEXT)

## Implementation Phases

### Phase 1: Foundation (OOP Basics)

**What You'll Learn:**
- Creating classes with properties
- Access modifiers (public, private, protected)
- Constructors and object initialization
- ToString() method overriding

**Tasks:**
- Create `Employee` class with properties
- Create `Department` class
- Implement proper encapsulation
- Add validation in property setters

### Phase 2: Database Connection

**What You'll Learn:**
- Connection strings and configuration
- Using Npgsql to connect to PostgreSQL
- IDisposable pattern and resource cleanup
- Exception handling for database operations

**Tasks:**
- Set up PostgreSQL database
- Create database tables
- Implement `DatabaseContext` class
- Test basic connection

### Phase 3: CRUD Operations (Repository Pattern)

**What You'll Learn:**
- Repository pattern for data access
- Async/await for database operations
- SQL parameterized queries
- Working with NpgsqlCommand and NpgsqlDataReader

**Tasks:**
- Create `IRepository<T>` interface (generics!)
- Implement `EmployeeRepository` with:
  - `CreateAsync()` - INSERT operation
  - `GetByIdAsync()` - SELECT single record
  - `GetAllAsync()` - SELECT all records
  - `UpdateAsync()` - UPDATE operation
  - `DeleteAsync()` - DELETE operation
- Implement CRUD for departments

### Phase 4: Business Logic Layer

**What You'll Learn:**
- Separation of concerns
- Input validation
- Business rules enforcement
- LINQ for filtering and sorting

**Tasks:**
- Create `EmployeeService` class
- Implement validation logic:
  - Email format validation
  - Salary range validation
  - Required field checks
- Add search and filter capabilities using LINQ
- Implement sorting options

### Phase 5: Console UI

**What You'll Learn:**
- Console formatting and colors
- Menu-driven applications
- User input validation
- Error message display

**Tasks:**
- Create main menu system
- Implement CRUD screens:
  - Add new employee
  - View all employees
  - Search employees
  - Update employee details
  - Delete employee
  - Manage departments
- Add confirmation prompts
- Display formatted tables

### Phase 6: Advanced Features (Optional)

**What You'll Learn:**
- LINQ advanced queries
- Working with related data (joins)
- Pagination
- Data export

**Tasks:**
- Search by multiple criteria
- Generate reports (employees by department)
- Calculate statistics (average salary, etc.)
- Export to CSV
- Implement pagination for large datasets

## Key Concepts Breakdown

### OOP Pillars in This Project

**Encapsulation** - What it is
- Hiding internal data and exposing only what's necessary
- Implementation: Private fields with public properties in `Employee` class
```csharp
public class Employee {
    private decimal _salary;
    public decimal Salary {
        get => _salary;
        set => _salary = value > 0 ? value : throw new ArgumentException("Salary must be positive");
    }
}
```

**Inheritance** - What it is
- Creating specialized classes from general ones
- Implementation: Base `Person` class with `Employee` inheriting from it
```csharp
public class Person {
    public string FirstName { get; set; }
    public string LastName { get; set; }
}

public class Employee : Person {
    public decimal Salary { get; set; }
    public DateTime HireDate { get; set; }
}
```

**Polymorphism** - What it is
- Different classes responding to the same method call differently
- Implementation: `IRepository<T>` interface implemented by different repository classes
```csharp
IRepository<Employee> employeeRepo = new EmployeeRepository();
IRepository<Department> deptRepo = new DepartmentRepository();
// Both use the same interface but different implementations
```

**Abstraction** - What it is
- Defining contracts without implementation details
- Implementation: `IRepository<T>` interface hiding database complexity
```csharp
public interface IRepository<T> {
    Task<T> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();
    Task CreateAsync(T entity);
    // ... etc
}
```

## Database Connection Concepts

**Connection String** - What it is
- Configuration that specifies how to connect to the database
```
Host=localhost;Port=5432;Database=ems_db;Username=postgres;Password=yourpassword
```

**Async/Await** - What it is
- Non-blocking operations for better performance
- Why: Database calls are I/O operations that benefit from async
```csharp
public async Task<Employee> GetByIdAsync(int id) {
    // await prevents blocking the thread while waiting for database
    await using var connection = new NpgsqlConnection(_connectionString);
    await connection.OpenAsync();
    // ...
}
```

**Using Statement / IDisposable** - What it is
- Automatic resource cleanup (closes database connections)
```csharp
await using var connection = new NpgsqlConnection(connectionString);
// Connection automatically closed when scope ends
```

## Learning Tips

**Start Simple:**
- Begin with Phase 1 - just create classes without database
- Test each phase before moving to the next
- Don't try to implement everything at once

**Debug and Experiment:**
- Use `Console.WriteLine()` to understand program flow
- Try breaking things to understand error messages
- Experiment with different implementations

**Incremental Complexity:**
- Start with synchronous operations, then move to async
- Begin with basic CRUD, then add validation
- Add advanced features only after basics work

**Practice OOP:**
- Refactor code to be more object-oriented
- Look for opportunities to extract classes
- Think about responsibilities and separation of concerns

## Next Steps

1. Set up your development environment
2. Install PostgreSQL and create the database
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
