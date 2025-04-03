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
using Task_2.Models;
using System.Data.SqlClient;
namespace Task_2.Data
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
                string query = "SELECT FirstName, MiddleName, LastName, Address, DOB, MobileNumber,Salary FROM Employee1";

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
                                Salary = reader.GetDecimal(reader.GetOrdinal("Salary")) // Reads as Decimal
                            });
                        }
                    }
                }
            }
            return employees;
        }
        public void InsertRow(Employee e)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                string query = "INSERT INTO Employee1 (FirstName, MiddleName, LastName, Address, DOB, MobileNumber,Salary) VALUES (@FirstName, @MiddleName, @LastName, @Address, @DOB, @MobileNumber,@Salary)";
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
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public decimal GetTotalSalaries()
        {
            decimal totalSalaries = 0;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                string query = "SELECT SUM(Salary) FROM Employee1";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    totalSalaries = (decimal)cmd.ExecuteScalar();
                }
            }
            return totalSalaries;
        }
        public List<Employee> GetEmployeesDOBLessThan2000()
        {
            List<Employee> employees = new List<Employee>();
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                string query = "SELECT * FROM Employee1 WHERE DOB < '2000-01-01'";
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
                                DOB = reader.GetDateTime(reader.GetOrdinal("DOB")),
                                MobileNumber = reader["MobileNumber"].ToString(),
                                Salary = reader.GetDecimal(reader.GetOrdinal("Salary"))
                            });
                        }
                    }
                }
            }
            return employees;
        }
        public int CountEmployeesMiddleNameNull()
        {
            int count = 0;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM Employee1 WHERE MiddleName IS NULL";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    count = (int)cmd.ExecuteScalar();
                }
            }
            return count;
        }
    }
}