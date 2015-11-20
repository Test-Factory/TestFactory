using System.Security.Cryptography.X509Certificates;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TestFactory.Business.Components;
using TestFactory.Business.Models;
using TestFactory.Filters;
using TestFactory.MVC.ViewModels;

namespace TestFactory.Controllers.Api
{
    [RoutePrefix("api/subjects")]
    public class SubjectsController : ApiController 
    {
         private readonly UserContext user;

         public SubjectsController()
        {
            this.user = new UserContext();
        }


        [HttpGet]
        [Route("all")]
         public IList<SubjectWithGroupViewModel> GetAll()
        {
            var subjects = Framework.SubjectManager.GetForFaculty(user.User.FacultyId).OrderBy(s => s.Name);
            var result = Mapper.Map<IList<SubjectWithGroupViewModel>>(subjects);
            return result;
        }
        
        [HttpGet]
        public IList<SubjectViewModel> Get([FromUri]string groupId)
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
            IEnumerable<Subject> subjects;
            subjects = Framework.SubjectManager.GetList().OrderBy(s => s.Name);
            var result = Mapper.Map<IList<SubjectViewModel>>(subjects);
            return result;
        }

        [HttpPost]
        [ValidateModel]
        public IHttpActionResult Create(SubjectWithGroupViewModel subject)
        {
            if (!User.IsInRole("Filler"))
            {
                return BadRequest("error");
            }
            var group = Framework.GroupManager.GetById(subject.GroupId);
            var result = group.Subjects.Any(s => s.Name == subject.Name);
            if (result)
            {
                return BadRequest();
            }

            Subject model = Mapper.Map<Subject>(subject);
            model.Id = Guid.NewGuid().ToString();
            model.FacultyId = user.User.FacultyId;
            Framework.SubjectManager.Create(model);
            return Ok(model);
        }

        [HttpPut]
        [ValidateModel]
        public IHttpActionResult Update(SubjectWithGroupViewModel subject)
        {
            if (!User.IsInRole("Filler"))
            {
                return BadRequest("error");
            }
            var model = Framework.SubjectManager.GetById(subject.Id);
            model.Name = subject.Name;
            Framework.SubjectManager.Update(model);
            return Ok();
        }
    }
}
