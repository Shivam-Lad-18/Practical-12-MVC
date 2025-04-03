using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Task_1.Data;
using Task_1.Models;

namespace Task_1.Controllers
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
        public ActionResult Update()
        {
            dataAccess.UpdateRow();
            ViewBag.Message = "Employee with ID 1 has been updated!";
            return View();
        }
        public ActionResult UpdateAll()
        {
            dataAccess.UpdateRowAll();
            ViewBag.Message = "All employees' middle names have been updated!";
            return View();
        }
        public ActionResult Delete()
        {
            dataAccess.DeleteRow();
            ViewBag.Message = "Employee(s) with ID less than 2 have been deleted!";
            return View();
        }
        public ActionResult DeleteAll()
        {
            dataAccess.DeleteAll();
            ViewBag.Message = "All employees have been deleted!";
            return View();
        }
    }
}