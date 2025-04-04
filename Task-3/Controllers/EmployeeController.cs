using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
        public ActionResult GetCountOnDesignation()
        {
            List<DesignationCount> list = dataAccess.getDesignationCount();
            return View(list);
        }
        public ActionResult getEmployeeWithDesignation()
        {
            List<EmployeeDesignation> list = dataAccess.getEmployeeWithDesignation();
            return View(list);
        }
        public ActionResult GetDesignationWithMoreThanOneEmployee()
        {
            List<DesignationCount> list = dataAccess.getDesignationWithMoreThanOneEmployee();
            return View(list);
        }
        public ActionResult EmployeeDetailsView()
        {
            List<EmployeeDetailsView> list = dataAccess.EmployeeDetailsViews();
            return View(list);
        }
        public ActionResult EmployeeDetailsViewWithDesignation()
        {
            List<EmployeeDetailsView> list = dataAccess.GetAllEmployeesStoredProcedure();
            return View(list);
        }
        public ActionResult GetDesignationId()
        {
            return View();
        }
        [HttpGet]
        public ActionResult EmployeesFromDesignationId(int id)
        {
            List<EmployeeDetailsView> list = dataAccess.GetEmployeesByDesignation(id);
            return View(list);
        }
    }
}