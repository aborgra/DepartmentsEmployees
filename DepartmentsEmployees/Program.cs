using DepartmentsEmployees.Data;
using DepartmentsEmployees.Models;
using System;
using System.Collections.Generic;

namespace DepartmentsEmployees
{
    class Program
    {
        static void Main(string[] args)
        {


            Console.WriteLine("What would you like to do?");
            Console.WriteLine("");
            Console.WriteLine("1. Show all departments");
            Console.WriteLine("2. Show specific department by Id");
            Console.WriteLine("3. Add new department");
            Console.WriteLine("4. Update department information");
            Console.WriteLine("5. Delete department by Id");
            Console.WriteLine("6. Show all employees");
            Console.WriteLine("7. Show specific employee by Id");
            Console.WriteLine("8. Show all employees by department");
            Console.WriteLine("9. Add new employee");
            Console.WriteLine("10. Update employee information");
            Console.WriteLine("11. Delete employee by Id");

            var choice = Console.ReadLine();
            DepartmentRepository departmentRepo = new DepartmentRepository();
            EmployeeRepository employeeRepo = new EmployeeRepository();

            switch (Int32.Parse(choice))
            {
                case 1:

                    Console.WriteLine("Getting All Departments:");
                    Console.WriteLine();

                    List<Department> allDepartments = departmentRepo.GetAllDepartments();

                    foreach (Department dept in allDepartments)
                    {
                        Console.WriteLine($"{dept.Id} {dept.DeptName}");
                    }
                    break;

                case 2:
                    Console.WriteLine("Enter Id of department");
                    var departmentChoice = int.Parse(Console.ReadLine());
                    Console.WriteLine($"Getting department with Id of {departmentChoice}");
                    Department singleDepartment = departmentRepo.GetDepartmentById(departmentChoice);

                    Console.WriteLine($"{singleDepartment.Id} {singleDepartment.DeptName}");
                    break;

                case 3:
                    Console.WriteLine("Enter the name of the new department");
                    var departmentName = Console.ReadLine();
                    Department newDepartment = new Department
                    {
                        DeptName = departmentName
                    };

                    departmentRepo.AddDepartment(newDepartment);
                    Console.WriteLine($"Added the new {departmentName} Department!");
                    break;

                case 4:
                    Console.WriteLine("Update Department Info");
                    //TBD
                    break;

                case 5:
                    Console.WriteLine("Enter the Id of the department you would like to delete");
                    var deleteDeptChoice =int.Parse(Console.ReadLine());

                    departmentRepo.DeleteDepartment(deleteDeptChoice);
                    Console.WriteLine($"Department with the Id of{deleteDeptChoice} has been deleted.");
                    break;

                case 6:
                    Console.WriteLine("Getting All Employees:");
                    Console.WriteLine();

                    List<Employee> allEmployees = employeeRepo.GetAllEmployees();

                    foreach (Employee emp in allEmployees)
                    {
                        Console.WriteLine($"{emp.FirstName} {emp.LastName}");
                    }

                    break;
                case 7:
                    Console.WriteLine("Enter the Id of the employee you would like to view");
                    var chosenEmployeeId = int.Parse(Console.ReadLine());
                    Employee singleEmployee = employeeRepo.GetEmployeeById(chosenEmployeeId);

                    Console.WriteLine($"Getting Employee with Id of {chosenEmployeeId}.");

                    Console.WriteLine($"{singleEmployee.Id} {singleEmployee.FirstName} {singleEmployee.LastName}");
                    break;

                case 8:
                    List<Employee> allDepartmentEmployees = employeeRepo.GetAllEmployeesWithDepartment();

                    foreach (Employee emp in allDepartmentEmployees)
                    {
                        Console.WriteLine($"{emp.FirstName} {emp.LastName} - {emp.Department.DeptName}");
                    }
                    break;

                case 9:
                    Console.WriteLine("Enter new employee's first name");
                    var newEmployeeFirstName = Console.ReadLine();
                    Console.WriteLine("Enter new employee's last name");
                    var newEmployeeLastName = Console.ReadLine();
                    Console.WriteLine("Enter new employee's department Id");
                    var newEmployeeDeptId = int.Parse(Console.ReadLine());

                    Employee newEmployee = new Employee
                    {
                        FirstName = newEmployeeFirstName,
                        LastName = newEmployeeLastName,
                        DepartmentId = newEmployeeDeptId
                    };

                    employeeRepo.AddEmployee(newEmployee);
                    Console.WriteLine($"Added {newEmployee.FirstName} {newEmployee.LastName} as a new employee!");
                    break;

                case 10:
                    Console.WriteLine("Update Employee Info");
                    //TBD
                    break;

                case 11:
                    Console.WriteLine("Enter the Id of the employee you would like to delete");
                    var deleteEmployeeId = int.Parse(Console.ReadLine());
                    employeeRepo.DeleteEmployee(deleteEmployeeId);
                    Console.WriteLine($"Employee with the Id of {deleteEmployeeId} has been deleted");
                    break;

                default:
                    break;
            }





        }
    }
}
