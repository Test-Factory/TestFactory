using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Providers.Entities;
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

        public StudentController(StudentManager studentManager, GroupManager groupManager, MarkManager markManager)
        {
            this.studentManager = studentManager;
            this.groupManager = groupManager;
            this.markManager = markManager;
            //addTestStudent();
        }

        // GET: /Students/

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
                mr.Category = null;
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
        public ActionResult List(string groupId = null)
        {
            IList<Student> students;
            if (string.IsNullOrEmpty(groupId))
            {
                students = studentManager.GetList();
            }
            else
            {
                students = studentManager.GetList(groupId);
                //checking role
                foreach (Student stud in students)
                {
                    stud.Marks = markManager.GetList(stud.Id);
                }

            }
            var result = Mapper.Map<List<StudentViewModel>>(students);
            return View("List", result);
        }

        [HttpPost]
        public ActionResult Create(string groupId)
        {
            var model = new StudentViewModel();
            model.GroupId = groupId;
            return PartialView(model);
        }

        [HttpPost]
        public ActionResult Create(IList<StudentViewModel> student)
        {
            var model = Mapper.Map<IList<Student>>(student);
            // TODO: take from model
            string groupId = RouteData.Values["groupId"].ToString();
            foreach (Student st in model)
            {
                st.GroupId = groupId;
                foreach (Mark mr in st.Marks)
                {
                    mr.StudentId = st.Id;
                    markManager.Create(mr);
                }
                studentManager.Create(st);
            }
            return RedirectToRoute("groupStudentList", new { groupId = groupId });
        }

         public ActionResult Update(string id)
        {
            var student = Mapper.Map<StudentViewModel>(studentManager.GetById(id));
            return View(student);
        }

        [HttpPost]
        public ActionResult Update(IList<StudentViewModel> student)
        {
            var model = Mapper.Map<IList<Student>>(student);
            // TODO: take from model
            string groupId = RouteData.Values["groupId"].ToString();
            foreach (Student st in model)
            {
                st.GroupId = groupId;
                foreach (Mark mr in st.Marks)
                {
                    mr.StudentId = st.Id;
                    
                    TestDescription tes = new TestDescription();
                    tes.Id = Guid.NewGuid().ToString();
                    mr.Category = tes;
                    markManager.Update(mr);
                }
                studentManager.Update(st);
            }
            return RedirectToRoute("groupStudentList", new { groupId = groupId });
        }
    }
}
