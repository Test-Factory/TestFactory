using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestFactory.Business.Components.Managers;
using TestFactory.Business.Models;
using TestFactory.MVC.ViewModels;

namespace TestFactory.Controllers
{
    public class GroupController : Controller
    {
        private GroupManager groupManager;

        public GroupController(GroupManager groupManager)
        {
            this.groupManager = groupManager;
        }

        public ActionResult List()
        {
            var groups = groupManager.GetList();
            var result = AutoMapper.Mapper.Map<List<GroupViewModel>>(groups);
            return View(result);
        }

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
                return View(group);
            }
            var model = AutoMapper.Mapper.Map<Group>(group);
            groupManager.Create(model);
            return RedirectToRoute("Default");
        }

        [HttpGet]
        public ActionResult Update(string id)
        {
            var group = groupManager.GetById(id);
            if (group == null)
            {
                return RedirectToRoute("NotFound");
            }

            var model = AutoMapper.Mapper.Map<GroupViewModel>(group);            
            return View(model);
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