using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using AutoMapper;
using TestFactory.Business.Components.Managers;
using TestFactory.Business.Models;
using TestFactory.Filters;
using TestFactory.MVC.ViewModels;
using System;
using TestFactory.Business.Components;
using System.Web;
using RoleNames = TestFactory.Resources.RoleNames;

namespace TestFactory.Controllers.Api
{
    [RoutePrefix("api/students")]
    public class StudentsController : ApiController
    {
        [HttpGet]
        public IList<StudentWithGroupViewModel> Get()
        {
            var students = new List<StudentWithGroup>();

            var groupsForFaculty = Framework.GroupManager.GetListForFaculty(Framework.UserContext.User.FacultyId);
            foreach (var group in groupsForFaculty)
            {
                var student = Framework.StudentWithGroupManager.GetByGroupId(group.Id);
                students.AddRange(student);
            }

            var subjects = Framework.SubjectManager.GetList().OrderBy(s => s.ShortName).ToList();

            for (int i = 0; i < students.Count; i++)
            {
                if (students[i].SubjectMarks.Count != subjects.Count)
                {
                    var subjectMarksForStudent = new List<SubjectMark>();
                    for (int j = 0; j < subjects.Count; j++)
                    {
                        bool check = false;
                        if (students[i].SubjectMarks.Count != 0)
                        {
                            foreach (var mark in students[i].SubjectMarks)
                            {
                                if (mark.SubjectId == subjects[j].Id)
                                {
                                    subjectMarksForStudent.Add(mark);
                                    check = true;
                                }
                            }
                        }
                        if(!check)
                        {
                            var newSubjectMark = new SubjectMark();
                            newSubjectMark.StudentId = students[i].Id;
                            newSubjectMark.SubjectId = subjects[j].Id;
                            newSubjectMark.Value = null;
                            subjectMarksForStudent.Add(newSubjectMark);
                        }
                    }
                    students[i].SubjectMarks = subjectMarksForStudent;
                }
            }

            IList<StudentWithGroup> sortedStudents = students.OrderBy(s => s.LastName).ToList();
            var result = Mapper.Map<IList<StudentWithGroupViewModel>>(sortedStudents);
            return result;
        }

        [HttpGet]
        public IList<StudentViewModel> Get([FromUri]string groupId)
        {
            if (string.IsNullOrEmpty(groupId))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            var group = Framework.GroupManager.GetById(groupId);
            if (group == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            IEnumerable<Student> students;
            students = Framework.StudentManager.GetList(groupId).OrderBy(s => s.LastName);
            var result = Mapper.Map<IList<StudentViewModel>>(students);
            return result;
        }

        [HttpPost]
        [ValidateModel]
        public IHttpActionResult Create(StudentViewModel student)
        {
            if (!User.IsInRole(RoleNames.Filler))
            {
                return BadRequest("error");
            }
            var currentGroup = Framework.GroupManager.GetById(student.GroupId);

            student.Year = currentGroup.Year;

            Student model = Mapper.Map<Student>(student);
            model.Id = Guid.NewGuid().ToString();
            for (int i = 0; i < model.Marks.Count; i++)
            {
                model.Marks[i].Id = Guid.NewGuid().ToString();
                model.Marks[i].StudentId = model.Id;
            }
            Framework.StudentManager.Create(model);
            return Ok(model);
        }

       
        [HttpPut]
        [ValidateModel]
        public IHttpActionResult Update(StudentViewModel student)
        {
            if (!User.IsInRole(RoleNames.Filler))
            {
                return BadRequest("error");
            }

            if (!Framework.GroupManager.HasAccessToGroup(Framework.UserContext.User.FacultyId, student.GroupId))
            {
                return BadRequest();
            }

            var model = Mapper.Map<Student>(student);
            Framework.StudentManager.Update(model);
            return Ok();
        }

        [HttpPost]
        [ValidateModel]
        [Route("delete")]
        public IHttpActionResult Delete(StudentViewModel student)
        {
            if (!User.IsInRole(RoleNames.Filler))
            {
                return BadRequest("error");
            }

            if (!Framework.GroupManager.HasAccessToGroup(Framework.UserContext.User.FacultyId, student.GroupId))
            {
                return BadRequest();
            }

            Framework.StudentManager.Delete(student.Id);
            return Ok();
        }
    }
}