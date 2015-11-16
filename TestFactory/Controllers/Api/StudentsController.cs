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
using Embedded_Resource;

namespace TestFactory.Controllers.Api
{
    [RoutePrefix("api/students")]
    public class StudentsController : ApiController
    {
        private readonly UserContext user;

        public StudentsController()
        {
            this.user = new UserContext();
        }

        [HttpGet]
        public IList<StudentWithGroupViewModel> Get()
        {
            var students = new List<StudentWithGroup>();
            var groupsForFaculty = Framework.groupManager.GetListForFaculty(user.User.FacultyId);
            foreach (var group in groupsForFaculty)
            {
                var student = Framework.studentWithGroupManager.GetByGroupId(group.Id);
                students.AddRange(student);
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
            var group = Framework.groupManager.GetById(groupId);
            if (group == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            IEnumerable<Student> students;
            students = Framework.studentManager.GetList(groupId).OrderBy(s => s.LastName);
            var result = Mapper.Map<IList<StudentViewModel>>(students);
            return result;
        }

        [HttpPost]
        [ValidateModel]
        public IHttpActionResult Create(StudentViewModel student)
        {
            if (!User.IsInRole("Filler"))
            {
                return BadRequest("error");
            }
            var currentGroup = Framework.groupManager.GetById(student.GroupId);

            student.Year = currentGroup.Year;

            Student model = Mapper.Map<Student>(student);
            model.Id = Guid.NewGuid().ToString();
            for (int i = 0; i < model.Marks.Count; i++)
            {
                model.Marks[i].Id = Guid.NewGuid().ToString();
                model.Marks[i].StudentId = model.Id;
            }
            Framework.studentManager.Create(model);
            return Ok(model);
        }

       
        [HttpPut]
        [ValidateModel]
        public IHttpActionResult Update(StudentViewModel student)
        {
            if (!User.IsInRole("Filler"))
            {
                return BadRequest("error");
            }

            if (!Framework.groupManager.HasAccessToGroup(user.User.FacultyId, student.GroupId))
            {
                return BadRequest();
            }

            var model = Mapper.Map<Student>(student);
            Framework.studentManager.Update(model);
            return Ok();
        }

        [HttpPost]
        [ValidateModel]
        [Route("delete")]
        public IHttpActionResult Delete(StudentViewModel student)
        {
            if (!User.IsInRole("Filler"))
            {
                return BadRequest("error");
            }

            if (!Framework.groupManager.HasAccessToGroup(user.User.FacultyId, student.GroupId))
            {
                return BadRequest();
            }

            Framework.studentManager.Delete(student.Id);
            return Ok();
        }
    }
}