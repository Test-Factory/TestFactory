using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using System.Web.Http;
using System.Web.Optimization;
using TestFactory.Business.Components.Managers;
using TestFactory.Business.Models;
using System.Web;
using System.Web.Providers.Entities;
using TestFactory.MVC.ViewModels;
using AutoMapper;

namespace TestFactory.Controllers.Api
{
    [RoutePrefix("api/students")]
    public class StudentsController : ApiController
    {
        private readonly StudentManager studentManager;

        private readonly MarkManager markManager;

        public StudentsController(StudentManager studentManager, MarkManager markManager)
        {
            this.studentManager = studentManager;
            this.markManager = markManager;
        }
        // GET: API/Student
        [HttpGet]
        public IEnumerable<Student> Get()
        {
            IList<Student> students;
            string groupId = "13b66a40-5b78-48a0-b209-1390e420a11e";
            if (string.IsNullOrEmpty(groupId))
            {
                students = studentManager.GetList();
            }
            else
            {
                students = studentManager.GetList(groupId);
                /*foreach (Student stud in students)
                {
                    stud.Marks = markManager.GetList(stud.Id);
                }*/
                //checking role

            }
            return students ;
        }
       
        [HttpPost]
        public IHttpActionResult Create(Student student)
        {
            return Ok(1);
        }
        [HttpPut]
        public IHttpActionResult Update(StudentViewModel student)
        {
            var model = Mapper.Map<Student>(student);
            // TODO: take from model
            studentManager.Update(model);
            foreach (Mark mr in model.Marks)
            {
                mr.StudentId = model.Id;
                markManager.Update(mr);
            }
            return Ok();
        }
    }
}