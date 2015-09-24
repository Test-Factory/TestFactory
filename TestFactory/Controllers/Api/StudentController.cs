using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using System.Web.Http;
using System.Web.Optimization;
using TestFactory.Business.Components.Managers;
using TestFactory.Business.Models;
using System.Web;
using System.Web.Providers.Entities;

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
        // GET: API/Student
        [HttpGet]
        public IEnumerable<Student> Get()
        {
            IList<Student> students;
            string groupId = "55f05128-1f74-4af6-aa75-54b7b2c2aa99";
            if (string.IsNullOrEmpty(groupId))
            {
                students = studentManager.GetList();
            }
            else
            {
                students = studentManager.GetList(groupId);
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
        public IHttpActionResult Update(Student student)
        {
            return Ok();
        }
    }
}