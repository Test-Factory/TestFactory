using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using System.Web.Mvc;
using TestFactory.Business.Components.Managers;
using TestFactory.Business.Models;
using TestFactory.MVC.ViewModels;
using RoleNames = TestFactory.Resources.RoleNames;

namespace TestFactory.Controllers
{
    public class SubjectController : Controller
    {

        [Authorize(Roles = RoleNames.AllRoles)]
        public ActionResult List(string groupId)
        {
            var group = Framework.GroupManager.GetById(groupId);
            var result = Mapper.Map<GroupViewModel>(group);
            return View(result);
        }

        [Authorize(Roles = RoleNames.Filler)]
        [HttpGet]
        public ActionResult Create()
        {
            return PartialView();
        }
    }
}