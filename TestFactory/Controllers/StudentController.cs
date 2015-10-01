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

namespace TestFactory.Controllers
{
    public class StudentController : Controller
    {
        private readonly GroupManager groupManager;

        public StudentController(GroupManager groupManager)
        {
            this.groupManager = groupManager;
        }

        [HttpGet]
        public ActionResult List(string groupId = null)
        {
            var group = groupManager.GetById(groupId);
            var result = Mapper.Map<GroupViewModel>(group);
            return View("List", result);
        }
    }
}
