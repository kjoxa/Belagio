using devarts.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace devarts.Controllers
{
    public class ChartsController : Controller
    {
        [LayoutInjecter("_adminLayout")]
        public ActionResult Index()
        {
            FinanceCharts finc = new FinanceCharts();            
            return View(finc.YearFinanceBar(2020));
        }
    }
}