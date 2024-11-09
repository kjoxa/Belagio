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
    public class AjaxReproductionController : Controller
    {
        AssistantRepository _assistantRepo;
        FinanceRepository _financeRepo;
        private static Logger nLog = LogManager.GetCurrentClassLogger();

        public AjaxReproductionController()
        {
            _assistantRepo = new AssistantRepository();
            _financeRepo = new FinanceRepository();
        }

        /// LISTA KRYĆ I CIECZEK
        /// _______________________________________________________________________________________________________________________________________________________

        public ActionResult ReproductionAjaxList()
        {
            int daysToBorn;
            int daysToNextEstrus;

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
                    var searchRutStartDate = Request.Form.GetValues("columns[3][search][value]").FirstOrDefault();
                    var searchProgessteronBestDay = Request.Form.GetValues("columns[4][search][value]").FirstOrDefault();
                    var searchProgessteronBestVal = Request.Form.GetValues("columns[5][search][value]").FirstOrDefault();

                    //Paging Size (10,20,50,100)  
                    int pageSize = length != null ? Convert.ToInt32(length) : 0;
                    int skip = start != null ? Convert.ToInt32(start) : 0;
                    int recordsTotal = 0;

                    var reproductionList = _assistantRepo.GetReproductionList();

                    //Sorting
                    if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
                    {
                        reproductionList = reproductionList.OrderBy(sortColumn + " " + sortColumnDir);
                    }

                    var culture = new System.Globalization.CultureInfo("pl-PL");
                    var day = culture.DateTimeFormat.GetDayName(DateTime.Today.DayOfWeek);
                    DateTime estimatedDay;

                    // domyślne wartości dla czasu trwania ciąży oraz przerwy między cieczkami
                    int valueOfBornDays = 61;
                    int valueOfEstrusDays = 180;

                    foreach (var pos in reproductionList)
                    {
                        // wprowadzono datę rozpoczęcia cieczki, bez wprowadzenia daty porodu - czyli odliczanie tylko czasu cieczki
                        if (pos.EstrusStartDate != null && pos.MatingDate_First == null && pos.EstrusStartDate.AddDays(valueOfBornDays) > DateTime.Now)
                        {
                            daysToNextEstrus = (pos.EstrusStartDate.AddDays(valueOfEstrusDays) - DateTime.Now).Days;
                            pos.Comment = "Kolejna cieczka (za "+daysToNextEstrus+" dni): <span class='red' style='font-weight:bold'>" + culture.DateTimeFormat.GetDayName(pos.EstrusStartDate.AddDays(valueOfEstrusDays).DayOfWeek) + " "
                                    + pos.EstrusStartDate.AddDays(valueOfEstrusDays).Day + " " + culture.DateTimeFormat.GetMonthName(pos.EstrusStartDate.AddDays(valueOfEstrusDays).Month) + " " + pos.EstrusStartDate.AddDays(valueOfEstrusDays).Year + "</span>" +
                                    "<div class='progress' style='height:10px'>" +
                                    "<div class='progress-bar progress-bar-striped  role='progressbar' style='width: "
                                    + ((valueOfEstrusDays - daysToNextEstrus) * 100 / valueOfEstrusDays).ToString() + "%; background-color:#1e7e9e;' aria-valuenow='10%' aria-valuemin='0' aria-valuemax='valueOfBornDays'></div>";
                        }
                        // wprowadzono datę krycia, więc obliczamy datę porodu
                        if (pos.MatingDate_First != null)
                        {
                            if (DateTime.Now < pos.MatingDate_First.Value.AddDays(valueOfBornDays))
                            {
                                pos.EstimationBornDate = pos.MatingDate_First.Value.AddDays(valueOfBornDays);

                                daysToBorn = (pos.EstimationBornDate - DateTime.Now).Value.Days;
                                estimatedDay = pos.EstimationBornDate.Value;
                                daysToNextEstrus = (pos.EstrusStartDate.AddDays(valueOfEstrusDays) - DateTime.Now).Days;
                                // data porodu jest dziś!
                                if (daysToBorn <= 0)
                                {
                                    pos.Comment = "<b>Spodziewany poród odbędzie się dziś! </b>" +
                                    "(<span class='red' style='font-weight:bold'>" + culture.DateTimeFormat.GetDayName(estimatedDay.DayOfWeek) + " " + estimatedDay.Day + " " +
                                    culture.DateTimeFormat.GetMonthName(estimatedDay.Month) + " " + estimatedDay.Year + "</span>)" +
                                    "<div class='progress' style='height:10px'>" +
                                    "<div class='progress-bar progress-bar-striped active' role='progressbar' style='animation-direction:reverse; width:"
                                    + ((valueOfBornDays - daysToBorn) * 100 / valueOfBornDays).ToString() + "%; background-color:chocolate;' aria-valuenow='1%' aria-valuemin='0' aria-valuemax='valueOfBornDays'></div></div>" +
                                    "Kolejna cieczka (za " + daysToNextEstrus + " dni): <span class='red' style='font-weight:bold'>" + culture.DateTimeFormat.GetDayName(pos.EstrusStartDate.AddDays(valueOfEstrusDays).DayOfWeek) + " "
                                    + pos.EstrusStartDate.AddDays(valueOfEstrusDays).Day + " " + culture.DateTimeFormat.GetMonthName(pos.EstrusStartDate.AddDays(valueOfEstrusDays).Month) + " " + pos.EstrusStartDate.AddDays(valueOfEstrusDays).Year + "</span>" +
                                    "<div class='progress' style='height:10px'>" +
                                    "<div class='progress-bar progress-bar-striped' role='progressbar' style='width: "
                                    + ((valueOfEstrusDays - daysToNextEstrus) * 100 / valueOfEstrusDays).ToString() + "%; background-color:#1e7e9e;' aria-valuenow='1%' aria-valuemin='0' aria-valuemax='valueOfEstrusDays'></div></div>";
                                }
                                // do daty porodu zostało ileś dni
                                else
                                {
                                    pos.Comment = "Pozostało <b>" + daysToBorn.ToString() + "</b> dni do narodzin " +
                                    "(<span class='red' style='font-weight:bold'>" + culture.DateTimeFormat.GetDayName(estimatedDay.DayOfWeek) + " " + estimatedDay.Day + " " +
                                    culture.DateTimeFormat.GetMonthName(estimatedDay.Month) + " " + estimatedDay.Year + "</span>)" +
                                    "<div class='progress' style='height:10px'>" +
                                    "<div class='progress-bar progress-bar-striped active' role='progressbar' style='animation-direction:reverse; width:"
                                    + ((valueOfBornDays - daysToBorn) * 100 / valueOfBornDays).ToString() + "%; background-color:chocolate;' aria-valuenow='1%' aria-valuemin='0' aria-valuemax='valueOfBornDays'></div></div>" +
                                    "Kolejna cieczka (za " + daysToNextEstrus + " dni): <span class='red' style='font-weight:bold'>" + culture.DateTimeFormat.GetDayName(pos.EstrusStartDate.AddDays(valueOfEstrusDays).DayOfWeek) + " "
                                    + pos.EstrusStartDate.AddDays(valueOfEstrusDays).Day + " " + culture.DateTimeFormat.GetMonthName(pos.EstrusStartDate.AddDays(valueOfEstrusDays).Month) + " " + pos.EstrusStartDate.AddDays(valueOfEstrusDays).Year + "</span>" +
                                    "<div class='progress' style='height:10px'>" +
                                    "<div class='progress-bar progress-bar-striped' role='progressbar' style='width: "
                                    + ((valueOfEstrusDays - daysToNextEstrus) * 100 / valueOfEstrusDays).ToString() + "%; background-color:#1e7e9e;' aria-valuenow='1%' aria-valuemin='0' aria-valuemax='valueOfEstrusDays'></div></div>";
                                }                               
                            }
                            else
                            {
                                daysToNextEstrus = (pos.EstrusStartDate.AddDays(valueOfEstrusDays) - DateTime.Now).Days;
                                if (daysToNextEstrus < 0)
                                {
                                    pos.Comment = "Kolejna cieczka: <span class='red' style='font-weight:bold'>" + culture.DateTimeFormat.GetDayName(pos.EstrusStartDate.AddDays(valueOfEstrusDays).DayOfWeek) + " "
                                        + pos.EstrusStartDate.AddDays(valueOfEstrusDays).Day + " " + culture.DateTimeFormat.GetMonthName(pos.EstrusStartDate.AddDays(valueOfEstrusDays).Month) + " " + pos.EstrusStartDate.AddDays(valueOfEstrusDays).Year + "</span>";
                                }
                                else
                                {
                                    pos.Comment = "Kolejna cieczka (za " + daysToNextEstrus + " dni): <span class='red' style='font-weight:bold'>" + culture.DateTimeFormat.GetDayName(pos.EstrusStartDate.AddDays(valueOfEstrusDays).DayOfWeek) + " "
                                    + pos.EstrusStartDate.AddDays(valueOfEstrusDays).Day + " " + culture.DateTimeFormat.GetMonthName(pos.EstrusStartDate.AddDays(valueOfEstrusDays).Month) + " " + pos.EstrusStartDate.AddDays(valueOfEstrusDays).Year + "</span>" +
                                    "<div class='progress' style='height:10px'>" +
                                    "<div class='progress-bar progress-bar-striped  role='progressbar' style='width: "
                                    + ((valueOfEstrusDays - daysToNextEstrus) * 100 / valueOfEstrusDays).ToString() + "%; background-color:#1e7e9e;' aria-valuenow='10%' aria-valuemin='0' aria-valuemax='valueOfBornDays'></div>";
                                }                                                                
                            }
                        }                     
                    }

                    //total number of rows count   
                    recordsTotal = reproductionList.Count();
                    //Paging   
                    var data = reproductionList.Skip(skip).Take(pageSize).OrderByDescending(rl => rl.EstrusStartDate).ToList();

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