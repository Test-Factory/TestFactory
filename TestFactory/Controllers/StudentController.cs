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

        public StudentController(StudentManager studentManager)
        {
            this.studentManager = studentManager;
        }

        // GET: /Students/

        [HttpGet]
        public ActionResult GetStudents(string groupId=null)
        {
            IList<Student> students;
            if (string.IsNullOrEmpty(groupId))
            {
                students = studentManager.GetList();
            }
            else
            {
                students = studentManager.GetList(groupId);
                
            }
            var result = Mapper.Map<List<StudentViewModel>>(students);
            return View("List", result);
        }
        //TODO:  modify as CreateStudent(string id)
        [HttpGet]
        public ActionResult CreateStudent(     )
        {
            //studentManager.Create();
            return View();
        }
        [HttpPost]
        public ActionResult CreateStudent(StudentViewModel student)
        {
            var model = Mapper.Map<Student>(student);
            model.GroupId = RouteData.Values["groupId"].ToString();
                //Request.Params["groupId"];
            studentManager.Create(model);
            return RedirectToRoute("listStudent", new { groupId = model.GroupId });
        }
        public ActionResult UpdateStudent(string id)
        {
            StudentViewModel student = Mapper.Map<StudentViewModel>(studentManager.GetById(id));
            return View(student);
        }
        [HttpPost]
        public ActionResult UpdateStudent(StudentViewModel student)
        {
            var model = Mapper.Map<Student>(student);
            studentManager.Update(model);
            return RedirectToRoute("listStudent");
        }
        public ActionResult DeleteStudent(string id)
        {
            studentManager.Delete(id);
            return RedirectToRoute("listStudent");
        }
    }
}
