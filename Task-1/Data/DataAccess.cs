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
using Task_1.Models;
using System.Data.SqlClient;
namespace Task_1.Data
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
                string query = "SELECT FirstName, MiddleName, LastName, Address, DOB, MobileNumber FROM Employee";

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
                                MobileNumber = reader["MobileNumber"].ToString()
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
                string query = "INSERT INTO Employee (FirstName, MiddleName, LastName, Address, DOB, MobileNumber) VALUES (@FirstName, @MiddleName, @LastName, @Address, @DOB, @MobileNumber)";
                using (DbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = query;
                    cmd.Parameters.Add(new SqlParameter("@FirstName", e.FirstName));
                    cmd.Parameters.Add(new SqlParameter("@MiddleName", e.MiddleName));
                    cmd.Parameters.Add(new SqlParameter("@LastName", e.LastName));
                    cmd.Parameters.Add(new SqlParameter("@Address", e.Address));
                    cmd.Parameters.Add(new SqlParameter("@DOB", e.DOB));
                    cmd.Parameters.Add(new SqlParameter("@MobileNumber", e.MobileNumber));
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdateRow()
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                string query = "UPDATE Employee SET FirstName = 'SQLPerson' WHERE Id = 1";
                using (DbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = query;
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void UpdateRowAll()
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                string query = "UPDATE Employee SET MiddleName = 'I'";
                using (DbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = query;
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void DeleteRow()
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                string query = "DELETE FROM Employee WHERE Id < 2";
                using (DbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = query;
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void DeleteAll()
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                string query = "DELETE FROM Employee";
                using (DbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = query;
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}