
using EmployeeManagement.Models;
using EmployeeManagement.Services;

EmployeeService employeeService = new EmployeeService();
DepartmentService departmentService = new DepartmentService();
SalaryService salaryService = new SalaryService();

while (true)
{
    Console.WriteLine("\n===== EMPLOYEE MANAGEMENT SYSTEM =====");
    Console.WriteLine("1. Employee Management");
    Console.WriteLine("2. Department Management");
    Console.WriteLine("3. Salary Management");
    Console.WriteLine("4. Exit");
    Console.Write("Enter your choice: ");

    if (!int.TryParse(Console.ReadLine(), out int choice))
    {
        Console.WriteLine("Please enter a valid number.");
        continue;
    }

    switch (choice)
    {
        case 1:
            EmployeeMenu();
            break;

        case 2:
            DepartmentMenu();
            break;

        case 3:
            SalaryMenu();
            break;

        case 4:
            Console.WriteLine("Application closed.");
            return;

        default:
            Console.WriteLine("Invalid choice.");
            break;
    }
}

void EmployeeMenu()
{
    while (true)
    {
        Console.WriteLine("\n--- Employee Management ---");
        Console.WriteLine("1. Add Employee");
        Console.WriteLine("2. View Employees");
        Console.WriteLine("3. Search Employee");
        Console.WriteLine("4. Update Employee");
        Console.WriteLine("5. Delete Employee");
        Console.WriteLine("6. Back to Main Menu");
        Console.Write("Enter choice: ");

        if (!int.TryParse(Console.ReadLine(), out int choice))
        {
            Console.WriteLine("Invalid input.");
            continue;
        }

        switch (choice)
        {
            case 1:
                Console.Write("Employee ID: ");
                if (!int.TryParse(Console.ReadLine(), out int id))
                {
                    Console.WriteLine("Invalid ID.");
                    break;
                }

                Console.Write("Employee Name: ");
                string name = Console.ReadLine() ?? "";

                Console.Write("Employee Age: ");
                if (!int.TryParse(Console.ReadLine(), out int age))
                {
                    Console.WriteLine("Invalid age.");
                    break;
                }

                Console.Write("Department ID: ");
                if (!int.TryParse(Console.ReadLine(), out int deptId))
                {
                    Console.WriteLine("Invalid department ID.");
                    break;
                }

                employeeService.AddEmployee(new Employee
                {
                    Id = id,
                    Name = name,
                    Age = age,
                    DepartmentId = deptId
                });
                break;

            case 2:
                employeeService.ViewEmployees();
                break;

            case 3:
                Console.Write("Employee ID: ");
                if (int.TryParse(Console.ReadLine(), out int searchId))
                    employeeService.SearchEmployee(searchId);
                else
                    Console.WriteLine("Invalid ID.");
                break;

            case 4:
                Console.Write("Employee ID: ");
                if (!int.TryParse(Console.ReadLine(), out int updateId))
                {
                    Console.WriteLine("Invalid ID.");
                    break;
                }

                Console.Write("New Name: ");
                string newName = Console.ReadLine() ?? "";

                Console.Write("New Age: ");
                if (!int.TryParse(Console.ReadLine(), out int newAge))
                {
                    Console.WriteLine("Invalid age.");
                    break;
                }

                employeeService.UpdateEmployee(updateId, newName, newAge);
                break;

            case 5:
                Console.Write("Employee ID: ");
                if (int.TryParse(Console.ReadLine(), out int deleteId))
                    employeeService.DeleteEmployee(deleteId);
                else
                    Console.WriteLine("Invalid ID.");
                break;

            case 6:
                return;

            default:
                Console.WriteLine("Invalid choice.");
                break;
        }
    }
}

void DepartmentMenu()
{
    while (true)
    {
        Console.WriteLine("\n--- Department Management ---");
        Console.WriteLine("1. Add Department");
        Console.WriteLine("2. View Departments");
        Console.WriteLine("3. Search Department");
        Console.WriteLine("4. Update Department");
        Console.WriteLine("5. Delete Department");
        Console.WriteLine("6. Back to Main Menu");
        Console.Write("Enter choice: ");

        if (!int.TryParse(Console.ReadLine(), out int choice))
        {
            Console.WriteLine("Invalid input.");
            continue;
        }

        switch (choice)
        {
            case 1:
                Console.Write("Department ID: ");
                if (!int.TryParse(Console.ReadLine(), out int id))
                {
                    Console.WriteLine("Invalid ID.");
                    break;
                }

                Console.Write("Department Name: ");
                string name = Console.ReadLine() ?? "";

                departmentService.AddDepartment(new Department
                {
                    Id = id,
                    Name = name
                });
                break;

            case 2:
                departmentService.ViewDepartments();
                break;

            case 3:
                Console.Write("Department ID: ");
                if (int.TryParse(Console.ReadLine(), out int searchId))
                    departmentService.SearchDepartment(searchId);
                else
                    Console.WriteLine("Invalid ID.");
                break;

            case 4:
                Console.Write("Department ID: ");
                if (!int.TryParse(Console.ReadLine(), out int updateId))
                {
                    Console.WriteLine("Invalid ID.");
                    break;
                }

                Console.Write("New Department Name: ");
                string newName = Console.ReadLine() ?? "";

                departmentService.UpdateDepartment(updateId, newName);
                break;

            case 5:
                Console.Write("Department ID: ");
                if (int.TryParse(Console.ReadLine(), out int deleteId))
                    departmentService.DeleteDepartment(deleteId);
                else
                    Console.WriteLine("Invalid ID.");
                break;

            case 6:
                return;

            default:
                Console.WriteLine("Invalid choice.");
                break;
        }
    }
}

void SalaryMenu()
{
    while (true)
    {
        Console.WriteLine("\n--- Salary Management ---");
        Console.WriteLine("1. Add Salary");
        Console.WriteLine("2. View All Salaries");
        Console.WriteLine("3. Search Salary");
        Console.WriteLine("4. Update Salary");
        Console.WriteLine("5. Delete Salary");
        Console.WriteLine("6. Back to Main Menu");
        Console.Write("Enter choice: ");

        if (!int.TryParse(Console.ReadLine(), out int choice))
        {
            Console.WriteLine("Invalid input.");
            continue;
        }

        switch (choice)
        {
            case 1:
                Console.Write("Employee ID: ");
                if (!int.TryParse(Console.ReadLine(), out int employeeId))
                {
                    Console.WriteLine("Invalid employee ID.");
                    break;
                }

                Console.Write("Basic Salary: ");
                if (!decimal.TryParse(Console.ReadLine(), out decimal basic))
                {
                    Console.WriteLine("Invalid basic salary.");
                    break;
                }

                Console.Write("Bonus: ");
                if (!decimal.TryParse(Console.ReadLine(), out decimal bonus))
                {
                    Console.WriteLine("Invalid bonus.");
                    break;
                }

                salaryService.AddSalary(new Salary
                {
                    EmployeeId = employeeId,
                    BasicSalary = basic,
                    Bonus = bonus
                });
                break;

            case 2:
                salaryService.ViewSalaries();
                break;

            case 3:
                Console.Write("Employee ID: ");
                if (int.TryParse(Console.ReadLine(), out int searchId))
                    salaryService.SearchSalary(searchId);
                else
                    Console.WriteLine("Invalid employee ID.");
                break;

            case 4:
                Console.Write("Employee ID: ");
                if (!int.TryParse(Console.ReadLine(), out int updateId))
                {
                    Console.WriteLine("Invalid employee ID.");
                    break;
                }

                Console.Write("New Basic Salary: ");
                if (!decimal.TryParse(Console.ReadLine(), out decimal newBasic))
                {
                    Console.WriteLine("Invalid basic salary.");
                    break;
                }

                Console.Write("New Bonus: ");
                if (!decimal.TryParse(Console.ReadLine(), out decimal newBonus))
                {
                    Console.WriteLine("Invalid bonus.");
                    break;
                }

                salaryService.UpdateSalary(updateId, newBasic, newBonus);
                break;

            case 5:
                Console.Write("Employee ID: ");
                if (int.TryParse(Console.ReadLine(), out int deleteId))
                    salaryService.DeleteSalary(deleteId);
                else
                    Console.WriteLine("Invalid employee ID.");
                break;

            case 6:
                return;

            default:
                Console.WriteLine("Invalid choice.");
                break;
        }
    }
}
