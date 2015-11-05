using System.Collections.Generic;
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
        private StudentManager studentManager;

        public GroupController(GroupManager groupManager, StudentManager studentManager, UserManager userManager)
        {
            this.groupManager = groupManager;
            this.user = new UserContext();
            this.userManager = userManager;
            this.studentManager = studentManager;
        }

        [Authorize(Roles = "Filler,Editor")]
        public ActionResult List()
        {
            var groups = groupManager.GetListForFaculty(user.User.Faculty);
            var result = AutoMapper.Mapper.Map<List<GroupViewModel>>(groups);
            return View(result);
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

        [HttpGet]
        public ActionResult Create()
        {
            return PartialView();
        }

        [Authorize(Roles = "Filler")]
        [HttpPost]
        public ActionResult Create(GroupViewModel group)
        {
            group.Faculty = user.User.Faculty;
            var model = AutoMapper.Mapper.Map<Group>(group);
            if (groupManager.IsIncludeHTMLAttributes(model) || groupManager.GroupIsAlreadyExist(model.ShortName))
            {
                throw new HttpException(403, GlobalRes_ua.forbidenAction);
            }

            if (!ModelState.IsValid)
            {
                return RedirectToRoute("Default");
            }
            groupManager.Create(model);

            return RedirectToRoute("groupStudentList", new { groupId = group.Id });
        }

        [Authorize(Roles = "Filler")]
        [HttpPost]
        public ActionResult Update(GroupViewModel group)
        {
            if (user.User.Faculty != groupManager.GetById(group.Id).Faculty)
            {
                throw new HttpException(403, GlobalRes_ua.error_403);
            }

            group.Faculty = user.User.Faculty;

            var model = AutoMapper.Mapper.Map<Group>(group);

            if (groupManager.IsIncludeHTMLAttributes(model))
            {
                throw new HttpException(403, GlobalRes_ua.forbidenAction);
            }

            if (!ModelState.IsValid)
            {
                return View(group);
            }

            groupManager.Update(model);

            return RedirectToRoute("Default");
        }

        
        [HttpPost]
        [Authorize(Roles="Filler")]
        public ActionResult Delete(string id)
        {
            if (!User.IsInRole("Filler") || user.User.Faculty != groupManager.GetById(id).Faculty)
            {
                return Json(false);
            }
            studentManager.DeleteByGroupId(id);
            groupManager.Delete(id);
            return Json(true);
        }
    }
}