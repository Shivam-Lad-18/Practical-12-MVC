using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Xml.Linq;
using Task_3.Models;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Ajax.Utilities;
using System.Security.Cryptography;
namespace Task_3.Data
{
    public class DataAccess
    {
        private string connString;
        public DataAccess()
        {
            connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }
        public List<Employee> getEmployees()
        {
            List<Employee> employees = new List<Employee>();
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                string query = "SELECT FirstName, MiddleName, LastName, Address, DOB, MobileNumber,Salary,DesignationId FROM Employee2";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            employees.Add(new Employee
                            {
                                FirstName = reader["FirstName"].ToString(),
                                MiddleName = reader.IsDBNull(reader.GetOrdinal("MiddleName")) ? null : reader["MiddleName"].ToString(),
                                LastName = reader["LastName"].ToString(),
                                Address = reader["Address"].ToString(),
                                DOB = reader.GetDateTime(reader.GetOrdinal("DOB")), // Reads as DateTime
                                MobileNumber = reader["MobileNumber"].ToString(),
                                Salary = reader.GetDecimal(reader.GetOrdinal("Salary")), // Reads as Decimal
                                DesignationId = reader.GetInt32(reader.GetOrdinal("DesignationId")) // Reads as Int
                            });
                        }
                    }
                }
            }
            return employees;
        }
        public List<Designation> getDesignations()
        {
            List<Designation> designations = new List<Designation>();
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                string query = "SELECT Id,DesignationName FROM Designation";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            designations.Add(new Designation() { Id = (int)reader["Id"], DesignationName = reader["DesignationName"].ToString() });
                        }
                    }
                }
            }
            return designations;
        }
        public void InsertEmployee(Employee e)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                string query = "INSERT INTO Employee2 (FirstName, MiddleName, LastName, Address, DOB, MobileNumber,Salary,DesignationId) VALUES (@FirstName, @MiddleName, @LastName, @Address, @DOB, @MobileNumber,@Salary,@DesignationId)";
                using (DbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = query;
                    cmd.Parameters.Add(new SqlParameter("@FirstName", e.FirstName));
                    cmd.Parameters.Add(new SqlParameter("@MiddleName", e.MiddleName));
                    cmd.Parameters.Add(new SqlParameter("@LastName", e.LastName));
                    cmd.Parameters.Add(new SqlParameter("@Address", e.Address));
                    cmd.Parameters.Add(new SqlParameter("@DOB", e.DOB));
                    cmd.Parameters.Add(new SqlParameter("@MobileNumber", e.MobileNumber));
                    cmd.Parameters.Add(new SqlParameter("@Salary", e.Salary));
                    cmd.Parameters.Add(new SqlParameter("@DesignationId", e.DesignationId));
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void InsertDesignation(Designation d)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                string query = "INSERT INTO Designation (DesignationName) VALUES (@DesignationName)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@DesignationName", SqlDbType.NVarChar, 50) { Value = d.DesignationName });
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Employee> getEmployeeWithDesignation()
        {
            List<Employee> employees = new List<Employee>();
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                string query = "SELECT e.FirstName, e.MiddleName, e.LastName, d.DesignationName FROM Employee2 e JOIN Designation d ON e.DesignationId = d.Id";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            employees.Add(new Employee
                            {
                                FirstName = reader["FirstName"].ToString(),
                                MiddleName = reader.IsDBNull(reader.GetOrdinal("MiddleName")) ? null : reader["MiddleName"].ToString(),
                                LastName = reader["LastName"].ToString(),
                                DesignationId = reader.GetInt32(reader.GetOrdinal("DesignationId")) // Reads as Int
                            });
                        }
                    }
                }
            }
            return employees;
        }
        public int getDesignationCount(string DesignationName)
        {
            int count = 0;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM Employee2 e JOIN Designation d ON e.DesignationId = d.Id WHERE d.DesignationName = @DesignationName";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@DesignationName", DesignationName));
                    count = (int)cmd.ExecuteScalar();
                }
            }
            return count;
        }
        public List<string> getDesignationWithMoreThanOneEmployee()
        {
            List<string> designations = new List<string>();
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                string query = "SELECT d.DesignationName FROM Employee2 e JOIN Designation d ON e.DesignationId = d.Id GROUP BY d.DesignationName HAVING COUNT(e.Id) > 1";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            designations.Add(reader["DesignationName"].ToString());
                        }
                    }
                }
            }
            return designations;
        }
        public Employee getEmployeeWithMaxSalary()
        {
            Employee employee = null;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                string query = "SELECT TOP 1 * FROM Employee2 ORDER BY Salary DESC";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            employee = new Employee
                            {
                                FirstName = reader["FirstName"].ToString(),
                                MiddleName = reader.IsDBNull(reader.GetOrdinal("MiddleName")) ? null : reader["MiddleName"].ToString(),
                                LastName = reader["LastName"].ToString(),
                                Address = reader["Address"].ToString(),
                                DOB = reader.GetDateTime(reader.GetOrdinal("DOB")), // Reads as DateTime
                                MobileNumber = reader["MobileNumber"].ToString(),
                                Salary = reader.GetDecimal(reader.GetOrdinal("Salary")), // Reads as Decimal
                                DesignationId = reader.GetInt32(reader.GetOrdinal("DesignationId")) // Reads as Int
                            };
                        }
                    }
                }
            }
            return employee;
        }
    }
}