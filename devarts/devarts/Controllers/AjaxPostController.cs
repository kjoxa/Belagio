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
    public class AjaxPostController : Controller
    {
        private PostRepository _postRepo;
        private static Logger nLog = LogManager.GetCurrentClassLogger();

        public AjaxPostController()
        {
            _postRepo = new PostRepository();
        }
        
        public JsonResult CheckPostLinkExists(string postLink)
        {
            var searchPost = _postRepo.GetPostByPostLink(postLink.ToLower());
            if (searchPost != null)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }            
        }

        public ActionResult PostsAjaxList()
        {
            try
            {

                {
                    var type = Request.Form.GetValues("type").FirstOrDefault();
                    var draw = Request.Form.GetValues("draw").FirstOrDefault();
                    var start = Request.Form.GetValues("start").FirstOrDefault();
                    var length = Request.Form.GetValues("length").FirstOrDefault();
                    var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
                    var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
                    var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();

                    var searchID = Request.Form.GetValues("columns[0][search][value]").FirstOrDefault();
                    var searchPostName = Request.Form.GetValues("columns[1][search][value]").FirstOrDefault();
                    var searchType = Request.Form.GetValues("columns[2][search][value]").FirstOrDefault();
                    var searchPostLink = Request.Form.GetValues("columns[3][search][value]").FirstOrDefault();
                    var searchPostDate = Request.Form.GetValues("columns[4][search][value]").FirstOrDefault();
                    var searchPostRate = Request.Form.GetValues("columns[5][search][value]").FirstOrDefault();
                    var searchPostShow = Request.Form.GetValues("columns[6][search][value]").FirstOrDefault();
                    var searchAllowComments = Request.Form.GetValues("columns[7][search][value]").FirstOrDefault();
                    var searchIsPublished = Request.Form.GetValues("columns[8][search][value]").FirstOrDefault();

                    //Paging Size (10,20,50,100)  
                    int pageSize = length != null ? Convert.ToInt32(length) : 0;
                    int skip = start != null ? Convert.ToInt32(start) : 0;
                    int recordsTotal = 0;

                    var postsList = _postRepo.GetAllPosts();

                    // filtorwanie po kategorii
                    if (!string.IsNullOrEmpty(type))
                    {
                        if (type != "All")
                        {
                            postsList = postsList.Where(c => c.Type.ToLower() == type.ToLower());
                        }                        
                    }

                    //Sorting  
                    if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
                    {
                        postsList = postsList.OrderBy(sortColumn + " " + sortColumnDir);
                    }

                    //Search  
                    if (!string.IsNullOrEmpty(searchValue))
                    {

                        postsList = postsList.Where(m => m.Id.ToString().Contains(searchValue) || m.PostName.Contains(searchValue) ||
                        m.Type.Contains(searchValue) || m.PostLink.Contains(searchValue) || m.ModifiedDate.Contains(searchValue) 
                        || m.PostRate.ToString().Contains(searchValue) || m.PostShow.ToString().Contains(searchValue) || m.AllowComments.ToString().Contains(searchValue)
                        || m.IsPublished.ToString().Contains(searchValue));
                    }

                    //Search ID
                    if (!string.IsNullOrEmpty(searchID))
                    {
                        postsList = postsList.Where(m => m.Id.ToString().Contains(searchID));
                    }

                    //Search PostName
                    if (!string.IsNullOrEmpty(searchPostName))
                    {
                        postsList = postsList.Where(m => m.PostName.Contains(searchPostName));
                    }

                    //Search Type
                    if (!string.IsNullOrEmpty(searchType))
                    {
                        postsList = postsList.Where(m => m.Type.Contains(searchType));
                    }

                    //Search PostLink
                    if (!string.IsNullOrEmpty(searchPostLink))
                    {
                        postsList = postsList.Where(m => m.PostLink.Contains(searchPostLink));
                    }

                    //Search PostDate
                    if (!string.IsNullOrEmpty(searchPostDate))
                    {
                        postsList = postsList.Where(m => m.PostDate.Contains(searchPostDate));
                    }

                    //Search PostRate
                    if (!string.IsNullOrEmpty(searchPostRate))
                    {
                        postsList = postsList.Where(m => m.PostRate.ToString().Contains(searchPostRate));
                    }
                   
                    //Search PostShow
                    if (!string.IsNullOrEmpty(searchPostShow))
                    {
                        postsList = postsList.Where(m => m.PostShow.ToString().Contains(searchPostShow));
                    }

                    //Search AllowComment
                    if (!string.IsNullOrEmpty(searchAllowComments))
                    {
                        postsList = postsList.Where(m => m.AllowComments.ToString().Contains(searchAllowComments));
                    }

                    //Search IsPublished
                    if (!string.IsNullOrEmpty(searchIsPublished))
                    {
                        postsList = postsList.Where(m => m.IsPublished.ToString().Contains(searchIsPublished));
                    }

                    //total number of rows count   
                    recordsTotal = postsList.Count();
                    //Paging   
                    var data = postsList.Skip(skip).Take(pageSize).ToList();

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
    }
}