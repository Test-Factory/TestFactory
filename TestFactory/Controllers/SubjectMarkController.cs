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
            var mark = Framework.SubjectMarkManager.GetList();
            var result = Mapper.Map<IList<SubjectMarkViewModel>>(mark);
            return View(result);
        }

        [HttpPost]
        public JsonResult Update(SubjectMarkViewModel marks)
        {
            if (!ModelState.IsValid)
            {
                return Json(false);
            }

            if (!User.IsInRole("Filler"))
            {
                return Json(true);
            }

            var model = Mapper.Map<SubjectMark>(marks);
            Framework.SubjectMarkManager.Update(model);
            return Json(true);
        }

        [HttpPost]
        public JsonResult Create(SubjectMarkViewModel marks)
        {
            if (!ModelState.IsValid)
            {
                return Json(false);
            }

            if (!User.IsInRole("Filler"))
            {
                return Json(false);
            }

            var model = Mapper.Map<SubjectMark>(marks);
            Framework.SubjectMarkManager.Create(model);
            return Json(true);
        }
    }
}