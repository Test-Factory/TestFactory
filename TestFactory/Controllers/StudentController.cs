using System;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using TestFactory.Business.Components.Managers;
using TestFactory.Business.Models;
using TestFactory.MVC.ViewModels;
using TestFactory.Business.Components;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Embedded_Resource;
using RoleNames = TestFactory.Resources.RoleNames;

namespace TestFactory.Controllers
{
    public class StudentController : Controller
    {
        private readonly UserContext user;

        public StudentController()
        {
           this.user = new UserContext();
        }

        [HttpGet]
        [Authorize(Roles = RoleNames.AllRoles)]
        public ActionResult List(string groupId = null)
        {
            if (String.IsNullOrEmpty(groupId))
            {
                throw new HttpException(400, GlobalRes_ua.error_400);
            }

            Group group = Framework.GroupManager.GetById(groupId);

            if (group == null)
            {
                throw new HttpException(404, GlobalRes_ua.error_404);
            }

            if (group.FacultyId != user.User.FacultyId)
            {
                throw new HttpException(403, GlobalRes_ua.noAccessToGroup);
            }

            var result = Mapper.Map<GroupViewModel>(group);
            return View("List", result);
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Search()
        {
            var groupsForUser = Framework.GroupManager.GetListForFaculty(user.User.FacultyId);
            var students = new List<Student>();

            foreach(var gr in groupsForUser)
            {
                var student = Framework.StudentManager.GetList(gr.Id);
                students.AddRange(student);
            }
            Framework.StudentManager.AddLuceneIndex(students);

            var viewStudent = AutoMapper.Mapper.Map<List<StudentViewModel>>(students);
            var categories = Framework.CategoryManager.GetList();
            var group = Framework.GroupManager.GetList();

            var tuple = new Tuple<IList<StudentViewModel>, IList<Category>, IList<Group>>(viewStudent, categories, group);
            return View(tuple);
        }

        [HttpGet]
        [Authorize(Roles = RoleNames.Editor)]
        public ActionResult ListAll()
        {
            return View("ListAllStudents");
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateInput(true)]
        public JsonResult SearchForStudents(string name)
        {
            var list = Framework.StudentManager.Search(name);
            return Json(list);
        }

        [HttpPost]
        [Authorize(Roles = RoleNames.Filler)]
        public JsonResult Delete(string studentId)
        {
            var student = Framework.StudentManager.GetById(studentId);

            if (!Framework.GroupManager.HasAccessToGroup(user.User.FacultyId, student.GroupId))
            {
                throw new HttpException(403, GlobalRes_ua.error_403);
            }

            Framework.StudentManager.Delete(studentId);
            return Json(true);
        }

        [HttpPost]
        public JsonResult Update(StudentViewModel student)
        {
            if (!ModelState.IsValid)
            {
                return Json(false);
            }

            if (!User.IsInRole(RoleNames.Filler) || !Framework.GroupManager
                .HasAccessToGroup(user.User.FacultyId, student.GroupId))
            {
                return Json(false);
            }
            var model = Mapper.Map<Student>(student);
            Framework.StudentManager.Update(model);
            return Json(true);
        }
    }
}
