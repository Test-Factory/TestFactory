using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Providers.Entities;
using System.Web.Routing;
using AutoMapper;
using TestFactory.Business.Components.Managers;
using TestFactory.Business.Models;
using TestFactory.MVC.ViewModels;
using TestFactory.Components;

namespace TestFactory.Controllers
{
    public class StudentController : Controller
    {
        private readonly GroupManager groupManager;

        private UserViewContext user;

        public StudentController(GroupManager groupManager)
        {
            this.groupManager = groupManager;
            this.user = new UserViewContext();
        }
        
        [HttpGet]
        public ActionResult List(string groupId = null)
        {
            if (!user.IsLoggedIn) 
                    return RedirectToRoute("login");

            var group = groupManager.GetById(groupId);
            var result = Mapper.Map<GroupViewModel>(group);
            return View("List", result);
        }
    }
}
