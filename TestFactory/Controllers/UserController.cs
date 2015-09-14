using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestFactory.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/

        public ActionResult Students()
        {
            return View();
        }

        public ActionResult LogIn()
        {
            return View();
        }

        public ActionResult Results()
        {
            return View();
        }
    }
}
