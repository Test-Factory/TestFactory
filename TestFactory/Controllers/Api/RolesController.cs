using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TestFactory.Business.Models;
using TestFactory.MVC.ViewModels;

namespace TestFactory.Controllers.Api
{
    [RoutePrefix("api/roles")]
    public class RolesController : ApiController
    {
        [HttpGet]
        public IList<RoleViewModel> Get()
        {
            var roles = Framework.RoleManager.GetList();
            IList<Role> sortedList = roles.Where(f => f.Name != "Admin").OrderBy(f => f.Id).ToList();
            var result = Mapper.Map<IList<RoleViewModel>>(sortedList);
            return result;
        }
    }
}
