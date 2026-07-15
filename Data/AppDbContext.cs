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
        optionsBuilder.UseNpgsql("Host=localhost;Database=ems_db;Username=postgres;Password=yourpassword");
    }
}
