using devarts.Helpers;
using devarts.Models;
using devarts.Repositories;
using NLog;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace devarts.Controllers
{
    [LayoutInjecter("_adminLayout")]
    [Authorize]
    public class FinanceController : Controller
    {
        private FinanceRepository _financeRepo;
        private static Logger nLog = LogManager.GetCurrentClassLogger();

        /*        
        Odżywianie
        Leczenie
        Profilaktyka
        Krycia
        Sprzedaż
        Akcesoria
        Wystawy
        Sport
        Inne
        */

        public JsonResult ShowText(string json)
        {
            string jsonr = new JavaScriptSerializer().Serialize(json);
            return Json(jsonr, JsonRequestBehavior.AllowGet);
        }

        public FinanceController()
        {
            _financeRepo = new FinanceRepository();

            ViewBag.Currency = Currency();
            ViewBag.Cathegories = Cathegories();
        }

        public List<SelectListItem> Currency()
        {
            List<SelectListItem> currency = new List<SelectListItem>
            {
                new SelectListItem { Text = "Polski złoty", Value = "PLN" },
                new SelectListItem { Text = "Euro", Value = "EUR" },
                new SelectListItem { Text = "Dolar", Value = "EUR" }
            };
            return currency;
        }

        public List<SelectListItem> Cathegories()
        {
            List<SelectListItem> currency = new List<SelectListItem>
            {
                new SelectListItem { Text = "Odżywianie", Value = "Odżywianie" },
                new SelectListItem { Text = "Leczenie", Value = "Leczenie" },
                new SelectListItem { Text = "Profilaktyka", Value = "Profilaktyka" },
                new SelectListItem { Text = "Krycia", Value = "Krycia" },
                new SelectListItem { Text = "Sprzedaż", Value = "Sprzedaż" },
                new SelectListItem { Text = "Akcesoria", Value = "Akcesoria" },
                new SelectListItem { Text = "Wystawy", Value = "Wystawy" },
                new SelectListItem { Text = "Sport", Value = "Sport" },
                new SelectListItem { Text = "Inne", Value = "Inne" }
            };
            return currency;
        }

        public JsonResult UpdateBarChart(int year)
        {
            FinanceCharts financeCharts = new FinanceCharts();
            var resultFin = financeCharts.YearFinanceBar(year);
            string jsonData = new JavaScriptSerializer().Serialize(resultFin);
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index()
        {
            NBP_API_Helper nbp = new NBP_API_Helper();            
            ViewBag.EUR = nbp.GetCurrencyToday("EUR");
            if (ViewBag.EUR.Contains("404: Brak wyników"))
            {
                ViewBag.EUR = "od 07:00";
            }
            
            nLog.Info("Odczytano kurs euro: " + ViewBag.EUR);
            ViewBag.USD = nbp.GetCurrencyToday("USD");
            if (ViewBag.USD.Contains("404: Brak wyników"))
            {
                ViewBag.USD = "od 07:00";
            }

            nLog.Info("Odczytano kurs dolara: " + ViewBag.USD);

            decimal sumExpense = 0;
            decimal sumNotExpense = 0;

            if (_financeRepo.GetFinances().Where(fin => fin.IsExpense).Count() > 0)
            {
                sumExpense = _financeRepo.GetFinances().Where(fin => fin.IsExpense).Select(fin => fin.Amount).Sum();
            }

            if (_financeRepo.GetFinances().Where(fin => fin.IsExpense == false).Count() > 0)
            {
                sumNotExpense = _financeRepo.GetFinances().Where(fin => fin.IsExpense == false).Select(fin => fin.Amount).Sum();
            }

            ViewBag.Expense = string.Format("{0:#,##0}", sumExpense) + " PLN";
            ViewBag.NotExpense = string.Format("{0:#,##0}", sumNotExpense) + " PLN";

            var nowYear = DateTime.Now.Year;
            FinanceCharts finc = new FinanceCharts();
            ViewBag.monthsNames = finc.YearFinanceBar(nowYear).monthsNames;
            ViewBag.monthsNamesJS = finc.YearFinanceBar(nowYear).monthsNamesJS;
            ViewBag.monthsIncome = finc.YearFinanceBar(nowYear).monthsIncome;
            ViewBag.monthsExpense = finc.YearFinanceBar(nowYear).monthsExpense;

            ViewBag.sumCathegories = finc.YearFinancePie(nowYear).sumCathegories;
            return View();
        }

        [HttpPost]
        public JsonResult AddFinance(Finance model)
        {
            NBP_API_Helper nbp = new NBP_API_Helper();
            ViewBag.EUR = nbp.GetCurrencyToday("EUR"); nLog.Info("Odczytano kurs euro: " + ViewBag.EUR);
            ViewBag.USD = nbp.GetCurrencyToday("USD"); nLog.Info("Odczytano kurs dolara: " + ViewBag.USD);

            if (ModelState.IsValid)
            {
                Finance fin = new Finance();
                DateTime createDate = DateTime.Now;
                fin.Name = model.Name;
                fin.Description = model.Description;

                fin.DateFrom = model.DateFrom;
                fin.DateTo = model.DateTo;
                fin.Cathegory = model.Cathegory;
                fin.Amount = model.Amount;
                fin.CurrencyName = model.CurrencyName;
                fin.IncludeFinance = model.IncludeFinance;
                fin.IsExpense = model.IsExpense;
                
                fin.CreateDate = createDate;
                fin.ModifiedDate = createDate;

                _financeRepo.AddFinance(fin);
                _financeRepo.SaveChanges();
                
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { success = false }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult EditFinance(int id)
        {
            try
            {
                var editFinance = _financeRepo.GetFinanceById(id);
                if (editFinance != null)
                {
                    Finance fin = editFinance;
                    return PartialView("_editFinance", fin);
                }

                return PartialView("_editFinance", null);
            }
            catch (Exception ex)
            {                
                nLog.Error("Błąd podczas odczytywania okna edytowania finansów: " + ex.ToString());
                return PartialView("_editFinance", null).WithError(this, "Błąd", "Błąd: " + ex.ToString());
            }
        }

        [HttpPost]
        public JsonResult EditFinance(Finance model)
        {
            try
            {
                var editFinance = _financeRepo.GetFinanceById(model.Id);
                if (ModelState.IsValid)
                {                    
                    editFinance.Name = model.Name;
                    editFinance.Description = model.Description;
                    editFinance.DateFrom = model.DateFrom;
                    editFinance.DateTo = model.DateTo;
                    editFinance.Cathegory = model.Cathegory;
                    editFinance.Amount = model.Amount;
                    editFinance.CurrencyName = model.CurrencyName;
                    editFinance.IncludeFinance = model.IncludeFinance;
                    editFinance.IsExpense = model.IsExpense;
                    editFinance.ModifiedDate = DateTime.Now;

                    TryUpdateModel(editFinance);
                    _financeRepo.SaveChanges();
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }

                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                nLog.Error("Błąd podczas edytowania finansów: " + ex.ToString());
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        // AJAX-owe wywołanie metody usuwającej daną pozycję finansową
        public void RemoveFinance(int id)
        {
            try
            {
                var finToRemove = _financeRepo.GetFinanceById(id);
                if (finToRemove != null)
                {
                    _financeRepo.RemoveFinance(finToRemove);
                    _financeRepo.SaveChanges();
                    Response.StatusCode = 200;
                }
            }
            catch
            {
                // wysłanie do jQuery statusu Error
                Response.StatusCode = 500;
            }
        }
    }
}