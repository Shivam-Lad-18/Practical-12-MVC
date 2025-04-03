using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Task_3.Data;
using Task_3.Models;

namespace Task_3.Controllers
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
        public ActionResult Create(Employee model)
        {
            if (ModelState.IsValid)
            {
                dataAccess.InsertEmployee(model);
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}