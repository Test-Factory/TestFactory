using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using System.Web.Mvc;
using TestFactory.Business.Components.Managers;
using TestFactory.Business.Models;
using TestFactory.MVC.ViewModels;
using RoleNames = TestFactory.Resources.RoleNames;

namespace TestFactory.Controllers
{
    public class SubjectController : Controller
    {

        [Authorize(Roles = RoleNames.AllRoles)]
        public ActionResult List()
        {
            var subjects = Framework.SubjectManager.GetList();   //TODO: getByGroupId()
            var result = Mapper.Map<IList<SubjectViewModel>>(subjects);
            return View(result);
        }

        [Authorize(Roles = RoleNames.Filler)]
        [HttpGet]
        public ActionResult Create()
        {
            return PartialView();
        }

        [Authorize(Roles = RoleNames.Filler)]
        [HttpPost]
        public ActionResult Create(SubjectViewModel subject)
        {
            //TODO: SubjectExist(subject.Name)
            //if (SubjectManager.SubjectExist(subject.Name))   
            //{
            //    throw new HttpException(403, GlobalRes_ua.forbidenAction);
            //}                                  

            if (!ModelState.IsValid)
            {
                return RedirectToRoute("Default");
            }
            //subject.FacultyId = "1cb8a5d5-e644-48f8-b8b6-ee0c3cf4700f";
            var model = Mapper.Map<Subject>(subject);
            Framework.SubjectManager.Create(model);

            return RedirectToRoute("subjectList"); //, new { groupId = subject.Id });
        }
    }
}