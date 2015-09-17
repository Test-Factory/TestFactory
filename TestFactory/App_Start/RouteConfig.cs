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
               name : "updateList",
               url : "updateStudents/{id}",
               defaults : new { controller = "Student", action = "UpdateStudent", id = UrlParameter.Optional }
           );

            routes.MapRoute(
            name : "listStudent",
            url : "listStudent",
            defaults : new { controller = "Student", action = "GetStudents" }
        );

            routes.MapRoute(
              name: "createGroup",
              url: "createGroup",
              defaults: new { controller = "Group", action = "CreateGroup" }
          );
            routes.MapRoute(
             name : "createStudent",
             url : "createStudent",
             defaults : new { controller = "Student", action = "CreateStudent" }
         );
            routes.MapRoute(
             name : "deleteStudent",
             url : "deleteStudent/{id}",
             defaults : new { controller = "Student", action = "DeleteStudent", id = UrlParameter.Optional }
         );

            routes.MapRoute(
             name: "deleteGroup",
             url: "deleteGroup/{id}",
             defaults: new { controller = "Group", action = "DeleteGroup", id=UrlParameter.Optional }
         );

            routes.MapRoute(
           name: "updateGroup",
           url: "updateGroup/{id}",
           defaults: new { controller = "Group", action = "UpdateGroup", id = UrlParameter.Optional }
       );

            routes.MapRoute(
               name: "login",
               url: "login",
               defaults: new { controller = "User", action = "LogIn" }
           );

            routes.MapRoute(
                name: "LogOut",
                url: "logout",
                defaults: new { controller = "User", action = "LogOut" }
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