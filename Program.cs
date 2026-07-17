using EMS.Services;
using EMS.Models;

Console.WriteLine("Welcome to the Employee Management System.");

while (true){


    Console.WriteLine("Please select one of the following options: \n1. View Employees \n2. Add Employee \n3. Update Employee \n4. Delete Employee \n5. Exit");
    
    string? input = Console.ReadLine();
    if (!int.TryParse(input, out int selection))
    {
        Console.WriteLine("Invalid choice!");
        continue;
    }
    
    switch (selection){

        // View all employees
        case 1: 
            
            Console.WriteLine("Here are all the Employees:");

            foreach (var emp in EmployeeService.ViewAll())
            {
                Console.WriteLine($"Id: {emp.Id}, Name: {emp.Name}, Age: {emp.Age}");
            }
            break;
        
        // Add employee
        case 2: 
        
            Console.WriteLine("Enter employee name:");
            string? employeeName = Console.ReadLine();
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
            
            EmployeeService.AddEmployee(employeeName, employeeAge);
            
            Console.WriteLine("Employee added successfully!");
            
            break;
        
        // Edit employee
        case 3: 
        
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
            
            if (EmployeeService.EditEmployee(employeeId, employeeName, employeeAge))
            {
                Console.WriteLine("Employee updated!");
            }
            else
            {
                Console.WriteLine("Employee not found with that id.");
            }            

            break;
        
        // Delete employee
        case 4: 
            
            Console.WriteLine("Please enter Employee id:");
            input = Console.ReadLine();
            if (!int.TryParse(input, out employeeId))
            {
                Console.WriteLine("Invalid id!");
                return;
            }
            
            if (EmployeeService.DeleteEmployee(employeeId))
            {
                Console.WriteLine("Employee removed!");
            }
            else
            {
                Console.WriteLine("Employee does not exist with that id.");
            }

            break;
        
        // Exit program
        case 5: 
        
            Console.WriteLine("Bye!");
            return;
        
        default: 
            break;
    }
}
