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
    [RoutePrefix("api/subjectsMark")]
    public class SubjectsMarkController : ApiController
    {
        private readonly UserContext user;

        public SubjectsMarkController()
        {
            this.user = new UserContext();
        }

        [HttpGet]
        [Route("subjectMarkAll")]
        public List<SubjectMarkViewModel> GetAll()
        {
            var subjects = Framework.SubjectManager.GetForFaculty(user.User.FacultyId).OrderBy(s => s.Name);
            List<SubjectMark> subjectsMarks = new List<SubjectMark>();
            foreach (var subj in subjects)
            {
                var marksForSubject = Framework.SubjectMarkManager.GetMarkForSubject(subj.Id);
                subjectsMarks.AddRange(marksForSubject);
            }
            var result = Mapper.Map<List<SubjectMarkViewModel>>(subjectsMarks);
            return result;
        }
    }
}