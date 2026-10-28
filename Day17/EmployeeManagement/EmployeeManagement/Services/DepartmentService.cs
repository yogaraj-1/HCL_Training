
using EmployeeManagement.Models;
using EmployeeManagement.Repositories;

namespace EmployeeManagement.Services;

public class DepartmentService
{
    private readonly DepartmentRepository repository;

    public DepartmentService()
    {
        repository = new DepartmentRepository();
    }

    public void AddDepartment(Department department)
    {
        if (department.Id <= 0 ||
            string.IsNullOrWhiteSpace(department.Name))
        {
            Console.WriteLine("Invalid department details.");
            return;
        }

        repository.AddDepartment(department);
    }

    public void ViewDepartments()
    {
        List<Department> departments = repository.GetAllDepartments();

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
        Department? department = repository.GetDepartmentById(id);

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
        if (id <= 0 || string.IsNullOrWhiteSpace(name))
        {
            Console.WriteLine("Invalid department details.");
            return;
        }

        bool updated = repository.UpdateDepartment(new Department
        {
            Id = id,
            Name = name
        });

        Console.WriteLine(updated
            ? "Department updated successfully."
            : "Department not found.");
    }

    public void DeleteDepartment(int id)
    {
        if (id <= 0)
        {
            Console.WriteLine("Invalid department ID.");
            return;
        }

        bool deleted = repository.DeleteDepartment(id);

        Console.WriteLine(deleted
            ? "Department deleted successfully."
            : "Department not found.");
    }
}
