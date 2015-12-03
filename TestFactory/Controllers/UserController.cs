using System.Web.Mvc;
using TestFactory.MVC.ViewModels;
using TestFactory.Business.Components.Managers;
using System.Web.Security;
using Embedded_Resource;
using RoleNames = TestFactory.Resources.RoleNames;
using System.Collections.Generic;

namespace TestFactory.Controllers
{
    public class UserController : Controller
    {
        [HttpGet]
        [AllowAnonymous]
        public ActionResult LogIn()
        {       
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult LogIn(UserViewModel user)
        {   
          
            if (!ModelState.IsValid)
            {
                return View(user);
            }

            if (!Framework.userManager.IsRoleAssigned(user.Email, user.Password))
            {
                ModelState.AddModelError("Email", GlobalRes_ua.invalidData);
            }
            else 
            {
                FormsAuthentication.SetAuthCookie(user.Email, false);
                if (User.IsInRole(RoleNames.Filler))
                {
                    return RedirectToRoute("Default");
                }
                if (User.IsInRole(RoleNames.Admin))
                {
                    return RedirectToRoute("facultyList");
                }

                return RedirectToRoute("studentListAll");
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
