
using Microsoft.Data.SqlClient;
using EmployeeManagement.Data;
using EmployeeManagement.Models;

namespace EmployeeManagement.Repositories;

public class SalaryRepository
{
    private readonly DbConnectionFactory connectionFactory;

    public SalaryRepository()
    {
        connectionFactory = new DbConnectionFactory();
    }

    // CREATE
    public void AddSalary(Salary salary)
    {
        string query = @"
            INSERT INTO Salaries (EmployeeId, BasicSalary, Bonus)
            VALUES (@EmployeeId, @BasicSalary, @Bonus)";

        using SqlConnection connection =
            connectionFactory.CreateConnection();

        using SqlCommand command = new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@EmployeeId", salary.EmployeeId);
        command.Parameters.AddWithValue("@BasicSalary", salary.BasicSalary);
        command.Parameters.AddWithValue("@Bonus", salary.Bonus);

        connection.Open();
        command.ExecuteNonQuery();

        Console.WriteLine("Salary added successfully.");
    }

    // READ ALL
    public List<Salary> GetAllSalaries()
    {
        List<Salary> salaries = new();

        string query = @"
            SELECT EmployeeId, BasicSalary, Bonus
            FROM Salaries";

        using SqlConnection connection =
            connectionFactory.CreateConnection();

        using SqlCommand command = new SqlCommand(query, connection);

        connection.Open();

        using SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            salaries.Add(new Salary
            {
                EmployeeId = reader.GetInt32(0),
                BasicSalary = reader.GetDecimal(1),
                Bonus = reader.GetDecimal(2)
            });
        }

        return salaries;
    }

    // READ ONE
    public Salary? GetSalaryByEmployeeId(int employeeId)
    {
        string query = @"
            SELECT EmployeeId, BasicSalary, Bonus
            FROM Salaries
            WHERE EmployeeId = @EmployeeId";

        using SqlConnection connection =
            connectionFactory.CreateConnection();

        using SqlCommand command = new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@EmployeeId", employeeId);

        connection.Open();

        using SqlDataReader reader = command.ExecuteReader();

        if (reader.Read())
        {
            return new Salary
            {
                EmployeeId = reader.GetInt32(0),
                BasicSalary = reader.GetDecimal(1),
                Bonus = reader.GetDecimal(2)
            };
        }

        return null;
    }

    // UPDATE
    public bool UpdateSalary(Salary salary)
    {
        string query = @"
            UPDATE Salaries
            SET BasicSalary = @BasicSalary,
                Bonus = @Bonus
            WHERE EmployeeId = @EmployeeId";

        using SqlConnection connection =
            connectionFactory.CreateConnection();

        using SqlCommand command = new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@EmployeeId", salary.EmployeeId);
        command.Parameters.AddWithValue("@BasicSalary", salary.BasicSalary);
        command.Parameters.AddWithValue("@Bonus", salary.Bonus);

        connection.Open();

        return command.ExecuteNonQuery() > 0;
    }

    // DELETE
    public bool DeleteSalary(int employeeId)
    {
        string query = @"
            DELETE FROM Salaries
            WHERE EmployeeId = @EmployeeId";

        using SqlConnection connection =
            connectionFactory.CreateConnection();

        using SqlCommand command = new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@EmployeeId", employeeId);

        connection.Open();

        return command.ExecuteNonQuery() > 0;
    }
}
