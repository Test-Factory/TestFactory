using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using AutoMapper;
using TestFactory.Business.Models;
using TestFactory.Business.Components;
using TestFactory.Business.Components.Managers;
using TestFactory.MVC.ViewModels;

namespace TestFactory.Controllers.Api
{
      [RoutePrefix("api/groups")]
    public class GroupsController : ApiController
    {     
        private readonly UserContext user;

        public GroupsController()
        {       
            this.user = new UserContext();
        }

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
        public IList<GroupViewModel> Get()
        {
            IList<Group> groups = Framework.GroupManager.GetList();
            var result = Mapper.Map<IList<GroupViewModel>>(groups);
            return result;
        }
    }
}
