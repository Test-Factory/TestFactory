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

namespace TestFactory.Controllers.Api
{
    [RoutePrefix("api/students")]
    public class StudentsController : ApiController
    {
        private readonly StudentManager studentManager;

        public StudentsController(StudentManager studentManager)
        {
            this.studentManager = studentManager;
        }

        [HttpGet]
        public IEnumerable<StudentViewModel> Get([FromUri]string groupId)
        {
            IEnumerable<Student> students;
            if (string.IsNullOrEmpty(groupId))
            {
                throw new HttpResponseException(HttpStatusCode.PreconditionFailed);
            }
            students = studentManager.GetList(groupId).OrderBy(s => s.LastName);
            if (students == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            var result = Mapper.Map<IEnumerable<StudentViewModel>>(students);
            return result;

        }
       
        [Authorize]
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
        [Authorize]
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