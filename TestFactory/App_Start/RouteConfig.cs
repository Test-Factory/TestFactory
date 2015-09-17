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

            routes.MapRoute(
                   name: "Default",
                   url: "",
                   defaults: new { controller = "Group", action = "ListGroups" }
                   );

            routes.MapRoute(
               name: "studentList",
               url: "listStudents/{id}",
               defaults: new { controller = "Student", action = "GetList", id = UrlParameter.Optional }
           );

            routes.MapRoute(
               name: "groupsList",
               url: "groupList",
               defaults: new { controller = "Group", action = "Groups" }
           );

            routes.MapRoute(
               name: "login",
               url: "login",
               defaults: new { controller = "User", action = "LogIn" }
           );

            routes.MapRoute(
               name: "results",
               url: "results/{id}",
               defaults: new { controller = "Result", action = "Results", id = UrlParameter.Optional }
           );

            routes.MapRoute(
                name: "NotFound",
                url:  "{*url}",
                defaults: new { controller = "Error", action = "Index"}
            );
        }
    }
}