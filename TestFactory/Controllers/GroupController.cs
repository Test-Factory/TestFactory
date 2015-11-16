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
    public class GroupController : Controller
    {     
        private readonly UserContext user;

        public GroupController( )
        {          
            this.user = new UserContext();
        }

        [Authorize(Roles = RoleNames.AllRoles)]
        public ActionResult List()
        {
            var groups = Framework.GroupManager.GetListForFaculty(user.User.FacultyId);
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

            group.FacultyId = user.User.FacultyId;
            var model = AutoMapper.Mapper.Map<Group>(group);
            Framework.GroupManager.Create(model);

            return RedirectToRoute("groupStudentList", new { groupId = group.Id });
        }

        [Authorize(Roles = RoleNames.Filler)]
        [HttpPost]
        public ActionResult Update(GroupViewModel group)
        {
            if (user.User.FacultyId != Framework.GroupManager.GetById(group.Id).FacultyId)
            {
                throw new HttpException(403, GlobalRes_ua.error_403);
            }

            if (!ModelState.IsValid)
            {
                return View(group);
            }

            group.FacultyId = user.User.FacultyId;
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

            if (!Framework.GroupManager.HasAccessToGroup(user.User.FacultyId, id))
            { 
                return Json("error");
            }

            Framework.GroupManager.Delete(id);
            return Json(true);
        }
    }
}