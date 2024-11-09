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
    public class ReproductionController : Controller
    {
        AssistantRepository _assistRepo;
        KennelRepository _kennelRepo;
        private static Logger nLog = LogManager.GetCurrentClassLogger();

        public ReproductionController()
        {
            _assistRepo = new AssistantRepository();
            _kennelRepo = new KennelRepository();
        }

        public List<SelectListItem> BitchesList()
        {
            List<SelectListItem> dogSelect = new List<SelectListItem>();
            var allBitches = _kennelRepo.GetAllDogs().Where(d => d.DogSex == false).OrderBy(d => d.BornDate);
            string dogName;
            foreach (var dog in allBitches)
            {
                dogName = dog.DogName.Substring(0, 1).ToUpper() + dog.DogName.Substring(1, dog.DogName.Length - 1).ToLower();
                dogSelect.Add(new SelectListItem { Text = dogName, Value = dogName });
            }

            return dogSelect;
        }

        public ActionResult Index()
        {                                   
            ViewBag.BitchesList = BitchesList();

            List<ReproductionAndDog> returnList = new List<ReproductionAndDog>();
            var allBitches = _kennelRepo.GetAllDogs().Where(d => d.DogSex == false).OrderBy(d => d.BornDate);

            foreach(var newPos in allBitches)
            {
                var searchInReproductions = _assistRepo.GetReproductionByLastDogName(newPos.DogName);
                if (searchInReproductions != null)
                {
                    ReproductionAndDog newRep = new ReproductionAndDog();
                    newRep.Dog = newPos;
                    newRep.Reproduction = searchInReproductions;
                    returnList.Add(newRep);
                }                
            }

            ReproductionStatisticView returnView = new ReproductionStatisticView
            {
                Reproduction = new Reproduction(),
                ReproductionAndDog = returnList 
            };
            
            return View(returnView);
        }            

        [HttpPost]
        public JsonResult AddReproduction(Reproduction model)
        {            
            if (ModelState.IsValid)
            {
                Reproduction rep = new Reproduction();
                DateTime createDate = DateTime.Now;
                rep.DogName = model.DogName;
                rep.FatherName = model.FatherName;
                rep.EstrusStartDate = model.EstrusStartDate;                
                rep.MatingDate_First = model.MatingDate_First;

                // pola muszą być wypełnione, bo inaczej nie można dodawać pozycji
                rep.RutStartDate = createDate;
                rep.EstrusEndDate = createDate;
                rep.RutEndDate = createDate;
                rep.MatingDate_Second = createDate;
                rep.MatingDate_Third = createDate;
                rep.EstimationBornDate = createDate;
                rep.NextEstrusDate = createDate;
                rep.DateOfBorn = createDate;

                var searchDog = _kennelRepo.GetAllDogs().FirstOrDefault(d => d.DogName.ToLower() == model.DogName);
                if (searchDog != null)
                {
                    rep.DogId = searchDog.Id;
                } 

                TryUpdateModel(rep);
                _assistRepo.Add(rep);
                _assistRepo.SaveChanges();

                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { success = false }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult EditReproduction(int id)
        {
            ViewBag.BitchesList = BitchesList();
            try
            {
                var editReproduction = _assistRepo.GetReproductionById(id);
                if (editReproduction != null)
                {
                    Reproduction fin = editReproduction;
                    return PartialView("_editReproduction", fin);
                }

                return PartialView("_editReproduction", null);
            }
            catch (Exception ex)
            {
                nLog.Error("Błąd podczas odczytywania okna edytowania cieczek: " + ex.ToString());
                return PartialView("_editReproduction", null).WithError(this, "Błąd", "Błąd: " + ex.ToString());
            }
        }

        [HttpPost]
        public JsonResult EditReproduction(Reproduction model)
        {
            ViewBag.BitchesList = BitchesList();
            try
            {
                var editReproduction = _assistRepo.GetReproductionById(model.Id);
                if (ModelState.IsValid)
                {
                    editReproduction.DogName = model.DogName;
                    
                    TryUpdateModel(editReproduction);
                    _assistRepo.SaveChanges();
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }

                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                nLog.Error("Błąd podczas edytowania cieczek: " + ex.ToString());
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        public void RemoveReproduction(int id)
        {
            try
            {
                var repToRemove = _assistRepo.GetReproductionById(id);
                if (repToRemove != null)
                {
                    _assistRepo.Remove(repToRemove);
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
    }
}