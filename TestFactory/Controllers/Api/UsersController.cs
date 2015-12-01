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
     [RoutePrefix("api/users")]
    public class UsersController : ApiController
    {
         private readonly UserContext user;

         public UsersController()
        {
            this.user = new UserContext();
        }

        [HttpGet]
         public IList<UserViewModel> Get()
        {
            var result =  new List<UserViewModel>();
            return result;
        }

        [HttpPost]
        [ValidateModel]
        public IHttpActionResult Create(UserViewModel user)
        {
            var model = new UserViewModel();
            return Ok(model);
        }

        [HttpPut]
        [ValidateModel]
        public IHttpActionResult Update(UserViewModel user)
        {
            return Ok();
        }

    }
}
