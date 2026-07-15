using Microsoft.EntityFrameworkCore;
using EMS.Models;
using EMS.Data;

string? input;
string? employeeName;

Console.WriteLine("Welcome to the Employee Management System.");

while (true){


    Console.WriteLine("Please select one of the following options: \n1. View Employees \n2. Add Employee \n3. Update Employee \n4. Delete Employee \n5. Exit");
    
    input = Console.ReadLine();
    if (!int.TryParse(input, out int selection))
    {
        Console.WriteLine("Invalid choice!");
    }
    
    switch (selection){

        case 1: // View all employees
            
            Console.WriteLine("Here are all the Employees:");
            using (var context = new AppDbContext())
            {
                var employees = context.Employees.ToList();
                foreach (var emp in employees)
                {
                    Console.WriteLine($"Id: {emp.Id}, Name: {emp.Name}, Age: {emp.Age}");
                }
            }
        
            break;
        
        case 2: // Add employee
        
            Console.WriteLine("Enter employee name:");
            employeeName = Console.ReadLine();
            if (string.IsNullOrEmpty(employeeName))
            {
                Console.WriteLine("Invalid name!");
                break;
            }

            Console.WriteLine("Enter employee age: ");
            input = Console.ReadLine();
            if (!int.TryParse(input, out int employeeAge))
            {
                Console.WriteLine("Invalid age!");
                break;
            }
            
            using (var context = new AppDbContext())
            {
                var employee = new Employee 
                { 
                    Name = employeeName, 
                    Age = employeeAge, 
                };
                context.Employees.Add(employee);
                context.SaveChanges(); // Actually saves to database
            }
            Console.WriteLine("Employee added successfully!");
            
            break;
            
        
        case 3: // Edit employee
        
            Console.WriteLine("Please enter Employee id:");
            input = Console.ReadLine();
            if (!int.TryParse(input, out int employeeId))
            {
                Console.WriteLine("Invalid id!");
                break;
            }

            Console.WriteLine("Enter employee name:");
            employeeName = Console.ReadLine();
            if (string.IsNullOrEmpty(employeeName))
            {
                Console.WriteLine("Invalid name!");
                break;
            }

            Console.WriteLine("Enter employee age: ");
            input = Console.ReadLine();
            if (!int.TryParse(input, out employeeAge))
            {
                Console.WriteLine("Invalid age!");
                break;
            }

            using (var context = new AppDbContext())
            {
                var employee = context.Employees.Find(employeeId); // Find by ID
                if (employee != null)
                {
                    employee.Name = employeeName;
                    employee.Age = employeeAge;
                    context.SaveChanges();
                    Console.WriteLine("Employee updated!");
                }
                else
                {
                    Console.WriteLine("Employee not found with that id.");
                    break;
                }
            }

            break;
        
        case 4: // Delete employee
        
            Console.WriteLine("Please enter Employee id:");
            input = Console.ReadLine();
            if (!int.TryParse(input, out employeeId))
            {
                Console.WriteLine("Invalid id!");
                break;
            }
            
            using (var context = new AppDbContext())
            {
                var employee = context.Employees.Find(employeeId);
                if (employee != null)
                {
                    context.Employees.Remove(employee);
                    context.SaveChanges();
                    Console.WriteLine("Employee removed!");
                } 
                else
                {
                    Console.WriteLine("Employee does not exist with that id.");
                    break;
                }
            }
            break;
        
        case 5: // Exit program
        
            Console.WriteLine("Bye!");
            return;
        
        default: 
            break;
    }
}
