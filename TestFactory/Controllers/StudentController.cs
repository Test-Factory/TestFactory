﻿using System;
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
        private readonly TestDescriptionManager testDescriptionManager;

        public StudentController(StudentManager studentManager, GroupManager groupManager, MarkManager markManager, TestDescriptionManager testDescriptionManager)
        {
            this.studentManager = studentManager;
            this.groupManager = groupManager;
            this.markManager = markManager;
            this.testDescriptionManager = testDescriptionManager;
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
            /*IList<Student> students;
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
            var result = Mapper.Map<List<StudentViewModel>>(students);*/
            return View(/*"List", result*/);
        }

        [HttpPost]
        public ActionResult Create(string groupId)
        {
            var model = new StudentViewModel();
            model.GroupId = groupId;
            return PartialView(model);
        }

        [HttpPost]
        public ActionResult Create(StudentViewModel student)
        {
            var model = Mapper.Map<Student>(student);
            // TODO: take from model
            string groupId = RouteData.Values["groupId"].ToString();
            model.GroupId = groupId;
            studentManager.Create(model);
            foreach (Mark mr in model.Marks)
            {
                mr.StudentId = model.Id;
                markManager.Create(mr);
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
