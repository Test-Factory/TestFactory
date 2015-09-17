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
            //manager.AddFirstRole();
        }

        [HttpGet]
        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(UserViewModel user, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToRoute("Default");
            }

            if (manager.IsPasswordValid(user.Email, user.Password))
            {
                FormsAuthentication.SetAuthCookie(user.Email, false);

                if (returnUrl != null) return Redirect(returnUrl);
                else
                    return RedirectToRoute("Default");
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
            return RedirectToRoute("Default");
        }

    }
}
