using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Providers.Entities;
using AutoMapper;
using TestFactory.Business.Components.Managers;
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
            var students = _studentManager.GetList();
            var result = Mapper.Map<List<StudentViewModel>>(students);
            return View(result);
        }
        public ActionResult CreateStudent()
        {
            return PartialView();
        }
        public ActionResult DeleteStudent(string id)
        {
            _studentManager.Delete(id);
            return RedirectToRoute("listStudent");
        }
    }
}
