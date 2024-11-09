using devarts.Repositories;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq.Dynamic;

namespace devarts.Controllers
{
    public class AjaxTreeController : Controller
    {
        KennelRepository _kennelRepo;
        private static Logger nLog = LogManager.GetCurrentClassLogger();

        public AjaxTreeController()
        {
            _kennelRepo = new KennelRepository();
        }

        /// LISTA DRZEW PSÓW W HODOWLI
        /// _______________________________________________________________________________________________________________________________________________________

        // GenerationsCount musi mieć wartość
        // Visible musi mieć wartość
        public ActionResult TreesList()
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
                    var searchDogLink = Request.Form.GetValues("columns[1][search][value]").FirstOrDefault();

                    //Paging Size (10,20,50,100)  
                    int pageSize = length != null ? Convert.ToInt32(length) : 0;
                    int skip = start != null ? Convert.ToInt32(start) : 0;
                    int recordsTotal = 0;

                    var treesList = _kennelRepo.GetTrees().Where(t => t.IsLitter == false);

                    //Sorting
                    if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
                    {
                        treesList = treesList.OrderBy(sortColumn + " " + sortColumnDir);
                    }

                    //Search  
                    //if (!string.IsNullOrEmpty(searchValue))
                    //{
                    //    treesList = treesList.Where(m => m.Id.ToString().Contains(searchValue) || m.FirstName.Contains(searchValue) ||
                    //    m.SurName.Contains(searchValue) || m.City.Contains(searchValue) || m.Colour.Contains(searchValue)
                    //    || m.DogForKennel.ToString().Contains(searchValue) || m.DogForSport.ToString().Contains(searchValue) || m.PaymentStatus.Contains(searchValue)
                    //    || m.CreateDate.ToString().Contains(searchValue));
                    //}

                    //if (!string.IsNullOrEmpty(searchDogForKennel))
                    //{
                    //    treesList = treesList.Where(m => m.Id.ToString().Contains(searchDogForKennel));
                    //}

                    //total number of rows count   
                    recordsTotal = treesList.Count();
                    //Paging   
                    var data = treesList.Skip(skip).Take(pageSize).OrderByDescending(r => r.Id).ToList();

                    //Returning Json Data  
                    return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
                }
            }
            catch (Exception ext)
            {
                nLog.Error(ext.ToString());
                throw;
            }
        }

        /// LISTA DRZEW MIOTÓW
        /// _______________________________________________________________________________________________________________________________________________________

        // GenerationsCount musi mieć wartość
        // Visible musi mieć wartość
        public ActionResult LittersTreesList()
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
                    var searchDogLink = Request.Form.GetValues("columns[1][search][value]").FirstOrDefault();

                    //Paging Size (10,20,50,100)  
                    int pageSize = length != null ? Convert.ToInt32(length) : 0;
                    int skip = start != null ? Convert.ToInt32(start) : 0;
                    int recordsTotal = 0;

                    var treesList = _kennelRepo.GetTrees().Where(t => t.IsLitter == true);

                    //Sorting
                    if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
                    {
                        treesList = treesList.OrderBy(sortColumn + " " + sortColumnDir);
                    }

                    //Search  
                    //if (!string.IsNullOrEmpty(searchValue))
                    //{
                    //    treesList = treesList.Where(m => m.Id.ToString().Contains(searchValue) || m.FirstName.Contains(searchValue) ||
                    //    m.SurName.Contains(searchValue) || m.City.Contains(searchValue) || m.Colour.Contains(searchValue)
                    //    || m.DogForKennel.ToString().Contains(searchValue) || m.DogForSport.ToString().Contains(searchValue) || m.PaymentStatus.Contains(searchValue)
                    //    || m.CreateDate.ToString().Contains(searchValue));
                    //}

                    //if (!string.IsNullOrEmpty(searchDogForKennel))
                    //{
                    //    treesList = treesList.Where(m => m.Id.ToString().Contains(searchDogForKennel));
                    //}

                    //total number of rows count   
                    recordsTotal = treesList.Count();
                    //Paging   
                    var data = treesList.Skip(skip).Take(pageSize).OrderByDescending(r => r.Id).ToList();

                    //Returning Json Data  
                    return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
                }
            }
            catch (Exception ext)
            {
                nLog.Error(ext.ToString());
                throw;
            }
        }
    }
}