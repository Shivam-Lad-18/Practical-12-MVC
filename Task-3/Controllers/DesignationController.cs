using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Task_3.Data;
using Task_3.Models;

namespace Task_3.Controllers
{
    public class DesignationController : Controller
    {

        private DataAccess dataAccess = new DataAccess();

        // GET: Designation
        public ActionResult Index()
        {
            List<Designation> list = dataAccess.getDesignations();
            return View(list);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Designation designation)
        {
            if (ModelState.IsValid)
            {
                dataAccess.InsertDesignation(designation);
                return RedirectToAction("Index");
            }
            return View(designation);
        }
    }
}