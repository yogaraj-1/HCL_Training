using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManagement.Models;

public class Salary
{
    public int EmployeeId { get; set; }
    public decimal BasicSalary { get; set; }
    public decimal Bonus { get; set; }

    public decimal TotalSalary => BasicSalary + Bonus;
}
