using devarts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq.Dynamic;
using NLog;

namespace devarts.Controllers
{
    public class AjaxKennelController : Controller
    {
        private KennelRepository _kennelRepo;
        private static Logger nLog = LogManager.GetCurrentClassLogger();

        public AjaxKennelController()
        {
            _kennelRepo = new KennelRepository();
        }

        public JsonResult CheckBreedLinkExists(string breedLink)
        {
            var searchPost = _kennelRepo.GetBreedByBreedLink(breedLink.ToLower());
            if (searchPost != null)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult CheckDogLinkExists(string dogLink)
        {
            var searchPost = _kennelRepo.GetDogByDogLink(dogLink.ToLower());
            if (searchPost != null)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult CheckLitterLinkExists(string litterLink)
        {
            var searchPost = _kennelRepo.GetLitterByLitterLink(litterLink.ToLower());
            if (searchPost != null)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult CheckPuppyLinkExists(string puppyLink)
        {
            var searchPost = _kennelRepo.GetPuppyByPuppyLink(puppyLink.ToLower());
            if (searchPost != null)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        // ZAPISYWANIE POZYCJI ZDJĘCIA        
        public JsonResult SetImageIndex(string id, string number)
        {
            try
            {
                var imageToSet = _kennelRepo.GetDogImageById(Convert.ToInt32(id));
                if (imageToSet != null)
                {
                    imageToSet.ImageIndex = Convert.ToInt32(number);
                    TryUpdateModel(imageToSet);
                    _kennelRepo.SaveChanges();

                    return Json(true, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

            return Json(false, JsonRequestBehavior.AllowGet);            
        }

        // ZAPISYWANIE TEKSTU ALTERNATYWNEGO ZDJĘCIA PSA W HODOWLI        
        public JsonResult SetAltForDogImage(string id, string altText)
        {
            try
            {
                var imageAltToSave = _kennelRepo.GetDogImageById(Convert.ToInt32(id));
                if (imageAltToSave != null)
                {
                    imageAltToSave.ImageAlt = altText;
                    TryUpdateModel(imageAltToSave);
                    _kennelRepo.SaveChanges();

                    return Json(true, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

            return Json(false, JsonRequestBehavior.AllowGet);
        }

        // ZAPISYWANIE POZYCJI ZDJĘCIA        
        public JsonResult SetPuppyImageIndex(string id, string number)
        {
            try
            {
                var imageToSet = _kennelRepo.GetPuppyImageById(Convert.ToInt32(id));
                if (imageToSet != null)
                {
                    imageToSet.ImageIndex = Convert.ToInt32(number);
                    TryUpdateModel(imageToSet);
                    _kennelRepo.SaveChanges();

                    return Json(true, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

            return Json(false, JsonRequestBehavior.AllowGet);
        }

        // ZAPISYWANIE TEKSTU ALTERNATYWNEGO ZDJĘCIA SZCZENIĘCIA W HODOWLI        
        public JsonResult SetAltForPuppyImage(string id, string altText)
        {
            try
            {
                var imageAltToSave = _kennelRepo.GetPuppyImageById(Convert.ToInt32(id));
                if (imageAltToSave != null)
                {
                    imageAltToSave.ImageAlt = altText;
                    TryUpdateModel(imageAltToSave);
                    _kennelRepo.SaveChanges();

                    return Json(true, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

            return Json(false, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DocumentsAjaxList()
        {
            try
            {

                {
                    var draw = Request.Form.GetValues("draw").FirstOrDefault();
                    var start = Request.Form.GetValues("start").FirstOrDefault();
                    var length = Request.Form.GetValues("length").FirstOrDefault();
                    var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
                    var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
                    var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();

                    var searchID = Request.Form.GetValues("columns[0][search][value]").FirstOrDefault();
                    var searchDocumentName = Request.Form.GetValues("columns[1][search][value]").FirstOrDefault();
                    var searchDescription = Request.Form.GetValues("columns[2][search][value]").FirstOrDefault();
                    var searchType = Request.Form.GetValues("columns[3][search][value]").FirstOrDefault();
                    var searchUrl = Request.Form.GetValues("columns[4][search][value]").FirstOrDefault();
                    var searchDocumentDate = Request.Form.GetValues("columns[5][search][value]").FirstOrDefault();
                    var searchDocumentVersion = Request.Form.GetValues("columns[6][search][value]").FirstOrDefault();
                    var searchUploadDate = Request.Form.GetValues("columns[7][search][value]").FirstOrDefault();
                    var searchIsActual = Request.Form.GetValues("columns[8][search][value]").FirstOrDefault();

                    //Paging Size (10,20,50,100)  
                    int pageSize = length != null ? Convert.ToInt32(length) : 0;
                    int skip = start != null ? Convert.ToInt32(start) : 0;
                    int recordsTotal = 0;


                    var documentsList = _kennelRepo.GetDocumetsList();

                    //Sorting  
                    if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
                    {
                        documentsList = documentsList.OrderBy(sortColumn + " " + sortColumnDir);
                    }

                    //Search  
                    if (!string.IsNullOrEmpty(searchValue))
                    {

                        documentsList = documentsList.Where(m => m.Id.ToString().Contains(searchValue) || m.DocumentName.Contains(searchValue) ||
                        m.Description.Contains(searchValue) || m.Type.Contains(searchValue) ||
                        m.Url.Contains(searchValue) || m.DocumentDate.Contains(searchValue) || m.DocumentVersion.Contains(searchValue));
                    }

                    //Search ID
                    if (!string.IsNullOrEmpty(searchID))
                    {
                        documentsList = documentsList.Where(m => m.Id.ToString().Contains(searchID));
                    }

                    //Search UserId
                    if (!string.IsNullOrEmpty(searchDocumentName))
                    {
                        documentsList = documentsList.Where(m => m.DocumentName.Contains(searchDocumentName));
                    }

                    //Search FirstName
                    if (!string.IsNullOrEmpty(searchDescription))
                    {
                        documentsList = documentsList.Where(m => m.Description.Contains(searchDescription));
                    }

                    //Search SurName
                    if (!string.IsNullOrEmpty(searchType))
                    {
                        documentsList = documentsList.Where(m => m.Type.Contains(searchType));
                    }

                    //Search BornDate
                    if (!string.IsNullOrEmpty(searchUrl))
                    {
                        documentsList = documentsList.Where(m => m.Url.Contains(searchUrl));
                    }

                    //Search DiplomaDate
                    if (!string.IsNullOrEmpty(searchDocumentDate))
                    {
                        documentsList = documentsList.Where(m => m.DocumentDate.Contains(searchDocumentDate));
                    }

                    //Search NumberOfDiploma
                    if (!string.IsNullOrEmpty(searchDocumentVersion))
                    {
                        documentsList = documentsList.Where(m => m.DocumentVersion.Contains(searchDocumentVersion));
                    }

                    //Search Name
                    if (!string.IsNullOrEmpty(searchUploadDate))
                    {
                        documentsList = documentsList.Where(m => m.UploadDate.Contains(searchUploadDate));
                    }

                    //Search FatherName
                    if (!string.IsNullOrEmpty(searchIsActual))
                    {
                        documentsList = documentsList.Where(m => m.IsActual.ToString().Contains(searchIsActual));
                    }

                    //total number of rows count   
                    recordsTotal = documentsList.Count();
                    //Paging   
                    var data = documentsList.Skip(skip).Take(pageSize).ToList();

                    //Returning Json Data  
                    return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
                }
            }
            catch (Exception)
            {
                throw;
            }

        }

        public ActionResult BitchesAjaxList()
        {
            try
            {

                {
                    var draw = Request.Form.GetValues("draw").FirstOrDefault();
                    var start = Request.Form.GetValues("start").FirstOrDefault();
                    var length = Request.Form.GetValues("length").FirstOrDefault();
                    var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
                    var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
                    var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();

                    var searchID = Request.Form.GetValues("columns[0][search][value]").FirstOrDefault();
                    var searchDogName = Request.Form.GetValues("columns[1][search][value]").FirstOrDefault();
                    var searchEstrusStartDate = Request.Form.GetValues("columns[2][search][value]").FirstOrDefault();
                    var searchEstrusEndDate = Request.Form.GetValues("columns[3][search][value]").FirstOrDefault();
                    var searchRutStartDate = Request.Form.GetValues("columns[4][search][value]").FirstOrDefault();
                    var searchProgessteronBestDay = Request.Form.GetValues("columns[5][search][value]").FirstOrDefault();
                    var searchProgessteronBestVal = Request.Form.GetValues("columns[6][search][value]").FirstOrDefault();
                    var searchComment = Request.Form.GetValues("columns[7][search][value]").FirstOrDefault();
                    var searchIsSuccess = Request.Form.GetValues("columns[8][search][value]").FirstOrDefault();

                    //Paging Size (10,20,50,100)  
                    int pageSize = length != null ? Convert.ToInt32(length) : 0;
                    int skip = start != null ? Convert.ToInt32(start) : 0;
                    int recordsTotal = 0;


                    var bitchesList = _kennelRepo.GetBitchesList();

                    //Sorting  
                    if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
                    {
                        bitchesList = bitchesList.OrderBy(sortColumn + " " + sortColumnDir);
                    }

                    //Search  
                    if (!string.IsNullOrEmpty(searchValue))
                    {

                        bitchesList = bitchesList.Where(m => m.Id.ToString().Contains(searchValue) || m.DogName.Contains(searchValue) ||
                        m.EstrusStartDate.ToString().Contains(searchValue) || m.RutStartDate.ToString().Contains(searchValue) ||
                        m.ProgessteronBestDay.Contains(searchValue) || m.ProgessteronBestVal.Contains(searchValue) || m.IsSuccess.ToString().Contains(searchValue));
                    }

                    //Search ID
                    if (!string.IsNullOrEmpty(searchID))
                    {
                        bitchesList = bitchesList.Where(m => m.Id.ToString().Contains(searchID));
                    }

                    //Search UserId
                    if (!string.IsNullOrEmpty(searchDogName))
                    {
                        bitchesList = bitchesList.Where(m => m.DogName.Contains(searchDogName));
                    }

                    //Search FirstName
                    if (!string.IsNullOrEmpty(searchEstrusStartDate))
                    {
                        bitchesList = bitchesList.Where(m => m.EstrusStartDate.ToString().Contains(searchEstrusStartDate));
                    }

                    //Search SurName
                    if (!string.IsNullOrEmpty(searchRutStartDate))
                    {
                        bitchesList = bitchesList.Where(m => m.RutStartDate.ToString().Contains(searchRutStartDate));
                    }

                    //Search BornDate
                    if (!string.IsNullOrEmpty(searchProgessteronBestDay))
                    {
                        bitchesList = bitchesList.Where(m => m.ProgessteronBestDay.Contains(searchProgessteronBestDay));
                    }

                    //Search DiplomaDate
                    if (!string.IsNullOrEmpty(searchProgessteronBestVal))
                    {
                        bitchesList = bitchesList.Where(m => m.ProgessteronBestVal.Contains(searchProgessteronBestVal));
                    }

                    //Search DiplomaDate
                    if (!string.IsNullOrEmpty(searchComment))
                    {
                        bitchesList = bitchesList.Where(m => m.Comment.Contains(searchComment));
                    }

                    //Search NumberOfDiploma
                    if (!string.IsNullOrEmpty(searchIsSuccess))
                    {
                        bitchesList = bitchesList.Where(m => m.IsSuccess.ToString().Contains(searchIsSuccess));
                    }                

                    //total number of rows count   
                    recordsTotal = bitchesList.Count();
                    //Paging   
                    var data = bitchesList.Skip(skip).Take(pageSize).ToList();

                    //Returning Json Data  
                    return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
                }
            }
            catch (Exception)
            {
                throw;
            }

        }

        /// LISTA MIOTÓW W HODOWLI
        public ActionResult LittersAjaxList()
        {
            try
            {
                {
                    var draw = Request.Form.GetValues("draw").FirstOrDefault();
                    var start = Request.Form.GetValues("start").FirstOrDefault();
                    var length = Request.Form.GetValues("length").FirstOrDefault();
                    var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
                    var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
                    var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();

                    var searchID = Request.Form.GetValues("columns[0][search][value]").FirstOrDefault();
                    var searchLPName = Request.Form.GetValues("columns[1][search][value]").FirstOrDefault();
                    var searchBreedLink = Request.Form.GetValues("columns[2][search][value]").FirstOrDefault();
                    var searchDogLink = Request.Form.GetValues("columns[3][search][value]").FirstOrDefault();
                    var searchDogFather = Request.Form.GetValues("columns[4][search][value]").FirstOrDefault();
                    var searchBornDate = Request.Form.GetValues("columns[5][search][value]").FirstOrDefault();
                    var searchFemaleCount = Request.Form.GetValues("columns[6][search][value]").FirstOrDefault();
                    var searchMaleCount = Request.Form.GetValues("columns[7][search][value]").FirstOrDefault();
                    var searchLitterLink = Request.Form.GetValues("columns[8][search][value]").FirstOrDefault();

                    //Paging Size (10,20,50,100)  
                    int pageSize = length != null ? Convert.ToInt32(length) : 0;
                    int skip = start != null ? Convert.ToInt32(start) : 0;
                    int recordsTotal = 0;


                    var littersList = _kennelRepo.GetAllLitters();

                    //Sorting  
                    if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
                    {
                        littersList = littersList.OrderBy(sortColumn + " " + sortColumnDir);
                    }

                    //Search  
                    if (!string.IsNullOrEmpty(searchValue))
                    {

                        littersList = littersList.Where(m => m.Id.ToString().Contains(searchValue) || m.LitterPresentationName.Contains(searchValue) ||
                        m.BreedLink.Contains(searchValue) || m.DogLink.Contains(searchValue) ||
                        m.DogFather.Contains(searchValue) || m.BornDate.Contains(searchValue) || m.FemaleCount.ToString().Contains(searchValue)
                        || m.MaleCount.ToString().Contains(searchValue) || m.LitterLink.Contains(searchValue));
                    }

                    //Search ID
                    if (!string.IsNullOrEmpty(searchID))
                    {
                        littersList = littersList.Where(m => m.Id.ToString().Contains(searchID));
                    }

                    //Search UserId
                    if (!string.IsNullOrEmpty(searchLPName))
                    {
                        littersList = littersList.Where(m => m.LitterPresentationName.Contains(searchLPName));
                    }

                    //Search FirstName
                    if (!string.IsNullOrEmpty(searchBreedLink))
                    {
                        littersList = littersList.Where(m => m.BreedLink.Contains(searchBreedLink));
                    }

                    //Search SurName
                    if (!string.IsNullOrEmpty(searchDogLink))
                    {
                        littersList = littersList.Where(m => m.DogLink.Contains(searchDogLink));
                    }

                    //Search BornDate
                    if (!string.IsNullOrEmpty(searchDogFather))
                    {
                        littersList = littersList.Where(m => m.DogFather.Contains(searchDogFather));
                    }

                    //Search DiplomaDate
                    if (!string.IsNullOrEmpty(searchBornDate))
                    {
                        littersList = littersList.Where(m => m.BornDate.Contains(searchBornDate));
                    }

                    //Search DiplomaDate
                    if (!string.IsNullOrEmpty(searchFemaleCount))
                    {
                        littersList = littersList.Where(m => m.FemaleCount.ToString().Contains(searchFemaleCount));
                    }

                    //Search NumberOfDiploma
                    if (!string.IsNullOrEmpty(searchMaleCount))
                    {
                        littersList = littersList.Where(m => m.MaleCount.ToString().Contains(searchMaleCount));
                    }

                    //Search NumberOfDiploma
                    if (!string.IsNullOrEmpty(searchLitterLink))
                    {
                        littersList = littersList.Where(m => m.LitterLink.Contains(searchLitterLink));
                    }

                    //total number of rows count   
                    recordsTotal = littersList.Count();
                    //Paging   
                    var data = littersList.Skip(skip).Take(pageSize).ToList();

                    //Returning Json Data  
                    return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
                }
            }
            catch (Exception)
            {
                throw;
            }

        }

        /// LISTA MIOTÓW W HODOWLI
        public ActionResult PuppiesAjaxList()
        {
            try
            {
                {
                    var draw = Request.Form.GetValues("draw").FirstOrDefault();
                    var start = Request.Form.GetValues("start").FirstOrDefault();
                    var length = Request.Form.GetValues("length").FirstOrDefault();
                    var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
                    var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
                    var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();

                    var searchID = Request.Form.GetValues("columns[0][search][value]").FirstOrDefault();
                    var searchPuppyName = Request.Form.GetValues("columns[1][search][value]").FirstOrDefault();
                    var searchBreedLink = Request.Form.GetValues("columns[2][search][value]").FirstOrDefault();
                    var searchLitterName = Request.Form.GetValues("columns[3][search][value]").FirstOrDefault();
                    var searchDogLink = Request.Form.GetValues("columns[4][search][value]").FirstOrDefault();
                    var searchWeight = Request.Form.GetValues("columns[5][search][value]").FirstOrDefault();
                    var searchBornDate = Request.Form.GetValues("columns[6][search][value]").FirstOrDefault();
                    var searchSex = Request.Form.GetValues("columns[7][search][value]").FirstOrDefault();
                    var searchColour = Request.Form.GetValues("columns[8][search][value]").FirstOrDefault();
                    var searchPuppyLink = Request.Form.GetValues("columns[9][search][value]").FirstOrDefault();

                    //Paging Size (10,20,50,100)  
                    int pageSize = length != null ? Convert.ToInt32(length) : 0;
                    int skip = start != null ? Convert.ToInt32(start) : 0;
                    int recordsTotal = 0;


                    var littersList = _kennelRepo.GetAllPuppies();

                    //Sorting  
                    if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
                    {
                        littersList = littersList.OrderBy(sortColumn + " " + sortColumnDir);
                    }

                    //Search  
                    if (!string.IsNullOrEmpty(searchValue))
                    {

                        littersList = littersList.Where(m => m.Id.ToString().Contains(searchValue) || m.PuppyName.Contains(searchValue) ||
                        m.BreedLink.Contains(searchValue) || m.DogLink.Contains(searchValue) ||
                        m.BornWeight.ToString().Contains(searchValue) || m.BornDate.Contains(searchValue) || m.PuppySex.ToString().Contains(searchValue)
                        || m.PuppyColour.Contains(searchValue) || m.PuppyLink.Contains(searchValue));
                    }

                    //Search ID
                    if (!string.IsNullOrEmpty(searchID))
                    {
                        littersList = littersList.Where(m => m.Id.ToString().Contains(searchID));
                    }

                    //Search UserId
                    if (!string.IsNullOrEmpty(searchPuppyName))
                    {
                        littersList = littersList.Where(m => m.PuppyName.Contains(searchPuppyName));
                    }

                    //Search FirstName
                    if (!string.IsNullOrEmpty(searchBreedLink))
                    {
                        littersList = littersList.Where(m => m.BreedLink.Contains(searchBreedLink));
                    }

                    if (!string.IsNullOrEmpty(searchLitterName))
                    {
                        littersList = littersList.Where(m => m.LitterLink.Contains(searchLitterName));
                    }

                    //Search SurName
                    if (!string.IsNullOrEmpty(searchDogLink))
                    {
                        littersList = littersList.Where(m => m.DogLink.Contains(searchDogLink));
                    }

                    //Search BornDate
                    if (!string.IsNullOrEmpty(searchWeight))
                    {
                        littersList = littersList.Where(m => m.BornWeight.ToString().Contains(searchWeight));
                    }

                    //Search DiplomaDate
                    if (!string.IsNullOrEmpty(searchBornDate))
                    {
                        littersList = littersList.Where(m => m.BornDate.Contains(searchBornDate));
                    }

                    //Search DiplomaDate
                    if (!string.IsNullOrEmpty(searchSex))
                    {
                        littersList = littersList.Where(m => m.PuppySex.ToString().Contains(searchSex));
                    }

                    //Search NumberOfDiploma
                    if (!string.IsNullOrEmpty(searchColour))
                    {
                        littersList = littersList.Where(m => m.PuppyColour.ToString().Contains(searchColour));
                    }

                    //Search NumberOfDiploma
                    if (!string.IsNullOrEmpty(searchPuppyLink))
                    {
                        littersList = littersList.Where(m => m.PuppyLink.Contains(searchPuppyLink));
                    }

                    //total number of rows count   
                    recordsTotal = littersList.Count();
                    //Paging   
                    var data = littersList.Skip(skip).Take(pageSize).ToList();

                    //Returning Json Data  
                    return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
                }
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}