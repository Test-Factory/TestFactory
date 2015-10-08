using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestFactory.Business.Components.Managers;
using TestFactory.Business.Models;
using TestFactory.MVC.ViewModels;
using TestFactory.Components;

namespace TestFactory.Controllers
{
    public class GroupController : Controller
    {
       private GroupManager groupManager;

       private UserViewContext user;

        public GroupController(GroupManager groupManager)
        {
            this.groupManager = groupManager;
            this.user = new UserViewContext();
        }

        public ActionResult List()
        {
            if (!user.IsLoggedIn)
                return RedirectToRoute("login");

            var groups = groupManager.GetList();
            var result = AutoMapper.Mapper.Map<List<GroupViewModel>>(groups);
            return View(result);
        }

        [HttpGet]
        public ActionResult Create()
        {
            if (!user.IsLoggedIn || user.User.Roles.Name != "Filler")
                return RedirectToRoute("login");

            return PartialView();
        }

        [HttpPost]
        public ActionResult Create(GroupViewModel group)
        {
            if (!ModelState.IsValid)
            {
                return View("Default");
            }
            var model = AutoMapper.Mapper.Map<Group>(group);
            groupManager.Create(model);

            return RedirectToRoute("groupStudentList", new { groupId = group.Id });
        }

        [HttpPost]
        public ActionResult Update(GroupViewModel group)
        {
            if (!user.IsLoggedIn)
                return RedirectToRoute("login");

            if (!ModelState.IsValid)
            {
                return View(group);
            }
            var model = AutoMapper.Mapper.Map<Group>(group);
            groupManager.Update(model);

            return RedirectToRoute("Default");
        }
    }
}