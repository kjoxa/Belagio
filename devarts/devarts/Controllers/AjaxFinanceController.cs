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
    public class AjaxFinanceController : Controller
    {
        AssistantRepository _assistantRepo;
        FinanceRepository _financeRepo;
        private static Logger nLog = LogManager.GetCurrentClassLogger();

        public AjaxFinanceController()
        {
            _assistantRepo = new AssistantRepository();
            _financeRepo = new FinanceRepository();
        }

        /// LISTA FINANSÓW
        /// _______________________________________________________________________________________________________________________________________________________

        public ActionResult FinanceAjaxList()
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
                    var searchName = Request.Form.GetValues("columns[1][search][value]").FirstOrDefault();
                    var searchDescription = Request.Form.GetValues("columns[2][search][value]").FirstOrDefault();
                    var searchCathegory = Request.Form.GetValues("columns[3][search][value]").FirstOrDefault();
                    var searchDateFrom = Request.Form.GetValues("columns[4][search][value]").FirstOrDefault();
                    var searchDateTo = Request.Form.GetValues("columns[5][search][value]").FirstOrDefault();
                    var searchAmout = Request.Form.GetValues("columns[5][search][value]").FirstOrDefault();

                    //Paging Size (10,20,50,100)  
                    int pageSize = length != null ? Convert.ToInt32(length) : 0;
                    int skip = start != null ? Convert.ToInt32(start) : 0;
                    int recordsTotal = 0;

                    var financesList = _financeRepo.GetFinances();

                    //Sorting
                    if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
                    {
                        financesList = financesList.OrderBy(sortColumn + " " + sortColumnDir);
                    }

                    if (!string.IsNullOrEmpty(searchValue))
                    {
                        financesList = financesList.Where(m => m.Amount.ToString().Contains(searchValue) || m.Name.Contains(searchValue) ||
                        m.Description.Contains(searchValue) || m.Cathegory.Contains(searchValue) || m.DateFrom.ToString().Contains(searchValue) ||
                        m.DateTo.ToString().Contains(searchValue) || m.CurrencyName.Contains(searchValue));
                    }

                    //Search ID
                    //if (!string.IsNullOrEmpty(searchID))
                    //{
                    //    reproductionList = reproductionList.Where(m => m.Id.ToString().Contains(searchID));
                    //}

                    ////Search UserId
                    //if (!string.IsNullOrEmpty(searchPuppyName))
                    //{
                    //    reproductionList = reproductionList.Where(m => m.PuppyName.Contains(searchPuppyName));
                    //}

                    ////Search FirstName
                    //if (!string.IsNullOrEmpty(searchBreedLink))
                    //{
                    //    reproductionList = reproductionList.Where(m => m.BreedLink.Contains(searchBreedLink));
                    //}

                    //if (!string.IsNullOrEmpty(searchLitterName))
                    //{
                    //    reproductionList = reproductionList.Where(m => m.LitterLink.Contains(searchLitterName));
                    //}

                    ////Search SurName
                    //if (!string.IsNullOrEmpty(searchDogLink))
                    //{
                    //    reproductionList = reproductionList.Where(m => m.DogLink.Contains(searchDogLink));
                    //}

                    ////Search BornDate
                    //if (!string.IsNullOrEmpty(searchWeight))
                    //{
                    //    reproductionList = reproductionList.Where(m => m.BornWeight.ToString().Contains(searchWeight));
                    //}

                    ////Search DiplomaDate
                    //if (!string.IsNullOrEmpty(searchBornDate))
                    //{
                    //    reproductionList = reproductionList.Where(m => m.BornDate.Contains(searchBornDate));
                    //}

                    ////Search DiplomaDate
                    //if (!string.IsNullOrEmpty(searchSex))
                    //{
                    //    reproductionList = reproductionList.Where(m => m.PuppySex.ToString().Contains(searchSex));
                    //}

                    ////Search NumberOfDiploma
                    //if (!string.IsNullOrEmpty(searchColour))
                    //{
                    //    reproductionList = reproductionList.Where(m => m.PuppyColour.ToString().Contains(searchColour));
                    //}

                    ////Search NumberOfDiploma
                    //if (!string.IsNullOrEmpty(searchPuppyLink))
                    //{
                    //    reproductionList = reproductionList.Where(m => m.PuppyLink.Contains(searchPuppyLink));
                    //}

                    var culture = new System.Globalization.CultureInfo("pl-PL");
                    var day = culture.DateTimeFormat.GetDayName(DateTime.Today.DayOfWeek);
                    DateTime estimatedDay;

                    //foreach (var pos in financesList)
                    //{
                    //    pos.Amount 
                    //}

                    //total number of rows count   
                    recordsTotal = financesList.Count();
                    //Paging   
                    var data = financesList.Skip(skip).Take(pageSize).ToList();

                    //Returning Json Data  
                    return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
                }
            }
            catch (Exception ext)
            {
                throw ext;
            }
        }
    }
}