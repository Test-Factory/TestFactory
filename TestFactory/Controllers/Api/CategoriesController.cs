﻿using System;
using System.Collections.Generic;
using System.Web.Http;
using TestFactory.Business.Components.Managers;
using TestFactory.Business.Models;
using TestFactory.MVC.ViewModels;
using AutoMapper;

namespace TestFactory.Controllers.Api
{ 
    [RoutePrefix("api/categories")]
    public class CategoriesController: ApiController
    {
       private readonly CategoryManager categoryManager;

       public CategoriesController(CategoryManager categoryManager)
       {
           this.categoryManager = categoryManager;
       }
        // GET: API/Student
        [HttpGet]
       public IEnumerable<CategoryWebModel> Get()
        {
            var category = categoryManager.GetList();
            var rezult = Mapper.Map<IEnumerable<CategoryWebModel>>(category);
            return rezult;
        }
       
        [HttpPost]
        public IHttpActionResult Create(Category category)
        {
            return Ok(1);
        }
        [HttpPut]
        public IHttpActionResult Update(Category category)
        {
            return Ok();
        }
    }
}