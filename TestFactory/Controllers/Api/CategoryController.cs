using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using TestFactory.Business.Components.Managers;
using TestFactory.Business.Models;
using TestFactory.MVC.ViewModels;

namespace TestFactory.Controllers.Api
{
    [RoutePrefix("api/categories")]
    public class CategoryController: ApiController
    {
       private readonly CategoryManager categoryManager;

       public CategoryController(CategoryManager categoryManager)
       {
           this.categoryManager = categoryManager;
       }
        // GET: API/Student
        [HttpGet]
       public IEnumerable<Category> Get()
        {
            return categoryManager.GetList();
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