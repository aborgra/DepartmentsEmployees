using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using DepartmentsEmployees.Models;

namespace DepartmentsEmployees.Data
{
    /// <summary>
    ///  An object to contain all database interactions.
    /// </summary>
    public class EmployeeRepository
    {
        /// <summary>
        ///  Represents a connection to the database.
        ///   This is a "tunnel" to connect the application to the database.
        ///   All communication between the application and database passes through this connection.
        /// </summary>
        public SqlConnection Connection
        {
            get
            {
                // This is "address" of the database
                string _connectionString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=DepartmentsEmployees;Integrated Security=True";
                return new SqlConnection(_connectionString);
            }
        }
        /// <summary>
        ///  Returns a list of all departments in the database
        /// </summary>
        public List<Employee> GetAllEmployees()
        {
            //  We must "use" the database connection.
            //  Because a database is a shared resource (other applications may be using it too) we must
            //  be careful about how we interact with it. Specifically, we Open() connections when we need to
            //  interact with the database and we Close() them when we're finished.
            //  In C#, a "using" block ensures we correctly disconnect from a resource even if there is an error.
            //  For database connections, this means the connection will be properly closed.
            using (SqlConnection conn = Connection)
            {
                // Note, we must Open() the connection, the "using" block   doesn't do that for us.
                conn.Open();

                // We must "use" commands too.
                using SqlCommand cmd = conn.CreateCommand();
                // Here we setup the command with the SQL we want to execute before we execute it.
                cmd.CommandText = "SELECT Id, firstName, lastName, departmentId FROM Employee";

                // Execute the SQL in the database and get a "reader" that will give us access to the data.
                SqlDataReader reader = cmd.ExecuteReader();

                // A list to hold the departments we retrieve from the database.
                List<Employee> employees = new List<Employee>();

                // Read() will return true if there's more data to read
                while (reader.Read())
                {
                    // The "ordinal" is the numeric position of the column in the query results.
                    //  For our query, "Id" has an ordinal value of 0 and "DeptName" is 1.
                    int idColumnPosition = reader.GetOrdinal("Id");

                    // We user the reader's GetXXX methods to get the value for a particular ordinal.
                    int idValue = reader.GetInt32(idColumnPosition);

                    int firstNameColumnPosition = reader.GetOrdinal("firstName");
                    string firstNameValue = reader.GetString(firstNameColumnPosition);

                    int lastNameColumnPosition = reader.GetOrdinal("lastName");
                    string lastNameValue = reader.GetString(lastNameColumnPosition);

                    int departmentIdColumnPosition = reader.GetOrdinal("departmentId");
                    int departmentIdValue = reader.GetInt32(departmentIdColumnPosition);


                    // Now let's create a new department object using the data from the database.
                    Employee employee = new Employee
                    {
                        Id = idValue,
                        FirstName = firstNameValue,
                        LastName = lastNameValue,
                        DepartmentId = departmentIdValue,
                        Department = null
                    };

                    // ...and add that department object to our list.
                    employees.Add(employee);
                }

                // We should Close() the reader. Unfortunately, a "using" block won't work here.
                reader.Close();

                // Return the list of departments who whomever called this method.
                return employees;
            }
        }

        public List<Employee> GetAllEmployeesWithDepartment()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT e.Id, e.firstName, e.lastName, e.departmentId, d.deptName as 'Department Name' FROM Employee e LEFT JOIN Department d on e.departmentId = d.Id";
                    //cmd.Parameters.Add(new SqlParameter("@Id", id));
                    SqlDataReader reader = cmd.ExecuteReader();
                    List<Employee> allDepartmentEmployees = new List<Employee>();

                    Employee employee = null;

                    // If we only expect a single row back from the database, we don't need a while loop.
                    while (reader.Read())

                        {
                            int idColumnPosition = reader.GetOrdinal("Id");
                            int IdValue = reader.GetInt32(idColumnPosition);

                            int firstNameColumnPosition = reader.GetOrdinal("firstName");
                            string firstNameValue = reader.GetString(firstNameColumnPosition);

                            int lastNameColumnPosition = reader.GetOrdinal("lastName");
                            string lastNameValue = reader.GetString(lastNameColumnPosition);

                            int departmentColumnPosition = reader.GetOrdinal("Department Name");
                            string departmentValue = reader.GetString(departmentColumnPosition);

                            int departmentIdColumnPosition = reader.GetOrdinal("departmentId");
                            int departmentIdValue = reader.GetInt32(departmentIdColumnPosition);

                            employee = new Employee
                            {
                                Id = IdValue,
                                FirstName = firstNameValue,
                                LastName = lastNameValue,
                                DepartmentId = departmentIdValue,
                                Department = new Department
                                {
                                    DeptName = departmentValue,
                                    Id = departmentIdValue
                                }

                            };

                            allDepartmentEmployees.Add(employee);
                        
                     
                    }
                 
                    reader.Close();

                    return allDepartmentEmployees;
                }
            }
        }
    
            
        
           


        /// <summary>
        ///  Returns a single department with the given id.
        /// </summary>
        public Employee GetEmployeeById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT e.Id, e.firstName, e.lastName, e.departmentId, d.deptName as 'Department Name' FROM Employee e LEFT JOIN Department d on e.departmentId = d.Id WHERE e.Id = @id";
                    cmd.Parameters.Add(new SqlParameter("@id", id));
                    SqlDataReader reader = cmd.ExecuteReader();

                    Employee employee = null;

                    // If we only expect a single row back from the database, we don't need a while loop.
                    if (reader.Read())

                    {
                        int idColumnPosition = reader.GetOrdinal("Id");
                        int IdValue = reader.GetInt32(idColumnPosition);

                        int firstNameColumnPosition = reader.GetOrdinal("firstName");
                        string firstNameValue = reader.GetString(firstNameColumnPosition);

                        int lastNameColumnPosition = reader.GetOrdinal("lastName");
                        string lastNameValue = reader.GetString(lastNameColumnPosition);

                        int departmentColumnPosition = reader.GetOrdinal("Department Name");
                        string departmentValue = reader.GetString(departmentColumnPosition);

                        int departmentIdColumnPosition = reader.GetOrdinal("departmentId");
                        int departmentIdValue = reader.GetInt32(departmentIdColumnPosition);

                        employee = new Employee
                        {
                            Id = IdValue,
                            FirstName = firstNameValue,
                            LastName = lastNameValue,
                            DepartmentId = departmentIdValue,
                            Department = new Department{
                                DeptName =departmentValue,
                                Id = departmentIdValue
                            }

                        };
                    }

                    reader.Close();

                    return employee;
                }
            }
        }
        /// <summary>
        ///  Add a new department to the database
        ///   NOTE: This method sends data to the database,
        ///   it does not get anything from the database, so there is nothing to return.
        /// </summary>
        public void AddEmployee(Employee employee)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    // These SQL parameters are annoying. Why can't we use string interpolation?
                    // ... sql injection attacks!!!
                    // sqlCommand allows you to write the code that will be executed by the sql server
                    cmd.CommandText = "INSERT INTO Employee (firstName, lastName, departmentId) OUTPUT INSERTED.Id Values (@firstName, @lastName, @departmentId)";
                    cmd.Parameters.Add(new SqlParameter("@firstName", employee.FirstName));
                    cmd.Parameters.Add(new SqlParameter("@lastName", employee.LastName));
                    cmd.Parameters.Add(new SqlParameter("@departmentId", employee.DepartmentId));

                    int id = (int)cmd.ExecuteScalar();

                    employee.Id = id;
                }
            }

            // when this method is finished we can look in the database and see the new department.
        }
        /// <summary>
        ///  Updates the department with the given id
        /// </summary>
        public void UpdateEmployee(int id, Employee employee)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE Employee
                                     SET FirstName = @firstName,
                                        LastName = @lastName,
                                        DepartmentId = @departmentId
                                     WHERE Id = @id";
                    cmd.Parameters.Add(new SqlParameter("@firstName", employee.FirstName));
                    cmd.Parameters.Add(new SqlParameter("@lastName", employee.LastName));
                    cmd.Parameters.Add(new SqlParameter("@departmentId", employee.DepartmentId));
                    cmd.Parameters.Add(new SqlParameter("@id", id));

                    cmd.ExecuteNonQuery();
                }
            }
        }
        /// <summary>
        ///  Delete the department with the given id
        /// </summary>
        public void DeleteEmployee(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM Employee WHERE Id = @id";
                    cmd.Parameters.Add(new SqlParameter("@id", id));
                    cmd.ExecuteNonQuery();
                }
            }
        }

    }
}