using System;
using System.Collections.Generic;
using System.Data.SqlClient;

class AdoExample
{
    const string connectionString =
        @"Server=10.148.73.5;Database=SoftUni;User=sa;Password=Q1w2e3r4";
    static void Main(string[] args)
    {
        SqlConnection connection;
        IList<Employee> newEmployee = new List<Employee>();

        using (connection = new SqlConnection(connectionString))
        {
            connection.Open();  
            
            SqlCommand command; 
            SqlCommand command2;

            using (command = new SqlCommand())
            {
                command.CommandText  = "SELECT TOP 10 * FROM Employees";
                command.Connection = connection;

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        newEmployee.Add(
                            new Employee()
                            {
                                FirstName = reader["FirstName"].ToString(),
                                MiddleName = reader["MiddleName"].ToString(),
                                LastName = reader["LastName"].ToString()
                            }
                        );
                    }
                }

                //parameterized queries to Preventing SQL Injection

                using (command2 = new SqlCommand())
                {
                    command2.CommandText = "SELECT * FROM Employees WHERE DepartmentID = @depId";
                    command2.Connection = connection;
                    command2.Parameters.AddWithValue("@depId", 1);

                    using (SqlDataReader reader = command2.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                           Console.WriteLine(reader["FirstName"].ToString()
                               + reader["MiddleName"].ToString()
                               + reader["LastName"].ToString());                           
                        }
                    }
                }
            }

            Console.WriteLine();
            foreach (var employee in newEmployee)
            {
                System.Console.Write(employee.FirstName);
                System.Console.Write(" ");
                System.Console.Write(employee.MiddleName);
                System.Console.Write(" ");
                System.Console.Write(employee.LastName);
                System.Console.WriteLine();
            }
        }
    }        
 }

