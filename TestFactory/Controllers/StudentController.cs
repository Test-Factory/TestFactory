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
        private StudentManager _studentManager;

        public StudentController(StudentManager studentManager)
        {
            _studentManager = studentManager;
        }
        // GET: /Students/

        public ActionResult GetStudents()
        {
            var students = _studentManager.GetList().ToList();
            IList<StudentViewModel> result = Mapper.Map<List<StudentViewModel>>(students);
            return View(result);
        }
        public ActionResult CreateStudent()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateStudent(StudentViewModel student)
        {
            var model = Mapper.Map<Student>(student);
            _studentManager.Create(model);
            return RedirectToRoute("Default");
        }
        public ActionResult DeleteStudent(string id)
        {
            _studentManager.Delete(id);
            return RedirectToRoute("listStudent");
        }
    }
}
