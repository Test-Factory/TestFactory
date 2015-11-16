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

        [HttpGet]
        [Route("average")]
        public IList<AverageMarkForFacultyViewModel> GetAverageMarks()
        {
            var averageMarks = Framework.AverageMarkForFacultyManager.GetMarksForFaculty(Framework.UserContext.User.FacultyId).OrderBy(c => c.Id);
           var result = Mapper.Map<IList<AverageMarkForFacultyViewModel>>(averageMarks);
           return result;
        }

        [HttpGet]
        [Route("standardDeviation")]
        public ArrayList GetStandardDeviationMarks()
        {
            var categories = Framework.CategoryManager.GetList().ToList();
            var standardDeviationMarks = new ArrayList();
            var frequencyMarksForCategory = Framework.FrequencyMarkForFacultyByCategoryManager
                                            .GetMarksForFaculty(Framework.UserContext.User.FacultyId)
                                            .OrderBy(f => f.CategoryId).ToList(); 

            foreach (var c in categories)
            {
                var count = Framework.MarkManager.CountMarksForCategory(c.Id);
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