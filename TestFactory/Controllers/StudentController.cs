using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Providers.Entities;
using System.Web.Routing;
using AutoMapper;
using TestFactory.Business.Components.Managers;
using TestFactory.Business.Models;
using TestFactory.MVC.ViewModels;

namespace TestFactory.Controllers
{
    public class StudentController : Controller
    {
        private readonly StudentManager studentManager;

        private readonly GroupManager groupManager;

        private readonly MarkManager markManager;

        private readonly CategoryManager categoryManager;

        public StudentController(StudentManager studentManager, GroupManager groupManager, MarkManager markManager, CategoryManager categoryManager)
        {
            this.studentManager = studentManager;
            this.groupManager = groupManager;
            this.markManager = markManager;
            this.categoryManager = categoryManager;
        }

        public ActionResult Result()
        {
            return View();
        }

        [HttpGet]
        public ActionResult List(string groupId = null)
        {
            return View("List", (object)groupId);
        }

        [HttpGet]
        public ActionResult Create(string groupId)
        {
        //    var model = new StudentViewModel();
        //    model.GroupId = groupId;
          return null;
        }

        [HttpPost]
        public ActionResult Create(StudentViewModel student)
        {
            var model = Mapper.Map<Student>(student);
            string groupId = RouteData.Values["groupId"].ToString();
            model.GroupId = groupId;
            studentManager.Create(model);
            IList<Category> tDesc = categoryManager.GetList();
            var i = 0;
            foreach (Mark mr in model.Marks)
            {
                mr.CategoryId = tDesc[i].Id;
                mr.StudentId = model.Id;
                markManager.Create(mr);
                i++;
            }
            return RedirectToRoute("groupStudentList", new { groupId = groupId });
        }

         public ActionResult Update(string id)
        {
            var student = Mapper.Map<StudentViewModel>(studentManager.GetById(id));
            return View(student);
        }

        [HttpPost]
        public ActionResult Update(StudentViewModel student)
        {
            var model = Mapper.Map<Student>(student);
            // TODO: take from model
            string groupId = RouteData.Values["groupId"].ToString();
            model.GroupId = groupId;
            studentManager.Update(model);
            foreach (Mark mr in model.Marks)
            {
                mr.StudentId = model.Id;
                markManager.Update(mr);
            }
            return RedirectToRoute("groupStudentList", new { groupId = groupId });
        }
    }
}
