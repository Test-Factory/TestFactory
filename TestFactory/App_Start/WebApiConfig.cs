using System.Web.Http;
using TestFactory.Filters.WebApi;

namespace TestFactory
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name : "DefaultApi",
                routeTemplate : "api/{controller}/{id}",
                defaults : new { id = RouteParameter.Optional }
            );

            config.Filters.Add(new ExceptionHandlingAttribute());
        }
    }
}
