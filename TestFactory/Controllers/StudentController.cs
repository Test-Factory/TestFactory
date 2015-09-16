using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Providers.Entities;
using TestFactory.Business.Components.Managers;

namespace TestFactory.Controllers
{
    public class StudentController : Controller
    {
        private StudentManager _studentManager;

        public StudentController(StudentManager studentManager)
        {
            _studentManager = studentManager;
        }
        // GET: /Students/

        public ActionResult GetList()
        {       _studentManager.
            return View( );
        }

    }
}
