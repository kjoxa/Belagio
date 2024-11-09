using devarts.Helpers;
using devarts.Models;
using devarts.Repositories;
using DocumentFormat.OpenXml.Wordprocessing;
using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Windows;

namespace devarts.Controllers
{
    ///https://www.freetimelearning.com/bootstrap-snippets/bootstrap-3-main-plugins.php?Bootstrap-Family-Tree-Vertical-Family-Tree----Free-Time-Learning&id=58
    /*
     
    DRZEWA GENEALOGICZNE
    1. Drzewa domyślnie tworzone będą podczas tworzenia psa
    2. Wówczas nie będzie trzeba tworzyć widoku wybierania psa, dla którego ma
       być stworzone drzewo
     
    */
    [LayoutInjecter("_adminLayout")]
    [Authorize]
    public class TreeController : Controller
    {
        KennelRepository _kennelRepo;        
        private static Logger nLog = LogManager.GetCurrentClassLogger();

        public TreeController()
        {
            _kennelRepo = new KennelRepository();
        }        

        public void CreateGenealogicTrees()
        {
            var allDogs = _kennelRepo.GetAllDogs();

            PrepareDogTrees pdp = new PrepareDogTrees();
            foreach (var dog in allDogs)
            {
                var dogTree = _kennelRepo.GetDogTreeByDogLink(dog.DogLink);
                if (dogTree == null)
                {
                    pdp.CreateDogGenealogicTree(dog.DogLink, dog.Id, "", 0, false, "/Dogs/" + dog.BreedLink + "/" + dog.DogLink + "/" + dog.MainPicture);
                }                
            }
        }

        public void CreateLitterGenealogicTrees()
        {
            var allLitters = _kennelRepo.GetAllLitters();

            PrepareDogTrees pdp = new PrepareDogTrees();
            foreach (var litter in allLitters)
            {
                var litterTree = _kennelRepo.GetLitterTreeByLitterLink(litter.LitterLink);
                if (litterTree == null)
                {
                    pdp.CreateDogGenealogicTree(litter.DogLink, litter.Id, litter.LitterLink, litter.Id, true, "/Litters/" + litter.BreedLink + "/" + litter.LitterLink + "/" + litter.MainPicture);
                }
            }
        }

        public void DropGenealogicTrees()
        {
            _kennelRepo.DropAndCreateGenealogicTreesTable();            
        }

        // Lista rodowodów psów
        public ActionResult Index()
        {
            //PrepareDogTrees pdp = new PrepareDogTrees();
            //if (pdp.CreateDogGenealogicTree("Namaste", 2)
            //    && pdp.CreateDogGenealogicTree("Macchiato", 3)
            //    && pdp.CreateDogGenealogicTree("Aurelio", 4)
            //    && pdp.CreateDogGenealogicTree("LaMora", 5)
            //    && pdp.CreateDogGenealogicTree("KimFlo", 6))
            //{
            //    return View().WithSuccess(this, "Sukces", "Udało się stworzyć wszystkie drzewa!");
            //}
            //return View().WithError(this, "Błąd", "Niestety nie udało się stworzyć drzew genealogicznych...");

            //DropGenealogicTrees();
            //CreateGenealogicTrees();

            return View();
        }

        // Lista rodowodów miotów
        public ActionResult LittersTrees()
        {
            //CreateLitterGenealogicTrees();
            return View();
        }

        /// RODOWODY PSÓW W HODOWLI
        public ActionResult EditTree(int id)
        {
            var editTree = _kennelRepo.GetTreeById(id);
            if(editTree != null)
            {
                return View(editTree);
            }
            return RedirectToAction("Index").WithError(this, "Błąd", "Nie można edytować drzewa genealogicznego.<br>Błąd: 20221026#1");
        }

        [ValidateInput(false)]
        public void Update(int id, string elementName, string boxText, string boxTooltip, string boxPicture)
        {
            try
            {
                var tree = _kennelRepo.GetTreeById(id);
                if (tree != null)
                {
                    switch (elementName)
                    {
                        case "DogTreeBox":
                            tree.DogTreeBox = boxText;
                            tree.DogTreeTooltip = boxTooltip;
                            tree.DogTreeTooltip_Image = boxPicture;
                            break;
                        case "A_DogTreeBox_Father_1":
                            tree.A_DogTreeBox_Father_1 = boxText;
                            tree.A_DogTreeTooltip_Father_1 = boxTooltip;
                            tree.A_DogTreeImage_Father_1 = boxPicture;
                            break;
                        case "A_DogTreeBox_Mother_2":
                            tree.A_DogTreeBox_Mother_2 = boxText;
                            tree.A_DogTreeTooltip_Mother_2 = boxTooltip;
                            tree.A_DogTreeImage_Mother_2 = boxPicture;
                            break;
                        case "B_DogTreeBox_Father_3":
                            tree.B_DogTreeBox_Father_3 = boxText;
                            tree.B_DogTreeTooltip_Father_3 = boxTooltip;
                            tree.B_DogTreeImage_Father_3 = boxPicture;
                            break;
                        case "B_DogTreeBox_Mother_4":
                            tree.B_DogTreeBox_Mother_4 = boxText;
                            tree.B_DogTreeTooltip_Mother_4 = boxTooltip;
                            tree.B_DogTreeImage_Mother_4 = boxPicture;
                            break;
                        case "B_DogTreeBox_Father_5":
                            tree.B_DogTreeBox_Father_5 = boxText;
                            tree.B_DogTreeTooltip_Father_5 = boxTooltip;
                            tree.B_DogTreeImage_Father_5 = boxPicture;
                            break;
                        case "B_DogTreeBox_Mother_6":
                            tree.B_DogTreeBox_Mother_6 = boxText;
                            tree.B_DogTreeTooltip_Mother_6 = boxTooltip;
                            tree.B_DogTreeImage_Mother_6 = boxPicture;
                            break;
                        case "C_DogTreeBox_Father_7":
                            tree.C_DogTreeBox_Father_7 = boxText;
                            tree.C_DogTreeTooltip_Father_7 = boxTooltip;
                            tree.C_DogTreeImage_Father_7 = boxPicture;
                            break;
                        case "C_DogTreeBox_Mother_8":
                            tree.C_DogTreeBox_Mother_8 = boxText;
                            tree.C_DogTreeTooltip_Mother_8 = boxTooltip;
                            tree.C_DogTreeImage_Mother_8 = boxPicture;
                            break;
                        case "C_DogTreeBox_Father_9":
                            tree.C_DogTreeBox_Father_9 = boxText;
                            tree.C_DogTreeTooltip_Father_9 = boxTooltip;
                            tree.C_DogTreeImage_Father_9 = boxPicture;
                            break;
                        case "C_DogTreeBox_Mother_10":
                            tree.C_DogTreeBox_Mother_10 = boxText;
                            tree.C_DogTreeTooltip_Mother_10 = boxTooltip;
                            tree.C_DogTreeImage_Mother_10 = boxPicture;
                            break;
                        case "C_DogTreeBox_Father_11":
                            tree.C_DogTreeBox_Father_11 = boxText;
                            tree.C_DogTreeTooltip_Father_11 = boxTooltip;
                            tree.C_DogTreeImage_Father_11 = boxPicture;
                            break;
                        case "C_DogTreeBox_Mother_12":
                            tree.C_DogTreeBox_Mother_12 = boxText;
                            tree.C_DogTreeTooltip_Mother_12 = boxTooltip;
                            tree.C_DogTreeImage_Mother_12 = boxPicture;
                            break;
                        case "C_DogTreeBox_Father_13":
                            tree.C_DogTreeBox_Father_13 = boxText;
                            tree.C_DogTreeTooltip_Father_13 = boxTooltip;
                            tree.C_DogTreeImage_Father_13 = boxPicture;
                            break;
                        case "C_DogTreeBox_Mother_14":
                            tree.C_DogTreeBox_Mother_14 = boxText;
                            tree.C_DogTreeTooltip_Mother_14 = boxTooltip;
                            tree.C_DogTreeImage_Mother_14 = boxPicture;
                            break;
                        default:
                            break;
                    }
                    tree.EditDate = DateTime.Now;
                    TryUpdateModel(tree);
                    _kennelRepo.SaveChanges();
                }

                Debug.WriteLine(id + " - " + elementName + " - " + boxText + " - " + boxTooltip + " - " + boxPicture);
                Response.StatusCode = 200;
            }
            catch
            {
                // wysłanie do jQuery statusu Error
                Response.StatusCode = 500;
            }
        }

        /// RODOWODY MIOTÓW W HODOWLI
        public ActionResult EditLitterTree(int id)
        {
            var editTree = _kennelRepo.GetTreeById(id);
            if (editTree != null)
            {
                return View(editTree);
            }
            return RedirectToAction("Index").WithError(this, "Błąd", "Nie można edytować drzewa genealogicznego.<br>Błąd: 20221026#3");
        }

        [ValidateInput(false)]
        public void UpdateLitterTree(int id, string elementName, string boxText, string boxTooltip, string boxPicture)
        {
            try
            {
                var tree = _kennelRepo.GetTreeById(id);
                if (tree != null)
                {
                    switch (elementName)
                    {
                        case "DogTreeBox":
                            tree.DogTreeBox = boxText;
                            tree.DogTreeTooltip = boxTooltip;
                            tree.DogTreeTooltip_Image = boxPicture;
                            break;
                        case "A_DogTreeBox_Father_1":
                            tree.A_DogTreeBox_Father_1 = boxText;
                            tree.A_DogTreeTooltip_Father_1 = boxTooltip;
                            tree.A_DogTreeImage_Father_1 = boxPicture;
                            break;
                        case "A_DogTreeBox_Mother_2":
                            tree.A_DogTreeBox_Mother_2 = boxText;
                            tree.A_DogTreeTooltip_Mother_2 = boxTooltip;
                            tree.A_DogTreeImage_Mother_2 = boxPicture;
                            break;
                        case "B_DogTreeBox_Father_3":
                            tree.B_DogTreeBox_Father_3 = boxText;
                            tree.B_DogTreeTooltip_Father_3 = boxTooltip;
                            tree.B_DogTreeImage_Father_3 = boxPicture;
                            break;
                        case "B_DogTreeBox_Mother_4":
                            tree.B_DogTreeBox_Mother_4 = boxText;
                            tree.B_DogTreeTooltip_Mother_4 = boxTooltip;
                            tree.B_DogTreeImage_Mother_4 = boxPicture;
                            break;
                        case "B_DogTreeBox_Father_5":
                            tree.B_DogTreeBox_Father_5 = boxText;
                            tree.B_DogTreeTooltip_Father_5 = boxTooltip;
                            tree.B_DogTreeImage_Father_5 = boxPicture;
                            break;
                        case "B_DogTreeBox_Mother_6":
                            tree.B_DogTreeBox_Mother_6 = boxText;
                            tree.B_DogTreeTooltip_Mother_6 = boxTooltip;
                            tree.B_DogTreeImage_Mother_6 = boxPicture;
                            break;
                        case "C_DogTreeBox_Father_7":
                            tree.C_DogTreeBox_Father_7 = boxText;
                            tree.C_DogTreeTooltip_Father_7 = boxTooltip;
                            tree.C_DogTreeImage_Father_7 = boxPicture;
                            break;
                        case "C_DogTreeBox_Mother_8":
                            tree.C_DogTreeBox_Mother_8 = boxText;
                            tree.C_DogTreeTooltip_Mother_8 = boxTooltip;
                            tree.C_DogTreeImage_Mother_8 = boxPicture;
                            break;
                        case "C_DogTreeBox_Father_9":
                            tree.C_DogTreeBox_Father_9 = boxText;
                            tree.C_DogTreeTooltip_Father_9 = boxTooltip;
                            tree.C_DogTreeImage_Father_9 = boxPicture;
                            break;
                        case "C_DogTreeBox_Mother_10":
                            tree.C_DogTreeBox_Mother_10 = boxText;
                            tree.C_DogTreeTooltip_Mother_10 = boxTooltip;
                            tree.C_DogTreeImage_Mother_10 = boxPicture;
                            break;
                        case "C_DogTreeBox_Father_11":
                            tree.C_DogTreeBox_Father_11 = boxText;
                            tree.C_DogTreeTooltip_Father_11 = boxTooltip;
                            tree.C_DogTreeImage_Father_11 = boxPicture;
                            break;
                        case "C_DogTreeBox_Mother_12":
                            tree.C_DogTreeBox_Mother_12 = boxText;
                            tree.C_DogTreeTooltip_Mother_12 = boxTooltip;
                            tree.C_DogTreeImage_Mother_12 = boxPicture;
                            break;
                        case "C_DogTreeBox_Father_13":
                            tree.C_DogTreeBox_Father_13 = boxText;
                            tree.C_DogTreeTooltip_Father_13 = boxTooltip;
                            tree.C_DogTreeImage_Father_13 = boxPicture;
                            break;
                        case "C_DogTreeBox_Mother_14":
                            tree.C_DogTreeBox_Mother_14 = boxText;
                            tree.C_DogTreeTooltip_Mother_14 = boxTooltip;
                            tree.C_DogTreeImage_Mother_14 = boxPicture;
                            break;
                        default:
                            break;
                    }
                    tree.EditDate = DateTime.Now;
                    TryUpdateModel(tree);
                    _kennelRepo.SaveChanges();
                }

                Debug.WriteLine(id + " - " + elementName + " - " + boxText + " - " + boxTooltip + " - " + boxPicture);
                Response.StatusCode = 200;
            }
            catch
            {
                // wysłanie do jQuery statusu Error
                Response.StatusCode = 500;
            }
        }
    }
}