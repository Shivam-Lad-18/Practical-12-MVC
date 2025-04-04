using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Task_3.Models
{
    public class EmployeeDesignation
    {
        //First Name, Middle Name, Last Name & Designation name
        public string FirstName { get; set; } // Varchar(50), Not Null
        public string MiddleName { get; set; } // Varchar(50), Null Allowed
        public string LastName { get; set; } // Varchar(50), Not Null
        public string DesignationName { get; set; } // Varchar(50), Not Null
    }
}