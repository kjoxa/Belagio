using devarts.Helpers;
using devarts.Models;
using devarts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace devarts.Controllers
{
    [LayoutInjecter("_adminLayout")]
    [Authorize]
    public class HealthController : Controller
    {
        private KennelRepository _kennelRepo;
        private AssistantRepository _assistantRepo;

        public HealthController()
        {
            _kennelRepo = new KennelRepository();
            _assistantRepo = new AssistantRepository();
        }

        public List<SelectListItem> DiseasesList()
        {
            List<SelectListItem> currency = new List<SelectListItem>();
            currency.Add(new SelectListItem { Text = "Wścieklizna", Value = "Odżywianie" });
            currency.Add(new SelectListItem { Text = "DHPPi", Value = "Leczenie" });
            currency.Add(new SelectListItem { Text = "Leptospiroza", Value = "Profilaktyka" });
            currency.Add(new SelectListItem { Text = "Borelioza", Value = "Krycia" });
            currency.Add(new SelectListItem { Text = "Herpeswiroza", Value = "Sprzedaż" });
            currency.Add(new SelectListItem { Text = "Przeciwgrzybicza", Value = "Akcesoria" });
            currency.Add(new SelectListItem { Text = "Koronawiroza", Value = "Akcesoria" });
            currency.Add(new SelectListItem { Text = "Immunostymulator", Value = "Wystawy" });
            currency.Add(new SelectListItem { Text = "Inne", Value = "Wystawy" });
            return currency;
        }

        public List<SelectListItem> VaccinesList()
        {
            List<SelectListItem> currency = new List<SelectListItem>();
            currency.Add(new SelectListItem { Text = "Wścieklizna", Value = "Odżywianie" });
            currency.Add(new SelectListItem { Text = "DHPPi", Value = "Leczenie" });
            currency.Add(new SelectListItem { Text = "Leptospiroza", Value = "Profilaktyka" });
            currency.Add(new SelectListItem { Text = "Borelioza", Value = "Krycia" });
            currency.Add(new SelectListItem { Text = "Herpeswiroza", Value = "Sprzedaż" });
            currency.Add(new SelectListItem { Text = "Przeciwgrzybicza", Value = "Akcesoria" });
            currency.Add(new SelectListItem { Text = "Immunostymulator", Value = "Wystawy" });
            currency.Add(new SelectListItem { Text = "Inne", Value = "Wystawy" });
            return currency;
        }

        public List<SelectListItem> ProfilacticList()
        {
            List<SelectListItem> currency = new List<SelectListItem>();
            currency.Add(new SelectListItem { Text = "Odrobaczenie", Value = "Odżywianie" });
            currency.Add(new SelectListItem { Text = "Pchły i kleszcze", Value = "Leczenie" });
            currency.Add(new SelectListItem { Text = "Inne", Value = "Profilaktyka" });
            currency.Add(new SelectListItem { Text = "Borelioza", Value = "Krycia" });
            currency.Add(new SelectListItem { Text = "Herpeswiroza", Value = "Sprzedaż" });
            currency.Add(new SelectListItem { Text = "Przeciwgrzybicza", Value = "Akcesoria" });
            currency.Add(new SelectListItem { Text = "Immunostymulator", Value = "Wystawy" });
            return currency;
        }

        public string GetColorByCathegory(string cathegory)
        {
            switch (cathegory)
            {
                case "Szczepienie": return "#c95d5d";
                case "Badanie": return "#3a8bbd";
                case "Profilaktyka": return "#3cad6a";
                case "Leczenie": return "#bd7e3a";
                default:
                    break;
            }
            return "blue";
        }

        public ActionResult HealthAndVaccines()
        {
            var dogs = _kennelRepo.GetAllDogs();
            var healthAndVaccines = _assistantRepo.GetHealthAndVaccinationsList();
            int barPercent;
            DateTime finishedDate;

            List<HAV_Dog> havList = new List<HAV_Dog>();            

            foreach (var dog in dogs)
            {
                HAV_Dog hav = new HAV_Dog();

                hav.Dog = dog;
                hav.HealthAndVaccinationsForDog = healthAndVaccines.Where(d => d.DogId == dog.Id);

                foreach (var health in hav.HealthAndVaccinationsForDog)
                {
                    finishedDate = (health.CreateDate.AddDays(health.DaysToRepeat));
                    barPercent = ((health.DaysToRepeat - (finishedDate - DateTime.Now).Days) * 100 / health.DaysToRepeat);
                    health.ProgressBar = "<div class='progress' style='height:10px'>" +
                                         "<div class='progress-bar progress-bar-striped role='progressbar' style='width: " +
                                         barPercent + "%; background-color:"+ GetColorByCathegory(health.Cathegory) +";' aria-valuenow='10%' aria-valuemin='0' aria-valuemax='valueOfBornDays'></div></div>";
                }

                havList.Add(hav);                
            }

            return View(havList);
        }
    }
}