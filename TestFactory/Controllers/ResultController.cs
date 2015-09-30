using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using TestFactory.Business.Components.Managers;
using TestFactory.Business.Models;
using TestFactory.MVC.ViewModels;

namespace TestFactory.Controllers
{
    public class ResultController : Controller
    {
        private readonly CategoryManager categoryManager;

        public ResultController(CategoryManager categoryManager)
        {
            this.categoryManager = categoryManager;
        }

        public ActionResult Results(StudentViewModel student = null)
        {
            var  studentSave = Mapper.Map<Student>(student);
            IList<Category> categories = categoryManager.GetList();
            //SaveToWord(studentSave, categories);
            return View();
        }
    }
}
