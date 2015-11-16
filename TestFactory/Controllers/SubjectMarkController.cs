using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using System.Web.Mvc;
using Embedded_Resource;
using TestFactory.Business.Components.Managers;
using TestFactory.Business.Models;
using TestFactory.MVC.ViewModels;

namespace TestFactory.Controllers
{
    public class SubjectMarkController : Controller
    {
        public ActionResult List()
        {
            var mark = Framework.subjectMarkManager.GetList();
            var result = Mapper.Map<IList<SubjectMarkViewModel>>(mark);
            return View(result);
        }
    }
}