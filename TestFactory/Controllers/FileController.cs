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
        private readonly GroupManager groupManager;

        public FileController(CategoryManager categoryManager, ResultManager resultManager, StudentManager studentManager, GroupManager groupManager)
        {
            this.categoryManager = categoryManager;
            this.resultManager = resultManager;
            this.studentManager = studentManager;
            this.groupManager = groupManager;
        }

        //[HttpGet]
        //[WordDocument]
        public ActionResult GetReport(StudentViewModel student)
        {
            var studentSave = Mapper.Map<Student>(student);
            IList<Category> categories = categoryManager.GetList();
            Student st1 = studentManager.GetById("04b0fd13-22e6-48ac-a2aa-85be9fa93d8b");

            Group gr = groupManager.GetById(st1.GroupId);


            //resultManager.SaveToWord(studentSave, categories);
            //resultManager.ConvertToWordX(studentSave, categories);
            //var result = AutoMapper.Mapper.Map<List<StudentViewModel>>(st);


            IList<Category> ct = new List<Category>();
            ct = categoryManager.GetList();
            var tuple = new Tuple<Student, IList<Category>, Group>(st1, ct, gr);
            return View(tuple);
            //return View(tuple);
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
