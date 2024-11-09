using devarts.Helpers;
using devarts.Models;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace devarts.Controllers
{
    public class ContactController : Controller
    {
        private static Logger nLog = LogManager.GetCurrentClassLogger();

        public ActionResult Index()
        {
            TempData["Eyfruntopas"] = DateTime.Now;

            return View();
        }

        public ActionResult Send()
        {            
            return View();
        }

        //public ActionResult Sent()
        //{
        //    return View();
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Send(Contact contact)
        {
            try
            {
                double secondsToFill = 0;

                if (TempData["Eyfruntopas"] != null)
                {
                    DateTime formStartTime = (DateTime)TempData["Eyfruntopas"];
                    DateTime formEndTime = DateTime.Now;

                    // Oblicz różnicę czasu w sekundach
                    TimeSpan timeToFill = formEndTime - formStartTime;
                    secondsToFill = timeToFill.TotalSeconds;
                }

                if (ModelState.IsValid && secondsToFill >= 30)
                {
                    // Wykorzystanie helpera do wysyłania e-maili
                    MailHelper.SendEmail(contact.Email, contact.Subject, contact.Content);
                    contact.Subject = "Potwierdzenie otrzymania wiadomości";
                    //contact.Content = "Szanowni Państwo!\n\nWiadomość została dostarczona na moją skrzynkę pocztową!\nPostaram się odpowiedzieć jak najszybciej!\n\nZ wyrazami szacunku\nDominika Kania";

                    contact.Content = "<h3 style='color: darkcyan; margin:0'><b>Szanowni Państwo!</b></h3>" +
                                  "Wiadomość została dostarczona na moją skrzynkę pocztową!<br><br>" +
                                  "Skontaktujemy się z Państwem jak najszybciej." +
                                  "<hr style='max-width: 100px; margin-left:0'>" +
                                  "<h3 style='color: darkcyan; margin:0'>Z wyrazami szacunku</h3>" +
                                  "<b>Dominika Kania</b>";

                    MailHelper.SendToRecepientEmail(contact.Email, contact.Subject, contact.Content);
                    return View("Sent");//.WithSuccess(this, "Wysłano", "Wiadomość została wysłana!");
                }
                else
                {
                    return View(contact).WithError(this, "Błąd", "Proszę uzupełnić lub poprawić pola w formularzu!");
                }
            }
            catch (Exception ex)
            {
                nLog.Error(ex.ToString());
                return RedirectToAction("Index", "Home");
            }            
        }
    }
}