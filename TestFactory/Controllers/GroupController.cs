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
        // GET: Group
       private GroupManager _groupManager;

        public GroupController(GroupManager GroupManager)
        {
            _groupManager = GroupManager;
        }

        public ActionResult List()
        {
            var groups = _groupManager.GetList();
            var result = AutoMapper.Mapper.Map<List<GroupViewModel>>(groups);
            return View(result);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(GroupViewModel group)
        {

            var model = AutoMapper.Mapper.Map<Group>(group);
            _groupManager.Create(model);
            return RedirectToRoute("Default");
        }

        [HttpPost]
        public ActionResult Update(string id)
        {
            var model = AutoMapper.Mapper.Map<GroupViewModel>(_groupManager.GetById(id));
            return View("CreateGroup",model);
        }

        [HttpPost]
        public ActionResult Update(GroupViewModel group)
        {
            var model = AutoMapper.Mapper.Map<Group>(group);
            _groupManager.Update(model);
            return RedirectToRoute("Default");
        }

        [HttpGet]
        public ActionResult Delete(string id)
        {
            _groupManager.Delete(id);
            return RedirectToRoute("");  
        }
    }
}