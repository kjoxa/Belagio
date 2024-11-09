using devarts.Helpers;
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
    [LayoutInjecter("_adminLayout")]
    public class NewsletterController : Controller
    {
        private AdminRepository _adminRepo;
        private static Logger nLog = LogManager.GetCurrentClassLogger();

        public NewsletterController()
        {
            _adminRepo = new AdminRepository();
        }
        // GET: Newsletter
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Send()
        {
            return View();
        }

        [ValidateInput(false)]        
        [HttpPost]
        public ActionResult Send(MailModel objModelMail, HttpPostedFileBase fileUploader)
        {
            if (ModelState.IsValid)
            {
                var allSubscribers = _adminRepo.GetAllSubscribers().Where(a => a.IsActive == true).Select(u => u.Email).ToList();
                if (allSubscribers.Count() > 0)
                {
                    var emailSender = new MailSender();
                    if (emailSender.SendNewsletter(allSubscribers, objModelMail.Subject, objModelMail.Body, fileUploader))
                    {

                        return View(objModelMail).WithSuccess(this, "Sukces", "Newsletter do "+ allSubscribers.Count().ToString() +" subskrybentów został wysłany");
                    }
                    else
                    {
                        return View().WithError(this, "Błąd", "Coś poszło nie tak - nie wysłano newslettera.");
                    }
                }
                else
                {
                    return View(objModelMail).WithInfo(this, "Pusta lista", "Brak subskrybentów - nie wysłano newslettera.");
                }                
            }
            else
            {
                return View().WithWarning(this, "Formularz", "Formularz jest nieprawidłowo uzupełniony");
            }
        }


    }
}