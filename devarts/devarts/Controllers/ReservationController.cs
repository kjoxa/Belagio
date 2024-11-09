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
    [Authorize]
    public class ReservationController : Controller
    {
        private AssistantRepository _assistRepo;
        private static Logger nLog = LogManager.GetCurrentClassLogger();

        public ReservationController()
        {
            _assistRepo = new AssistantRepository();
        }

        public List<SelectListItem> Breed()
        {
            List<SelectListItem> breed = new List<SelectListItem>();
            breed.Add(new SelectListItem { Text = "--- Rasa / Breed ---", Value = null });
            breed.Add(new SelectListItem { Text = "Charcik Włoski", Value = "Charcik Włoski" });
            breed.Add(new SelectListItem { Text = "Chart Rosyjski (Borzoi)", Value = "Chart Rosyjski (Borzoi)" });
            return breed;
        }

        public List<SelectListItem> Sex()
        {
            List<SelectListItem> sex = new List<SelectListItem>();
            sex.Add(new SelectListItem { Text = "--- Płeć / Sex ---", Value = null });
            sex.Add(new SelectListItem { Text = "Suka", Value = "Suka" });
            sex.Add(new SelectListItem { Text = "Pies", Value = "Pies" });
            sex.Add(new SelectListItem { Text = "Nieistotne", Value = "Nieistotne" });
            return sex;
        }

        /// ItalianGreyhound
        //public List<SelectListItem> Colour()
        //{
        //    List<SelectListItem> tail = new List<SelectListItem>();
        //    tail.Add(new SelectListItem { Text = "--- Colour / Kolor ---", Value = null });
        //    tail.Add(new SelectListItem { Text = "Błękitny", Value = "Błękitny" });
        //    tail.Add(new SelectListItem { Text = "Izabelowaty", Value = "Izabelowaty" });
        //    tail.Add(new SelectListItem { Text = "Czarny", Value = "Czarny" });
        //    tail.Add(new SelectListItem { Text = "Nieistotne", Value = "Nieistotne" });
        //    return tail;
        //}

        /// SpanishWaterDog
        public List<SelectListItem> Colour()
        {
            List<SelectListItem> colour = new List<SelectListItem>();
            colour.Add(new SelectListItem { Text = "--- Colour / Kolor ---", Value = null });
            colour.Add(new SelectListItem { Text = "Biały / White", Value = "Biały / White" });
            colour.Add(new SelectListItem { Text = "Czarny / Black", Value = "Czarny / Black" });
            colour.Add(new SelectListItem { Text = "Brązowy / Brown", Value = "Brązowy / Brown" });
            colour.Add(new SelectListItem { Text = "Biało - Czarny / White - Black", Value = "Biało - Czarny / White - Black" });
            colour.Add(new SelectListItem { Text = "Czarno - Biały / Black - White", Value = "Czarno - Biały / Black - White" });
            colour.Add(new SelectListItem { Text = "Biało - Brązowy / White - Brown", Value = "Biało - Brązowy / White - Brown" });
            colour.Add(new SelectListItem { Text = "Brązowo - Biały / Brown - White", Value = "Brązowo - Biały / Brown - White" });            
            return colour;
        }

        /// Ogon, Tails
        public List<SelectListItem> Taillen()
        {
            List<SelectListItem> tail = new List<SelectListItem>();
            tail.Add(new SelectListItem { Text = "--- Ogon / Tail ---", Value = null });
            tail.Add(new SelectListItem { Text = "Długi / Long tail", Value = "Długi / Long tail" });
            tail.Add(new SelectListItem { Text = "Krótki / Short tail", Value = "Krótki / Short tail" });
            tail.Add(new SelectListItem { Text = "Nieistotne / Irrelevant", Value = "Nieistotne / Irrelevant" });
            return tail;
        }

        public List<SelectListItem> PrefferedEnergyLevel()
        {
            List<SelectListItem> energyLevel = new List<SelectListItem>();
            energyLevel.Add(new SelectListItem { Text = "--- Energia / Energy level ---", Value = null });
            energyLevel.Add(new SelectListItem { Text = "Niski", Value = "Niski" });
            energyLevel.Add(new SelectListItem { Text = "Średni", Value = "Średni" });
            energyLevel.Add(new SelectListItem { Text = "Wysoki", Value = "Wysoki" });
            energyLevel.Add(new SelectListItem { Text = "Nieistotne", Value = "Nieistotne" });
            return energyLevel;
        }

        public List<SelectListItem> PrefferedSize()
        {
            List<SelectListItem> energyLevel = new List<SelectListItem>();
            energyLevel.Add(new SelectListItem { Text = "--- Rozmiar / Size ---", Value = null });
            energyLevel.Add(new SelectListItem { Text = "Mały", Value = "Mały" });
            energyLevel.Add(new SelectListItem { Text = "Średni", Value = "Średni" });
            energyLevel.Add(new SelectListItem { Text = "Większy", Value = "Większy" });
            energyLevel.Add(new SelectListItem { Text = "Nieistotne", Value = "Nieistotne" });
            return energyLevel;
        }

        public List<SelectListItem> ReadyForDog()
        {
            List<SelectListItem> readyForDog = new List<SelectListItem>();
            readyForDog.Add(new SelectListItem { Text = "--- Gotowość za / Ready for time ---", Value = null });
            readyForDog.Add(new SelectListItem { Text = "W przeciągu miesiąca", Value = "W przeciągu miesiąca" });
            readyForDog.Add(new SelectListItem { Text = "W przeciągu 3 miesięcy", Value = "W przeciągu 3 miesięcy" });
            readyForDog.Add(new SelectListItem { Text = "Za pół roku", Value = "Za pół roku" });
            readyForDog.Add(new SelectListItem { Text = "Za rok", Value = "Za rok" });
            readyForDog.Add(new SelectListItem { Text = "Nieistotne", Value = "Nieistotne" });
            return readyForDog;
        }

        public List<SelectListItem> Priority()
        {
            List<SelectListItem> priority = new List<SelectListItem>();
            priority.Add(new SelectListItem { Text = "Niski", Value = "Niski" });
            priority.Add(new SelectListItem { Text = "Średni", Value = "Średni" });
            priority.Add(new SelectListItem { Text = "Wysoki", Value = "Wysoki" });
            return priority;
        }

        public List<SelectListItem> ForBreeding()
        {
            List<SelectListItem> forBree = new List<SelectListItem>();            
            forBree.Add(new SelectListItem { Text = "Nie", Value = "0" });
            forBree.Add(new SelectListItem { Text = "Tak, do hodowli", Value = "1" });

            return forBree;
        }

        public ActionResult Index()
        {
            try
            {
                ViewBag.DropdownBreed = Breed();
                ViewBag.DropdownSex = Sex();
                ViewBag.DropdownColour = Colour();
                ViewBag.PrefferedEnergyLevel = PrefferedEnergyLevel();
                ViewBag.PrefferedSize = PrefferedSize();
                ViewBag.ReadyForDog = ReadyForDog();
                ViewBag.PriorityLevel = Priority();
                ViewBag.ForBreeding = ForBreeding();
                return View();
            }
            catch (Exception ex)
            {
                nLog.Error(ex.ToString);
                return RedirectToAction("Index","Admin");
            }            
        }

        [HttpPost]
        public JsonResult AddReservation(Reservation model)
        {
            model.Rodo = true;
            ViewBag.DropdownBreed = Breed();
            ViewBag.DropdownSex = Sex();
            ViewBag.DropdownColour = Colour();
            ViewBag.PrefferedEnergyLevel = PrefferedEnergyLevel();
            ViewBag.PrefferedSize = PrefferedSize();
            ViewBag.ReadyForDog = ReadyForDog();
            ViewBag.PriorityLevel = Priority();
            ViewBag.ForBreeding = ForBreeding();

            if (ModelState.IsValid)
            {
                Reservation res = new Reservation();
                DateTime createDate = DateTime.Now;
                DateTime modifiedDate = createDate;

                res.FirstName = model.FirstName;
                res.SurName = model.SurName;
                res.Email = model.Email;
                res.PhoneNumber = model.PhoneNumber;
                res.Country = model.Country;
                res.City = model.City;

                res.Breed = model.Breed;
                res.Colour = model.Colour;
                res.DogSize = model.DogSize;
                res.AdditionalResidents = model.AdditionalResidents;
                res.PreferredMother = model.PreferredMother;
                res.Sex = model.Sex;
                res.EnergyLevel = model.EnergyLevel;
                res.ReadyToDog = model.ReadyToDog;
                res.Alergies = model.Alergies;
                res.PreferredFather = model.PreferredFather;

                res.ClientComments = model.ClientComments;
                res.DogForKennel = model.DogForKennel;
                res.DogForSport = model.DogForSport;
                res.Priority = model.Priority;
                res.DeliveryMethod = model.DeliveryMethod;
                res.PaymentStatus = model.PaymentStatus;
                res.BackendComments = model.BackendComments;
                res.Rodo = model.Rodo;

                res.CreateByClient = false;
                res.CreateDate = createDate;
                res.ModifiedDate = modifiedDate;
                //TryUpdateModel(rep);
                _assistRepo.AddReservation(res);
                _assistRepo.SaveChanges();

                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            } 
            else
            {
                string messages = string.Join("; ", ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage));
                nLog.Error(messages);
            }
            return Json(new { success = false }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult EditReservation(int id)
        {
            ViewBag.DropdownBreed = Breed();
            ViewBag.DropdownSex = Sex();
            ViewBag.DropdownColour = Colour();
            ViewBag.PrefferedEnergyLevel = PrefferedEnergyLevel();
            ViewBag.PrefferedSize = PrefferedSize();
            ViewBag.ReadyForDog = ReadyForDog();
            ViewBag.PriorityLevel = Priority();

            try
            {
                var editReservation = _assistRepo.GetReservationById(id);
                if (editReservation != null)
                {
                    Reservation res = editReservation;
                    return PartialView("_editReservation", res);
                }

                return PartialView("_editReservation", null);
            }
            catch (Exception ex)
            {
                nLog.Error("Błąd podczas odczytywania okna edytowania rezerwacji: " + ex.ToString());
                return PartialView("_editReservation", null).WithError(this, "Błąd", "Błąd: " + ex.ToString());
            }
        }

        [HttpPost]
        public JsonResult EditReservation(Reservation model)
        {
            ViewBag.DropdownBreed = Breed();
            ViewBag.Sex = Sex();
            ViewBag.Colour = Colour();
            ViewBag.PrefferedEnergyLevel = PrefferedEnergyLevel();
            ViewBag.PrefferedSize = PrefferedSize();
            ViewBag.DropdownReadyForDog = ReadyForDog();
            ViewBag.PriorityLevel = Priority();

            try
            {
                var res = _assistRepo.GetReservationById(model.Id);
                DateTime modifiedDate = DateTime.Now;

                if (ModelState.IsValid)
                {
                    res.FirstName = model.FirstName;
                    res.SurName = model.SurName;
                    res.Email = model.Email;
                    res.PhoneNumber = model.PhoneNumber;
                    res.Country = model.Country;
                    res.City = model.City;

                    res.Breed = model.Breed;
                    res.Colour = model.Colour;
                    res.DogSize = model.DogSize;
                    res.AdditionalResidents = model.AdditionalResidents;
                    res.PreferredMother = model.PreferredMother;
                    res.Sex = model.Sex;
                    res.EnergyLevel = model.EnergyLevel;
                    res.ReadyToDog = model.ReadyToDog;
                    res.Alergies = model.Alergies;
                    res.PreferredFather = model.PreferredFather;

                    res.ClientComments = model.ClientComments;
                    res.DogForKennel = model.DogForKennel;
                    res.DogForSport = model.DogForSport;
                    res.Priority = model.Priority;
                    res.DeliveryMethod = model.DeliveryMethod;
                    res.PaymentStatus = model.PaymentStatus;
                    res.BackendComments = model.BackendComments;
                    res.Rodo = model.Rodo;
                    res.ModifiedDate = DateTime.Now;

                    res.CreateByClient = false;
                    res.ModifiedDate = modifiedDate;

                    TryUpdateModel(res);
                    _assistRepo.SaveChanges();
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }

                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                nLog.Error("Błąd podczas edytowania rezerwacji: " + ex.ToString());
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        public void ReverseReadStatus(int id)
        {
            try
            {
                var resToChange = _assistRepo.GetReservationById(id);
                if (resToChange != null)
                {
                    if (resToChange.IsReaded == false)
                    {
                        resToChange.IsReaded = true;
                    }
                    else{
                        resToChange.IsReaded = false;
                    }
                    _assistRepo.SaveChanges();
                    Response.StatusCode = 200;
                }
            }
            catch
            {
                // wysłanie do jQuery statusu Error
                Response.StatusCode = 500;
            }
        }

        public void ReverseClosedStatus(int id)
        {
            try
            {
                var resToChange = _assistRepo.GetReservationById(id);
                if (resToChange != null)
                {
                    if (resToChange.IsClosed == false)
                    {
                        resToChange.IsClosed = true;
                    }
                    else
                    {
                        resToChange.IsClosed = false;
                    }
                    _assistRepo.SaveChanges();
                    Response.StatusCode = 200;
                }
            }
            catch
            {
                // wysłanie do jQuery statusu Error
                Response.StatusCode = 500;
            }
        }

        public void RemoveReservation(int id)
        {
            try
            {
                var resToRemove = _assistRepo.GetReservationById(id);
                if (resToRemove != null)
                {
                    _assistRepo.RemoveReservation(resToRemove);
                    _assistRepo.SaveChanges();
                    Response.StatusCode = 200;
                }
            }
            catch
            {
                // wysłanie do jQuery statusu Error
                Response.StatusCode = 500;
            }
        }

        [HttpGet]
        public ActionResult SendEmail(int id)
        {
            var sentTo = _assistRepo.GetReservationById(id);
            if (sentTo != null)
            {
                Contact con = new Contact();
                con.Email = sentTo.Email;
                con.Subject = "Belagio - Powiadomienie o dostępności szczenięcia";
                con.Content = "Sz. P.\n\nPojawiło się szczenię spełniające oczekiwania z formularza rezerwacji.\n\nProszę o kontakt telefoniczny pod numerem: + 48 886-614-215\n\nZ wyrazami szacunku\nDominika Kania";
                return PartialView("_sendEmail", con);
            }

            return PartialView("_sendEmail", null);
        }

        [HttpPost]
        public ActionResult SendEmail(ReservationAndSendViewModel model)
        {
            try
            {
                Reservation res = new Reservation();
                res.Id = model.reservation.Id;

                Contact con = new Contact();
                con.Email = model.contact.Email;
                con.Subject = "Belagio - Powiadomienie o dostępności szczenięcia";
                con.Content = "<b>Sz. P.</b><br><br>Pojawiło się szczenię spełniające oczekiwania z formularza rezerwacji.<br>Proszę o kontakt telefoniczny pod numerem:<br><b> + 48 886-614-215</b><br><br><b>Z wyrazami szacunku</b><br>Dominika Kania";
                MailHelper.SendEmailFromReservationList(con.Email, con.Subject, con.Content);
                return RedirectToAction("Index", "Reservation", null).WithSuccess(this, "Wysłano", "Wiadomość do " + con.Email + " została wysłana.");
            }
            catch
            {
                return RedirectToAction("Index", "Reservation", null).WithError(this, "Błąd", "Wiadomość nie została wysłana."); ;
            }             
        }
    }
}