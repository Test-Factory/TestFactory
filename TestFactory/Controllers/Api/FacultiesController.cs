using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TestFactory.Business.Components;
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
            var result = new List<FacultyViewModel>();
            return result;
        }
        [HttpPost]
        [ValidateModel]
        public IHttpActionResult Create(FacultyViewModel faculty)
        {
            var model = new FacultyViewModel();
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
