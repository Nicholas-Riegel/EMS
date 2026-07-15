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
