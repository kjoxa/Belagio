using devarts.Helpers;
using devarts.Models;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebMatrix.WebData;

namespace devarts.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private static Logger nLog = LogManager.GetCurrentClassLogger();

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (WebSecurity.Login(model.Email, model.Password))
                {
                    //var token = WebSecurity.GeneratePasswordResetToken("Administrator");
                    //WebSecurity.ResetPassword(token, "Spanishwaterdog2022!");
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    return View().WithError(this, "Błąd", "Nieprawidłowa nazwa użytkownika lub hasło!");
                }
            }
            else
            {                
                return View(model).WithError(this, "Błąd", "Podaj wymagane dane!");
            }            
        }

        [HttpGet]
        public ActionResult LogOff()
        {
            WebSecurity.Logout();
            return RedirectToAction("Index", "Home");
        }
    }
}