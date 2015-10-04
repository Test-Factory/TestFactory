using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TestFactory.Business.Components.Managers;
using TestFactory.Business.Models;
using TestFactory.MVC.ViewModels;
using System.Web.Mvc;

namespace TestFactory.Controllers.Api
{
    public class FileController : Controller
    {
        private readonly CategoryManager categoryManager;
        private readonly ResultManager resultManager;

        public FileController(CategoryManager categoryManager, ResultManager resultManager)
        {
            this.categoryManager = categoryManager;
            this.resultManager = resultManager;
        }

        [HttpPost]
        public bool GetReport(StudentViewModel student)
        {  
            var  studentSave = Mapper.Map<Student>(student);
            IList<Category> categories = categoryManager.GetList();
            resultManager.SaveToWord(studentSave, categories);
            return true;
        }
    }
}
