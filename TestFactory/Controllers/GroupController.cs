using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using TestFactory.Business.Components.Managers;
using TestFactory.Business.Models;
using TestFactory.MVC.ViewModels;
using TestFactory.Business.Components;
using Embedded_Resource;
using RoleNames = TestFactory.Resources.RoleNames;

namespace TestFactory.Controllers
{
    [Authorize(Roles = RoleNames.Filler)]
    public class GroupController : Controller
    {     

        [Authorize(Roles = RoleNames.AllUserRoles)]
        public ActionResult List()
        {
            var groups = Framework.GroupManager.GetListForFaculty(Framework.UserContext.User.FacultyId);
            var result = AutoMapper.Mapper.Map<List<GroupViewModel>>(groups);
            return View(result);
        }
        
        public ActionResult GetStudentCount(string id)
        {
            int count = Framework.GroupManager.GetStudentsCount(id);
            return View(count);
        }

        public JsonResult GetStudentsCount(string id)
        {
            int count = Framework.GroupManager.GetStudentsCount(id);
            return Json(count);
        }

        [Authorize(Roles = RoleNames.Filler)]
        [HttpGet]
        public ActionResult Create()
        {
            return PartialView();
        }

        [Authorize(Roles = RoleNames.Filler)]
        [HttpPost]
        public ActionResult Create(GroupViewModel group)
        {
            if (Framework.GroupManager.GroupIsAlreadyExist(group.ShortName))
            {
                throw new HttpException(403, GlobalRes_ua.forbidenAction);
            }

            if (!ModelState.IsValid)
            {
                return RedirectToRoute("Default");
            }

            group.FacultyId = Framework.UserContext.User.FacultyId;
            var model = AutoMapper.Mapper.Map<Group>(group);
            Framework.GroupManager.Create(model);

            return RedirectToRoute("groupStudentList", new { groupId = group.Id });
        }

        [Authorize(Roles = RoleNames.Filler)]
        [HttpPost]
        public ActionResult Update(GroupViewModel group)
        {
            if (Framework.UserContext.User.FacultyId != Framework.GroupManager.GetById(group.Id).FacultyId)
            {
                throw new HttpException(403, GlobalRes_ua.error_403);
            }

            if (!ModelState.IsValid)
            {
                return View(group);
            }

            group.FacultyId = Framework.UserContext.User.FacultyId;
            var model = AutoMapper.Mapper.Map<Group>(group);
            Framework.GroupManager.Update(model);

            return RedirectToRoute("Default");
        }

        
        [HttpPost]
        public JsonResult Delete(string id)
        {
            if (!User.IsInRole(RoleNames.Filler))
            { 
                return Json(false);
            }

            if (!Framework.GroupManager.HasAccessToGroup(Framework.UserContext.User.FacultyId, id))
            { 
                return Json("error");
            }

            Framework.GroupManager.Delete(id);
            return Json(true);
        }
    }
}