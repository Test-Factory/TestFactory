using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using System.Web.Http;
using TestFactory.Business.Components.Managers;
using TestFactory.Business.Models;
using TestFactory.MVC.ViewModels;

namespace TestFactory.Controllers
{
    public class ResultController : Controller
    {
        private readonly CategoryManager categoryManager;

        private readonly ResultManager resultManager;

        public ResultController(CategoryManager categoryManager, ResultManager resultManager)
        {
            this.categoryManager = categoryManager;
            this.resultManager = resultManager;
        }

        public bool Result(StudentViewModel student = null)
        {
            var  studentSave = Mapper.Map<Student>(student);
            IList<Category> categories = categoryManager.GetList();
            resultManager.SaveToWord(studentSave, categories);
            return true;
        }
    }
}
