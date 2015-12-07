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

            if (!User.IsInRole(RoleNames.Admin))
            {
                return BadRequest("error");
            }

            var newFacultyViewModel = new FacultyViewModel();
            newFacultyViewModel.Name = faculty.Name;
            Faculty newFaculty = AutoMapper.Mapper.Map<Faculty>(newFacultyViewModel);       
            Framework.FacultyManager.Create(newFaculty);

            
            foreach(UserViewModel uv in faculty.Users)
            {
                UserViewModel userViewModel = new UserViewModel();

                userViewModel.Email = uv.Email;

                userViewModel.FacultyId = newFaculty.Id;
                
                userViewModel.PasswordSalt = HashDecoder.GenarateSalt();

                userViewModel.Password = HashDecoder.ComputeHash(uv.Password, userViewModel.PasswordSalt);

                userViewModel.Roles_id = uv.Roles_id;

                userViewModel.Roles = Framework.RoleManager.GetById(uv.Roles_id);

                User user = AutoMapper.Mapper.Map<User>(userViewModel);

                Framework.userManager.Create(user);
            }

            return Ok(newFaculty);
        }

      
        [HttpPut]
        [ValidateModel]
        public IHttpActionResult Update(FacultyViewModel faculty)
        {
            if (!User.IsInRole(RoleNames.Admin))
            {
                return BadRequest("error");
            }

            Faculty updatedFaculty = Framework.FacultyManager.GetById(faculty.Id);
            updatedFaculty.Name = faculty.Name;
        
            Framework.FacultyManager.Update(updatedFaculty);

            foreach (UserViewModel uv in faculty.Users)
            {
                User updatedUser = Framework.userManager.GetById(uv.Id);

                updatedUser.Email = uv.Email;
                if(updatedUser.Password != uv.Password)
                {
                    updatedUser.PasswordSalt = HashDecoder.GenarateSalt();
                    updatedUser.Password = HashDecoder.ComputeHash(uv.Password, updatedUser.PasswordSalt);
                }
                Framework.userManager.Update(updatedUser);
            }
            return Ok();
        }
    }
}
