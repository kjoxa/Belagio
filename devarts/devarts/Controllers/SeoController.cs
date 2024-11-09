using devarts.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace devarts.Controllers
{
    [LayoutInjecter("_adminLayout")]
    [Authorize]
    public class SeoController : Controller
    {
        public ActionResult ImageAlts()
        {
            return View();
        }
    }
}