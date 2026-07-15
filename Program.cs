using EMS.Models;

var employeesList = new List<Employee>();

Console.WriteLine("Welcome to the Employee Management System");

while (true){

    Console.WriteLine("Please select one of the following options: \n1. Add Employee \n2. View Employees \n3. Exit");
    
    string? input0 = Console.ReadLine();
    if (!int.TryParse(input0, out int selection))
    {
        Console.WriteLine("Invalid choice!");
    }
    
    switch (selection){

        case 1:
            
            Console.WriteLine("Enter employee name:");
            string? employeeName = Console.ReadLine();
            if (string.IsNullOrEmpty(employeeName))
            {
                Console.WriteLine("Invalid name!");
                break;
            }

            Console.WriteLine("Enter employee age: ");
            string? input1 = Console.ReadLine();
            if (!int.TryParse(input1, out int employeeAge))
            {
                Console.WriteLine("Invalid age!");
                break;
            }
            
            Employee newEmployee = new(){ Name = employeeName, Age = employeeAge};
            employeesList.Add(newEmployee);
            Console.WriteLine("Employee added successfully!");
            
            break;
        
        case 2:
        
            Console.WriteLine("Here are all the Employees");
            foreach (Employee e in employeesList)
            {
                Console.WriteLine($"Name: {e.Name}, Age: {e.Age}");
            }
        
            break;
        
        case 3:
        
            Console.WriteLine("Bye!");
            return;
        
        default: 
            break;
    }
}
