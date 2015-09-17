﻿using System;
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


        public ActionResult ListGroups()
        {
            var groups = _groupManager.GetList();
            var result = AutoMapper.Mapper.Map<List<GroupViewModel>>(groups);
            return View(result);
        }

        [HttpPost]
        public ActionResult CreateGroup(GroupViewModel group)
        {

            var model = AutoMapper.Mapper.Map<Group>(group);
            _groupManager.Create(model);
            return View();
        }

        [HttpGet]
        public ActionResult CreateGroup()
        {
            return View();
        }

        [HttpGet]
        public ActionResult DeleteGroup(string id)
        {
            _groupManager.Delete(id);
            return RedirectToRoute("listGroup");  
        }
    }
}