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
        private readonly UserContext user;

        public FileController()
        {          
            this.user = new UserContext();         
        }

        [Authorize(Roles = "Filler,Editor")]
        [WordDocument]
        public ActionResult GetReport(string id)
        {
            Student student = Framework.studentManager.GetById(id);

            if (student == null)
            {
                throw new HttpException(404, GlobalRes_ua.error_404);
            }

            Group group = Framework.groupManager.GetById(student.GroupId);

            if (group.FacultyId != user.User.FacultyId)
            {
                throw new HttpException(403, GlobalRes_ua.error_403);
            }

            IList<Category> categories = Framework.categoryManager.GetList();

            ViewBag.WordDocumentFilename = "(" + group.ShortName + ") " + student.FirstName + " " + student.LastName;

            var tuple = new Tuple<Student, IList<Category>, Group>(student, categories, group);
            return View(tuple);
        }

        [Authorize(Roles = "Filler,Editor")]
        [WordDocument]
        public ActionResult GetAllReport(string groupId)
        {
            Group group = Framework.groupManager.GetById(groupId);

            if (group.FacultyId != user.User.FacultyId)
            {
                throw new HttpException(403, GlobalRes_ua.error_403);
            }

            IList<Student> students = Framework.studentManager.GetList(groupId);

            IList<Category> categories = Framework.categoryManager.GetList();

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
