
using EmployeeManagement.Models;
using EmployeeManagement.Repositories;

namespace EmployeeManagement.Services;

public class SalaryService
{
    private readonly SalaryRepository repository;

    public SalaryService()
    {
        repository = new SalaryRepository();
    }

    public void AddSalary(Salary salary)
    {
        if (salary.EmployeeId <= 0 ||
            salary.BasicSalary < 0 ||
            salary.Bonus < 0)
        {
            Console.WriteLine("Invalid salary details.");
            return;
        }

        repository.AddSalary(salary);
    }

    public void ViewSalaries()
    {
        List<Salary> salaries = repository.GetAllSalaries();

        if (salaries.Count == 0)
        {
            Console.WriteLine("No salary records found.");
            return;
        }

        foreach (Salary salary in salaries)
        {
            Console.WriteLine($"Employee ID: {salary.EmployeeId}");
            Console.WriteLine($"Basic Salary: {salary.BasicSalary:N2}");
            Console.WriteLine($"Bonus: {salary.Bonus:N2}");
            Console.WriteLine($"Total Salary: {salary.TotalSalary:N2}");
            Console.WriteLine("-------------------------");
        }
    }

    public void SearchSalary(int employeeId)
    {
        Salary? salary = repository.GetSalaryByEmployeeId(employeeId);

        if (salary == null)
        {
            Console.WriteLine("Salary record not found.");
            return;
        }

        Console.WriteLine($"Employee ID: {salary.EmployeeId}");
        Console.WriteLine($"Basic Salary: {salary.BasicSalary:N2}");
        Console.WriteLine($"Bonus: {salary.Bonus:N2}");
        Console.WriteLine($"Total Salary: {salary.TotalSalary:N2}");
    }

    public void UpdateSalary(
        int employeeId,
        decimal basicSalary,
        decimal bonus)
    {
        if (employeeId <= 0 || basicSalary < 0 || bonus < 0)
        {
            Console.WriteLine("Invalid salary details.");
            return;
        }

        Salary salary = new Salary
        {
            EmployeeId = employeeId,
            BasicSalary = basicSalary,
            Bonus = bonus
        };

        bool updated = repository.UpdateSalary(salary);

        Console.WriteLine(updated
            ? "Salary updated successfully."
            : "Salary record not found.");
    }

    public void DeleteSalary(int employeeId)
    {
        if (employeeId <= 0)
        {
            Console.WriteLine("Invalid employee ID.");
            return;
        }

        bool deleted = repository.DeleteSalary(employeeId);

        Console.WriteLine(deleted
            ? "Salary deleted successfully."
            : "Salary record not found.");
    }
}
