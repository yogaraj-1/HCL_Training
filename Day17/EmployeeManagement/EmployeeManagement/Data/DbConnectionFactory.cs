using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Data.SqlClient;

namespace EmployeeManagement.Data;

public class DbConnectionFactory
{
    private readonly string connectionString =
        @"Server=.\SQLEXPRESS;Database=EmployeeManagementDb;Trusted_Connection=True;TrustServerCertificate=True;";

    public SqlConnection CreateConnection()
    {
        return new SqlConnection(connectionString);
    }
}
