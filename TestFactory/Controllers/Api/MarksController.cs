using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using AutoMapper;
using TestFactory.Business.Components;
using TestFactory.Business.Components.Managers;
using TestFactory.Business.Models;
using TestFactory.Filters;
using TestFactory.MVC.ViewModels;

namespace TestFactory.Controllers.Api
{
    [RoutePrefix("api/marks")]
    public class MarksController: ApiController
    {
        private readonly MarkManager markManager;
        private readonly AverageMarkForFacultyManager averageMarkForFacultyManager;
        private UserContext user;

        public MarksController(MarkManager markManager, AverageMarkForFacultyManager averageMarkForFacultyManager)
        {
            this.markManager = markManager;
            this.averageMarkForFacultyManager = averageMarkForFacultyManager;
            this.user = new UserContext();
        }

        [HttpGet]
        [Route("average")]
        [ValidateModel]
        public IEnumerable<AverageMarkForFacultyViewModel> GetAverageMarks()
        {
           var averageMarks = averageMarkForFacultyManager.GetMarksForFaculty(user.User.Faculty).OrderBy(c=>c.Id);
            var result = Mapper.Map<IEnumerable<AverageMarkForFacultyViewModel>>(averageMarks);
            return result;
        }
    }
}