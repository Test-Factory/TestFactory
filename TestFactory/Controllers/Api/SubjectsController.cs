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
        public string[] GetAll()
        {
            var subjects = Framework.SubjectManager.GetForFaculty(user.User.FacultyId);
            var result = subjects.Select(s=>s.Name).ToArray();
            return result;
            //new [] {"Math", "Literature", "Programming", "Databases", "Algorithms"};
        }
        
        [HttpGet]
        public IList<SubjectViewModel> Get([FromUri]string groupId)
        {
            if (string.IsNullOrEmpty(groupId))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            var group = Framework.groupManager.GetById(groupId);
            if (group == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            IEnumerable<Subject> subjects;
            subjects = Framework.SubjectManager.GetList().OrderBy(s => s.Name); //TODO: error
            var result = Mapper.Map<IList<SubjectViewModel>>(subjects);
            return result;
        }

        [HttpPost]
        [ValidateModel]
        public IHttpActionResult Create(SubjectViewModel subject)
        {
            if (!User.IsInRole("Filler"))
            {
                return BadRequest("error");
            }
            //if (!Framework.groupManager.HasAccessToGroup(user.User.FacultyId, subject.GroupId))
            //{
            //    return BadRequest();
            //}
            Subject model = Mapper.Map<Subject>(subject);
            model.Id = Guid.NewGuid().ToString();
            model.FacultyId = user.User.FacultyId;
            Framework.SubjectManager.Create(model);
            var group = Framework.groupManager.GetById("1cb8a5d5-e644-48f8-b8b6-ee0c3cf4700f"); //TODO: move to manager
            group.Subjects.Add(model);
            Framework.groupManager.Update(group);
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

            var model = Mapper.Map<Subject>(subject);
            model.FacultyId = user.User.FacultyId;
            Framework.SubjectManager.Update(model);
            return Ok();
        }

        [HttpDelete]
        [ValidateModel]
        public IHttpActionResult Delete(SubjectViewModel subject)
        {
            if (!User.IsInRole("Filler"))
            {
                return BadRequest("error");
            }
            //var group = Framework.groupManager.GetById("1cb8a5d5-e644-48f8-b8b6-ee0c3cf4700f"); //TODO: move to manager
            //group.Subjects.Remove(Mapper.Map<Subject>(subject));
            //Framework.groupManager.Update(group);
            Framework.SubjectManager.Delete(subject.Id);
            return Ok();
        }
    }
}
