Version tags:
1. v0              Version 0: Minimal version. No db. User input with list.



---------------
1. Create project command:
dotnet new console

2. to run the program: (use this for this project)
dotnet run

3. to watch the program:
dotnet watch

4. create a tag for v0
```bash
git tag v0
```

Or with a message:
```bash
git tag -a v0 -m "Version 0: Initial working CRUD app"
```
The `-a` flag in step 4 creates an "annotated tag" which stores more info (author, date, message). It's better for version marking.

**To see your tags later:**
```bash
git tag
```

**To go back to v0 later:**
```bash
git checkout v0
```
To see the annotation messages with your tags, you need to use:

```bash
git tag -n
```

This shows the first line of each tag's message. For more lines, use `git tag -n5` (shows first 5 lines).

To see full details of a specific tag:
```bash
git show v0
```

That's the `less` pager that Git uses for output. Press **`q`** to exit.

---

## Version 1: EF Core + Postgres Setup

### - [x] **Step 1: Install NuGet packages**
```bash
dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL --version 8.0.4
dotnet add package Microsoft.EntityFrameworkCore.Design --version 8.0.10
dotnet add package Microsoft.EntityFrameworkCore.Tools --version 8.0.10
```
**What these do:**
- `Npgsql.EntityFrameworkCore.PostgreSQL` - EF Core provider for Postgres
- `Microsoft.EntityFrameworkCore.Design` - Enables design-time features (migrations)
- `Microsoft.EntityFrameworkCore.Tools` - CLI tools for migrations (`dotnet ef` commands)

**Version note:** These versions (8.x) are compatible with .NET 8.0. Version 10.x requires .NET 10.0.

**After adding packages, build to make them available:**
```bash
dotnet build
```

**Why this matters:**
- `dotnet build` compiles the project and makes the packages available to the IDE
- Without this, you'll see red squiggly lines in your code because VS Code doesn't recognize the EF Core types yet
- The red lines should disappear after you build

---

### - [x] **Step 2: Create `Data/AppDbContext.cs` file**

First create the Data folder:
```bash
mkdir Data
```

Then create `Data/AppDbContext.cs` with this content:

```csharp
using Microsoft.EntityFrameworkCore;
using EMS.Models;

namespace EMS.Data;

public class AppDbContext : DbContext
{
    // This represents the Employees table in the database
    public DbSet<Employee> Employees { get; set; }
    
    // Configure the database connection
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Replace with your actual Postgres credentials
        optionsBuilder.UseNpgsql("Host=localhost;Database=ems_db;Username=nicholas");
    }

    // for timestamps
    public override int SaveChanges()
    {
        var entries = ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

        foreach (var entry in entries)
        {
            if (entry.Entity is Employee employee)
            {
                if (entry.State == EntityState.Added)
                {
                    employee.CreatedAt = DateTime.UtcNow;
                }
                employee.UpdatedAt = DateTime.UtcNow;
            }
        }

        return base.SaveChanges();
    }
}
```

**Key concepts:**
- `DbContext` - The main class that coordinates EF functionality for your data model
- `DbSet<Employee>` - Represents a collection of all Employee entities in the database (the table)
- `OnConfiguring` - Where you specify the connection string to your database

---

### - [x] **Step 2.5: Find your Postgres connection details**

Before you can connect, you need to know your Postgres credentials. Here's how to find them:

**Connection string format:**
```
Host=localhost;Database=ems_db;Username=YOUR_USERNAME;Password=YOUR_PASSWORD
```

**Finding your credentials:**

**Host** - Usually `localhost` if Postgres is installed on your machine

**Database** - Choose any name you want (e.g., `ems_db`). EF Core will create it if it doesn't exist yet.

**Testing your connection:**
```bash
psql -U YOUR_USERNAME -d postgres
```
- If it asks for a password, enter it
- If it connects, you've found the right username/password!
- Type `\q` to exit

**Example connection strings:**
```csharp
// With password
"Host=localhost;Database=ems_db;Username=postgres;Password=mypassword"

// No password (common with Homebrew/Postgres.app)
"Host=localhost;Database=ems_db;Username=nicholas"
```

Once you have your credentials, update them in `AppDbContext.cs` in the `OnConfiguring` method.

---

### - [x] **Step 3: Update `Employee` model for EF Core**

Open `Models/Employee.cs` and make sure it looks like this:

```csharp
using System.ComponentModel.DataAnnotations;

namespace EMS.Models;

public class Employee
{
    [Key] // Marks this as the primary key
    public int Id { get; set; }
    
    public string Name { get; set; } = string.Empty;
    public int Age { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
```

**What changed:**
- Added `using System.ComponentModel.DataAnnotations;` for the `[Key]` attribute
Great question! `[Key]` is an **attribute** in C#. Attributes are metadata tags that provide extra information about your code to frameworks like EF Core.

**The syntax:**
```csharp
[AttributeName]  // The attribute
public int Id { get; set; }  // The thing it decorates
```

**How it works:**
- Square brackets `[ ]` are C# syntax for attributes (nothing to do with arrays)
- Attributes go on the line above (or sometimes same line) as what they describe
- `[Key]` tells EF Core: "This property is the primary key for the database table"

**Other common attributes you'll see:**

```csharp
[Required]  // Field cannot be null
public string Name { get; set; }

[MaxLength(100)]  // Maximum string length
public string Position { get; set; }

[Column("employee_salary")]  // Custom column name in database
public decimal Salary { get; set; }
```

**In your case:**
- `[Key]` comes from `System.ComponentModel.DataAnnotations`
- It's optional if your property is named `Id` or `EmployeeId` (EF recognizes those automatically)
- But using it makes your intent explicit and clear

Think of attributes as sticky notes you put on your code to tell frameworks special instructions!
Yes, exactly! C# `[Attributes]` are the same concept as:

- **Python decorators:** `@property`, `@staticmethod`
- **Java annotations:** `@Override`, `@Entity`, `@Column`
- **TypeScript decorators:** `@Component`, `@Injectable`

They all do the same thing - add metadata or modify behavior. Just different syntax:

```python
# Python
@property
def name(self):
    return self._name
```

```java
// Java
@Entity
@Table(name = "employees")
public class Employee { }
```

```csharp
// C#
[Key]
[Required]
public int Id { get; set; }
```

C# went with square brackets `[ ]` while most other modern languages standardized on `@`. Same idea, different punctuation!

- Added `[Key]` attribute to `Id` (tells EF this is the primary key)
- Made sure all properties have `{ get; set; }` (required for EF)

**Note:** If your property is named `Id` or `EmployeeId`, EF will automatically recognize it as the key. The `[Key]` attribute is optional in that case but makes it explicit.

---

### - [x] **Step 4: Set up your Postgres database credentials**

Before running migrations, make sure you know:
- **Host** - Usually `localhost` if Postgres is on your machine
- **Database name** - Choose a name like `ems_db` (doesn't need to exist yet, EF will create it)
- **Username** - Default is often `postgres`
- **Password** - The password you set when installing Postgres

Update the connection string in `AppDbContext.cs`:
```csharp
optionsBuilder.UseNpgsql("Host=localhost;Database=ems_db;Username=YOUR_USERNAME;Password=YOUR_PASSWORD");
```

**Security tip:** For now it's okay to hardcode this for learning, but in real projects you'd use environment variables or `appsettings.json`.

---

### - [x] **Step 5: Create initial migration**

```bash
dotnet ef migrations add InitialCreate
```

**What this does:**
- Scans your `DbContext` and models
- Creates a `Migrations` folder with migration files
- The migration contains code to create the `Employees` table

**Expected output:**
```
Build started...
Build succeeded.
Done. To undo this action, use 'dotnet ef migrations remove'
```

**If you get "No executable found matching command 'dotnet-ef'":**
- Install the EF Core CLI tools globally:
  ```bash
  dotnet tool install --global dotnet-ef
  ```

---

### - [x] **Step 6: Apply migration to create the database**

```bash
dotnet ef database update
```

**What this does:**
- Connects to Postgres
- Creates the database (if it doesn't exist)
- Creates the `Employees` table with columns: Id, Name, Position, Salary
- Creates a special `__EFMigrationsHistory` table to track which migrations have been applied

**Expected output:**
```
Build started...
Build succeeded.
Applying migration '20260715123456_InitialCreate'.
Done.
```

**Verify it worked:**
- Open your Postgres client (pgAdmin, psql, etc.)
- Check that the `ems_db` database exists
- Check that it has an `Employees` table with your columns

---

### - [ ] **Step 7: Update Program.cs to use EF Core**

Replace your in-memory list code with database operations. Here's the pattern:

**Add using statements at the top:**
```csharp
using EMS.Data;
using Microsoft.EntityFrameworkCore;
```

**Wrap database operations in `using` blocks:**
```csharp
// CREATE example
using (var context = new AppDbContext())
{
    var employee = new Employee 
    { 
        Name = "John Doe", 
        Position = "Developer", 
        Salary = 75000 
    };
    context.Employees.Add(employee);
    context.SaveChanges(); // Actually saves to database
}

// READ example (list all)
using (var context = new AppDbContext())
{
    var employees = context.Employees.ToList();
    foreach (var emp in employees)
    {
        Console.WriteLine($"{emp.Id}: {emp.Name} - {emp.Position} - ${emp.Salary}");
    }
}

// UPDATE example
using (var context = new AppDbContext())
{
    var employee = context.Employees.Find(employeeId); // Find by ID
    if (employee != null)
    {
        employee.Salary = 80000;
        context.SaveChanges();
    }
}

// DELETE example
using (var context = new AppDbContext())
{
    var employee = context.Employees.Find(employeeId);
    if (employee != null)
    {
        context.Employees.Remove(employee);
        context.SaveChanges();
    }
}
```

**Important concepts:**
- `using` statement - Automatically disposes the DbContext when done (closes DB connection)
- `context.SaveChanges()` - **REQUIRED** to actually write changes to the database
- `.ToList()` - Executes the query and brings data into memory
- `.Find(id)` - Finds an entity by its primary key

---

### - [ ] **Step 8: Test all CRUD operations**

Run your app and test:
- Create a new employee
- List all employees
- Update an employee's salary
- Delete an employee
- List again to verify deletion

**Tip:** Use a Postgres client to view the database directly and confirm changes are persisting.

---

### - [ ] **Step 9: Commit and create v1 tag**

```bash
git add .
git commit -m "Add EF Core with Postgres database support"
git tag -a v1 -m "Version 1: EF Core with Postgres database"
```

**View your tags:**
```bash
git tag -n
```

