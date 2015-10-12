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
            Student st = studentManager.GetById("ff01b3cd-951a-4faf-9bf5-781b3f700eae");
            Group gr = groupManager.GetById(st.GroupId);
            IList<Category> ct = new List<Category>();
            ct = categoryManager.GetList();
            var tuple = new Tuple<Student, IList<Category>, Group>(st, ct, gr);
            return View(tuple);
        }

        [WordDocument]
        public ActionResult GetAllReport(string groupId)
        {
            IList<Category> categories = categoryManager.GetList();
            IList<Student> st = studentManager.GetList(groupId);
            Group gr = groupManager.GetById(groupId);
            IList<Category> ct = new List<Category>();
            ct = categoryManager.GetList();
            var tuple = new Tuple<IList<Student>, IList<Category>, Group>(st, ct, gr);
            return View(tuple);
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
