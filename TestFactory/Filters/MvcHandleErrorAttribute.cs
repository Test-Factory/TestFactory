using System;
using System.Web;
using System.Web.Mvc;
using Embedded_Resource;
using TestFactory.Business.Models;

namespace TestFactory.Filters
{
    public class MvcHandleErrorAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            Exception ex = filterContext.Exception;
            filterContext.ExceptionHandled = true;

            var httpError = ex as HttpException;

            var model = new Error()
            {
                HttpErrorCode = httpError != null ? httpError.GetHttpCode() : 500,
                Message = httpError != null ? ex.Message : GlobalRes_ua.error_500
            };

            filterContext.Result = new ViewResult()
            {
                ViewName = "Error",
                ViewData = new ViewDataDictionary(model)
            };
        }
    }
}