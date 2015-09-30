using System.Collections.Generic;
using System.Web.Http;
using AutoMapper;
using TestFactory.Business.Components.Managers;
using TestFactory.Business.Models;
using TestFactory.MVC.ViewModels;
using System;

namespace TestFactory.Controllers.Api
{
    [RoutePrefix("api/students")]
    public class StudentsController : ApiController
    {
        private readonly StudentManager studentManager;

        private readonly MarkManager markManager;

        private readonly CategoryManager categoryManager;

        public StudentsController(StudentManager studentManager, MarkManager markManager, CategoryManager categoryManager)
        {
            this.studentManager = studentManager;
            this.markManager = markManager;
            this.categoryManager = categoryManager;
        }

        [HttpGet]
        public IEnumerable<StudentViewModel> Get([FromUri]string groupId)
        {
            IList<Student> students;
            if (string.IsNullOrEmpty(groupId))
            {
                students = studentManager.GetList();
            }
            else
            {
                students = studentManager.GetList(groupId);
            }
            var result = Mapper.Map<IEnumerable<StudentViewModel>>(students);
            return result;
        }
       
        [HttpPost]
        public IHttpActionResult Create(StudentViewModel student)
        {
            var model = new Student();
            model = Mapper.Map<Student>(student);
            model.Id =  Guid.NewGuid().ToString();
            for (int i = 0; i < model.Marks.Count; i++ )
            {
                model.Marks[i].Id = Guid.NewGuid().ToString();
                model.Marks[i].StudentId = model.Id;
            }
            studentManager.Create(model);
            return Ok(model);
        }

        [HttpPut]
        public IHttpActionResult Update(StudentViewModel student)
        {
            var model = Mapper.Map<Student>(student);
            studentManager.Update(model);
            return Ok();
        }
    }
}