using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Providers.Entities;
using System.Web.Routing;
using AutoMapper;
using Embedded_Resource;
using TestFactory.Business.Components.Managers;
using TestFactory.Business.Models;
using TestFactory.MVC.ViewModels;
using TestFactory.Business.Components;


namespace TestFactory.Controllers
{
    public class StudentController : Controller
    {
        private readonly GroupManager groupManager;

        private UserContext user;

        public StudentController(GroupManager groupManager)
        {
            this.groupManager = groupManager;
            this.user = new UserContext();
        }

        [System.Web.Mvc.HttpGet]
        [System.Web.Mvc.Authorize(Roles = "Filler, Editor")]
        public ActionResult List(string groupId = null)
        {
            if (groupId == null)
                throw new HttpException(400, GlobalRes_ua.GroupIdIsEmpty);
             Group group = groupManager.GetById(groupId);
            if (group == null)
                throw new HttpException(404, GlobalRes_ua.NoSuchGroupFound);
         
            if (groupManager.HasAccessToGroup(groupId,user.User.Id))
            {
                var result = Mapper.Map<GroupViewModel>(group);
                return View("List", result);
            }
            return RedirectToRoute("forbiddenAction");
        }
    }
}
