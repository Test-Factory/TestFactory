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
    [RoutePrefix("api/faculties")]
    public class FacultiesController : ApiController
    {
        private readonly UserContext user;
        public FacultiesController()
        {
            this.user = new UserContext();
        }

        [HttpGet]
        public IList<FacultyViewModel> Get()
        {
            var faculties = Framework.FacultyManager.GetList();
            IList<Faculty> sortedList = faculties.OrderBy(f => f.Name).ToList();
            var result = Mapper.Map<IList<FacultyViewModel>>(sortedList);
            return result;
        }
        [HttpPost]
        [ValidateModel]
        public IHttpActionResult Create(FacultyViewModel faculty)
        {
            var model = new FacultyViewModel();
            model.Id = "111";
            model.Name = faculty.Name;
            model.Users = faculty.Users;
            model.Users[0].Id = "1";
            model.Users[0].FacultyId = model.Id;
            model.Users[1].Id = "2";
            model.Users[1].FacultyId = model.Id;
            return Ok(model);
        }
        [HttpPut]
        [ValidateModel]
        public IHttpActionResult Update(FacultyViewModel faculty)
        {
            return Ok();
        }
    }
}
