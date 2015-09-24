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
using TestFactory.Business.DataProviderContracts;
using TestFactory.Business.Models;
using System.Security.Cryptography;
using SimpleCrypto;

namespace TestFactory.Controllers
{
    public class UserController : Controller
    {
        UserManager manager;
        RoleManager roleManager;

        public UserController(UserManager manager, RoleManager provider)
        {
            this.manager = manager;
            this.roleManager = provider;
            AddFirstRole();
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
            rol.Name = "Filler";
            roleManager.Create(rol);
            admin.Roles = rol;

            manager.Create(admin);
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
