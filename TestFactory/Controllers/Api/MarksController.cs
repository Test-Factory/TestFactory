using System;
using System.Web.Http;
using TestFactory.Business.Components.Managers;
using TestFactory.Filters;

namespace TestFactory.Controllers.Api
{
    [RoutePrefix("api/marks")]
    public class MarksController: ApiController
    {
        private readonly MarkManager markManager;
        private readonly StudentManager studentManager;

        public MarksController(MarkManager markManager, StudentManager studentManager)
        {
            this.markManager = markManager;
            this.studentManager = studentManager;
        }

        [HttpGet]
        [Route("average")]
        [ValidateModel]
        public string GetAverageMarkByCaregotyId([FromUri]string categoryId)
        {
            double averageMark = markManager.AveregeMarkByCategoryId(categoryId);
            averageMark = Math.Round(averageMark, 2);
            return averageMark.ToString();
        }
    }
}