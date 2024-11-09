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
    public class AjaxNewsletterController : Controller
    {
        private AdminRepository _adminRepo;
        private static Logger nLog = LogManager.GetCurrentClassLogger();

        public AjaxNewsletterController()
        {
            _adminRepo = new AdminRepository();
        }

        public ActionResult NewsletterAjaxList()
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
                    var searchEmail = Request.Form.GetValues("columns[1][search][value]").FirstOrDefault();
                    var searchSubject = Request.Form.GetValues("columns[2][search][value]").FirstOrDefault();
                    var searchCreateDate = Request.Form.GetValues("columns[3][search][value]").FirstOrDefault();
                    var searchSendDate = Request.Form.GetValues("columns[4][search][value]").FirstOrDefault();
                    var searchIsActive = Request.Form.GetValues("columns[5][search][value]").FirstOrDefault();                    

                    //Paging Size (10,20,50,100)  
                    int pageSize = length != null ? Convert.ToInt32(length) : 0;
                    int skip = start != null ? Convert.ToInt32(start) : 0;
                    int recordsTotal = 0;


                    var newsletterList = _adminRepo.GetAllNewsletters();

                    //Sorting  
                    if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
                    {
                        newsletterList = newsletterList.OrderBy(sortColumn + " " + sortColumnDir);
                    }

                    //Search  
                    if (!string.IsNullOrEmpty(searchValue))
                    {

                        newsletterList = newsletterList.Where(m => m.Id.ToString().Contains(searchValue) || m.Email.Contains(searchValue) ||
                        m.Subject.Contains(searchValue) || m.CreateDate.Contains(searchValue) || m.SendDate.Contains(searchValue)
                        || m.IsActive.ToString().Contains(searchValue));
                    }

                    //Search ID
                    if (!string.IsNullOrEmpty(searchID))
                    {
                        newsletterList = newsletterList.Where(m => m.Id.ToString().Contains(searchID));
                    }

                    //Search Email
                    if (!string.IsNullOrEmpty(searchEmail))
                    {
                        newsletterList = newsletterList.Where(m => m.Email.Contains(searchEmail));
                    }

                    //Search Subject
                    if (!string.IsNullOrEmpty(searchSubject))
                    {
                        newsletterList = newsletterList.Where(m => m.Subject.Contains(searchSubject));
                    }

                    //Search CreateDate
                    if (!string.IsNullOrEmpty(searchCreateDate))
                    {
                        newsletterList = newsletterList.Where(m => m.CreateDate.Contains(searchCreateDate));
                    }

                    //Search SendDate
                    if (!string.IsNullOrEmpty(searchSendDate))
                    {
                        newsletterList = newsletterList.Where(m => m.SendDate.Contains(searchSendDate));
                    }

                    //Search IsActive
                    if (!string.IsNullOrEmpty(searchIsActive))
                    {
                        newsletterList = newsletterList.Where(m => m.IsActive.ToString().Contains(searchIsActive));
                    }                    

                    //total number of rows count   
                    recordsTotal = newsletterList.Count();
                    //Paging   
                    var data = newsletterList.Skip(skip).Take(pageSize).ToList();

                    //Returning Json Data  
                    return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
                }
            }
            catch (Exception ex)
            {
                string errorMsg = ex.ToString();
                throw;
            }
        }

        public ActionResult SubscriberAjaxList()
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
                    var searchEmail = Request.Form.GetValues("columns[1][search][value]").FirstOrDefault();                    
                    var searchCreateDate = Request.Form.GetValues("columns[2][search][value]").FirstOrDefault();                    
                    var searchIsActive = Request.Form.GetValues("columns[3][search][value]").FirstOrDefault();

                    //Paging Size (10,20,50,100)  
                    int pageSize = length != null ? Convert.ToInt32(length) : 0;
                    int skip = start != null ? Convert.ToInt32(start) : 0;
                    int recordsTotal = 0;


                    var subscriberswList = _adminRepo.GetAllSubscribers();

                    //Sorting  
                    if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
                    {
                        subscriberswList = subscriberswList.OrderBy(sortColumn + " " + sortColumnDir);
                    }

                    //Search  
                    if (!string.IsNullOrEmpty(searchValue))
                    {

                        subscriberswList = subscriberswList.Where(m => m.Id.ToString().Contains(searchValue) || m.Email.Contains(searchValue) ||
                        m.CreateDate.Contains(searchValue) || m.IsActive.ToString().Contains(searchValue));
                    }

                    //Search ID
                    if (!string.IsNullOrEmpty(searchID))
                    {
                        subscriberswList = subscriberswList.Where(m => m.Id.ToString().Contains(searchID));
                    }

                    //Search Email
                    if (!string.IsNullOrEmpty(searchEmail))
                    {
                        subscriberswList = subscriberswList.Where(m => m.Email.Contains(searchEmail));
                    }

                    //Search CreateDate
                    if (!string.IsNullOrEmpty(searchCreateDate))
                    {
                        subscriberswList = subscriberswList.Where(m => m.CreateDate.Contains(searchCreateDate));
                    }

                    //Search IsActive
                    if (!string.IsNullOrEmpty(searchIsActive))
                    {
                        subscriberswList = subscriberswList.Where(m => m.IsActive.ToString().Contains(searchIsActive));
                    }

                    //total number of rows count   
                    recordsTotal = subscriberswList.Count();
                    //Paging   
                    var data = subscriberswList.Skip(skip).Take(pageSize).ToList();

                    //Returning Json Data  
                    return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
                }
            }
            catch (Exception ex)
            {
                string errorMsg = ex.ToString();
                throw;
            }
        }

        /// Ustawianie aktywności subskrybenta
        public void SetSubscriberStatus(int id)
        {
            try
            {
                var subscriberStatus = _adminRepo.GetSubscriberById(id);
                if (subscriberStatus.IsActive == true)
                {
                    subscriberStatus.IsActive = false;

                    TryUpdateModel(subscriberStatus);
                    _adminRepo.SaveChanges();
                }
                else
                {
                    subscriberStatus.IsActive = true;

                    TryUpdateModel(subscriberStatus);
                    _adminRepo.SaveChanges();
                }
            }
            catch
            {
                // wysłanie do jQuery statusu Error
                Response.StatusCode = 500;
            }
        }

        /// Usuwanie subskrybenta newslettera
        public void RemoveSubscriber(int id)
        {
            try
            {
                var subscriberToRemove = _adminRepo.GetSubscriberById(id);
                _adminRepo.DeleteSubscriber(subscriberToRemove);
                //_adminRepo.SaveChanges();
            }
            catch
            {
                // wysłanie do jQuery statusu Error
                Response.StatusCode = 500;
            }
        }

    }
}