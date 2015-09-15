using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestFactory.MVC.ViewModels;
using TestFactory.Business.Components.Managers;
using System.Web.Security;

namespace TestFactory.Controllers
{
    public class UserController : Controller
    {
        UserManager manager;

        public UserController(UserManager manager)
        {
            this.manager = manager;
        }


        public ActionResult Students()
        {
            return View();
        }

        [HttpGet]
        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(UserViewModel user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }

            if (manager.IsPasswordValid(user.Email, user.Password))
            {
                FormsAuthentication.SetAuthCookie(user.Email, false);

                return RedirectToRoute("Home");
            }
            else
            {
                ModelState.AddModelError("Password", "Pass is not correct");
            }

            return View(user);
        }

        [HttpGet]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToRoute("Home");
        }

        public ActionResult Results()
        {
            return View();
        }
    }
}
