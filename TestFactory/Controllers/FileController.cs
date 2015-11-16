using System;
using System.Collections.Generic;
using TestFactory.Business.Components.Managers;
using TestFactory.Business.Models;
using System.Web.Mvc;
using System.Web;
using TestFactory.Business.Components;
using Embedded_Resource;
using RoleNames = TestFactory.Resources.RoleNames;

namespace TestFactory.Controllers.Api
{
    public class FileController : Controller
    {

        [Authorize(Roles = RoleNames.AllRoles)]
        [WordDocument]
        public ActionResult GetReport(string id)
        {
            Student student = Framework.StudentManager.GetById(id);

            if (student == null)
            {
                throw new HttpException(404, GlobalRes_ua.error_404);
            }

            Group group = Framework.GroupManager.GetById(student.GroupId);

            if (group.FacultyId != Framework.UserContext.User.FacultyId)
            {
                throw new HttpException(403, GlobalRes_ua.error_403);
            }

            IList<Category> categories = Framework.CategoryManager.GetList();

            ViewBag.WordDocumentFilename = "(" + group.ShortName + ") " + student.FirstName + " " + student.LastName;

            var tuple = new Tuple<Student, IList<Category>, Group>(student, categories, group);
            return View(tuple);
        }

        [Authorize(Roles = RoleNames.AllRoles)]
        [WordDocument]
        public ActionResult GetAllReport(string groupId)
        {
            Group group = Framework.GroupManager.GetById(groupId);

            if (group.FacultyId != Framework.UserContext.User.FacultyId)
            {
                throw new HttpException(403, GlobalRes_ua.error_403);
            }

            IList<Student> students = Framework.StudentManager.GetList(groupId);

            IList<Category> categories = Framework.CategoryManager.GetList();

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
