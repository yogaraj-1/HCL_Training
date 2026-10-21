

using System;
using System.Collections.Generic;
using System.IO;
using System.Globalization;
using System.Linq;

class Employee
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Department { get; set; } = "";
    public decimal Salary { get; set; }
}

class Program
{
    static readonly string filePath =
        Path.Combine(AppContext.BaseDirectory, "employees.txt");

    static List<Employee> employees = new List<Employee>();

    static void Main()
    {
        LoadEmployees();

        while (true)
        {
            Console.WriteLine("\n===== EMPLOYEE MANAGEMENT =====");
            Console.WriteLine("1. Add Employee");
            Console.WriteLine("2. View Employees");
            Console.WriteLine("3. Search Employee");
            Console.WriteLine("4. Delete Employee");
            Console.WriteLine("5. Exit");
            Console.Write("Enter choice: ");

            if (!int.TryParse(Console.ReadLine(), out int choice))
            {
                Console.WriteLine("Enter a valid menu number.");
                continue;
            }

            switch (choice)
            {
                case 1:
                    AddEmployee();
                    break;

                case 2:
                    ViewEmployees();
                    break;

                case 3:
                    SearchEmployee();
                    break;

                case 4:
                    DeleteEmployee();
                    break;

                case 5:
                    Console.WriteLine("Data is saved. Goodbye!");
                    return;

                default:
                    Console.WriteLine("Choose between 1 and 5.");
                    break;
            }
        }
    }

    static void LoadEmployees()
    {
        try
        {
            if (!File.Exists(filePath))
            {
                File.WriteAllText(filePath, "");
            }

            employees = File.ReadAllLines(filePath)
                .Where(line => !string.IsNullOrWhiteSpace(line))
                .Select(line =>
                {
                    string[] data = line.Split('|');

                    if (data.Length != 4)
                        throw new FormatException("Invalid employee record.");

                    return new Employee
                    {
                        Id = int.Parse(data[0]),
                        Name = data[1],
                        Department = data[2],
                        Salary = decimal.Parse(
                            data[3], CultureInfo.InvariantCulture)
                    };
                }).ToList();

            Console.WriteLine(
                $"{employees.Count} employee record(s) loaded.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Unable to load employee data: " + ex.Message);
            Console.WriteLine("Please check the employees.txt file.");
            Environment.ExitCode = 1;
            throw;
        }
    }

    static void SaveEmployees()
    {
        var lines = employees.Select(e =>
            $"{e.Id}|{e.Name}|{e.Department}|" +
            e.Salary.ToString(CultureInfo.InvariantCulture));

        File.WriteAllLines(filePath, lines);
    }

    static void AddEmployee()
    {
        Console.Write("Enter employee ID: ");
        if (!int.TryParse(Console.ReadLine(), out int id) ||
            id <= 0 || employees.Any(e => e.Id == id))
        {
            Console.WriteLine("Invalid or duplicate employee ID.");
            return;
        }

        Console.Write("Enter name: ");
        string name = Console.ReadLine()?.Trim() ?? "";

        Console.Write("Enter department: ");
        string department = Console.ReadLine()?.Trim() ?? "";

        if (string.IsNullOrWhiteSpace(name) ||
            string.IsNullOrWhiteSpace(department) ||
            name.Contains('|') || department.Contains('|'))
        {
            Console.WriteLine("Enter valid name and department.");
            return;
        }

        Console.Write("Enter salary: ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal salary) ||
            salary < 0)
        {
            Console.WriteLine("Enter a valid non-negative salary.");
            return;
        }

        employees.Add(new Employee
        {
            Id = id,
            Name = name,
            Department = department,
            Salary = salary
        });

        try
        {
            SaveEmployees();
            Console.WriteLine("Employee added and saved successfully!");
        }
        catch (IOException ex)
        {
            employees.RemoveAll(e => e.Id == id);
            Console.WriteLine("Could not save employee: " + ex.Message);
        }
        catch (UnauthorizedAccessException ex)
        {
            employees.RemoveAll(e => e.Id == id);
            Console.WriteLine("File access denied: " + ex.Message);
        }
    }

    static void ViewEmployees()
    {
        if (employees.Count == 0)
        {
            Console.WriteLine("No employee records found.");
            return;
        }

        foreach (Employee e in employees)
        {
            Console.WriteLine(
                $"ID: {e.Id} | Name: {e.Name} | " +
                $"Department: {e.Department} | Salary: {e.Salary:F2}");
        }
    }

    static void SearchEmployee()
    {
        Console.Write("Enter employee ID to search: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Invalid employee ID.");
            return;
        }

        Employee? employee = employees.Find(e => e.Id == id);

        if (employee == null)
        {
            Console.WriteLine("Employee not found.");
            return;
        }

        Console.WriteLine($"Name: {employee.Name}");
        Console.WriteLine($"Department: {employee.Department}");
        Console.WriteLine($"Salary: {employee.Salary:F2}");
    }

    static void DeleteEmployee()
    {
        Console.Write("Enter employee ID to delete: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Invalid employee ID.");
            return;
        }

        Employee? employee = employees.Find(e => e.Id == id);

        if (employee == null)
        {
            Console.WriteLine("Employee not found.");
            return;
        }

        employees.Remove(employee);

        try
        {
            SaveEmployees();
            Console.WriteLine("Employee deleted successfully!");
        }
        catch (IOException ex)
        {
            employees.Add(employee);
            Console.WriteLine("Could not save changes: " + ex.Message);
        }
        catch (UnauthorizedAccessException ex)
        {
            employees.Add(employee);
            Console.WriteLine("File access denied: " + ex.Message);
        }
    }
}
