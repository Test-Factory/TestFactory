using System;
using System.Collections.Generic;
using TestFactory.Business.Components.Managers;
using TestFactory.Business.Models;
using System.Web.Mvc;
using System.Web;
using Embedded_Resource;
using TestFactory.Business.Components;

namespace TestFactory.Controllers.Api
{
    public class FileController : Controller
    {
        private readonly CategoryManager categoryManager;
        private readonly StudentManager studentManager;
        private readonly GroupManager groupManager;
        private UserContext user;

        public FileController(CategoryManager categoryManager, StudentManager studentManager,UserManager userManager, GroupManager groupManager)
        {
            this.categoryManager = categoryManager;
            this.studentManager = studentManager;
            this.user = new UserContext();
            this.groupManager = groupManager;
        }
        [Authorize(Roles = "Filler,Editor")]
        [WordDocument]
        public ActionResult GetReport(string id)
        {
            
            Student student = studentManager.GetById(id);
            if (student == null)
            {
                throw new HttpException(404, GlobalRes_ua.error_404);
            }
            Group group = groupManager.GetById(student.GroupId);          
            IList<Category> categories = categoryManager.GetList();

            ViewBag.WordDocumentFilename = "(" + group.ShortName + ") " + student.FirstName + " " + student.LastName;

            var tuple = new Tuple<Student, IList<Category>, Group>(student, categories, group);
            return View(tuple);
        }
        [Authorize(Roles = "Filler,Editor")]
        [WordDocument]
        public ActionResult GetAllReport(string groupId)
        {
            if (!groupManager.HasAccessToGroup(user.User.Id, groupId)) 
            {
            throw new HttpException(403, GlobalRes_ua.error_403);
            }
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
