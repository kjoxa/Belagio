using devarts.Models;
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
    public class AjaxStatisticController : Controller
    {
        private AdminRepository _adminRepo;
        private static Logger nLog = LogManager.GetCurrentClassLogger();

        public AjaxStatisticController()
        {
            _adminRepo = new AdminRepository();
            //_litterRepo = new LittersRepository();
        }

        public ActionResult StatisticsAjaxList()
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
                    var searchLogonTime = Request.Form.GetValues("columns[1][search][value]").FirstOrDefault();
                    var searchHostIP = Request.Form.GetValues("columns[2][search][value]").FirstOrDefault();
                    var searchHostName = Request.Form.GetValues("columns[3][search][value]").FirstOrDefault();
                    var searchDeviceType = Request.Form.GetValues("columns[4][search][value]").FirstOrDefault();
                    var searchContinent = Request.Form.GetValues("columns[5][search][value]").FirstOrDefault();
                    var searchCountry = Request.Form.GetValues("columns[6][search][value]").FirstOrDefault();
                    var searchGeoLong = Request.Form.GetValues("columns[7][search][value]").FirstOrDefault();
                    var searchGeoLat = Request.Form.GetValues("columns[8][search][value]").FirstOrDefault();

                    //Paging Size (10,20,50,100)  
                    int pageSize = length != null ? Convert.ToInt32(length) : 0;
                    int skip = start != null ? Convert.ToInt32(start) : 0;
                    int recordsTotal = 0;

                    var logonList = _adminRepo.GetStatisticList();

                    //Sorting  
                    if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
                    {
                        logonList = logonList.OrderBy(sortColumn + " " + sortColumnDir);
                    }

                    //Search  
                    if (!string.IsNullOrEmpty(searchValue))
                    {

                        logonList = logonList.Where(m => m.DateTime.Contains(searchValue) || m.IpAddress.Contains(searchValue) ||
                        m.Name.Contains(searchValue) || m.Continent.Contains(searchValue) ||
                        m.Country.Contains(searchValue) || m.GeoLong.Contains(searchValue) || m.GeoLat.Contains(searchValue));
                    }

                    //Search ID
                    if (!string.IsNullOrEmpty(searchID))
                    {
                        logonList = logonList.Where(m => m.Id.ToString().Contains(searchID));
                    }

                    //Search UserId
                    if (!string.IsNullOrEmpty(searchLogonTime))
                    {
                        logonList = logonList.Where(m => m.DateTime.ToString().Contains(searchLogonTime));
                    }

                    //Search FirstName
                    if (!string.IsNullOrEmpty(searchHostIP))
                    {
                        logonList = logonList.Where(m => m.IpAddress.Contains(searchHostIP));
                    }

                    //Search SurName
                    if (!string.IsNullOrEmpty(searchHostName))
                    {
                        logonList = logonList.Where(m => m.Name.Contains(searchHostName));
                    }

                    //Search BornDate
                    if (!string.IsNullOrEmpty(searchDeviceType))
                    {
                        logonList = logonList.Where(m => m.DeviceType.ToString().Contains(searchDeviceType));
                    }

                    //Search DiplomaDate
                    if (!string.IsNullOrEmpty(searchContinent))
                    {
                        logonList = logonList.Where(m => m.Continent.Contains(searchContinent));
                    }

                    //Search NumberOfDiploma
                    if (!string.IsNullOrEmpty(searchCountry))
                    {
                        logonList = logonList.Where(m => m.Country.ToString().Contains(searchCountry));
                    }

                    //Search Name
                    if (!string.IsNullOrEmpty(searchGeoLong))
                    {
                        logonList = logonList.Where(m => m.GeoLong.Contains(searchGeoLong));
                    }

                    //Search FatherName
                    if (!string.IsNullOrEmpty(searchGeoLat))
                    {
                        logonList = logonList.Where(m => m.GeoLat.ToString().Contains(searchGeoLat));
                    }

                    //total number of rows count   
                    recordsTotal = logonList.Count();
                    //Paging   
                    var data = logonList.Skip(skip).Take(pageSize).ToList();
                    //var result = from c in data
                    //             select new Statistic {
                    //                                    Id = c.Id,
                    //                                    DateTime = c.DateTime,
                    //                                    IpAddress = c.IpAddress,
                    //                                    Name = c.Name,
                    //                                    DeviceType = (c.DeviceType == 0 ? "<i class='fa fa-desktop'></i>" : "<i class='fa fa-2x fa-mobile'></i>"),
                    //                                    c.Continent,
                    //                                    c.Country,
                    //                                    c.GeoLong,
                    //                                    c.GeoLat
                    //                                };
                    //Returning Json Data  
                    return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
                }
            }
            catch (Exception)
            {
                throw;
            }

        }

        /// <summary>
        /// ------------------------------- AJAX - DataTables - Trasy użytkowników ----------------------------------------------------------------
        /// </summary>

        public ActionResult TracingAjaxList(JQueryDataTableParamModel param)
        {
            var tracingList = _adminRepo.GetTracingList();
            //var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
            IEnumerable<Tracing> filteredCompanies;
            //Check whether the companies should be filtered by keyword
            if (!string.IsNullOrEmpty(param.sSearch))
            {
                //Used if particulare columns are filtered 
                var idFilter = Convert.ToString(Request["sSearch_1"]);
                var dateTimeFilter = Convert.ToString(Request["sSearch_2"]);
                var ipAddressFilter = Convert.ToString(Request["sSearch_3"]);
                var nameFilter = Convert.ToString(Request["sSearch_4"]);

                //Optionally check whether the columns are searchable at all 
                var isIdSearchable = Convert.ToBoolean(Request["bSearchable_1"]);
                var isDateTimeSearchable = Convert.ToBoolean(Request["bSearchable_2"]);
                var isIpAddressSearchable = Convert.ToBoolean(Request["bSearchable_3"]);
                var isNameSearchable = Convert.ToBoolean(Request["bSearchable_4"]);

                filteredCompanies = _adminRepo.GetTracingList()
                   .Where(c => c.Id.ToString().ToLower().Contains(param.sSearch.ToLower())
                               ||
                               c.DateTime.ToLower().Contains(param.sSearch.ToLower())
                               ||
                               c.HostAndIp.ToLower().Contains(param.sSearch.ToLower())
                               ||
                               c.Url.ToLower().Contains(param.sSearch.ToLower()));
            }
            else
            {
                filteredCompanies = tracingList;
            }

            var isIdSortable = Convert.ToBoolean(Request["bSortable_1"]);
            var isDateTimeSortable = Convert.ToBoolean(Request["bSortable_2"]);
            var isIpAddressSortable = Convert.ToBoolean(Request["bSortable_3"]);
            var isNameSortable = Convert.ToBoolean(Request["bSortable_4"]);

            /// problem z sortowaniem polega na tym, że pole Id jest sortowane jako string
            var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);
            Func<Tracing, object> orderingFunction = (c => /*sortColumnIndex == 0 && isIdSortable ? c.Id.ToString() :*/
                                                           sortColumnIndex == 1 && isDateTimeSortable ? c.DateTime :
                                                           sortColumnIndex == 2 && isIpAddressSortable ? c.Url :
                                                           sortColumnIndex == 3 && isNameSortable ? c.HostAndIp :
                                                           "");

            var sortdirection = Convert.ToString(Request["ssortdir_0"]);
            if (sortdirection == "asc")
                filteredCompanies = filteredCompanies.OrderBy(orderingFunction);
            else
                filteredCompanies = filteredCompanies.OrderByDescending(orderingFunction);

            var displayedCompanies = filteredCompanies.Skip(param.iDisplayStart).Take(param.iDisplayLength);
            var result = from c in displayedCompanies
                         select new object[] {
                c.Id,
                c.Url,
                c.HostAndIp,
                c.DateTime,
                (c.DeviceType == 0 ? "<i class='fa fa-desktop'></i>" : "<i class='fa fa-2x fa-mobile'></i>")
            };

            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = tracingList.Count(),
                iTotalDisplayRecords = filteredCompanies.Count(),
                aaData = result
            }, JsonRequestBehavior.AllowGet);
        }
    }
}