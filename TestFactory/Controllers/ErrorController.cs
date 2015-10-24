using Embedded_Resource;
using System.Web.Mvc;
using TestFactory.Business.Models;

namespace TestFactory.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error

        public ActionResult Error(int code = 500)
        {
            var error = new Error()
            {
                HttpErrorCode = code,
                Message = MapErrorMessage(code)
            };
            return View(error);
        }

        private string MapErrorMessage(int statusCode)
        {
            return GlobalRes_ua.ResourceManager.GetString(string.Format("error_{0}", statusCode)) ??
                   GlobalRes_ua.error_500;
        }
    }
}