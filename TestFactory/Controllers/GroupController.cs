using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestFactory.Business.Components.Managers;
using TestFactory.Business.Models;
using TestFactory.MVC.ViewModels;
using TestFactory.Business.Components;
using Embedded_Resource;

namespace TestFactory.Controllers
{
    public class GroupController : Controller
    {
        private readonly GroupManager groupManager;
        private readonly UserManager userManager;
        private UserContext user;
        private GroupForUserManager groupForUserManager;

        public GroupController(GroupManager groupManager, StudentManager studentManager,UserManager userManager,GroupForUserManager groupForUserManager)
        {
            this.groupManager = groupManager;
            this.user = new UserContext();
            this.userManager = userManager;
            this.groupForUserManager = groupForUserManager;
        }

        [Authorize(Roles = "Filler,Editor")]
        public ActionResult List()
        {
            var groups = groupManager.GetListForUser(user.User.Id);
            var result = AutoMapper.Mapper.Map<List<GroupViewModel>>(groups);
            return View(result);
        }

        private bool isIncludeHTMLAttributes(GroupViewModel group) 
        {
            string regEx = @"<\\?([A-Z][A-Z0-9]*)\b[^>]*>";
            System.Text.RegularExpressions.Regex regular = new System.Text.RegularExpressions.Regex(regEx, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            var matches = regular.Match(group.FullName + group.ShortName);
            return matches.Success;
        }
        
        public ActionResult GetStudentCount(string id)
        {
            int count = groupManager.GetCount(id);
            return View(count);
        }

        public ActionResult GetStudentsCount(string id)
        {
            int count = groupManager.GetCount(id);
            return Json(count);
        }

        [Authorize(Roles = "Filler,Editor")]
        [HttpGet]
        public ActionResult Create()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult Create(GroupViewModel group)
        {
            if (isIncludeHTMLAttributes(group) || groupManager.GroupIsAlreadyExist(group.ShortName)) 
            {
                throw new HttpException(403,GlobalRes_ua.forbidenAction);
            }

            if (!ModelState.IsValid)
            {
                return RedirectToRoute("Default");
            }

            var model = AutoMapper.Mapper.Map<Group>(group);
            groupManager.Create(model);
     
            GroupForUser gfu = new GroupForUser();
            gfu.GroupId = group.Id;
            gfu.UserId = user.User.Id;
            groupForUserManager.Create(gfu);
            return RedirectToRoute("groupStudentList", new { groupId = group.Id });
        }

        [HttpPost]
        public ActionResult Update(GroupViewModel group)
        {
            if (isIncludeHTMLAttributes(group))
            {
                throw new HttpException(403, GlobalRes_ua.forbidenAction);
            }

            if (!ModelState.IsValid)
            {
                return View(group);
            }

            var model = AutoMapper.Mapper.Map<Group>(group);
            groupManager.Update(model);

            return RedirectToRoute("Default");
        }

        [HttpPost]
        public void Delete(string id)
        {
        }

    }
}