using devarts.Helpers;
using devarts.Models;
using devarts.Repositories;
using NLog;
using NLog.Fluent;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace devarts.Controllers
{
    [Authorize]
    [LayoutInjecter("_adminLayout")]
    public class KennelController : Controller
    {

        private KennelRepository _kennelRepo;
        private KennelDbContext _db;
        private static Logger nLog = LogManager.GetCurrentClassLogger();

        public KennelController()
        {
            _kennelRepo = new KennelRepository();
            _db = new KennelDbContext();
            ViewBag.AvailableStatusDropdown = AvailableStatusDropdown();
        }

        public List<SelectListItem> DogSex()
        {
            List<SelectListItem> entryType = new List<SelectListItem>();
            entryType.Add(new SelectListItem { Text = "Suka", Value = "False" });
            entryType.Add(new SelectListItem { Text = "Samiec", Value = "True" });            

            return entryType;
        }

        public List<SelectListItem> GetBootstrapColumn()
        {
            List<SelectListItem> bootstrapCol = new List<SelectListItem>();
            bootstrapCol.Add(new SelectListItem { Text = "Cały rząd (1 element)", Value = "col-lg-12" });
            bootstrapCol.Add(new SelectListItem { Text = "1/2 rzędu (2 elementy)", Value = "col-lg-6" });
            bootstrapCol.Add(new SelectListItem { Text = "1/3 rzędu (3 elementy)", Value = "col-lg-4" });

            return bootstrapCol;
        }

        public List<SelectListItem> AvailableStatusDropdown()
        {
            List<SelectListItem> currency = new List<SelectListItem>();
            currency.Add(new SelectListItem { Text = "--- empty ---", Value = "" });
            currency.Add(new SelectListItem { Text = "AVAILABLE", Value = "AVAILABLE" });
            currency.Add(new SelectListItem { Text = "RESERVED", Value = "RESERVED" });
            currency.Add(new SelectListItem { Text = "PRE-RESERVED", Value = "PRE-RESERVED" });
            currency.Add(new SelectListItem { Text = "SOLD", Value = "SOLD" });
            currency.Add(new SelectListItem { Text = "UNDER OBSERVATION", Value = "UNDER OBSERVATION" });
            currency.Add(new SelectListItem { Text = "STAY WITH US", Value = "STAY WITH US" });
            return currency;
        }

        public List<SelectListItem> GetLetters()
        {
            List<SelectListItem> bootstrapCol = new List<SelectListItem>();
            for (char c = 'A'; c <= 'Z'; c++)
            {
                bootstrapCol.Add(new SelectListItem { Text = c.ToString(), Value = c.ToString() });
            }            

            return bootstrapCol;
        }

        public ActionResult Index()
        {
            var dogBreeds = _kennelRepo.GetAllDogBreed();
            var dogs = _kennelRepo.GetAllDogs();
            var litters = _kennelRepo.GetAllLitters();

            var breedAndDogs = new BreedDogLitterViewModel();
            breedAndDogs.dogBreeds = dogBreeds;
            breedAndDogs.dogs = dogs;
            breedAndDogs.litters = litters;

            //szybka konwersja daty
            //foreach (var dpos in dogs)
            //{
            //    dpos.BornDateDateTime = DateTime.Parse(dpos.BornDate);
            //    TryUpdateModel(dpos);
            //    _kennelRepo.SaveChanges();
            //}

            //var filePath = Path.Combine(HostingEnvironment.MapPath("~/Dogs/ItalianGreyhound/" + "Lea" + "/" + "m1.jpg"));
            //var dirPath = Path.Combine(HostingEnvironment.MapPath("~/Dogs/ItalianGreyhound/" + "Lea" + "/" + "X"));
            //if (System.IO.File.Exists(filePath) == true)
            //{
            //    System.IO.File.Delete(filePath);
            //    System.IO.Directory.Delete(dirPath);
            //    return View(breedAndDogs).WithSuccess(this, "Okej", "Ten plik istnieje");
            //}
            //else
            //{
            //    return View(breedAndDogs).WithError(this, "NIE!", "Ten plik nie istnieje");
            //}

            return View(breedAndDogs);
        }

        /// RASY

        [HttpGet]
        public ActionResult AddBreed()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddBreed(DogBreed model)
        {
            if (ModelState.IsValid)
            {
                var addBreed = new DogBreed();

                addBreed.BreedSpeciesName = model.BreedSpeciesName;
                addBreed.BreedSpeciesDisplayName = model.BreedSpeciesDisplayName;
                addBreed.BreedSpeciesDisplayNameEng = model.BreedSpeciesDisplayNameEng;
                addBreed.BreedDescription = model.BreedDescription;
                addBreed.BreedLink = model.BreedLink;

                addBreed.CreateDate = DateTime.Now.ToString();
                addBreed.Visibility = true;

                _kennelRepo.AddDogBreed(addBreed);
                _kennelRepo.SaveChanges();

                return RedirectToAction("Index").WithSuccess(this, "Gratulacje", "Rasa psa została dodana!");
            }
            return View();
        }

        /// PSY W RASACH
        
        [HttpGet]        
        public ActionResult AddDog(int id)
        {
            ViewBag.DogSex = DogSex();

            try
            {                
                var breed = _kennelRepo.GetDogBreedById(id);
                var dog = new Dog();

                if (breed != null)
                {                    
                    ViewBag.BreedName = breed.BreedSpeciesDisplayName;                    
                    dog.BreedLink = breed.BreedLink;

                    // pobieranie ID kolejnego rekordu przed submitem
                    var dogNextId = _kennelRepo.GetAllDogs().OrderByDescending(o => o.Id).FirstOrDefault();
                    if (dogNextId != null)
                    {
                        TempData["ProbablyNextDogId"] = dogNextId.Id + 1;
                    }
                    else
                    {
                        TempData["ProbablyNextDogId"] = 1;
                    }

                    return View(dog);
                }
                else
                {
                    return RedirectToAction("Index").WithError(this, "Błąd", "<br>Nie znaleziono rasy o podanym ID.<br> Nie można dodać psa.");
                }
            }
            catch
            {
                return RedirectToAction("Index").WithError(this,"Błąd", "<br>Nie rozpoznano ID rasy - nie można dodać zwierzęcia.");
            }            
        }

        [HttpPost]
        public ActionResult AddDog(int id, Dog dogModel)
        {
            ViewBag.DogSex = DogSex();
            string dateTime = DateTime.Now.ToString();

            try
            {
                if (ModelState.IsValid)
                {
                    var breed = _kennelRepo.GetDogBreedById(id);

                    var newDog = new Dog();
                    newDog.SpeciesId = breed.Id;

                    // ustawianie zdjęcia głównego psa
                    var mainImage = _kennelRepo.GetImagesByDogLink(dogModel.DogLink).FirstOrDefault();                    
                    newDog.ImageId = mainImage.Id;
                    newDog.MainPicture = mainImage.ImageFileName;

                    // STANDARDOWE DANE
                    newDog.BreedLink = dogModel.BreedLink;
                    newDog.DogLink = dogModel.DogLink;
                    newDog.DogName = dogModel.DogName;
                    newDog.DogPetName = dogModel.DogPetName;
                    newDog.BornDate = dogModel.BornDate;
                    newDog.BornDateDateTime = DateTime.ParseExact(dogModel.BornDate, "dd.MM.yyyy", null);
                    newDog.DeathDate = dogModel.DeathDate;
                    newDog.CauseOfDeath = dogModel.CauseOfDeath;
                    newDog.DogColour = dogModel.DogColour;
                    newDog.DogSex = dogModel.DogSex;
                    newDog.DogDescription = dogModel.DogDescription;

                    // DANE HODOWLANE
                    newDog.Height = dogModel.Height;
                    newDog.Weight = dogModel.Weight;
                    newDog.CoursingLicenceNumber = dogModel.CoursingLicenceNumber;
                    newDog.BloodlineNumber = dogModel.BloodlineNumber;
                    newDog.DepartmentNumber = dogModel.DepartmentNumber;
                    newDog.ChipNumber = dogModel.ChipNumber;

                    // POCHODZENIE PSA
                    newDog.BreedName = dogModel.BreedName;
                    newDog.OwnerName = dogModel.OwnerName;
                    newDog.OwnerContact = dogModel.OwnerContact;
                    newDog.BreedArchiveUrl = dogModel.BreedArchiveUrl;
                    newDog.DogFatherName = dogModel.DogFatherName;
                    newDog.DogMotherName = dogModel.DogMotherName;
                    newDog.DogFatherBreedName = dogModel.DogFatherBreedName;
                    newDog.DogMotherBreedName = dogModel.DogMotherBreedName;
                    newDog.DogFatherUrl = dogModel.DogFatherUrl;
                    newDog.DogMotherUrl = dogModel.DogMotherUrl;
                    newDog.DogFatherCountry = dogModel.DogFatherCountry;
                    newDog.DogMotherCountry = dogModel.DogMotherCountry;
                    newDog.DogFatherCity = dogModel.DogFatherCity;
                    newDog.DogMotherCity = dogModel.DogMotherCity;

                    // BADANIA
                    newDog.DogMedicalDescription = dogModel.DogMedicalDescription;

                    // OSIĄGNIĘCIA
                    newDog.DegreeDescription = dogModel.DegreeDescription;
                    newDog.AchievementsDescription = dogModel.AchievementsDescription;

                    // WSPÓŁWŁASNOŚĆ
                    newDog.Coowner = dogModel.Coowner;
                    newDog.CoownerFirstName = dogModel.CoownerFirstName;
                    newDog.CoownerSurName = dogModel.CoownerSurName;
                    newDog.CoownerAddress = dogModel.CoownerAddress;
                    newDog.CoownerContact = dogModel.CoownerContact;

                    // WŁAŚCIWOŚCI
                    newDog.Visibility = false;
                    newDog.IsRetired = dogModel.IsRetired;
                    newDog.CreateDate = dateTime;
                    newDog.ModifiedDate = dateTime;
                    newDog.ShowCount = 0;
                    newDog.Tags = dogModel.Tags;
                    newDog.IsForBreeding = dogModel.IsForBreeding;
                    newDog.IsReproductor = dogModel.IsReproductor;                    

                    // NAVBAR
                    newDog.NavbarDogName = dogModel.NavbarDogName;
                    newDog.NavbarDogNameEng = dogModel.NavbarDogNameEng;
                    newDog.NavbarEnabled = false;
                    newDog.NavbarVisible = false;

                    // CO PIERWSZE
                    newDog.DogKennelNameFirst = false;
                    newDog.DogMotherKennelNameFirst = false;
                    newDog.DogFatherKennelNameFirst = false;


                    _kennelRepo.AddDog(newDog);
                    _kennelRepo.SaveChanges();

                    // TWORZENIE DRZEWA GENEALOGICZNEGO
                    PrepareDogTrees pdp = new PrepareDogTrees();
                    pdp.CreateDogGenealogicTree(newDog.DogLink, newDog.Id, "", 0, false, "/Dogs/" + newDog.BreedLink + "/" + newDog.DogLink + "/" + newDog.MainPicture);

                    return RedirectToAction("Index", "Kennel").WithSuccess(this, "Sukces", "<br>Dodano nowe zwierzę!");
                }
                else
                {
                    return View(dogModel).WithError(this, "Błąd", "<br>Formularz jest nieprawidłowo wypełniony.<br>Popraw formularz i spróbuj ponownie.");
                }
            }
            catch
            {
                return RedirectToAction("Index", "Kennel").WithError(this, "Błąd", "<br>Wystąpił problem podczas próby dodania zwierzęcia.<br>ID wyjątku: 2012X29b8");
            }                        
        }

        /// EDYCJA ZWIERZĘCIA        
        [HttpGet]
        public ActionResult EditDog([DefaultValue(0)] int id)
        {
            if (id == 0)
            {
                return RedirectToAction("Index").WithInfo(this, "<br>Wskazówka", "<br>Jeśli chcesz edytować zwierzę skorzystaj z przycisku 'Edytuj' znajdującego się obok zdjęcia zwierzęcia.");
            }
            ViewBag.DogSex = DogSex();

            var dog = _kennelRepo.GetDogById(id);
            var imagesByDog = _kennelRepo.GetImagesByDogId(id);

            if (!(dog != null && imagesByDog != null))
            {
                return RedirectToAction("Index").WithError(this, "Błąd", "<br>Nie znaleziono zwierzęcia o danym ID lub nie znaleziono zdjęć dopisanych do niego.");
            }

            DogWithImages dogWithImg = new DogWithImages
            {
                dog = dog,
                images = imagesByDog.AsQueryable()
            };            

            return View(dogWithImg);
        }

        [HttpPost]
        public ActionResult EditDog(DogWithImages model, int id)
        {
            try
            {
                ViewBag.DogSex = DogSex();
                string dateTime = DateTime.Now.ToString();

                var editiedDog = _kennelRepo.GetDogById(id);
                var imagesByDog = _kennelRepo.GetImagesByDogId(id);

                if (!(editiedDog != null && imagesByDog != null))
                {
                    return RedirectToAction("Index").WithError(this, "Błąd", "<br>Nie znaleziono zwierzęcia o danym ID lub nie znaleziono zdjęć dopisanych do niego.");
                }

                if (editiedDog != null)
                {
                    if (ModelState.IsValid)
                    {
                        var breed = _kennelRepo.GetDogBreedById(editiedDog.SpeciesId);
                        editiedDog.SpeciesId = breed.Id;

                        if (string.IsNullOrEmpty(editiedDog.MainPicture))
                        {
                            // ustawianie zdjęcia głównego psa
                            var mainImage = _kennelRepo.GetImagesByDogLink(model.dog.DogLink).FirstOrDefault();
                            editiedDog.ImageId = mainImage.Id;
                            editiedDog.MainPicture = mainImage.ImageFileName;
                        }

                        // STANDARDOWE DANE
                        editiedDog.BreedLink = model.dog.BreedLink;
                        editiedDog.DogLink = model.dog.DogLink;
                        editiedDog.DogName = model.dog.DogName;
                        editiedDog.DogPetName = model.dog.DogPetName;
                        editiedDog.BornDate = model.dog.BornDate;                        
                        editiedDog.BornDateDateTime = DateTime.ParseExact(model.dog.BornDate, "dd.MM.yyyy", null);
                        editiedDog.DeathDate = model.dog.DeathDate;
                        editiedDog.CauseOfDeath = model.dog.CauseOfDeath;
                        editiedDog.DogColour = model.dog.DogColour;
                        editiedDog.DogSex = model.dog.DogSex;
                        editiedDog.DogDescription = model.dog.DogDescription;

                        // DANE HODOWLANE
                        editiedDog.Height = model.dog.Height;
                        editiedDog.Weight = model.dog.Weight;
                        editiedDog.CoursingLicenceNumber = model.dog.CoursingLicenceNumber;
                        editiedDog.BloodlineNumber = model.dog.BloodlineNumber;
                        editiedDog.DepartmentNumber = model.dog.DepartmentNumber;
                        editiedDog.ChipNumber = model.dog.ChipNumber;

                        // POCHODZENIE PSA
                        editiedDog.BreedName = model.dog.BreedName;
                        editiedDog.OwnerName = model.dog.OwnerName;
                        editiedDog.OwnerContact = model.dog.OwnerContact;
                        editiedDog.BreedArchiveUrl = model.dog.BreedArchiveUrl;
                        editiedDog.DogFatherName = model.dog.DogFatherName;
                        editiedDog.DogMotherName = model.dog.DogMotherName;
                        editiedDog.DogFatherBreedName = model.dog.DogFatherBreedName;
                        editiedDog.DogMotherBreedName = model.dog.DogMotherBreedName;
                        editiedDog.DogFatherUrl = model.dog.DogFatherUrl;
                        editiedDog.DogMotherUrl = model.dog.DogMotherUrl;
                        editiedDog.DogFatherCountry = model.dog.DogFatherCountry;
                        editiedDog.DogMotherCountry = model.dog.DogMotherCountry;
                        editiedDog.DogFatherCity = model.dog.DogFatherCity;
                        editiedDog.DogMotherCity = model.dog.DogMotherCity;

                        // BADANIA
                        editiedDog.DogMedicalDescription = model.dog.DogMedicalDescription;

                        // OSIĄGNIĘCIA
                        editiedDog.DegreeDescription = model.dog.DegreeDescription;
                        editiedDog.AchievementsDescription = model.dog.AchievementsDescription;

                        // WSPÓŁWŁASNOŚĆ
                        editiedDog.Coowner = model.dog.Coowner;
                        editiedDog.CoownerFirstName = model.dog.CoownerFirstName;
                        editiedDog.CoownerSurName = model.dog.CoownerSurName;
                        editiedDog.CoownerAddress = model.dog.CoownerAddress;
                        editiedDog.CoownerContact = model.dog.CoownerContact;

                        // WŁAŚCIWOŚCI
                        editiedDog.Visibility = false;
                        editiedDog.IsRetired = model.dog.IsRetired;
                        editiedDog.CreateDate = dateTime;
                        editiedDog.ModifiedDate = dateTime;
                        editiedDog.ShowCount = 0;
                        editiedDog.Tags = model.dog.Tags;
                        editiedDog.IsForBreeding = model.dog.IsForBreeding;
                        editiedDog.IsReproductor = model.dog.IsReproductor;

                        // NAVBAR
                        editiedDog.NavbarDogName = model.dog.NavbarDogName;
                        editiedDog.NavbarDogNameEng = model.dog.NavbarDogNameEng;
                        editiedDog.NavbarEnabled = false;
                        editiedDog.NavbarVisible = false; 
                        
                        editiedDog.DogKennelNameFirst = model.dog.DogKennelNameFirst;
                        editiedDog.DogMotherKennelNameFirst = model.dog.DogMotherKennelNameFirst;
                        editiedDog.DogFatherKennelNameFirst = model.dog.DogFatherKennelNameFirst;

                        TryUpdateModel(editiedDog);
                        _kennelRepo.SaveChanges();

                        return RedirectToAction("Index", "Kennel").WithSuccess(this, "Sukces", "<br>Zakończono edycję");
                    }
                }
            }
            catch (Exception ex)
            {
                nLog.Error(ex.ToString());
            }
            

            return View();
        }

        /// DODAWANIE MIOTU
        
        [HttpGet]
        public ActionResult AddLitter([DefaultValue(0)] int id)
        {            
            ViewBag.DogSex = DogSex();
            ViewBag.Letters = GetLetters();

            ViewBag.BootstrapColumn = new SelectList(GetBootstrapColumn(), "Value", "Text", "Wybierz");

            try
            {
                var dog = _kennelRepo.GetDogById(id);

                if (dog != null)
                {
                    var addLitter = new Litter();

                    var dogView = _kennelRepo.GetDogById(id);
                    var breed = _kennelRepo.GetDogBreedById(dog.SpeciesId);                    

                    // pobieranie ID kolejnego rekordu przed submitem
                    /// błąd tu polega na tym, że jak ostatnia pozycja to np. 6 a zostały usunięte rekordy to następny będzie miał ID np 19, a to poniżej wstawia... 7
                    var litterNextId = _kennelRepo.GetAllLitters().OrderByDescending(o => o.Id).FirstOrDefault();
                    if (litterNextId != null)
                    {
                        TempData["ProbablyNexLitterId"] = litterNextId.Id + 1;
                    }
                    else
                    {
                        TempData["ProbablyNexLitterId"] = 1;
                    }

                    addLitter.BreedId = breed.Id;
                    addLitter.DogId = dogView.Id;

                    addLitter.BreedLink = breed.BreedLink;
                    addLitter.DogLink = dogView.DogLink;

                    return View(addLitter);
                }
                else
                {
                    return RedirectToAction("Index").WithInfo(this, "Wskazówka", "<br>Jeśli chcesz dodać miot, skorzystaj z zakładki 'Mioty' znajdującej się pod dodanym zwierzęciem.");
                }
            }
            catch
            {
                return RedirectToAction("Index").WithInfo(this, "Wskazówka", "<br>Jeśli chcesz dodać miot, skorzystaj z zakładki 'Mioty' znajdującej się pod dodanym zwierzęciem.");
            }
        }                

        [HttpPost]
        public ActionResult AddLitter(Litter model, int id)
        {
            ViewBag.DogSex = DogSex();
            ViewBag.Letters = GetLetters();
            string createDate = DateTime.Now.ToString();

            ViewBag.BootstrapColumn = new SelectList(GetBootstrapColumn(), "Value", "Text", "Wybierz");

            try
            {
                var dog = _kennelRepo.GetDogById(id);

                if (dog != null)
                {
                    var addLitter = new Litter();
                    var dogView = _kennelRepo.GetDogById(id);
                    var breed = _kennelRepo.GetDogBreedById(dog.SpeciesId);                    
                    addLitter.BreedId = breed.Id;
                    addLitter.DogId = dogView.Id;

                    addLitter.BreedLink = breed.BreedLink;
                    addLitter.DogLink = dogView.DogLink;

                    string dateTime = DateTime.Now.ToString();

                    if (ModelState.IsValid)
                    {
                        //addLitter.MainPicture = 
                        addLitter.LitterLink = model.LitterLink;
                        addLitter.Letter = model.Letter;
                        addLitter.LitterPresentationName = model.LitterPresentationName;
                        addLitter.MaleCount = model.MaleCount;
                        addLitter.FemaleCount = model.FemaleCount;
                        addLitter.DogFather = model.DogFather;
                        addLitter.DogFatherDegree = model.DogFatherDegree;
                        addLitter.DogFatherLink = model.DogFatherLink;
                        addLitter.Description = model.Description;
                        addLitter.BornDate = model.BornDate;

                        try
                        {
                            DateTime systemDate = DateTime.ParseExact(model.BornDate, "dd.MM.yyyy", CultureInfo.InvariantCulture);
                            addLitter.BornDateOkay = systemDate;
                        }
                        catch
                        {
                            addLitter.BornDateOkay = DateTime.Now;
                        }

                        addLitter.Tags = model.Tags;
                        addLitter.BootstrapColumn = model.BootstrapColumn;
                        addLitter.CreateDate = createDate;
                        addLitter.ModifiedDate = createDate;
                        //addLitter.ShowsCount = model.ShowsCount;

                        var mainImage = _kennelRepo.GetLitterImagesByLitterLink(model.LitterLink).FirstOrDefault();

                        if (mainImage != null)
                        {
                            addLitter.MainPicture = mainImage.ImageFileName;
                        }

                        // TWORZENIE DRZEWA GENEALOGICZNEGO
                        PrepareDogTrees pdp = new PrepareDogTrees();
                        pdp.CreateDogGenealogicTree(
                            addLitter.DogLink,
                            addLitter.Id,
                            addLitter.LitterLink,
                            0, 
                            true, // tu było false, a jest to parametr [IsLitter], dzięki któremu wiadomo, że dodana pozycja jest miotem
                            "/Litters/" + addLitter.BreedLink +
                            "/" + addLitter.LitterLink + 
                            "/" + addLitter.MainPicture);

                        _kennelRepo.AddLitter(addLitter);
                        _kennelRepo.SaveChanges();
                          
                        return RedirectToAction("Index").WithSuccess(this, "Sukces", "<br>Prawidłowo dodano nowy miot!");
                    }
                    else
                    {
                        return View(addLitter).WithError(this, "Błąd", "<br>Formularz jest wypełniony nieprawidłowo!");
                    }
                }
                else
                {
                    return RedirectToAction("Index").WithError(this, "Błąd", "<br>Nie znaleziono psa o ID - " + id.ToString() + " lub nie podano parametru ID.");
                }
            }
            catch
            {
                return RedirectToAction("Index").WithInfo(this, "Wskazówka", "<br>Jeśli chcesz dodać miot, skorzystaj z zakładki 'Mioty' znajdującej się pod dodanym zwierzęciem.");
            }            
        }

        /// EDYTOWANIE MIOTU

        [HttpGet]
        public ActionResult EditLitter(int id)
        {
            ViewBag.DogSex = DogSex();
            ViewBag.Letters = GetLetters();

            ViewBag.BootstrapColumn = new SelectList(GetBootstrapColumn(), "Value", "Text", "Wybierz");

            try
            {
                var editLitter = _kennelRepo.GetLitterById(id);
                var imagesByLitter = _kennelRepo.GetLitterImagesByLitterLink(editLitter.LitterLink);
                var dog = _kennelRepo.GetDogByDogLink(editLitter.DogLink);

                if (editLitter != null)
                {
                    //var dogView = _kennelRepo.GetDogById(editLitter.DogId);
                    //var breed = _kennelRepo.GetDogBreedById(editLitter.BreedId);

                    //var pathForLitterImage = _kennelRepo.GetLitterImageByLitterLink(editLitter.LitterLink);

                    //TempData["Image"] = editLitter.BreedLink + "/" + editLitter.LitterLink + "/" + pathForLitterImage.ImageFileName;

                    //editLitter.BreedId = breed.Id;
                    //editLitter.DogId = dogView.Id;

                    //editLitter.BreedLink = breed.BreedLink;
                    //editLitter.DogLink = dogView.DogLink;

                    LitterWithImagesAndDog ltWithImages = new LitterWithImagesAndDog
                    {
                        litter = editLitter,
                        dog = dog,
                        litterImages = imagesByLitter.AsQueryable()
                    };                    

                    return View(ltWithImages);
                }
                else
                {
                    return RedirectToAction("Index").WithError(this, "Błąd", "<br>Nie znaleziono miotu o ID - " + id.ToString() + " lub nie podano parametru ID (2234x23).");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index").WithError(this, "", ex.ToString());//.WithError(this, "Błąd", "<br>Nie znaleziono miotu o ID - " + id.ToString() + " lub nie podano parametru ID.");
            }
        }

        [HttpPost]
        public ActionResult EditLitter(LitterWithImagesAndDog model, int id)
        {
            string modifiedDate = DateTime.Now.ToString();
            ViewBag.DogSex = DogSex();
            ViewBag.Letters = GetLetters();

            ViewBag.BootstrapColumn = new SelectList(GetBootstrapColumn(), "Value", "Text", "Wybierz");

            try
            {
                var editLitter = _kennelRepo.GetLitterById(id);

                if (editLitter != null)
                {
                    if (ModelState.IsValid)
                    {
                        var dogView = _kennelRepo.GetDogById(editLitter.DogId);
                        var breed = _kennelRepo.GetDogBreedById(editLitter.BreedId);

                        editLitter.BreedId = breed.Id;
                        editLitter.DogId = dogView.Id;

                        editLitter.BreedLink = breed.BreedLink;
                        editLitter.DogLink = dogView.DogLink;
                        
                        editLitter.Letter = model.litter.Letter;
                        editLitter.LitterPresentationName = model.litter.LitterPresentationName;
                        editLitter.MaleCount = model.litter.MaleCount;
                        editLitter.FemaleCount = model.litter.FemaleCount;
                        editLitter.DogFather = model.litter.DogFather;
                        editLitter.DogFatherDegree = model.litter.DogFatherDegree;
                        editLitter.DogFatherLink = model.litter.DogFatherLink;
                        editLitter.Description = model.litter.Description;
                        editLitter.BornDate = model.litter.BornDate;
                        editLitter.Visibility = model.litter.Visibility;

                        editLitter.MotherLitterDescription = model.litter.MotherLitterDescription;
                        editLitter.FatherLitterDescription = model.litter.FatherLitterDescription;

                        try
                        {
                            DateTime systemDate = DateTime.ParseExact(model.litter.BornDate, "dd.MM.yyyy", CultureInfo.InvariantCulture);
                            editLitter.BornDateOkay = systemDate;
                        }
                        catch
                        {
                            editLitter.BornDateOkay = DateTime.Now;
                        }

                        editLitter.Tags = model.litter.Tags;
                        editLitter.BootstrapColumn = model.litter.BootstrapColumn;
                        //editLitter.CreateDate = model.DogFather;
                        editLitter.ModifiedDate = modifiedDate;
                        //editLitter.ShowsCount = model.ShowsCount;
                        try
                        {
                            var pathForLitterImage = _kennelRepo.GetLitterImageByLitterLink(editLitter.LitterLink);
                            if (pathForLitterImage != null)
                            {
                                editLitter.MainPicture = pathForLitterImage.ImageFileName;
                            }
                        }
                        catch
                        {

                        }
                        TryUpdateModel(editLitter);
                        _kennelRepo.SaveChanges();

                        return RedirectToAction("EditLitter", new { id = editLitter.Id }).WithSuccess(this, "Zapisano zmiany", "<br>Pomyślnie zapisano zmiany!");
                    }
                    else
                    {
                        return View(editLitter).WithError(this, "Błąd", "<br>Popraw dane formularza!");
                    }
                    
                }
                else
                {
                    return RedirectToAction("Index").WithError(this, "Błąd", "<br>Nie znaleziono miotu o ID - " + id.ToString() + " lub nie podano parametru ID.");
                }
            }
            catch
            {
                return RedirectToAction("Index").WithError(this, "Błąd", "<br>Nie znaleziono miotu o ID - " + id.ToString() + " lub nie podano parametru ID.");
            }
        }

        /// DODAWANIE SZCZENIĘCIA DO MIOTU

        [HttpGet]
        public ActionResult AddPuppy([DefaultValue (0)] int litterId)
        {
            if (litterId == 0)
            {
                return RedirectToAction("Index").WithInfo(this, "Wskazówka", "<br>Jeśli chcesz dodać szczenię, wybierz najpierw miot, do którego chcesz je przypisać.");
            }
            string createDate = DateTime.Now.ToString();
            ViewBag.DogSex = DogSex();

            /// wysłać do widoku miot, do którego będzie przypisywane szczenie
            var forLitter = _kennelRepo.GetLitterById(litterId);

            if (forLitter != null)
            {
                var addPuppy = new Puppy();
                addPuppy.BreedId = forLitter.BreedId;
                addPuppy.BreedLink = forLitter.BreedLink;
                addPuppy.DogId = forLitter.DogId;
                addPuppy.DogLink = forLitter.DogLink;
                addPuppy.LitterId = forLitter.Id;
                addPuppy.LitterLink = forLitter.LitterLink;

                return View(addPuppy);
            }

            return View().WithError(this, "Błąd", "<br>Brak danych miotu, do którego dodawane jest szczenię.<br>Nie podano argumentu Id miotu lub podany argument Id jest błędny!");
        }

        [HttpPost]
        public ActionResult AddPuppy(Puppy model, int litterId)
        {
            string createDate = DateTime.Now.ToString();
            ViewBag.DogSex = DogSex();

            var forLitter = _kennelRepo.GetLitterById(litterId);

            if (ModelState.IsValid)
            {
                if (forLitter != null)
                {
                    var addPuppy = new Puppy();
                    addPuppy.BreedId = forLitter.BreedId;
                    addPuppy.BreedLink = forLitter.BreedLink;
                    addPuppy.DogId = forLitter.DogId;
                    addPuppy.DogLink = forLitter.DogLink;
                    addPuppy.LitterId = forLitter.Id;
                    addPuppy.LitterLink = forLitter.LitterLink;
                    addPuppy.PuppyLink = model.PuppyLink;

                    addPuppy.MainPicture = model.MainPicture;
                    addPuppy.PuppyName = model.PuppyName;
                    addPuppy.PuppyAchievements = model.PuppyAchievements;
                    addPuppy.PuppyColour = model.PuppyColour;
                    addPuppy.PuppySex = model.PuppySex;
                    addPuppy.IsForBreeding = model.IsForBreeding;
                    addPuppy.IsForSale = model.IsForSale;
                    addPuppy.IsReproductor = model.IsReproductor;
                    addPuppy.Defects = model.Defects;
                    addPuppy.BornDate = model.BornDate;
                    addPuppy.BornWeight = model.BornWeight;
                    addPuppy.DeathDate = model.DeathDate;
                    addPuppy.CauseOfDeath = model.CauseOfDeath;
                    addPuppy.AdultWeight = model.AdultWeight;
                    addPuppy.AdultHeight = model.AdultHeight;
                    addPuppy.Country = model.Country;
                    addPuppy.City = model.City;
                    addPuppy.Address = model.Address;
                    addPuppy.Description = model.Description;
                    addPuppy.DescriptionEng = model.DescriptionEng;
                    addPuppy.Url = model.Url;
                    addPuppy.BootstrapColumn = model.BootstrapColumn;
                    addPuppy.Tags = model.Tags;
                    addPuppy.CreateDate = createDate;
                    addPuppy.ModifiedDate = model.ModifiedDate;
                    addPuppy.ShowsCount = model.ShowsCount;

                    addPuppy.AvailableStatus = model.AvailableStatus;

                    var mainImage = _kennelRepo.GetPuppyImagesByLitterLinkAndPuppyLink(model.LitterLink, model.PuppyLink).FirstOrDefault();

                    if (mainImage != null)
                    {
                        addPuppy.MainPicture = mainImage.ImageFileName;
                    }

                    _kennelRepo.AddPuppy(addPuppy);
                    _kennelRepo.SaveChanges();


                    return RedirectToAction("EditLitter", new { id = litterId }).WithSuccess(this, "Sukces", "<br>Prawidłowo dodano szczenię do miotu");
                }                
            }
            else
            {
                return View(model).WithError(this, "Błąd", "<br>Formularz jest nieprawidłowo wypełniony.");
            }

            return View();
        }

        /// EDYTOWANIE SZCZENIĘCIA W MIOCIE

        [HttpGet]
        public ActionResult EditPuppy([DefaultValue(0)] int id)
        {
            string modifiedDate = DateTime.Now.ToString();
            ViewBag.DogSex = DogSex();

            try
            {
                var editPuppy = _kennelRepo.GetPuppyById(id);
                if (editPuppy != null)
                {
                    var imagesByPuppy = _kennelRepo.GetPuppyImagesByBreedLinkLitterLinkAndPuppyLink(editPuppy.BreedLink, editPuppy.LitterLink, editPuppy.PuppyLink);
                    if (imagesByPuppy != null)
                    {
                        var puppyAndImages = new PuppyAndImages
                        {
                            puppy = editPuppy,
                            puppyImages = imagesByPuppy.AsQueryable()
                        };

                        return View(puppyAndImages);
                    }
                    return RedirectToAction("Index").WithInfo(this, "Wskazówka", "<br>Jeśli chcesz edytować szczenię, wybierz je z listy 'Lista szczeniąt'.");
                }
                else
                {
                    return RedirectToAction("Index").WithInfo(this, "Wskazówka", "<br>Jeśli chcesz edytować szczenię, wybierz je z listy 'Lista szczeniąt'.");
                }                
            }
            catch
            {
                return RedirectToAction("Index").WithInfo(this, "Wskazówka", "<br>Jeśli chcesz edytować szczenię, wybierz je z listy 'Lista szczeniąt'.");
            }
        }

        [HttpPost]
        public ActionResult EditPuppy(PuppyAndImages model, int id)
        {
            string modifiedDate = DateTime.Now.ToString();
            ViewBag.DogSex = DogSex();

            var editPuppy = _kennelRepo.GetPuppyById(id);

            if (ModelState.IsValid)
            {
                if (editPuppy != null)
                {
                    var addPuppy = new Puppy();
                    editPuppy.BreedId = editPuppy.BreedId;
                    editPuppy.BreedLink = editPuppy.BreedLink;
                    editPuppy.DogId = editPuppy.DogId;
                    editPuppy.DogLink = editPuppy.DogLink;
                    editPuppy.LitterId = editPuppy.Id;
                    editPuppy.LitterLink = editPuppy.LitterLink;
                    editPuppy.PuppyLink = editPuppy.PuppyLink;

                    //editPuppy.MainPicture = model.puppy.MainPicture;
                    editPuppy.PuppyName = model.puppy.PuppyName;
                    editPuppy.PuppyAchievements = model.puppy.PuppyAchievements;
                    editPuppy.PuppyColour = model.puppy.PuppyColour;
                    editPuppy.PuppySex = model.puppy.PuppySex;
                    editPuppy.IsForBreeding = model.puppy.IsForBreeding;
                    editPuppy.IsForSale = model.puppy.IsForSale;
                    editPuppy.IsReproductor = model.puppy.IsReproductor;
                    editPuppy.Defects = model.puppy.Defects;
                    editPuppy.BornDate = model.puppy.BornDate;
                    editPuppy.BornWeight = model.puppy.BornWeight;
                    editPuppy.DeathDate = model.puppy.DeathDate;
                    editPuppy.CauseOfDeath = model.puppy.CauseOfDeath;
                    editPuppy.AdultWeight = model.puppy.AdultWeight;
                    editPuppy.AdultHeight = model.puppy.AdultHeight;
                    editPuppy.Country = model.puppy.Country;
                    editPuppy.City = model.puppy.City;
                    editPuppy.Address = model.puppy.Address;
                    editPuppy.Description = model.puppy.Description;
                    editPuppy.DescriptionEng = model.puppy.DescriptionEng;
                    editPuppy.Url = model.puppy.Url;
                    editPuppy.BootstrapColumn = model.puppy.BootstrapColumn;
                    editPuppy.Tags = model.puppy.Tags;
                    //editPuppy.CreateDate = createDate;
                    editPuppy.ModifiedDate = modifiedDate;
                    //editPuppy.ShowsCount = model.puppy.ShowsCount;
                    editPuppy.AvailableStatus = model.puppy.AvailableStatus;

                    TryUpdateModel(addPuppy);
                    _kennelRepo.SaveChanges();

                    return RedirectToAction("Puppies").WithSuccess(this, "Sukces", "<br>Prawidłowo zapisano zmiany.");
                }
            }
            else
            {
                return View(model).WithError(this, "Błąd", "<br>Formularz jest nieprawidłowo wypełniony.");
            }

            return View();
        }

        /// LISTA MIOTÓW
        
        public ActionResult Litters()
        {
            return View();
        }

        /// LISTA SZCZENIĄT

        public ActionResult Puppies()
        {
            return View();
        }

        /// D O G
        /// USTAWIANIE GŁÓWNEGO ZDJĘCIA
        public ActionResult SetDogImageAsMainImage(string dogLink, int mainImageId)
        {
            var setForDog = _kennelRepo.GetDogByDogLink(dogLink);
            var mainImage = _kennelRepo.GetDogImageById(mainImageId);

            try
            {                
                setForDog.ImageId = mainImageId;
                setForDog.MainPicture = mainImage.ImageFileName;

                // GENEALOGIC TREE MAIN PICTURE UPDATE
                var tree = _kennelRepo.GetDogTreeByDogLink(dogLink);
                if (tree != null)
                {
                    tree.DogTreeTooltip_Image = "/Dogs/" + setForDog.BreedLink + "/" + setForDog.DogLink + "/" + setForDog.MainPicture;
                }

                TryUpdateModel(setForDog);
                TryUpdateModel(tree);
                _kennelRepo.SaveChanges();

                return RedirectToAction("EditDog", "Kennel", new { id = setForDog.Id }).WithSuccess(this, "Sukces", "<br>Pomyślnie ustawiono zdjęcie główne jako: " + mainImage.ImageFileName);

            }
            catch
            {
                return RedirectToAction("EditDog", "Kennel", new { id = setForDog.Id }).WithError(this, "Błąd", "<br>Wystąpił błąd podczas ustawiania zdjęcia głównego (Kod - 0x00003)");
            }
        }

        /// L I T T E R
        /// USTAWIANIE GŁÓWNEGO ZDJĘCIA
        public ActionResult SetLitterImageAsMainImage(string litterLink, int mainImageId)
        {
            var setForLitter = _kennelRepo.GetLitterByLitterLink(litterLink);
            var mainImage = _kennelRepo.GetLitterImageById(mainImageId);

            try
            {
                // GENEALOGIC TREE MAIN PICTURE UPDATE
                var tree = _kennelRepo.GetLitterTreeByLitterLink(litterLink);
                if (tree != null)
                {
                    tree.DogTreeTooltip_Image = "/Litters/" + setForLitter.BreedLink + "/" + setForLitter.LitterLink + "/" + setForLitter.MainPicture;
                }

                setForLitter.MainPicture = mainImage.ImageFileName;
                TryUpdateModel(setForLitter);
                _kennelRepo.SaveChanges();

                return RedirectToAction("EditLitter", "Kennel", new { id = setForLitter.Id }).WithSuccess(this, "Sukces", "<br>Pomyślnie ustawiono zdjęcie główne jako: " + mainImage.ImageFileName);

            }
            catch
            {
                return RedirectToAction("EditDog", "Kennel", new { id = setForLitter.Id }).WithError(this, "Błąd", "<br>Wystąpił błąd podczas ustawiania zdjęcia głównego (Kod - 0x00003)");
            }
        }

        /// L I T T E R
        /// USTAWIANIE ZDJĘCIA OJCA
        public ActionResult SetFatherLitter(string litterLink, int mainImageId)
        {
            var setForLitter = _kennelRepo.GetLitterByLitterLink(litterLink);
            var mainImage = _kennelRepo.GetLitterImageById(mainImageId);

            try
            {
                setForLitter.FatherPictureName = mainImage.ImageFileName;
                TryUpdateModel(setForLitter);
                _kennelRepo.SaveChanges();

                return RedirectToAction("EditLitter", "Kennel", new { id = setForLitter.Id }).WithSuccess(this, "Sukces", "<br>Pomyślnie ustawiono zdjęcie ojca jako: " + mainImage.ImageFileName);

            }
            catch
            {
                return RedirectToAction("EditDog", "Kennel", new { id = setForLitter.Id }).WithError(this, "Błąd", "<br>Wystąpił błąd podczas ustawiania zdjęcia ojca (Kod - 0x00004)");
            }
        }

        /// L I T T E R
        /// USUWANIE ZDJĘCIA Z SERWERA I BAZY DANYCH
        public ActionResult RemoveImageFromLitter(string imgFileName, string litterLink)
        {
            var imgToRemove = _kennelRepo.GetLitterImageFileByImageFileNameAndLitter(imgFileName, litterLink);
            var litter = _kennelRepo.GetLitterByLitterLink(litterLink);

            if (imgToRemove != null && litter != null)
            {
                var filePath = Path.Combine(HostingEnvironment.MapPath("~/Litters/" + litter.BreedLink + "/" + litter.LitterLink + "/"), imgToRemove.ImageFileName);

                // usuwanie z bazy danych
                _kennelRepo.RemoveLitterImage(imgToRemove);
                _kennelRepo.SaveChanges();

                // usuwanie z systemu plików
                System.IO.File.Delete(filePath);

                return RedirectToAction("EditLitter", "Kennel", new { id = litter.Id }).WithSuccess(this, "Sukces", "<br>Pomyślnie usunięto plik: " + Path.Combine(HostingEnvironment.MapPath("~/Litters/" + litter.BreedLink + "/" + litterLink), imgToRemove.ImageFileName));
            }
            else
            {
                return RedirectToAction("EditLitter", "Kennel", new { id = litter.Id }).WithError(this, "Błąd", "<br>Nie znaleziono wybranego zdjęcia XXdve90");
            }
        }

        /// D O G
        /// USUWANIE ZDJĘCIA Z SERWERA I BAZY DANYCH
        public ActionResult RemoveImageFromDog(string imgFileName, string dogLink)
        {
            var imgToRemove = _kennelRepo.GetDogImageFileByImageFileNameAndDog(imgFileName, dogLink);
            var dog = _kennelRepo.GetDogByDogLink(dogLink);

            if (imgToRemove != null && dog != null)
            {
                var filePath = Path.Combine(HostingEnvironment.MapPath("~/Dogs/" + dog.BreedLink + "/" + dog.DogLink + "/"), imgToRemove.ImageFileName);

                // usuwanie z bazy danych
                _kennelRepo.DeleteImage(imgToRemove);
                _kennelRepo.SaveChanges();

                // usuwanie z systemu plików
                System.IO.File.Delete(filePath);

                return RedirectToAction("EditDog", "Kennel", new { id = dog.Id }).WithSuccess(this, "Sukces", "<br>Pomyślnie usunięto plik: " + Path.Combine(HostingEnvironment.MapPath("~/Posts/" + dogLink), imgToRemove.ImageFileName));
            }
            else
            {
                return RedirectToAction("EditDog", "Kennel", new { id = dog.Id }).WithError(this, "Błąd", "<br>Nie znaleziono wybranego zdjęcia XXdve30");
            }
        }

        /// P U P P Y
        /// USTAWIANIE GŁÓWNEGO ZDJĘCIA
        public ActionResult SetPuppyImageAsMainImage(string breedLink, string puppyLink, int mainImageId)
        {
            var setForPuppy = _kennelRepo.GetPuppyByBreedLinkAndPuppyLink(breedLink, puppyLink);
            var mainImage = _kennelRepo.GetPuppyImageById(mainImageId);

            try
            {
                setForPuppy.MainPicture = mainImage.ImageFileName;
                TryUpdateModel(setForPuppy);
                _kennelRepo.SaveChanges();

                return RedirectToAction("EditPuppy", "Kennel", new { id = setForPuppy.Id }).WithSuccess(this, "Sukces", "<br>Pomyślnie ustawiono zdjęcie główne jako: " + mainImage.ImageFileName);

            }
            catch
            {
                return RedirectToAction("EditPuppy", "Kennel", new { id = setForPuppy.Id }).WithError(this, "Błąd", "<br>Wystąpił błąd podczas ustawiania zdjęcia głównego (Kod - 0x00008)");
            }
        }

        /// P U P P Y
        /// USUWANIE ZDJĘCIA Z SERWERA I BAZY DANYCH
        public ActionResult RemoveImageFromPuppy(string imgFileName, string breedLink, string litterLink, string puppyLink)
        {            
            var imageToRemove = _kennelRepo.GetPuppyImageByBreedLinkLitterLinkAndPuppyLink(imgFileName, breedLink, litterLink, puppyLink);
            var puppy = _kennelRepo.GetPuppyByPuppyLink(imageToRemove.PuppyLink);

            if (imageToRemove != null && puppy != null)
            {
                var filePath = Path.Combine(HostingEnvironment.MapPath("~/Puppies/" + imageToRemove.BreedLink + "/" + imageToRemove.PuppyLink + "/"), imageToRemove.ImageFileName);

                // usuwanie z bazy danych
                _kennelRepo.RemovePuppyImage(imageToRemove);
                _kennelRepo.SaveChanges();

                // usuwanie z systemu plików
                System.IO.File.Delete(filePath);

                return RedirectToAction("EditPuppy", "Kennel", new { id = puppy.Id }).WithSuccess(this, "Sukces", "<br>Pomyślnie usunięto plik: " + Path.Combine(HostingEnvironment.MapPath("~/Puppies/" + imageToRemove.BreedLink + "/" + imageToRemove.PuppyLink + "/"), imageToRemove.ImageFileName));
            }
            else
            {
                return RedirectToAction("EditPuppy", "Kennel", new { id = puppy.Id }).WithError(this, "Błąd", "<br>Nie znaleziono wybranego zdjęcia XXdve30puppy");
            }
        }
    }
}