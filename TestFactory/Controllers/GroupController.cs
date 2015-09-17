using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestFactory.Business.Components.Managers;
using TestFactory.Business.Models;
using TestFactory.MVC.ViewModels;

namespace TestFactory.Controllers
{
    public class GroupController : Controller
    {
        // GET: Group
       private GroupManager _groupManager;

        public GroupController(GroupManager GroupManager)
        {
            _groupManager = GroupManager;
        }


        public ActionResult ListGroups()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateGroup(GroupViewModel viewModel)
        {
            var model = AutoMapper.Mapper.Map<Group>(viewModel);
            _groupManager.Create(model);

            return RedirectToRoute("groupsList");
        }

    }
}