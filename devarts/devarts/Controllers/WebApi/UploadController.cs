using devarts.Models;
using devarts.Repositories;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Helpers;
using System.Web.Hosting;
using System.Web.Http;

namespace devarts.Controllers.WebApi
{
    public class UploadController : ApiController
    {
        AdminRepository _adminRepo;
        //LittersRepository _litterRepo;
        PostRepository _postRepo;
        KennelRepository _kennelRepo;

        public UploadController()
        {
            _adminRepo = new AdminRepository();
            //_litterRepo = new LittersRepository();
            _postRepo = new PostRepository();
            _kennelRepo = new KennelRepository();
        }

        // zapisywanie obrazów w formacie internetowym
        public static void SaveImage(HttpPostedFile image, string pathToSave)
        {
            WebImage wi = new WebImage(image.InputStream);
            wi.Resize(width: 1500, height: 1500, preserveAspectRatio: true, preventEnlarge: true);
            wi.Save(pathToSave);
        }

        public string GetBytesReadable(long i)
        {
            // Get absolute value
            long absolute_i = (i < 0 ? -i : i);
            // Determine the suffix and readable value
            string suffix;
            double readable;
            if (absolute_i >= 0x1000000000000000) // Exabyte
            {
                suffix = "EB";
                readable = (i >> 50);
            }
            else if (absolute_i >= 0x4000000000000) // Petabyte
            {
                suffix = "PB";
                readable = (i >> 40);
            }
            else if (absolute_i >= 0x10000000000) // Terabyte
            {
                suffix = "TB";
                readable = (i >> 30);
            }
            else if (absolute_i >= 0x40000000) // Gigabyte
            {
                suffix = "GB";
                readable = (i >> 20);
            }
            else if (absolute_i >= 0x100000) // Megabyte
            {
                suffix = "MB";
                readable = (i >> 10);
            }
            else if (absolute_i >= 0x400) // Kilobyte
            {
                suffix = "KB";
                readable = i;
            }
            else
            {
                return i.ToString("0 B"); // Byte
            }
            // Divide by 1024 to get fractional value
            readable = (readable / 1024);
            // Return formatted number with suffix
            return readable.ToString("0.### ") + suffix;
        }

        /// POSTY
        [Route("~/api/upload/post/{id}")]
        [HttpGet]
        [HttpPost]
        public HttpResponseMessage Post(string id)
        {
            // Get a reference to the file that our jQuery sent.  Even with multiple files, they will all be their own request and be the 0 index
            HttpPostedFile file = HttpContext.Current.Request.Files[0];

            if (file != null && file.ContentLength > 0)
            {
                // extract only the filename
                var fileName = Path.GetFileName(file.FileName);
                // store the file inside ~/App_Data/uploads folder
                if (!Directory.Exists(id))
                {
                    Directory.CreateDirectory(HostingEnvironment.MapPath("~/Posts/" + id));
                }

                var path = Path.Combine(HostingEnvironment.MapPath("~/Posts/" + id), fileName);
                try
                {
                    long len = file.ContentLength;
                    PostImage createFile = new PostImage();
                    createFile.PostId = 0;
                    createFile.PostLink = id;
                    createFile.ImageFileName = fileName;
                    createFile.ImageFileSize = len.ToString(); //GetBytesReadable(len);
                    createFile.ImageDate = DateTime.Now.ToString("dd-MM-yyyy");
                    createFile.IsPublished = true;

                    _postRepo.AddImage(createFile);
                    _postRepo.SaveChanges();

                    SaveImage(file, path);
                    /// w razie problemów z zapisywaniem komentujemy SaveImage i odkomentowujemy to poniżej
                    //file.SaveAs(path);
                }
                catch
                {

                }
            }

            // Now we need to wire up a response so that the calling script understands what happened
            HttpContext.Current.Response.ContentType = "text/plain";
            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            var result = new { name = file.FileName };

            HttpContext.Current.Response.Write(serializer.Serialize(result));
            HttpContext.Current.Response.StatusCode = 200;

            // For compatibility with IE's "done" event we need to return a result as well as setting the context.response
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        /// DODAWANIE PSÓW
        [Route("~/api/upload/dog/{breedLink:alpha}/{dogLink}/{dogId:int}")]
        [HttpGet]
        [HttpPost]
        public HttpResponseMessage Dog(string breedLink, string dogLink, string dogId)
        {
            // Get a reference to the file that our jQuery sent.  Even with multiple files, they will all be their own request and be the 0 index
            HttpPostedFile file = HttpContext.Current.Request.Files[0];

            if (file != null && file.ContentLength > 0)
            {
                // extract only the filename
                var fileName = Path.GetFileName(file.FileName);
                // store the file inside ~/App_Data/uploads folder
                if (!Directory.Exists(dogLink))
                {
                    Directory.CreateDirectory(HostingEnvironment.MapPath("~/Dogs/" + breedLink + "/" + dogLink));
                }

                var path = Path.Combine(HostingEnvironment.MapPath("~/Dogs/" + breedLink + "/" + dogLink), fileName);
                var dirName = new DirectoryInfo(Path.GetDirectoryName(path)).Name;
                try
                {
                    long len = file.ContentLength;
                    DogImages createFile = new DogImages();
                    createFile.DogId = Convert.ToInt32(dogId);
                    createFile.DogLink = dogLink;
                    createFile.ImageFileName = fileName;
                    createFile.ImageFileSize = len.ToString();
                    createFile.ImageDate = DateTime.Now.ToString("dd-MM-yyyy");
                    createFile.IsPublished = true;

                    _kennelRepo.AddDogImage(createFile);
                    _kennelRepo.SaveChanges();

                    SaveImage(file, path);
                    /// w razie problemów z zapisywaniem komentujemy SaveImage i odkomentowujemy to poniżej
                    //file.SaveAs(path);
                }
                catch
                {

                }
            }

            // Now we need to wire up a response so that the calling script understands what happened
            HttpContext.Current.Response.ContentType = "text/plain";
            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            var result = new { name = file.FileName };

            HttpContext.Current.Response.Write(serializer.Serialize(result));
            HttpContext.Current.Response.StatusCode = 200;

            // For compatibility with IE's "done" event we need to return a result as well as setting the context.response
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        /// DODAWANIE MIOTÓW
        [Route("~/api/upload/litter/{breedLink}/{litterLink}/{breedId:int}/{dogId:int}/{litterId:int}/{dogLink}")]
        [HttpGet]
        [HttpPost]
        public HttpResponseMessage Litter(string breedLink, string litterLink, string breedId, string dogId, string litterId, string dogLink)
        //public HttpResponseMessage Litter(string breedLink, string litterLink)
        {
            // Get a reference to the file that our jQuery sent.  Even with multiple files, they will all be their own request and be the 0 index
            HttpPostedFile file = HttpContext.Current.Request.Files[0];

            if (file != null && file.ContentLength > 0)
            {
                // extract only the filename
                var fileName = Path.GetFileName(file.FileName);
                // store the file inside ~/App_Data/uploads folder
                if (!Directory.Exists(litterLink))
                {
                    Directory.CreateDirectory(HostingEnvironment.MapPath("~/Litters/" + breedLink + "/" + litterLink));
                }

                var path = Path.Combine(HostingEnvironment.MapPath("~/Litters/" + breedLink + "/" + litterLink), fileName);
                var dirName = new DirectoryInfo(Path.GetDirectoryName(path)).Name;
                try
                {
                    int convertedDogId = Convert.ToInt32(dogId);
                    var dog = _kennelRepo.GetDogById(convertedDogId);
                    var breed = Convert.ToInt32(breedId);

                    // Sprawdzanie czy ten miot jest już dodany (czyli tryb edycji) i jeśli jest dodany, to aktualizowanie zdjęcia zapowiedzi
                    //var isLitterEdit = _kennelRepo.GetLitterById(Convert.ToInt32(litterId));
                    //if (isLitterEdit != null)
                    //{
                    //    // 1. Usunięcie poprzedniego pliku zapowiedzi
                    //    var litterImage = _kennelRepo.GetLitterImageByLitterId(Convert.ToInt32(litterId));
                    //    var pathFileToRemove = Path.Combine(HostingEnvironment.MapPath("~/Litters/" + breedLink + "/" + litterLink), litterImage.ImageFileName);
                    //    if (File.Exists(pathFileToRemove))
                    //    {
                    //        File.Delete(pathFileToRemove);

                    //        // 2. Usunięcie pozycji LitterImage wskazującej na zdjęcie zapowiedzi
                    //        _kennelRepo.RemoveLitterImage(litterImage);
                    //        _kennelRepo.SaveChanges();
                    //    }
                    //}

                    // dalej jest już upload zdjęcia
                    long len = file.ContentLength;
                    LitterImages createFile = new LitterImages();

                    createFile.DogId = dog.Id;
                    createFile.BreedId = breed;
                    createFile.LitterId = Convert.ToInt32(litterId);
                    createFile.BreedLink = breedLink;
                    createFile.DogLink = dog.DogLink;
                    createFile.LitterLink = litterLink;

                    createFile.ImageFileName = fileName;
                    createFile.ImageFileSize = len.ToString();
                    createFile.ImageDate = DateTime.Now.ToString("dd-MM-yyyy");
                    createFile.IsPublished = true;

                    _kennelRepo.AddLitterImage(createFile);
                    _kennelRepo.SaveChanges();

                    SaveImage(file, path);
                    /// w razie problemów z zapisywaniem komentujemy SaveImage i odkomentowujemy to poniżej
                    //file.SaveAs(path);
                }
                catch
                {

                }
            }

            // Now we need to wire up a response so that the calling script understands what happened
            HttpContext.Current.Response.ContentType = "text/plain";
            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            var result = new { name = file.FileName };

            HttpContext.Current.Response.Write(serializer.Serialize(result));
            HttpContext.Current.Response.StatusCode = 200;

            // For compatibility with IE's "done" event we need to return a result as well as setting the context.response
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        /// DODAWANIE SZCZENIĄT
        [Route("~/api/upload/puppy/{breedLink}/{litterLink}/{breedId:int}/{dogId:int}/{litterId:int}/{dogLink}/{puppyLink}")]
        [HttpGet]
        [HttpPost]
        public HttpResponseMessage Puppy(string breedLink, string litterLink, string breedId, string dogId, string litterId, string dogLink, string puppyLink)
        //public HttpResponseMessage Litter(string breedLink, string litterLink)
        {
            // Get a reference to the file that our jQuery sent.  Even with multiple files, they will all be their own request and be the 0 index
            HttpPostedFile file = HttpContext.Current.Request.Files[0];

            if (file != null && file.ContentLength > 0)
            {
                // extract only the filename
                var fileName = Path.GetFileName(file.FileName);
                // store the file inside ~/App_Data/uploads folder
                if (!Directory.Exists(litterLink))
                {
                    Directory.CreateDirectory(HostingEnvironment.MapPath("~/Puppies/" + breedLink + "/" + puppyLink));
                }

                var path = Path.Combine(HostingEnvironment.MapPath("~/Puppies/" + breedLink + "/" + puppyLink), fileName);
                var dirName = new DirectoryInfo(Path.GetDirectoryName(path)).Name;
                try
                {
                    int convertedDogId = Convert.ToInt32(dogId);
                    var dog = _kennelRepo.GetDogById(convertedDogId);
                    var breed = Convert.ToInt32(breedId);

                    // Sprawdzanie czy ten miot jest już dodany (czyli tryb edycji) i jeśli jest dodany, to aktualizowanie zdjęcia zapowiedzi
                    var isLitterEdit = _kennelRepo.GetLitterById(Convert.ToInt32(litterId));
                    if (isLitterEdit != null)
                    {
                        // 1. Usunięcie poprzedniego pliku zapowiedzi
                        var litterImage = _kennelRepo.GetLitterImageByLitterId(Convert.ToInt32(litterId));
                        var pathFileToRemove = Path.Combine(HostingEnvironment.MapPath("~/Puppies/" + breedLink + "/" + puppyLink), litterImage.ImageFileName);
                        if (File.Exists(pathFileToRemove))
                        {
                            File.Delete(pathFileToRemove);

                            // 2. Usunięcie pozycji LitterImage wskazującej na zdjęcie zapowiedzi
                            _kennelRepo.RemoveLitterImage(litterImage);
                            _kennelRepo.SaveChanges();
                        }
                    }

                    // dalej jest już upload zdjęcia
                    long len = file.ContentLength;
                    PuppyImages createFile = new PuppyImages();

                    createFile.DogId = dog.Id;
                    createFile.BreedId = breed;
                    createFile.LitterId = Convert.ToInt32(litterId);
                    createFile.BreedLink = breedLink;
                    createFile.DogLink = dog.DogLink;
                    createFile.LitterLink = litterLink;
                    createFile.PuppyLink = puppyLink;

                    createFile.ImageFileName = fileName;
                    createFile.ImageFileSize = len.ToString();
                    createFile.ImageDate = DateTime.Now.ToString("dd-MM-yyyy");
                    createFile.IsPublished = true;

                    _kennelRepo.AddPuppyImage(createFile);
                    _kennelRepo.SaveChanges();

                    SaveImage(file, path);
                    /// w razie problemów z zapisywaniem komentujemy SaveImage i odkomentowujemy to poniżej
                    //file.SaveAs(path);
                }
                catch (Exception ex)
                {
                    string exceptionString = ex.ToString();
                }
            }

            // Now we need to wire up a response so that the calling script understands what happened
            HttpContext.Current.Response.ContentType = "text/plain";
            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            var result = new { name = file.FileName };

            HttpContext.Current.Response.Write(serializer.Serialize(result));
            HttpContext.Current.Response.StatusCode = 200;

            // For compatibility with IE's "done" event we need to return a result as well as setting the context.response
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}
