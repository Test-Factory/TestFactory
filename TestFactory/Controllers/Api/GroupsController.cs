using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using AutoMapper;
using System.Linq;
using TestFactory.Business.Models;
using TestFactory.Business.Components;
using TestFactory.Business.Components.Managers;
using TestFactory.MVC.ViewModels;

namespace TestFactory.Controllers.Api
{
    [RoutePrefix("api/groups")]
    public class GroupsController : ApiController
    {     
        [HttpGet]
        public GroupViewModel Get([FromUri]string groupId)
        {
            if (string.IsNullOrEmpty(groupId))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            Group group = Framework.GroupManager.GetById(groupId);

            if (group == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            var result = Mapper.Map<GroupViewModel>(group);
            return result;
        }

        [HttpGet]
        [Route("name")]
        public string[] GetNames()
        {
            IList<Group> groups = Framework.GroupManager.GetList();
            var result = groups.Select(x => x.ShortName).Distinct().ToArray();
            return result;
        }

        [HttpGet]
        [Route("year")]
        public int[] GetYears()
        {
            IList<Group> groups = Framework.GroupManager.GetList();
            var result = groups.Select(x => x.Year).Distinct().ToArray();
            return result;
        }

    }
}
