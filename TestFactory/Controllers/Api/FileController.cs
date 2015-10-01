using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using TestFactory.Business.Components.Managers;
using TestFactory.Business.Models;
using TestFactory.MVC.ViewModels;

namespace TestFactory.Controllers.Api
{
    public class FileController : ApiController
    {
        private readonly CategoryManager categoryManager;
        private readonly ResultManager resultManager;

        public FileController(CategoryManager categoryManager, ResultManager resultManager)
        {
            this.categoryManager = categoryManager;
            this.resultManager = resultManager;
        }

        [HttpGet]
        public bool GetReport(StudentViewModel student)
        {  
            var  studentSave = Mapper.Map<Student>(student);
            IList<Category> categories = categoryManager.GetList();
            resultManager.SaveToWord(studentSave, categories);
            return true;
        }
    }
}
