using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using System.Web.Mvc;
using Embedded_Resource;
using TestFactory.Business.Components.Managers;
using TestFactory.Business.Models;
using TestFactory.MVC.ViewModels;
using RoleNames = TestFactory.Resources.RoleNames;

namespace TestFactory.Controllers
{
    [Authorize(Roles = RoleNames.Filler)]
    public class SubjectMarkController : Controller
    {
        [HttpGet]
        public ActionResult List(string groupId, string subjectId)
        {
            var students = Framework.StudentManager.GetList(groupId);
            var group = Framework.GroupManager.GetById(groupId);
            var markForSubject = Framework.SubjectMarkManager.GetMarkForSubject(subjectId);
            var subject = Framework.SubjectManager.GetById(subjectId);

            markForSubject = Framework.SubjectMarkManager.SetNullProportyForMark(students, markForSubject, subject.Id);

            var resultGroup = Mapper.Map<GroupViewModel>(group);
            var resultStudents = Mapper.Map<IList<StudentViewModel>>(students);
            var resultMark = Mapper.Map<IList<SubjectMarkViewModel>>(markForSubject);
            var resultSubject = Mapper.Map<SubjectViewModel>(subject);

            var tuple = new Tuple<GroupViewModel, SubjectViewModel, IList<StudentViewModel>, IList<SubjectMarkViewModel>>(resultGroup, resultSubject, resultStudents, resultMark);
            return View(tuple);
        }

        [HttpPost]
        public JsonResult Update(SubjectMarkViewModel marks)
        {
            if (!User.IsInRole("Filler") || marks.Value < 0)
            {
                return Json(false);
            }

            if (ModelState.IsValid)
            {
                var model = Mapper.Map<SubjectMark>(marks);
                Framework.SubjectMarkManager.Update(model);
                return Json(true);
            }
            return Json(false);
        }

        [HttpPost]
        public JsonResult Create(SubjectMarkViewModel marks)
        {
            if (!User.IsInRole("Filler") || marks.Value < 0)
            {
                return Json(false);
            }

            if (ModelState.IsValid)
            {
                var model = Mapper.Map<SubjectMark>(marks);
                Framework.SubjectMarkManager.Create(model);
                return Json(true);
            }
            return Json(false);
        }
    }
}