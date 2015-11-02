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
        //[HttpGet]
        //[Route("~/api/students/{studentId}/marks")]
        //[ValidateModel]
        //public IEnumerable<MarkWebModel> Get(string studentId)
        //{
        //    if (string.IsNullOrEmpty(studentId))
        //    {
        //        throw new HttpResponseException(HttpStatusCode.PreconditionFailed);
        //    }
        //    var marks = markManager.Get(studentId).OrderBy(m => m.CategoryId);
        //    if (marks == null)
        //    {
        //        throw new HttpResponseException(HttpStatusCode.NotFound);
        //    }
        //    var result = Mapper.Map<IEnumerable<MarkWebModel>>(marks);
        //    return result;
        //}
    }
}