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

namespace TestFactory.Controllers
{
    public class SubjectMarkController : Controller
    {
        [HttpGet]
        public ActionResult List(string groupId, string subjectId)
        {
            var students = Framework.StudentManager.GetList(groupId);
            var group = Framework.GroupManager.GetById(groupId);
            var markForSubject = Framework.SubjectMarkManager.GetList();
            var subject = Framework.SubjectManager.GetById(subjectId);

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
            if (!ModelState.IsValid)
            {
                return Json(false);
            }

            if (!User.IsInRole("Filler"))
            {
                return Json(true);
            }

            var model = Mapper.Map<SubjectMark>(marks);
            Framework.SubjectMarkManager.Update(model);
            return Json(true);
        }

        [HttpPost]
        public JsonResult Create(SubjectMarkViewModel marks)
        {
            if (!ModelState.IsValid)
            {
                return Json(false);
            }

            if (!User.IsInRole("Filler"))
            {
                return Json(false);
            }

            var model = Mapper.Map<SubjectMark>(marks);
            Framework.SubjectMarkManager.Create(model);
            return Json(true);
        }
    }
}