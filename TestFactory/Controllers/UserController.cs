using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestFactory.MVC.ViewModels;
using TestFactory.Business.Components.Managers;
using System.Web.Security;
using TestFactory.Business.Components.Rols;
using TestFactory.Components;
using TestFactory.Business.Components;

namespace TestFactory.Controllers
{
    public class UserController : Controller
    {
        UserManager manager;

        public UserController(UserManager manager)
        {
            this.manager = manager;
            //manager.AddFirstRole();
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
                return RedirectToRoute("Default");
            }
            else
            {
                ModelState.AddModelError("Email", "Дані не коректні");
            }

            return View(user);
        }

        [HttpGet]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToRoute("Default");
        }

    }
}
