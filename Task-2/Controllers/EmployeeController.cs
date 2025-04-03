using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Task_2.Data;
using Task_2.Models;

namespace Task_2.Controllers
{
    public class EmployeeController : Controller
    {
        private DataAccess dataAccess = new DataAccess();

        // GET: Employee
        public ActionResult Index()
        {
            List<Employee> list = dataAccess.getEmployees();
            return View(list);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                dataAccess.InsertRow(employee);
                return RedirectToAction("Index");
            }
            return View(employee);
        }
        public ActionResult GetSalary()
        {
            decimal totalSalaries = dataAccess.GetTotalSalaries();
            ViewBag.TotalSalaries = totalSalaries;
            return View();
        }
        public ActionResult GetEmployeesDOBLessThan2000()
        {
            List<Employee> employees = dataAccess.GetEmployeesDOBLessThan2000();
            return View(employees);
        }
        public ActionResult GetCountEmployeesMiddleNameNull()
        {
            int count = dataAccess.CountEmployeesMiddleNameNull();
            ViewBag.Count = count;
            return View();
        }
    }
}