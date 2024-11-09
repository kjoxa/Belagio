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
    public class ItalianGreyhoundController : Controller
    {
        private KennelRepository _kennelRepo;
        private KennelDbContext _db;
        private static Logger nLog = LogManager.GetCurrentClassLogger();

        public ItalianGreyhoundController()
        {
            _kennelRepo = new KennelRepository();
            _db = new KennelDbContext();
        }
        // GET: Pedigree
        public ActionResult Index()
        {
            var dogs = _kennelRepo.GetAllDogs().Where(d => d.BreedLink.Contains("Ital")).ToList();
            return View(dogs);
        }

        public ActionResult StudDogs()
        {
            var dogs = _kennelRepo.GetAllDogs().Where(d => d.BreedLink.Contains("Ital") && d.DogSex == true && d.IsReproductor == true).ToList();
            return View(dogs);
        }
    }
}