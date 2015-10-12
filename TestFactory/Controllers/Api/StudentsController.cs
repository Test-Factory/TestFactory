using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Services.Description;
using AutoMapper;
using TestFactory.Business.Components.Managers;
using TestFactory.Business.Models;
using TestFactory.Filters;
using TestFactory.MVC.ViewModels;
using System;
using TestFactory.Controllers;

namespace TestFactory.Controllers.Api
{
    [RoutePrefix("api/students")]
    public class StudentsController : ApiController
    {
        private readonly StudentManager studentManager;
        private readonly GroupManager groupManager;

        public StudentsController(StudentManager studentManager, GroupManager groupManager)
        {
            this.studentManager = studentManager;
            this.groupManager = groupManager;
        }

        [HttpGet]
        public IEnumerable<StudentViewModel> Get([FromUri]string groupId)
        {
            IEnumerable<Student> students;
            if (string.IsNullOrEmpty(groupId))
            {
                throw new HttpResponseException(HttpStatusCode.PreconditionFailed);
            }
            var group = groupManager.GetById(groupId);
            if (group == null)
            {
                throw new HttpResponseException(HttpStatusCode.PreconditionFailed);
            }
            
            students = studentManager.GetList(groupId).OrderBy(s => s.LastName).ThenBy(s=>s.FirstName);
            //students = students.Select(s => s.Marks.OrderBy(m => m.CategoryId)); 
            if (students == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            var result = Mapper.Map<IEnumerable<StudentViewModel>>(students);
            return result;

        }

        [HttpPost]
        [ValidateModel]
        public IHttpActionResult Create(StudentViewModel student)
        {
                Student model = Mapper.Map<Student>(student);
                model.Id = Guid.NewGuid().ToString();
                for (int i = 0; i < model.Marks.Count; i++)
                {
                    model.Marks[i].Id = Guid.NewGuid().ToString();
                    model.Marks[i].StudentId = model.Id;
                }
                studentManager.Create(model);
                return Ok(model);
        }
        [HttpPut]
        [ValidateModel]
        public IHttpActionResult Update(StudentViewModel student)
        {
                 var model = Mapper.Map<Student>(student);
                 studentManager.Update(model);
                 return Ok();
             }
    }
}