
using Microsoft.Data.SqlClient;
using EmployeeManagement.Data;
using EmployeeManagement.Models;

namespace EmployeeManagement.Repositories;

public class DepartmentRepository
{
    private readonly DbConnectionFactory connectionFactory;

    public DepartmentRepository()
    {
        connectionFactory = new DbConnectionFactory();
    }

    // CREATE
    public void AddDepartment(Department department)
    {
        string query = @"
            INSERT INTO Departments (DepartmentId, DepartmentName)
            VALUES (@Id, @Name)";

        using SqlConnection connection =
            connectionFactory.CreateConnection();

        using SqlCommand command = new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@Id", department.Id);
        command.Parameters.AddWithValue("@Name", department.Name);

        connection.Open();
        command.ExecuteNonQuery();

        Console.WriteLine("Department added successfully.");
    }

    // READ ALL
    public List<Department> GetAllDepartments()
    {
        List<Department> departments = new();

        string query = @"
            SELECT DepartmentId, DepartmentName
            FROM Departments";

        using SqlConnection connection =
            connectionFactory.CreateConnection();

        using SqlCommand command = new SqlCommand(query, connection);

        connection.Open();

        using SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            departments.Add(new Department
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1)
            });
        }

        return departments;
    }

    // READ ONE
    public Department? GetDepartmentById(int id)
    {
        string query = @"
            SELECT DepartmentId, DepartmentName
            FROM Departments
            WHERE DepartmentId = @Id";

        using SqlConnection connection =
            connectionFactory.CreateConnection();

        using SqlCommand command = new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@Id", id);

        connection.Open();

        using SqlDataReader reader = command.ExecuteReader();

        if (reader.Read())
        {
            return new Department
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1)
            };
        }

        return null;
    }

    // UPDATE
    public bool UpdateDepartment(Department department)
    {
        string query = @"
            UPDATE Departments
            SET DepartmentName = @Name
            WHERE DepartmentId = @Id";

        using SqlConnection connection =
            connectionFactory.CreateConnection();

        using SqlCommand command = new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@Id", department.Id);
        command.Parameters.AddWithValue("@Name", department.Name);

        connection.Open();

        return command.ExecuteNonQuery() > 0;
    }

    // DELETE
    public bool DeleteDepartment(int id)
    {
        string query = @"
            DELETE FROM Departments
            WHERE DepartmentId = @Id";

        using SqlConnection connection =
            connectionFactory.CreateConnection();

        using SqlCommand command = new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@Id", id);

        connection.Open();

        return command.ExecuteNonQuery() > 0;
    }
}

