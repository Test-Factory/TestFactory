using System;
using System.Collections;
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
        [Route("standardDeviation")]
        public ArrayList GetStandardDeviationMarks()
        {
            var categories = categoryManager.GetList().ToList();
            var standardDeviationMarks = new ArrayList();
            var frequencyMarksForCategory = frequencyMarkForFacultyByCategoryManager
                                            .GetMarksForFaculty(user.User.Faculty)
                                            .OrderBy(f => f.CategoryId).ToList(); 

            foreach (var c in categories)
            {
                var count = markManager.CountMarksForCategory(c.Id);
                var sd = this.GetStandardDeviationMarkByCategoryId(frequencyMarksForCategory
                          .Where(f => f.CategoryId == c.Id), count);
                standardDeviationMarks.Add(Math.Round(sd,2));
            }
            return standardDeviationMarks;
        }

        private double GetStandardDeviationMarkByCategoryId(IEnumerable<FrequencyMarkForFacultyByCategory> frequencyMarks, int count)
        {
            var frequencyMarksList = frequencyMarks.ToList();
            double M = 0;
            double D = 0;
            foreach (var f in frequencyMarksList)
            {
                M += (f.Value * ((double)f.Count / count));
                D += (Math.Pow(f.Value, 2) * ((double)f.Count / count));
            }
            double standardDeviation = Math.Sqrt((D - Math.Pow(M,2)));
            return standardDeviation;
        }
    }
}