using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestFactory.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CreateGroup()
        {
            ViewBag.Message = "Это частичное представление.";
            return PartialView();
        }
    }
}
