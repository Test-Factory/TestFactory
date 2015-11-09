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
        private readonly CategoryManager categoryManager;
        private readonly AverageMarkForFacultyManager averageMarkForFacultyManager;
        private readonly FrequencyMarkForFacultyByCategoryManager frequencyMarkForFacultyByCategoryManager;
        private UserContext user;

        public MarksController(MarkManager markManager, AverageMarkForFacultyManager averageMarkForFacultyManager, CategoryManager categoryManager, FrequencyMarkForFacultyByCategoryManager frequencyMarkForFacultyByCategoryManager)
        {
            this.markManager = markManager;
            this.categoryManager = categoryManager;
            this.averageMarkForFacultyManager = averageMarkForFacultyManager;
            this.frequencyMarkForFacultyByCategoryManager = frequencyMarkForFacultyByCategoryManager;
            this.user = new UserContext();
        }

        [HttpGet]
        [Route("average")]
        public IEnumerable<AverageMarkForFacultyViewModel> GetAverageMarks()
        {
           var averageMarks = averageMarkForFacultyManager.GetMarksForFaculty(user.User.Faculty).OrderBy(c=>c.Id);
            var result = Mapper.Map<IEnumerable<AverageMarkForFacultyViewModel>>(averageMarks);
            return result;
        }
        [HttpGet]
        [Route("frequency")]
        public void GetFrequencyMarks()
        {
            //string[] categories = categoryManager.GetList().Select(c => c.Code);
            var frequencyMarksForCategoryR = frequencyMarkForFacultyByCategoryManager.GetMarksForFaculty(user.User.Faculty).Where(f => f.Code == "R");
            double mForR ;
            foreach (var f in frequencyMarksForCategoryR)
            {
                 //mForR += f
            }

        }
    }
}