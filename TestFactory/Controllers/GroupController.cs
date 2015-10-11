using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestFactory.Business.Components.Managers;
using TestFactory.Business.Models;
using TestFactory.MVC.ViewModels;

using TestFactory.Business.Components;

namespace TestFactory.Controllers
{
    public class GroupController : Controller
    {
       private GroupManager groupManager;

       private UserContext user;

        public GroupController(GroupManager groupManager)
        {
            this.groupManager = groupManager;
            this.user = new UserContext();
        }

       [Authorize(Roles="Filler, Editor")]
        public ActionResult List()
        {
            var groups = groupManager.GetList();
            var result = AutoMapper.Mapper.Map<List<GroupViewModel>>(groups);
            return View(result);
        }

        [Authorize(Roles="Filler")]
        [HttpGet]
        public ActionResult Create()
        {              
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