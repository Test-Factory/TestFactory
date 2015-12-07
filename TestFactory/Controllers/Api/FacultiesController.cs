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
            if (Framework.FacultyManager.FacultyIsAlreadyExist(faculty.Name))
                return BadRequest("faculty");

            var newFacultyViewModel = new FacultyViewModel();
            newFacultyViewModel.Name = faculty.Name;
            Faculty newFaculty = AutoMapper.Mapper.Map<Faculty>(newFacultyViewModel);

            if (faculty.Users[0].Email == faculty.Users[1].Email)
                return BadRequest();

            foreach (UserViewModel item in faculty.Users)
            {
                if (Framework.userManager.GetByEmail(item.Email) != null)
                    return BadRequest();
            }

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

            if (faculty.Users[0].Email == faculty.Users[1].Email)
                return BadRequest();
            Faculty updatedFaculty = Framework.FacultyManager.GetById(faculty.Id);

            if (Framework.FacultyManager.FacultyIsAlreadyExist(faculty.Name) && updatedFaculty.Name != faculty.Name)
                return BadRequest("faculty");

            updatedFaculty.Name = faculty.Name;

            foreach (UserViewModel item in faculty.Users)
            {
                User temporaryUpdatedUser = Framework.userManager.GetById(item.Id);

                if (Framework.userManager.GetByEmail(item.Email) != null && temporaryUpdatedUser.Email != item.Email)
                    return BadRequest();
            }
        
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
            updatedFaculty = Framework.FacultyManager.GetById(faculty.Id);//something weired
            var model = Mapper.Map<FacultyViewModel>(updatedFaculty);
            return Ok(model);
        }
    }
}
