using System;
using System.Collections.Generic;
using TestFactory.Business.Components.Managers;
using TestFactory.Business.Models;
using System.Web.Mvc;

namespace TestFactory.Controllers.Api
{
    public class FileController : Controller
    {
        private readonly CategoryManager categoryManager;
        private readonly StudentManager studentManager;
        private readonly GroupManager groupManager;

        public FileController(CategoryManager categoryManager, StudentManager studentManager, GroupManager groupManager)
        {
            this.categoryManager = categoryManager;
            this.studentManager = studentManager;
            this.groupManager = groupManager;
        }

        [WordDocument]
        public ActionResult GetReport(string id)
        {
            Student student = studentManager.GetById(id);
            Group group = groupManager.GetById(student.GroupId);
            IList<Category> categories = categoryManager.GetList();

            ViewBag.WordDocumentFilename = "(" + group.ShortName + ") " + student.FirstName + " " + student.LastName;

            var tuple = new Tuple<Student, IList<Category>, Group>(student, categories, group);
            return View(tuple);
        }

        [WordDocument]
        public ActionResult GetAllReport(string groupId)
        {
            IList<Student> students = studentManager.GetList(groupId);
            Group group = groupManager.GetById(groupId);
            IList<Category> categories = categoryManager.GetList();

            ViewBag.WordDocumentFilename = group.ShortName;

            var tuple = new Tuple<IList<Student>, IList<Category>, Group>(students, categories, group);
            return View(tuple);
        }

        public ActionResult GenerateChart(Student student, IList<Category> category)
        {
            var tuple = new Tuple<Student, IList<Category>>(student, category);
            return View(tuple);
        }
    }
}
