using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManagement.Models;

public class Employee
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Age { get; set; }
    public int DepartmentId { get; set; }
}