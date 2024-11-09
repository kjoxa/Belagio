using PagedList;
using devarts.Models;
using devarts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NLog;

namespace devarts.Controllers
{
    public class BlogController : Controller
    {
        private PostRepository _postRepo;
        private static Logger nLog = LogManager.GetCurrentClassLogger();

        public BlogController()
        {
            _postRepo = new PostRepository();
        }

        // GET: Blog
        public ActionResult Index(int? page)
        {
            int pageSize = 6;
            int pageNumber = (page ?? 1);

            //ViewBag.Count = _adminRepo.GetCountRecords();
            var postsList = _postRepo.GetAllPosts();
            //var imagesList = _adminRepo.GetAllImages().ToList();


            List<PostWithMainImageUrl> postWithImgUrl = new List<PostWithMainImageUrl>();
            foreach (var item in postsList)
            {
                PostWithMainImageUrl addItem = new PostWithMainImageUrl();
                addItem.Id = item.Id;
                addItem.ImageId = item.ImageId;
                addItem.AuthorName = item.AuthorName;
                addItem.PostName = item.PostName;
                addItem.PostContentShort = item.PostContentShort;
                addItem.CategoryName = item.CategoryName;
                addItem.Type = item.Type;
                addItem.PostContent = item.PostContent;
                addItem.PostLink = item.PostLink;
                addItem.Tags = item.Tags;
                addItem.PostDate = item.PostDate;
                addItem.PostRate = item.PostRate;
                addItem.mainImage = _postRepo.GetPostImageById(item.ImageId).ImageFileName;
                postWithImgUrl.Add(addItem);
            }

            return View(postWithImgUrl.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Single()
        {
            return View();
        }
    }
}