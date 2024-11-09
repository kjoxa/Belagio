using devarts.Models;
using devarts.Repositories;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace devarts.Controllers
{
    public class PedigreeController : Controller
    {
        private static Logger nLog = LogManager.GetCurrentClassLogger();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Lea()
        {
            return View();
        }

        public ActionResult RacingMini()
        {
            return View();
        }

        public ActionResult OrionsMagnificMarionette()
        {
            return View();
        }

        public ActionResult SugarPlumFairy()
        {
            return View();
        }

        public ActionResult NamasteBlackBaraka()
        {
            return View();
        }

        public ActionResult ElediGraceIulius()
        {
            return View();
        }

        public ActionResult CyreneSperidesDeSalkinAidan()
        {
            return View();
        }

        public ActionResult PikopsMacchiato()
        {
            return View();
        }
    }
}