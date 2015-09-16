using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestFactory.Controllers
{
    public class StudentController : Controller
    {
        //
        // GET: /Students/

        public ActionResult ListStudents()
        {
            return View();
        }
        public ActionResult CreateStudent()
        {
            ViewBag.Message = "Это частичное представление.";
            return PartialView();
        }
    }
}
