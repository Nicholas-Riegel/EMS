using EMS.Data;
using EMS.Models;

namespace EMS.Services;

public class EmployeeService
{

    public static List<Employee> ViewAll()
    {
        using var context = new AppDbContext();
        return [.. context.Employees];
    }

    public static void AddEmployee(string employeeName, int employeeAge)
    {
        using var context = new AppDbContext();
        var employee = new Employee
        {
            Name = employeeName,
            Age = employeeAge,
        };
        context.Employees.Add(employee);
        context.SaveChanges(); // Actually saves to database
    }

    public static bool EditEmployee(int id, string name, int age)
    {
        using var context = new AppDbContext();
        var employee = context.Employees.Find(id); // Find by ID
        if (employee != null)
        {
            employee.Name = name;
            employee.Age = age;
            context.SaveChanges();
            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool DeleteEmployee(int id)
    {
        using var context = new AppDbContext();
        var employee = context.Employees.Find(id);
        if (employee != null)
        {
            context.Employees.Remove(employee);
            context.SaveChanges();
            return true;
        } 
        else
        {
            return false;
        }
    }
}