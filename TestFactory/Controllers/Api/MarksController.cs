using System.Collections.Generic;
using System.Web.Http;
using AutoMapper;
using TestFactory.Business.Components.Managers;
using TestFactory.MVC.ViewModels;

namespace TestFactory.Controllers.Api
{
    public class MarksController: ApiController
    {
        private readonly MarkManager markManager;

        public MarksController(MarkManager markManager)
        {
            this.markManager = markManager;
        }

        [HttpGet]
        [Route("~/api/students/{studentId}/marks")]
        public IEnumerable<MarkWebModel> Get(string studentId)
        {
            var marks = markManager.Get(studentId);
            var result = Mapper.Map<IEnumerable<MarkWebModel>>(marks);
            return result;
        }
    }
}