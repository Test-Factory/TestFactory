﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TestFactory
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            #region Student

            routes.MapRoute(
                name: "studentCreate",
                url: "group/{groupId}/students/create",
                defaults: new {controller = "Student", action = "Create"}
           );

            routes.MapRoute(
                name: "studentUpdate",
                url: "group/{groupId}/students/update",
                defaults: new {controller = "Student", action = "Update"}
           );

            routes.MapRoute(
                name: "groupStudentList",
                url: "group/{groupId}/students",
                defaults : new { controller = "Student", action = "List", groupId = UrlParameter.Optional }
           );

            routes.MapRoute(
                name: "studentsResult",
                url: "{groupId}/students/results",
                defaults: new { controller = "File", action = "GetAllReport", groupId = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "studentList",
                url: "students",
                defaults: new {controller = "Student", action = "List"}
           );
           
            #endregion


            #region Group map routes

            routes.MapRoute(
             name: "updateGroup",
             url: "group/update/{id}",
             defaults: new { controller = "Group", action = "Update", id = UrlParameter.Optional }
           );

            routes.MapRoute(
             name: "GetStudentCount",
             url: "GetStudentCount/{id}",
             defaults: new { controller = "Group", action = "GetStudentCount", id = UrlParameter.Optional }
           );

            routes.MapRoute(
             name: "createGroup",
             url: "group/create",
             defaults: new { controller = "Group", action = "Create" }
           );

            #endregion

            #region User map routes

            routes.MapRoute(
             name: "login",
             url: "login",
                defaults: new {controller = "User", action = "LogIn"}
           );

            routes.MapRoute(
             name: "LogOut",
             url: "logout",
                defaults: new {controller = "User", action = "LogOut"}
           );

            #endregion

            

            routes.MapRoute(
                name: "saveZip",
                url: "SaveZip",
                defaults: new { controller = "File", action = "SaveZip" }
                );

            routes.MapRoute(
             name: "results",
             url: "results/{id}",
                defaults: new {controller = "Result", action = "Results", id = UrlParameter.Optional}
                );

            routes.MapRoute(
             name : "Default",
             url : "",
             defaults : new { controller = "Group", action = "List" }
           );

            routes.MapRoute(
              name: "NotFound",
                url: "{*url}",
                defaults: new {controller = "Error", action = "NotFound"}
            );
            routes.MapRoute(
               name: "studentResult",
               url: "group/{groupId}/{Id}",
               defaults: new { controller = "File", action = "GetReport", groupId = UrlParameter.Optional, Id = UrlParameter.Optional }
            );
        }
    }
}