using System;
using System.Collections.Generic;
using System.Text;

using EmployeeManagement.Models;

namespace EmployeeManagement.Services;

public class SalaryService
{
    private readonly List<Salary> salaries = new();

    public void AddSalary(Salary salary)
    {
        if (salaries.Any(s => s.EmployeeId == salary.EmployeeId))
        {
            Console.WriteLine("Salary record already exists.");
            return;
        }

        salaries.Add(salary);
        Console.WriteLine("Salary added successfully.");
    }

    public void ViewSalaries()
    {
        if (salaries.Count == 0)
        {
            Console.WriteLine("No salary records found.");
            return;
        }

        foreach (Salary salary in salaries)
        {
            Console.WriteLine($"Employee ID: {salary.EmployeeId}");
            Console.WriteLine($"Basic Salary: {salary.BasicSalary:C}");
            Console.WriteLine($"Bonus: {salary.Bonus:C}");
            Console.WriteLine($"Total Salary: {salary.TotalSalary:C}");
            Console.WriteLine("----------------------------");
        }
    }

    public void SearchSalary(int employeeId)
    {
        Salary? salary =
            salaries.FirstOrDefault(s => s.EmployeeId == employeeId);

        if (salary == null)
        {
            Console.WriteLine("Salary record not found.");
            return;
        }

        Console.WriteLine($"Employee ID: {salary.EmployeeId}");
        Console.WriteLine($"Basic Salary: {salary.BasicSalary:C}");
        Console.WriteLine($"Bonus: {salary.Bonus:C}");
        Console.WriteLine($"Total Salary: {salary.TotalSalary:C}");
    }

    public void UpdateSalary(
        int employeeId,
        decimal basicSalary,
        decimal bonus)
    {
        Salary? salary =
            salaries.FirstOrDefault(s => s.EmployeeId == employeeId);

        if (salary == null)
        {
            Console.WriteLine("Salary record not found.");
            return;
        }

        salary.BasicSalary = basicSalary;
        salary.Bonus = bonus;

        Console.WriteLine("Salary updated successfully.");
    }

    public void DeleteSalary(int employeeId)
    {
        Salary? salary =
            salaries.FirstOrDefault(s => s.EmployeeId == employeeId);

        if (salary == null)
        {
            Console.WriteLine("Salary record not found.");
            return;
        }

        salaries.Remove(salary);
        Console.WriteLine("Salary deleted successfully.");
    }
}
