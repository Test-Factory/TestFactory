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
                   defaults: new { controller = "Group", action = "List" }
                   );

            #region Student map routes

            routes.MapRoute(
               name: "studentList",
               url: "listStudents/{id}",
               defaults: new { controller = "Student", action = "List", id = UrlParameter.Optional }
           );

            routes.MapRoute(
               name : "updateList",
               url : "updateStudents/{id}",
               defaults : new { controller = "Student", action = "Update", id = UrlParameter.Optional }
           );

            routes.MapRoute(
            name : "listStudent",
            url : "listStudent",
            defaults : new { controller = "Student", action = "List" }
           );

            routes.MapRoute(
             name : "createStudent",
             url : "createStudent/{groupId}",
             defaults : new { controller = "Student", action = "Create", groupId = UrlParameter.Optional }
           );
           
            #endregion

            #region Group map routes

            routes.MapRoute(
             name: "deleteGroup",
             url: "deleteGroup/{id}",
             defaults: new { controller = "Group", action = "Delete", id=UrlParameter.Optional }
           );

            routes.MapRoute(
             name: "updateGroup",
             url: "updateGroup/{id}",
             defaults: new { controller = "Group", action = "Update", id = UrlParameter.Optional }
           );

            routes.MapRoute(
             name: "createGroup",
             url: "createGroup",
             defaults: new { controller = "Group", action = "Create" }
           );
            #endregion

            #region User map routes

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

            #endregion

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