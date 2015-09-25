using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using System.Web.Http;
using System.Web.Optimization;
using System.Web.Routing;
using AutoMapper;
using TestFactory.Business.Components.Managers;
using TestFactory.Business.Models;
using System.Web;
using System.Web.Providers.Entities;
using TestFactory.MVC.ViewModels;
using AutoMapper;

namespace TestFactory.Controllers.Api
{
    //[RoutePrefix("api/students")]
    public class StudentController : ApiController
    {
        private readonly StudentManager studentManager;

        private readonly MarkManager markManager;
        private readonly CategoryManager categoryManager;

        public StudentController(StudentManager studentManager, MarkManager markManager, CategoryManager categoryManager)
        {
            this.studentManager = studentManager;
            this.markManager = markManager;
            this.categoryManager = categoryManager;
        }
        // GET: API/Student
        [HttpGet]
        public IEnumerable<StudentViewModel> Get(string GroupId)
        {
            IList<Student> students;
            string groupId = GroupId;
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
            var result = Mapper.Map<IEnumerable<StudentViewModel>>(students);
            return result;
        }
       
        [HttpPost]
        public IHttpActionResult Create(Student student)
        {
            /*var model = Mapper.Map<Student>(student);
            // TODO: take from model
            string groupId = "13b66a40-5b78-48a0-b209-1390e420a11e";
            model.GroupId = groupId;
            studentManager.Create(model);
            IList<Category> tDesc = categoryManager.GetList();
            var i = 0;
            foreach (Mark mr in model.Marks)
            {
                mr.CategoryId = tDesc[i].Id;
                mr.StudentId = model.Id;
                markManager.Create(mr);
                i++;
            }*/
            return Ok(1);
        }
        [HttpPut]
        public IHttpActionResult Update(StudentViewModel student)
        {
            var model = Mapper.Map<Student>(student);
            // TODO: take from model
            foreach (Mark mr in model.Marks)
            {
                mr.StudentId = model.Id;
                markManager.Update(mr);
            }
            studentManager.Update(model);
            return Ok();
        }
    }
}