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

            if (students.Count != markForSubject.Count)
            {
                foreach (var st in students)
                {
                    bool isToAssess = false;
                    foreach (var mr in markForSubject)
                    {
                        if (st.Id.Equals(mr.StudentId))
                        {
                            isToAssess = true;
                            break;
                        }
                    }
                    if (!isToAssess)
                    {
                        var newMark = new SubjectMark();
                        newMark.StudentId = st.Id;
                        newMark.SubjectId = subject.Id;
                        newMark.Value = null;
                        markForSubject.Add(newMark);
                    }
                }
            }

            var resultGroup = Mapper.Map<GroupViewModel>(group);
            var resultStudents = Mapper.Map<IList<StudentViewModel>>(students);
            var resultMark = Mapper.Map<IList<SubjectMarkViewModel>>(markForSubject);
            var resultSubject = Mapper.Map<SubjectViewModel>(subject);

            var tuple = new Tuple<GroupViewModel, SubjectViewModel, IList<StudentViewModel>, IList<SubjectMarkViewModel>>(resultGroup, resultSubject, resultStudents, resultMark);
            return View(tuple);
        }

        [HttpPost]
        public void Update(SubjectMarkViewModel marks)
        {
            if (!User.IsInRole("Filler"))
            {
                throw new HttpException(403, GlobalRes_ua.error_403);
            }

            if (ModelState.IsValid)
            {
                var model = Mapper.Map<SubjectMark>(marks);
                Framework.SubjectMarkManager.Update(model);
            }
        }

        [HttpPost]
        public void Create(SubjectMarkViewModel marks)
        {
            if (!User.IsInRole("Filler"))
            {
                throw new HttpException(403, GlobalRes_ua.error_403);
            }

            if (ModelState.IsValid)
            {
                var model = Mapper.Map<SubjectMark>(marks);
                Framework.SubjectMarkManager.Create(model);
            }
        }
    }
}