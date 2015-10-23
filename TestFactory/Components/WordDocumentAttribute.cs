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
            {
                result.MasterName = "~/Views/Shared/_LayoutWord.cshtml";
            }

            filterContext.Controller.ViewBag.WordDocumentMode = true;

            base.OnActionExecuted(filterContext);
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            var filename = filterContext.Controller.ViewBag.WordDocumentFilename;
            filename = filename ?? DefaultFilename ?? "Document";

            byte[] bytesName = Encoding.UTF8.GetBytes(filename);
            filename = Encoding.UTF8.GetString(bytesName);

            string name = "attachment; filename=\"" + filename + "\"; filename*=UTF-8''" + Uri.EscapeDataString(filename);
            filterContext.HttpContext.Response.AppendHeader("Content-Disposition", string.Format("filename={0}.doc;", name));
            filterContext.HttpContext.Response.ContentType = "application/msword";

            base.OnResultExecuted(filterContext);
        }
    }
}