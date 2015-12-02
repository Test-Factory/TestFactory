using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestFactory.MVC.ViewModels;

namespace TestFactory.Controllers
{
    public class FacultyController : Controller
    {
        public ActionResult List()
        {
            var result = new List<FacultyViewModel>();
            return View("List");
        }
    }
}