using devarts.Models;
using devarts.Repositories;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace devarts.Controllers
{
    public class PortfolioController : Controller
    {
        private PostRepository _postRepo;
        private static Logger nLog = LogManager.GetCurrentClassLogger();

        public PortfolioController()
        {
            _postRepo = new PostRepository();
        }

        public ActionResult Single(string postLink)
        {
            var singlePost = _postRepo.GetPostByPostLink(postLink);
            var postWithImages = new PostWithAllImages();
            postWithImages.Post = singlePost;

            ViewBag.MainImage = _postRepo.GetPostImageById(singlePost.ImageId).ImageFileName;

            return View(postWithImages);
        }
    }
}