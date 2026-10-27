using System;
using System.Collections.Generic;
using System.Text;

using EmployeeManagement.Models;

namespace EmployeeManagement.Services;

public class EmployeeService
{
    private readonly List<Employee> employees = new();

    public void AddEmployee(Employee employee)
    {
        if (employees.Any(e => e.Id == employee.Id))
        {
            Console.WriteLine("Employee ID already exists.");
            return;
        }

        employees.Add(employee);
        Console.WriteLine("Employee added successfully.");
    }

    public void ViewEmployees()
    {
        if (employees.Count == 0)
        {
            Console.WriteLine("No employees found.");
            return;
        }

        foreach (Employee employee in employees)
        {
            Console.WriteLine(
                $"ID: {employee.Id}, " +
                $"Name: {employee.Name}, " +
                $"Age: {employee.Age}, " +
                $"Department ID: {employee.DepartmentId}");
        }
    }

    public void SearchEmployee(int id)
    {
        Employee? employee = employees.FirstOrDefault(e => e.Id == id);

        if (employee == null)
        {
            Console.WriteLine("Employee not found.");
            return;
        }

        Console.WriteLine($"ID: {employee.Id}");
        Console.WriteLine($"Name: {employee.Name}");
        Console.WriteLine($"Age: {employee.Age}");
        Console.WriteLine($"Department ID: {employee.DepartmentId}");
    }

    public void UpdateEmployee(int id, string name, int age)
    {
        Employee? employee = employees.FirstOrDefault(e => e.Id == id);

        if (employee == null)
        {
            Console.WriteLine("Employee not found.");
            return;
        }

        employee.Name = name;
        employee.Age = age;

        Console.WriteLine("Employee updated successfully.");
    }

    public void DeleteEmployee(int id)
    {
        Employee? employee = employees.FirstOrDefault(e => e.Id == id);

        if (employee == null)
        {
            Console.WriteLine("Employee not found.");
            return;
        }

        employees.Remove(employee);
        Console.WriteLine("Employee deleted successfully.");
    }
}
