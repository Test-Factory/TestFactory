using System;
using System.Web;
using System.Web.Mvc;
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
            if (String.IsNullOrEmpty(groupId))
            {
                throw new HttpException(400, GlobalRes_ua.error_400);
            }

            Group group = groupManager.GetById(groupId);

            if (group == null)
            {
                throw new HttpException(404, GlobalRes_ua.error_404);
            }

            if (group.Faculty!=user.User.Faculty)
            {
                throw new HttpException(403, GlobalRes_ua.noAccessToGroup);
            }

            var result = Mapper.Map<GroupViewModel>(group);
            return View("List", result);
        }
    }
}
