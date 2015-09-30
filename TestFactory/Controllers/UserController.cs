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
            //AddFirstRole();
        }

        public void AddFirstRole()
        {
            User admin = new User();
            admin.Email = "TF.Filler@ukr.net";
            admin.FirstName = "Filler";
            admin.LastName = "TF";
            admin.PasswordSalt = new PBKDF2().GenerateSalt();
            admin.Password = new PBKDF2().Compute("IFiller", admin.PasswordSalt);

            Role rol = new Role();
            rol.Id = "12dc6a23-8454-419f-ac75-2ea0560d27ef";
            rol.Name = "Filler";
            roleManager.Create(rol);
            admin.Roles = rol;

            userManager.Create(admin);
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

            if (userManager.IsPasswordValid(user.Email, user.Password))
            {
                FormsAuthentication.SetAuthCookie(user.Email, false);
                UserViewContext c = new UserViewContext();
                c.IsLogged(user.Email);
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
