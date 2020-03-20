using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DepartmentsEmployees.Models
{
    // C# representation of the Department table
    public class Department
    {
        public int Id { get; set; }
        public string DeptName { get; set; }

        public List<Employee> Employees { get; set; }
    }
}