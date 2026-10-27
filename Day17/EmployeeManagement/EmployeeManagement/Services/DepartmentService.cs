using System;
using System.Collections.Generic;
using System.Text;

using EmployeeManagement.Models;

namespace EmployeeManagement.Services;

public class DepartmentService
{
    private readonly List<Department> departments = new();

    public void AddDepartment(Department department)
    {
        if (departments.Any(d => d.Id == department.Id))
        {
            Console.WriteLine("Department ID already exists.");
            return;
        }

        departments.Add(department);
        Console.WriteLine("Department added successfully.");
    }

    public void ViewDepartments()
    {
        if (departments.Count == 0)
        {
            Console.WriteLine("No departments found.");
            return;
        }

        foreach (Department department in departments)
        {
            Console.WriteLine(
                $"ID: {department.Id}, Name: {department.Name}");
        }
    }

    public void SearchDepartment(int id)
    {
        Department? department =
            departments.FirstOrDefault(d => d.Id == id);

        if (department == null)
        {
            Console.WriteLine("Department not found.");
            return;
        }

        Console.WriteLine($"ID: {department.Id}");
        Console.WriteLine($"Name: {department.Name}");
    }

    public void UpdateDepartment(int id, string name)
    {
        Department? department =
            departments.FirstOrDefault(d => d.Id == id);

        if (department == null)
        {
            Console.WriteLine("Department not found.");
            return;
        }

        department.Name = name;
        Console.WriteLine("Department updated successfully.");
    }

    public void DeleteDepartment(int id)
    {
        Department? department =
            departments.FirstOrDefault(d => d.Id == id);

        if (department == null)
        {
            Console.WriteLine("Department not found.");
            return;
        }

        departments.Remove(department);
        Console.WriteLine("Department deleted successfully.");
    }
}
