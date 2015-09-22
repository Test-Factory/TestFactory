using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using System.Web.Http;
using TestFactory.Business.Models;

namespace TestFactory.Controllers.Api
{
    [RoutePrefix("api/students")]
    public class StudentsController : ApiController
    {
        // GET: API/Student
        [HttpGet]
        public IEnumerable<Student> Get()
        {
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