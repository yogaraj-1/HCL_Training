
using Microsoft.Data.SqlClient;
using EmployeeManagement.Data;
using EmployeeManagement.Models;

namespace EmployeeManagement.Repositories;

public class EmployeeRepository
{
    private readonly DbConnectionFactory connectionFactory;

    public EmployeeRepository()
    {
        connectionFactory = new DbConnectionFactory();
    }

    // CREATE: Add an employee
    public void AddEmployee(Employee employee)
    {
        string query = @"
            INSERT INTO Employees
                (EmployeeId, EmployeeName, Age, DepartmentId)
            VALUES
                (@Id, @Name, @Age, @DepartmentId)";

        try
        {
            using SqlConnection connection =
                connectionFactory.CreateConnection();

            using SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@Id", employee.Id);
            command.Parameters.AddWithValue("@Name", employee.Name);
            command.Parameters.AddWithValue("@Age", employee.Age);
            command.Parameters.AddWithValue(
                "@DepartmentId", employee.DepartmentId);

            connection.Open();
            command.ExecuteNonQuery();

            Console.WriteLine("Employee added successfully.");
        }
        catch (SqlException ex) when (ex.Number == 547)
        {
            Console.WriteLine(
                "Cannot add employee. The specified department does not exist, " +
                "or another foreign key constraint was violated.");
        }
        catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
        {
            Console.WriteLine("Employee ID already exists.");
        }
        catch (SqlException)
        {
            Console.WriteLine("A database error occurred while adding the employee.");
        }
    }

    // READ: Get all employees
    public List<Employee> GetAllEmployees()
    {
        List<Employee> employees = new();

        string query = @"
            SELECT EmployeeId, EmployeeName, Age, DepartmentId
            FROM Employees";

        using SqlConnection connection =
            connectionFactory.CreateConnection();

        using SqlCommand command = new SqlCommand(query, connection);

        connection.Open();

        using SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            employees.Add(new Employee
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
                Age = reader.GetInt32(2),
                DepartmentId = reader.GetInt32(3)
            });
        }

        return employees;
    }

    // READ: Get one employee by ID
    public Employee? GetEmployeeById(int id)
    {
        string query = @"
            SELECT EmployeeId, EmployeeName, Age, DepartmentId
            FROM Employees
            WHERE EmployeeId = @Id";

        using SqlConnection connection =
            connectionFactory.CreateConnection();

        using SqlCommand command = new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@Id", id);

        connection.Open();

        using SqlDataReader reader = command.ExecuteReader();

        if (reader.Read())
        {
            return new Employee
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
                Age = reader.GetInt32(2),
                DepartmentId = reader.GetInt32(3)
            };
        }

        return null;
    }

    // UPDATE: Modify employee details
    public bool UpdateEmployee(Employee employee)
    {
        string query = @"
            UPDATE Employees
            SET EmployeeName = @Name,
                Age = @Age,
                DepartmentId = @DepartmentId
            WHERE EmployeeId = @Id";

        using SqlConnection connection =
            connectionFactory.CreateConnection();

        using SqlCommand command = new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@Id", employee.Id);
        command.Parameters.AddWithValue("@Name", employee.Name);
        command.Parameters.AddWithValue("@Age", employee.Age);
        command.Parameters.AddWithValue(
            "@DepartmentId", employee.DepartmentId);

        connection.Open();

        return command.ExecuteNonQuery() > 0;
    }

    // DELETE: Remove an employee
    public bool DeleteEmployee(int id)
    {
        string query = @"
            DELETE FROM Employees
            WHERE EmployeeId = @Id";

        using SqlConnection connection =
            connectionFactory.CreateConnection();

        using SqlCommand command = new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@Id", id);

        connection.Open();

        return command.ExecuteNonQuery() > 0;
    }
}
