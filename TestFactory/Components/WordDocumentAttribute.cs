using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;

namespace TestFactory
{
    public class WordDocumentAttribute : ActionFilterAttribute
    {
        public string DefaultFilename { get; set; }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var result = filterContext.Result as ViewResult;

            if (result != null)
                result.MasterName = "~/Views/Shared/_LayoutWord.cshtml";

            filterContext.Controller.ViewBag.WordDocumentMode = true;

            base.OnActionExecuted(filterContext);
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            var filename = filterContext.Controller.ViewBag.WordDocumentFilename;

            byte[] bytesName = Encoding.UTF8.GetBytes(filename);
            filename = Encoding.UTF8.GetString(bytesName);

            filename = filename ?? DefaultFilename ?? "Document";

            filterContext.HttpContext.Response.AppendHeader("Content-Disposition", string.Format("filename={0}.doc; charset=utf-8", filename));
            filterContext.HttpContext.Response.ContentType = "application/msword; charset=utf-8";


            base.OnResultExecuted(filterContext);
        }
    }
}