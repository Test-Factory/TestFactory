﻿using System.Collections.Generic;
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
        private readonly GroupManager groupManager;
        private UserContext user;

        public GroupsController( GroupManager groupManager)
        {
            this.groupManager = groupManager;
            this.user = new UserContext();
        }

        [HttpGet]
        public GroupViewModel Get([FromUri]string groupId)
        {
            if (string.IsNullOrEmpty(groupId))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            Group group = groupManager.GetById(groupId);

            if (group == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            var result = Mapper.Map<GroupViewModel>(group);
            return result;
        }

        [HttpGet]
        public IEnumerable<GroupViewModel> Get()
        {
            IEnumerable<Group> groups = groupManager.GetList();
            var result = Mapper.Map<IEnumerable<GroupViewModel>>(groups);
            return result;
        }
    }
}
