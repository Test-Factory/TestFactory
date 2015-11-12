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

        private readonly UserContext user;

        private readonly StudentManager studentManager;

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
            var groups = groupManager.GetListForFaculty(user.User.FacultyId);
            var result = AutoMapper.Mapper.Map<List<GroupViewModel>>(groups);
            return View(result);
        }
        
        public ActionResult GetStudentCount(string id)
        {
            int count = groupManager.GetStudentsCount(id);
            return View(count);
        }

        public JsonResult GetStudentsCount(string id)
        {
            int count = groupManager.GetStudentsCount(id);
            return Json(count);
        }

        [Authorize(Roles = "Filler")]
        [HttpGet]
        public ActionResult Create()
        {
            return PartialView();
        }

        [Authorize(Roles = "Filler")]
        [HttpPost]
        public ActionResult Create(GroupViewModel group)
        {
            if (groupManager.GroupIsAlreadyExist(group.ShortName))
            {
                throw new HttpException(403, GlobalRes_ua.forbidenAction);
            }

            if (!ModelState.IsValid)
            {
                return RedirectToRoute("Default");
            }

            group.FacultyId = user.User.FacultyId;
            var model = AutoMapper.Mapper.Map<Group>(group);
            groupManager.Create(model);

            return RedirectToRoute("groupStudentList", new { groupId = group.Id });
        }

        [Authorize(Roles = "Filler")]
        [HttpPost]
        public ActionResult Update(GroupViewModel group)
        {
            if (user.User.FacultyId != groupManager.GetById(group.Id).FacultyId)
            {
                throw new HttpException(403, GlobalRes_ua.error_403);
            }

            if (!ModelState.IsValid)
            {
                return View(group);
            }

            group.FacultyId = user.User.FacultyId;
            var model = AutoMapper.Mapper.Map<Group>(group);
            groupManager.Update(model);

            return RedirectToRoute("Default");
        }

        
        [HttpPost]
        public JsonResult Delete(string id)
        {
            if (!User.IsInRole("Filler"))
            { 
                return Json(false);
            }

            if (!groupManager.HasAccessToGroup(user.User.FacultyId, id))
            { 
                return Json("error");
            }

            groupManager.Delete(id);
            return Json(true);
        }
    }
}