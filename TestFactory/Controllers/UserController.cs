using System.Web.Mvc;
using TestFactory.MVC.ViewModels;
using TestFactory.Business.Components.Managers;
using System.Web.Security;

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
        }

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

            if (!userManager.IsPasswordValid(user.Email, user.Password))
            {
                ModelState.AddModelError("Email", "Введені дані не коректні");
            }
            else 
            {
                FormsAuthentication.SetAuthCookie(user.Email, false);

                if (User.IsInRole("Filler"))
                {
                    return RedirectToRoute("Default");
                }
                if (User.IsInRole("Editor"))
                {
                    return RedirectToRoute("studentListAll");
                }
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
