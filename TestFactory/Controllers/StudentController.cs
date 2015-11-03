using System;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Embedded_Resource;
using TestFactory.Business.Components.Managers;
using TestFactory.Business.Models;
using TestFactory.MVC.ViewModels;
using TestFactory.Business.Components;
using System.Collections.Generic;


namespace TestFactory.Controllers
{
    public class StudentController : Controller
    {
        private readonly GroupManager groupManager;

        private readonly StudentManager studentManager;

        private readonly CategoryManager categoryManager;

        private UserContext user;

        public StudentController(GroupManager groupManager, StudentManager studentManager, CategoryManager categoryManager)
        {
            this.groupManager = groupManager;
            this.studentManager = studentManager;
            this.user = new UserContext();
            this.categoryManager = categoryManager;
        }

        [HttpGet]
        [Authorize(Roles = "Filler, Editor")]
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

            if (group.Faculty != user.User.Faculty)
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
            var student = studentManager.GetList();
            studentManager.AddLuceneIndex(student);
            var viewStudent = AutoMapper.Mapper.Map<List<StudentViewModel>>(student);
            var categories = categoryManager.GetList();
            var group = groupManager.GetList();
            var tuple = new Tuple<IList<StudentViewModel>, IList<Category>, IList<Group>>(viewStudent, categories, group);
            return View(tuple);
        }

        [HttpGet]
        [Authorize(Roles = "Filler, Editor")]
        public ActionResult ListAll()
        {
            return View("ListAllStudents");
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult SearchForStudents(string name)
        {
            var list = studentManager.Search(name);
            return Json(list);
        }

        [HttpPost]
        [Authorize(Roles = "Filler")]
        public JsonResult Delete(string studentId)
        {
            var student = studentManager.GetById(studentId);
            var group = groupManager.GetById(student.GroupId);

            if (!groupManager.HasAccessToGroup(user.User.Faculty, student.GroupId))
                throw new HttpException(403, GlobalRes_ua.error_403);

            studentManager.Delete(studentId);
            return Json(true);
        }
    }
}
