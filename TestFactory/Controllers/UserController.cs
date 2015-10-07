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
using TestFactory.Business.Models;
using SimpleCrypto;

namespace TestFactory.Controllers
{
    public class UserController : Controller
    {
        UserManager userManager;
        RoleManager roleManager;

        public UserController(UserManager userManager, RoleManager roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
           // userManager.AddFirstRole();
        }

       
        [HttpGet]
        public ActionResult LogIn()
        {
         
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(UserViewModel user, string returnUrl)
        {
            string decodedUrl = "";
            if (!string.IsNullOrEmpty(returnUrl))
                decodedUrl = Server.UrlDecode(returnUrl);
            if (!ModelState.IsValid)
            {
                return View(user);
            }

            if (userManager.IsPasswordValid(user.Email, user.Password))
            {
                FormsAuthentication.SetAuthCookie(user.Email, false);
                if (Url.IsLocalUrl(decodedUrl))
                {
                    return Redirect(decodedUrl);
                }
                return RedirectToRoute("Default");
            }
            else
            {
                ModelState.AddModelError("Email", "Введені дані не коректні");
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
