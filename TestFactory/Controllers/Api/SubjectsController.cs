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
using Embedded_Resource;
using System.Web;

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
         public IList<SubjectViewModel> GetAll()
        {
            var subjects = Framework.SubjectManager.GetForFaculty(user.User.FacultyId).OrderBy(s => s.Name);
            var result = Mapper.Map<IList<SubjectViewModel>>(subjects);
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
        public IHttpActionResult Create(SubjectViewModel subject)
        {
            if (Framework.SubjectManager.SubjectIsAlreadyExist(subject.Name))
            {
                return BadRequest();
            }
            if (!User.IsInRole("Filler"))
            {
                return BadRequest();
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
        public IHttpActionResult Update(SubjectViewModel subject)
        {
            
            if (!User.IsInRole("Filler"))
            {
                return BadRequest("error");
            }
            var model = Framework.SubjectManager.GetById(subject.Id);
            if ((Framework.SubjectManager.SubjectIsAlreadyExist(subject.Name)) && !(subject.Name == model.Name))
            {
                throw new HttpException(403, GlobalRes_ua.forbidenAction);
            }
            model.Name = subject.Name;
            Framework.SubjectManager.Update(model);
            return Ok();
        }
    }
}
