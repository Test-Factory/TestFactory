using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Providers.Entities;
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

        public StudentController(StudentManager studentManager, GroupManager groupManager, MarkManager markManager)
        {
            this.studentManager = studentManager;
            this.groupManager = groupManager;
            this.markManager = markManager;
        }

        // GET: /Students/

        [HttpGet]
        public ActionResult List(string groupId = null)
        {
            IList<Student> students;
            if (string.IsNullOrEmpty(groupId))
            {
                students = studentManager.GetList();
            }
            else
            {
                students = studentManager.GetList(groupId);
                //checking role
                foreach (Student stud in students)
                {
                    stud.Marks = markManager.GetList(stud.Id);
                    var Id = Guid.NewGuid().ToString();
                }

            }
            var result = Mapper.Map<List<StudentViewModel>>(students);
            return View("List", result);
        }

        [HttpGet]
        public ActionResult Create(string groupId)
        {
            var model = new StudentViewModel();
            model.GroupId = groupId;
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(StudentViewModel student)
        {
            var model = Mapper.Map<Student>(student);
            // TODO: take from model
            //string groupId = RouteData.Values["groupId"].ToString();
            //model.GroupId = groupId;
            studentManager.Create(model);
            return RedirectToRoute("groupStudentList", new { groupId = model.GroupId });
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
            studentManager.Update(model);
            return RedirectToRoute("groupStudentList", new { groupId = model.GroupId });
        }
    }
}
