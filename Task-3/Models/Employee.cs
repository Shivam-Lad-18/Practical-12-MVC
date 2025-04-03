using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Task_3.Models
{
    public class Employee
    {
        public string FirstName { get; set; } // Varchar(50), Not Null
        public string MiddleName { get; set; } // Varchar(50), Null Allowed
        public string LastName { get; set; } // Varchar(50), Not Null
        public string Address { get; set; }
        public DateTime DOB { get; set; } // Date, Not Null
        public string MobileNumber { get; set; } // Varchar(10), Not Null
        public decimal Salary { get; set; } // Decimal(18,2), Not Null`
        public int DesignationId { get; set; } 
    }
}