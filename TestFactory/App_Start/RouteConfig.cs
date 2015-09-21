using System;
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

            // create student in group
            routes.MapRoute(
                name: "studentCreate",
                url: "Groups/{groupId}/Students/Create",
                defaults: new {controller = "Student", action = "Create"}
           );

            // update student in group
            routes.MapRoute(
                name: "studentUpdate",
                url: "Groups/{groupId}/Students/Update/{id}",
                defaults: new {controller = "Student", action = "Update"}
           );

            // students by group
            routes.MapRoute(
                name: "groupStudentList",
                url: "Groups/{groupId}/Students",
                defaults: new {controller = "Student", action = "List"}
           );

            // all students
            routes.MapRoute(
                name: "studentList",
                url: "Students",
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
                defaults: new {controller = "Error", action = "Index"}
            );
        }
    }
}