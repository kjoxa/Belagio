using devarts.Helpers;
using devarts.Models;
using devarts.Repositories;
using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Web.Mvc;

namespace devarts.Controllers
{
    // wow! injecter działa!
    [LayoutInjecter("_adminLayout")]
    [Authorize]
    public class AdminController : Controller
    {
        AdminRepository _adminRepo;
        PostRepository _postRepo;
        private static Logger nLog = LogManager.GetCurrentClassLogger();

        public AdminController()
        {
            _adminRepo = new AdminRepository();
            _postRepo = new PostRepository();
        }

        public FileResult SafeDownload(string fileName)
        {
            if (fileName.ToLower().Contains("web.config") || fileName.ToLower().Contains("site") || fileName.ToLower().Contains("msc"))
            {
                RedirectToAction("NotFound", "Error").WithError(this, "Nie wolno!", "Operacja nie może zostać wykonana!");
            }

            byte[] fileBytes = System.IO.File.ReadAllBytes(HostingEnvironment.MapPath("~/DocumentsFiles/" + fileName));
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

        public List<SelectListItem> PostType()
        {
            List<SelectListItem> entryType = new List<SelectListItem>();
            entryType.Add(new SelectListItem { Text = "News", Value = "News" });
            entryType.Add(new SelectListItem { Text = "Blog", Value = "Blog" });
            entryType.Add(new SelectListItem { Text = "Portfolio", Value = "Portfolio" });
            entryType.Add(new SelectListItem { Text = "Wywiady", Value = "Wywiady" });

            return entryType;
        }

        public List<SelectListItem> PostCathegories()
        {
            List<SelectListItem> category = new List<SelectListItem>();
            category.Add(new SelectListItem { Text = "Szczenięta", Value = "Szczenięta" });            
            category.Add(new SelectListItem { Text = "Wystawy", Value = "Wystawy" });
            category.Add(new SelectListItem { Text = "Hodowla", Value = "Hodowla" });
            category.Add(new SelectListItem { Text = "Wydarzenia", Value = "Wydarzenia" });
            category.Add(new SelectListItem { Text = "Inne", Value = "Inne" });

            return category;
        }

        public ActionResult Licence()
        {
            return View();
        }        

        public ActionResult Progress()
        {
            var prg = new OperationsStatus();
            prg.Operation1 = true;
            prg.Operation2 = true;
            prg.Operation2 = true;
            prg.Operation3 = true;
            prg.Operation4 = true;
            prg.Operation5 = true;
            prg.Operation6 = true;
            prg.Operation7 = true;
            prg.Operation8 = false;
            prg.Operation9 = true;
            prg.Operation10 = false;
            prg.Operation11 = false;
            prg.Operation12 = true;
            prg.Operation13 = false;
            prg.Operation14 = false;
            prg.Operation15 = false;
            prg.Operation16 = false;
            prg.Operation17 = false;
            prg.Operation18 = false;
            prg.Operation19 = false;
            prg.Operation20 = false;
            return View(prg);
        }

        public ActionResult Index()
        {
            ViewBag.siteName = WebConfigurationManager.AppSettings["siteName"];
            ViewBag.devaArtsCMS = WebConfigurationManager.AppSettings["devArtsCMS"];
            ViewBag.ownerFirstName = WebConfigurationManager.AppSettings["ownerFirstName"];
            ViewBag.ownerSurName = WebConfigurationManager.AppSettings["ownerSurName"];

            return View();
        }

        /// ZARZĄDZANIE NEWSLETTEREM

        /// <summary>
        ///  Lista newslettera
        /// </summary>
        /// <returns></returns>

        public ActionResult Newsletter()
        {
            return View();
        }

        /// ZARZĄDZANIE POSTAMI

        /// <summary>
        ///  Lista postów
        /// </summary>
        /// <returns></returns>
        public ActionResult Posts()
        {
            return View();
        }

        // podgląd posta w oknie modalnym za pomocą AJAX-owych metod
        [LayoutInjecter(null)]
        public ActionResult PreviewPost(int id)
        {

            var selectPost = _postRepo.GetPostById(id);
            return PartialView("_previewPost", selectPost);
        }

        /// <summary>
        ///  Dodawanie posta
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult CreatePost()
        {
            var postModel = new Post();
            postModel.Type = "Aktualności";
            ViewBag.PostType = PostType();
            ViewBag.PostCathegories = PostCathegories();

            return View();
        }

        [HttpPost]
        public ActionResult CreatePost(Post postModel)
        {
            ViewBag.PostType = PostType();
            ViewBag.PostCathegories = PostCathegories();

            var postDate = DateTime.Now.ToString("dd.MM.yyyy");

            if (ModelState.IsValid)
            {
                var createPost = new Post();
                //createPost.ImageId = 0;
                createPost.AuthorName = postModel.AuthorName;
                createPost.PostName = postModel.PostName;
                createPost.PostNameEng = postModel.PostNameEng;

                createPost.Type = postModel.Type;
                createPost.CategoryName = "";//postModel.CategoryName;

                createPost.PostContentShort = postModel.PostContentShort;
                createPost.PostContentShortEng = postModel.PostContentShortEng;

                createPost.PostContent = postModel.PostContent;
                createPost.PostContentEng = postModel.PostContentEng;

                createPost.PostDate = postModel.PostDate;
                createPost.ModifiedDate = postDate;
                
                try
                {
                    DateTime systemDate = DateTime.ParseExact(postModel.PostDate, "dd.MM.yyyy", CultureInfo.InvariantCulture);
                    createPost.PostDateOkay = systemDate;
                }
                catch
                {
                    createPost.PostDateOkay = DateTime.Now;
                }

                createPost.PostLink = postModel.PostLink;

                createPost.PostRate = 0;
                createPost.PostShow = 0;
                createPost.PostCommentsCount = 0;
                createPost.PostLikes = 0;
                createPost.AllowComments = false;
                createPost.IsPublished = false;

                createPost.Tags = postModel.Tags;

                // ustawianie zdjęcia głównego - pierwszego lepszego, aby nie było błędów                
                var mainImage = _postRepo.GetImagesByPostLink(postModel.PostLink).FirstOrDefault();
                createPost.ImageId = mainImage.Id;
                createPost.MainPicture = mainImage.ImageFileName;

                _postRepo.AddPost(createPost);
                _postRepo.SaveChanges();

                return RedirectToAction("Posts").WithSuccess(this, "Sukces!", "Pomyślnie dodano nowy wpis!");
            }
            else
            {
                var message = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return View().WithError(this, "Błąd", message);
            }
        }

        [HttpGet]
        public ActionResult EditPost(string postLink)
        {
            ViewBag.PostType = PostType();
            ViewBag.PostCathegories = PostCathegories();

            try
            {
                var editPost = _postRepo.GetPostByPostLink(postLink);
                var imagesByPost = _postRepo.GetImagesByPostLink(postLink);

                try
                {
                    string mainImageFile = _postRepo.GetPostImageById(editPost.ImageId).ImageFileName;
                    ViewBag.MainImage = mainImageFile;
                }
                catch
                {
                    ViewBag.MainImage = "empty.jpg";
                }

                var fullPostModel = new PostWithAllImages();

                fullPostModel.Post = editPost;
                fullPostModel.imgFiles = imagesByPost;

                return View(fullPostModel).WithSuccess(this, "Sukces", "Post został zapisany.");
            }
            catch
            {
                return RedirectToAction("Posts").WithError(this, "Błąd!", "<br>Nie istnieje wpis o podanym ID!");
            }
        }

        [HttpPost]
        public ActionResult EditPost(PostWithAllImages model, string postLink)
        {
            ViewBag.PostType = PostType();
            ViewBag.PostCathegories = PostCathegories();

            var modifiedDate = DateTime.Now.ToString("dd.MM.yyyy");

            try
            {
                var editPost = _postRepo.GetPostByPostLink(postLink);
                var imagesByPost = _postRepo.GetImagesByPostLink(postLink);

                string mainImageFile = _postRepo.GetPostImageById(editPost.ImageId).ImageFileName;
                ViewBag.MainImage = mainImageFile;

                var fullPostModel = new PostWithAllImages();

                fullPostModel.Post = editPost;
                fullPostModel.imgFiles = imagesByPost;

                if (editPost != null)
                {
                    if (ModelState.IsValid)
                    {
                        //editPost.ImageId = 0;

                        editPost.AuthorName = model.Post.AuthorName;
                        editPost.PostName = model.Post.PostName;
                        editPost.PostNameEng = model.Post.PostNameEng;
                        editPost.CategoryName = model.Post.CategoryName;
                        editPost.PostContentShort = model.Post.PostContentShort;
                        editPost.PostContent = model.Post.PostContent;
                        editPost.PostContentShortEng = model.Post.PostContentShortEng;
                        editPost.PostContentEng = model.Post.PostContentEng;

                        editPost.PostLink = model.Post.PostLink;
                        editPost.ModifiedDate = DateTime.Now.ToString("dd.MM.yyyy");
                        editPost.PostDate = model.Post.PostDate;
                        try
                        {
                            DateTime systemDate = DateTime.ParseExact(model.Post.PostDate, "dd.MM.yyyy", CultureInfo.InvariantCulture);
                            editPost.PostDateOkay = systemDate;
                        }
                        catch
                        {
                            editPost.PostDateOkay = DateTime.Now;
                        }                        
                        editPost.Type = model.Post.Type;
                        editPost.Tags = model.Post.Tags;

                        TryUpdateModel(editPost);
                        _postRepo.SaveChanges();
                    }
                    else
                    {

                    }
                    return View(fullPostModel).WithSuccess(this, "Sukces", "Post został zapisany."); ;
                }
                else
                {
                    return RedirectToAction("Posts").WithError(this, "Błąd!", "Nie istnieje wpis o podanym ID!");
                }
            }
            catch
            {
                return RedirectToAction("Posts").WithError(this, "Błąd!", "Nie istnieje wpis o podanym ID!");
            }
        }

        // zarządzanie hodowlą
        public ActionResult KennelDocuments()
        {
            return View();
        }

        public ActionResult KennelBitches()
        {
            return View();
        }

        public ActionResult Presentation()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AjaxPresentation()
        {
            List<object> iData = new List<object>();
            //Creating sample data  
            DataTable dt = new DataTable();
            dt.Columns.Add("Employee", System.Type.GetType("System.String"));
            dt.Columns.Add("Credit", System.Type.GetType("System.Int32"));

            DataRow dr = dt.NewRow();
            dr["Employee"] = "Sam";
            dr["Credit"] = 123;
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Employee"] = "Alex";
            dr["Credit"] = 456;
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Employee"] = "Michael";
            dr["Credit"] = 587;
            dt.Rows.Add(dr);
            //Looping and extracting each DataColumn to List<Object>  
            foreach (DataColumn dc in dt.Columns)
            {
                List<object> x = new List<object>();
                x = (from DataRow drr in dt.Rows select drr[dc.ColumnName]).ToList();
                iData.Add(x);
            }
            //Source data returned as JSON  
            return Json(iData, JsonRequestBehavior.AllowGet);
        }

        // USTAWIANIE ZDJĘCIA GŁÓWNEGO
        public ActionResult SetAsMainImage(string postLink, int mainImageId)
        {
            try
            {
                var setForPost = _postRepo.GetPostByPostLink(postLink);
                var mainImage = _postRepo.GetPostImageById(mainImageId);
                setForPost.ImageId = mainImageId;
                setForPost.MainPicture = mainImage.ImageFileName;
                TryUpdateModel(setForPost);
                _postRepo.SaveChanges();

                return RedirectToAction("EditPost", "Admin", new { postLink = postLink }).WithSuccess(this, "Sukces", "Pomyślnie ustawiono zdjęcie główne jako: " + mainImage.ImageFileName);
            }
            catch
            {
                return RedirectToAction("EditPost", "Admin", new { postLink = postLink }).WithError(this, "Błąd", "Wystąpił błąd podczas ustawiania zdjęcia głównego (Kod - 0x00001)");
            }
        }

        /// USUWANIE ZDJĘĆ Z SERWERA I BAZY DANYCH
        public ActionResult RemoveImageFromNews(string imgFileName, string postLink)
        {
            var imgToRemove = _postRepo.GetImageFileByImageFileNameAndPost(imgFileName, postLink);

            if (imgToRemove != null)
            {
                var filePath = Path.Combine(HostingEnvironment.MapPath("~/Posts/" + postLink), imgToRemove.ImageFileName);

                // usuwanie z bazy danych
                _postRepo.DeleteImage(imgToRemove);
                _postRepo.SaveChanges();

                // usuwanie z systemu plików
                System.IO.File.Delete(filePath);

                return RedirectToAction("EditPost", "Admin", new { postLink = postLink }).WithSuccess(this, "Sukces", "Pomyślnie usunięto plik: " + Path.Combine(HostingEnvironment.MapPath("~/Posts/" + postLink), imgToRemove.ImageFileName));
            }
            else
            {
                return RedirectToAction("Post").WithError(this, "Błąd", "Nie znaleziono wybranego zdjęcia");
            }
        }

        // dla nie AJAXOWEGO wywołania
        public ActionResult SetPostVisibilityRender(int id)
        {
            try
            {
                var postVisibility = _postRepo.GetPostById(id);
                if (postVisibility.IsPublished == true)
                {
                    postVisibility.IsPublished = false;

                    TryUpdateModel(postVisibility);
                    _postRepo.SaveChanges();

                    return RedirectToAction("EditPost", "Admin", new { postLink = postVisibility.PostLink }).WithSuccess(this, "Sukces", "Post został ukryty");
                }
                else
                {
                    postVisibility.IsPublished = true;

                    TryUpdateModel(postVisibility);
                    _postRepo.SaveChanges();

                    return RedirectToAction("EditPost", "Admin", new { postLink = postVisibility.PostLink }).WithSuccess(this, "Sukces", "Post został opublikowany");
                }
            }
            catch
            {
                // wysłanie do jQuery statusu Error
                Response.StatusCode = 500;
                return View("Posts", "Admin").WithError(this, "Błąd", "Coś poszło nie tak (0x11345)");
            }
        }

        /// <summary>
        /// AJAXOWE METODY
        /// </summary>       

        /// Ustawianie widoczności posta
        public void SetPostVisibility(int id)
        {
            try
            {
                var postVisibility = _postRepo.GetPostById(id);
                if (postVisibility.IsPublished == true)
                {
                    postVisibility.IsPublished = false;

                    TryUpdateModel(postVisibility);
                    _postRepo.SaveChanges();
                }
                else
                {
                    postVisibility.IsPublished = true;

                    TryUpdateModel(postVisibility);
                    _postRepo.SaveChanges();
                }
            }
            catch
            {
                // wysłanie do jQuery statusu Error
                Response.StatusCode = 500;
            }
        }

        // AJAX-owe wywołanie metody usuwającej statystyki odwiedzin
        public void RemoveStatisticRecords()
        {
            try
            {
                if (_adminRepo.DeleteAllPositionsFromStatistics())
                {
                    _adminRepo.SaveChanges();
                }
            }
            catch
            {
                // wysłanie do jQuery statusu Error
                Response.StatusCode = 500;
            }
        }

        // AJAX-owe wywołanie metody usuwającej trasy użytkowników
        public void RemoveTracingRecords()
        {
            try
            {
                if (_adminRepo.DeleteAllPositionsFromTracing())
                {
                    _adminRepo.SaveChanges();
                }
            }
            catch
            {
                // wysłanie do jQuery statusu Error
                Response.StatusCode = 500;
            }
        }
    }
}