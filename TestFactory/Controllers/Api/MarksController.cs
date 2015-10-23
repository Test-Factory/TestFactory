using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using AutoMapper;
using TestFactory.Business.Components.Managers;
using TestFactory.Filters;
using TestFactory.MVC.ViewModels;

namespace TestFactory.Controllers.Api
{
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
        [Route("~/api/students/{studentId}/marks")]
        [ValidateModel]
        public IEnumerable<MarkWebModel> Get(string studentId)
        {
            if (string.IsNullOrEmpty(studentId))
            {
                throw new HttpResponseException(HttpStatusCode.PreconditionFailed);
            }
            var marks = markManager.Get(studentId).OrderBy(m => m.CategoryId);
            if (marks == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            var result = Mapper.Map<IEnumerable<MarkWebModel>>(marks);
            return result;
        }
    }
}