using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestFactory.MVC.ViewModels;
using RoleNames = TestFactory.Resources.RoleNames;
namespace TestFactory.Controllers
{
    public class FacultyController : Controller
    {
        [Authorize(Roles = RoleNames.Admin)]
        public ActionResult List()
        {
            var result = new List<FacultyViewModel>();
            return View("List");
        }
    }
}