﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestFactory.Controllers
{
    public class GroupController : Controller
    {
        // GET: Group
        public ActionResult ListGroups()
        {
            return View();
        }
        public ActionResult CreateGroup()
        {
            return PartialView();
        }
    }
}