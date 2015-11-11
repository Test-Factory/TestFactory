﻿using System.Collections.Generic;
using System.Linq;
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

        [HttpGet]
        public IEnumerable<CategoryViewModel> Get()
        {
            IEnumerable<Category> category = categoryManager.GetList().OrderBy(c => c.Id);
            var result = Mapper.Map<IEnumerable<CategoryViewModel>>(category);
            return result;
        }
    }
}