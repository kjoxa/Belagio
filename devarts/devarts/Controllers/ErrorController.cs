using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace devarts.Controllers
{
    public class ErrorController : Controller
    {
        private static Logger nLog = LogManager.GetCurrentClassLogger();

        // GET: Error
        public ActionResult NotAuthorized()
        {
            return View();
        }

        public ActionResult NotFound(string aspxerrorpath)
        {
            ViewBag.RequestNotFound = aspxerrorpath;
            return View();
        }

        public ActionResult BadRequest()
        {
            return View();
        }

        public ActionResult ServerError()
        {
            return View();
        }
    }
}