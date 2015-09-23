using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using System.Web.Http;
using TestFactory.Business.Components.Managers;
using TestFactory.Business.Models;

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
            IEnumerable<Student> students = studentManager.GetList();
            
            return new List<Student>();
        }
       
        [HttpPost]
        public IHttpActionResult Create()
        {
            return Ok();
        }
        [HttpPut]
        public IHttpActionResult Update()
        {
            return Ok();
        }
    }
}