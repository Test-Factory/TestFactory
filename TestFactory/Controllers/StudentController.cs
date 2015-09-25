using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Providers.Entities;
using System.Web.Routing;
using AutoMapper;
using TestFactory.Business.Components.Managers;
using TestFactory.Business.Models;
using TestFactory.MVC.ViewModels;

namespace TestFactory.Controllers
{
    public class StudentController : Controller
    {
        private readonly StudentManager studentManager;
        private readonly GroupManager groupManager;
        private readonly MarkManager markManager;
        private readonly CategoryManager categoryManager;

        public StudentController(StudentManager studentManager, GroupManager groupManager, MarkManager markManager, CategoryManager categoryManager)
        {
            this.studentManager = studentManager;
            this.groupManager = groupManager;
            this.markManager = markManager;
            this.categoryManager = categoryManager;
        }

        // GET: /Students/

        private void addTestDiscription()
        {
            Category tDesc = new Category();
            tDesc.Name = "Артистический";
            tDesc.Code = "A";
            tDesc.LongDescription = "rlkgklrjt krt gkrjtgjl gdtjhgl dg";
            tDesc.ShortDescription = "lkjrgtdskhfle h kger";
            categoryManager.Create(tDesc);
        }

        private void ListDiscription()
        {
            IList<Category> tDesc = categoryManager.GetList();
        }


        private void addTestStudent()
        {
            Student st = new Student();
            st.FirstName = "Bogdan";
            st.LastName = "Semenets";
            st.GroupId = "13b66a40-5b78-48a0-b209-1390e420a11e";
            var t = st.Id;
            List<Mark> mrList = new List<Mark>();
            for (int i = 0; i < 6; i++)
            {
                Mark mr = new Mark();
                mr.StudentId = st.Id;
                mr.CategoryId = null;
                mr.Value = 85;
                mrList.Add(mr);
            }
            studentManager.Create(st);
            st.Marks = mrList;
            foreach (var model in mrList)
            {

                markManager.Create(model);
            }
            st.Marks = mrList;
        }


        [HttpGet]
        public ActionResult List(string groupId)
        {
            //var r = (int)groupId;
            return View("List", (object)groupId);
        }

        [HttpGet]
        public ActionResult Create(string groupId)
        {
        //    var model = new StudentViewModel();
        //    model.GroupId = groupId;
          return null;
        }

        [HttpPost]
        public ActionResult Create(StudentViewModel student)
        {
            var model = Mapper.Map<Student>(student);
            // TODO: take from model
            string groupId = RouteData.Values["groupId"].ToString();
            model.GroupId = groupId;
            studentManager.Create(model);
            IList<Category> tDesc = categoryManager.GetList();
            var i = 0;
            foreach (Mark mr in model.Marks)
            {
                mr.CategoryId = tDesc[i].Id;
                mr.StudentId = model.Id;
                markManager.Create(mr);
                i++;
            }
            return RedirectToRoute("groupStudentList", new { groupId = groupId });
        }

         public ActionResult Update(string id)
        {
            var student = Mapper.Map<StudentViewModel>(studentManager.GetById(id));
            return View(student);
        }

        [HttpPost]
        public ActionResult Update(StudentViewModel student)
        {
            var model = Mapper.Map<Student>(student);
            // TODO: take from model
            string groupId = RouteData.Values["groupId"].ToString();
            model.GroupId = groupId;
            studentManager.Update(model);
            foreach (Mark mr in model.Marks)
            {
                mr.StudentId = model.Id;
                markManager.Update(mr);
            }
            return RedirectToRoute("groupStudentList", new { groupId = groupId });
        }
    }
}
