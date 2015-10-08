using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TestFactory.Business.Components.Managers;
using TestFactory.Business.Models;
using TestFactory.MVC.ViewModels;
using System.Web.Mvc;
using System.IO.Compression;

namespace TestFactory.Controllers.Api
{
    public class FileController : Controller
    {
        private readonly CategoryManager categoryManager;
        private readonly ResultManager resultManager;
        private readonly StudentManager studentManager;

        public FileController(CategoryManager categoryManager, ResultManager resultManager, StudentManager studentManager)
        {
            this.categoryManager = categoryManager;
            this.resultManager = resultManager;
            this.studentManager = studentManager;
        }

        [HttpPost]
        public bool GetReport(StudentViewModel student)
        {  
            var  studentSave = Mapper.Map<Student>(student);
            IList<Category> categories = categoryManager.GetList();
            //resultManager.SaveToWord(studentSave, categories);
            resultManager.ConvertToWordX(studentSave, categories);
            return true;
        }

        [HttpPost]
        public void SaveZip()
        {
            IList<Student> students = studentManager.GetList("13b66a40-5b78-48a0-b209-1390e420a11e");
            var studentSave = Mapper.Map<IList<Student>>(students);
            IList<Category> categories = categoryManager.GetList();
            resultManager.SaveToZip(studentSave, categories);
        }
    }
}
