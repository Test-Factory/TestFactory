using AutoMapper;
using Embedded_Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Security;
using TestFactory.Business.Components;
using TestFactory.Business.Models;
using TestFactory.Filters;
using TestFactory.MVC.ViewModels;
using RoleNames = TestFactory.Resources.RoleNames;

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
            if (!User.IsInRole(RoleNames.Admin))
            {
                throw new HttpException(403, GlobalRes_ua.forbidenAction);
            }
            var faculties = Framework.FacultyManager.GetList();
            IList<Faculty> sortedList = faculties.Where(f => f.Users.Count() == 2).OrderBy(f => f.Name).ToList();
            var result = Mapper.Map<IList<FacultyViewModel>>(sortedList);
            return result;
        }

        
        [HttpPost]
        [ValidateModel]
        public IHttpActionResult Create(FacultyViewModel faculty)
        {

            //var model1 = new UserViewModel();
            //model1.Id = Guid.NewGuid().ToString();
            //model1.Email = faculty.Users[0].Email;
            //model1.FacultyId = faculty.Id;
            //model1.PasswordSalt = HashDecoder.GenarateSalt();
            //model1.Password = HashDecoder.ComputeHash(faculty.Users[0].Password,model1.PasswordSalt);
            //model1.Roles_id = "12dc6a23-8454-419f-ac75-2ea0560d27ef";

            //User user = AutoMapper.Mapper.Map<User>(model1);

            //Framework.userManager.Create(user);

            if (!User.IsInRole(RoleNames.Admin))
            {
                return BadRequest("error");
            }
            

           // Framework.FacultyManager.Create(FAC);

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
            if (!User.IsInRole(RoleNames.Admin))
            {
                return BadRequest("error");
            }
            return Ok();
        }
    }
}
