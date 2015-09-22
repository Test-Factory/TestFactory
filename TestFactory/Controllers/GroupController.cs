using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web;
using TestFactory.Business.Components.Managers;
using TestFactory.Business.Models;
using TestFactory.MVC.ViewModels;
using TestFactory.Business.Components;
using System.Web.Security;

namespace TestFactory.Controllers
{
    public class GroupController : Controller
    {
        // GET: Group
       private GroupManager groupManager;

        public GroupController(GroupManager groupManager)
        {
            this.groupManager = groupManager;
        }

        public ActionResult List()
        {
            //string[] rolename = Roles.GetRolesForUser();
            var tt = HttpContext.User.Identity.IsAuthenticated;
            //var t = System.Web.HttpContext.Current.User.IsInRole("Filler");

            if (UserContext.Current.IsLogged("Filler"))
            {
           
                var groups = groupManager.GetList();
                var result = AutoMapper.Mapper.Map<List<GroupViewModel>>(groups);
                return View(result);
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(GroupViewModel group)
        {
            if (!ModelState.IsValid)
                return View(group);

            var model = AutoMapper.Mapper.Map<Group>(group);
            groupManager.Create(model);
            return RedirectToRoute("Default");
        }

        [HttpGet]
        public ActionResult Update(string id)
        {
            var group = groupManager.GetById(id);

            if (group == null)
                return RedirectToRoute("NotFound");

            var model = AutoMapper.Mapper.Map<GroupViewModel>(group);            
            return View(model);
        }

        [HttpPost]
        public ActionResult Update(GroupViewModel group)
        {
            if (!ModelState.IsValid)
                return View(group);

            var model = AutoMapper.Mapper.Map<Group>(group);
            groupManager.Update(model);
            return RedirectToRoute("Default");
        }
    }
}