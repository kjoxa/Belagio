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
    public class AjaxHealthController : Controller
    {
        AssistantRepository _assistantRepo;
        private static Logger nLog = LogManager.GetCurrentClassLogger();

        public AjaxHealthController()
        {
            _assistantRepo = new AssistantRepository();
        }

        /// LISTA CHORÓB I BADAŃ
        /// _______________________________________________________________________________________________________________________________________________________

        public ActionResult HealthAndVaccinationsAjaxList()
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
                    var searchFirstName = Request.Form.GetValues("columns[1][search][value]").FirstOrDefault();
                    var searchSurName = Request.Form.GetValues("columns[2][search][value]").FirstOrDefault();
                    var searchCity = Request.Form.GetValues("columns[3][search][value]").FirstOrDefault();
                    var searchColour = Request.Form.GetValues("columns[4][search][value]").FirstOrDefault();
                    var searchDogForKennel = Request.Form.GetValues("columns[5][search][value]").FirstOrDefault();
                    var searchDogForSport = Request.Form.GetValues("columns[6][search][value]").FirstOrDefault();
                    var searchPaymentStatus = Request.Form.GetValues("columns[7][search][value]").FirstOrDefault();
                    var searchCreateDate = Request.Form.GetValues("columns[8][search][value]").FirstOrDefault();

                    //Paging Size (10,20,50,100)  
                    int pageSize = length != null ? Convert.ToInt32(length) : 0;
                    int skip = start != null ? Convert.ToInt32(start) : 0;
                    int recordsTotal = 0;

                    var healthAndVaccinationsList = _assistantRepo.GetHealthAndVaccinationsList();

                    //Sorting
                    if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
                    {
                        healthAndVaccinationsList = healthAndVaccinationsList.OrderBy(sortColumn + " " + sortColumnDir);
                    }

                    //Search  
                    if (!string.IsNullOrEmpty(searchValue))
                    {
                        healthAndVaccinationsList = healthAndVaccinationsList.Where(m => m.Id.ToString().Contains(searchValue) || m.DogName.Contains(searchValue) ||
                        m.Name.Contains(searchValue) || m.Type.Contains(searchValue) || m.Comment.Contains(searchValue)
                        || m.ProgressBar.ToString().Contains(searchValue) || m.NextDate.ToString().Contains(searchValue));
                    }

                    if (!string.IsNullOrEmpty(searchDogForKennel))
                    {
                        healthAndVaccinationsList = healthAndVaccinationsList.Where(m => m.Id.ToString().Contains(searchDogForKennel));
                    }

                    //total number of rows count   
                    recordsTotal = healthAndVaccinationsList.Count();
                    //Paging   
                    var data = healthAndVaccinationsList.Skip(skip).Take(pageSize).OrderByDescending(rl => rl.Id).ToList();

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