using System;
using System.Collections.Generic;
using System.Text;


using EmployeeManagement.Models;
using EmployeeManagement.Repositories;

namespace EmployeeManagement.Services;

public class EmployeeService
{
    private readonly EmployeeRepository repository;

    public EmployeeService()
    {
        repository = new EmployeeRepository();
    }

    public void AddEmployee(Employee employee)
    {
        if (employee.Id <= 0 ||
            string.IsNullOrWhiteSpace(employee.Name) ||
            employee.Age <= 0 ||
            employee.DepartmentId <= 0)
        {
            Console.WriteLine("Invalid employee details.");
            return;
        }

        repository.AddEmployee(employee);
    }

    public void ViewEmployees()
    {
        List<Employee> employees = repository.GetAllEmployees();

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
        Employee? employee = repository.GetEmployeeById(id);

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

    public void UpdateEmployee(int id, string name, int age, int departmentId)
    {
        if (id <= 0 ||
            string.IsNullOrWhiteSpace(name) ||
            age <= 0 ||
            departmentId <= 0)
        {
            Console.WriteLine("Invalid employee details.");
            return;
        }

        Employee employee = new Employee
        {
            Id = id,
            Name = name,
            Age = age,
            DepartmentId = departmentId
        };

        bool updated = repository.UpdateEmployee(employee);

        Console.WriteLine(updated
            ? "Employee updated successfully."
            : "Employee not found.");
    }

    public void DeleteEmployee(int id)
    {
        if (id <= 0)
        {
            Console.WriteLine("Invalid employee ID.");
            return;
        }

        bool deleted = repository.DeleteEmployee(id);

        Console.WriteLine(deleted
            ? "Employee deleted successfully."
            : "Employee not found.");
    }
}
