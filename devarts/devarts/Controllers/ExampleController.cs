using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace devarts.Controllers
{
    public class ExampleController : Controller
    {
        private static Logger nLog = LogManager.GetCurrentClassLogger();

        // GET: Example
        public ActionResult Index()
        {
            return View();
        }
    }
}