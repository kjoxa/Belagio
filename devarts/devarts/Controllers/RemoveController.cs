using devarts.Helpers;
using devarts.Models;
using devarts.Repositories;
using NLog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace devarts.Controllers
{
    public class RemoveController : Controller
    {
        private KennelRepository _kennelRepo;
        private AdminRepository _adminRepo;
        private PostRepository _postRepo;
        private static Logger nLog = LogManager.GetCurrentClassLogger();

        public RemoveController()
        {
            _kennelRepo = new KennelRepository();
            _adminRepo = new AdminRepository();
            _postRepo = new PostRepository();
        }

        /*
         1. Usuwanie wszystkich szczeniąt
         - puppy, imagesByPuppies, serwer

         2. Usuwanie wszystkich miotów
         - litters, litterImages, serwer

         3. Usuwanie psa
         - Dogs, dogImages, serwer       */
        public ActionResult RemoveDog(int id)
        {
            var removeDog = _kennelRepo.GetDogById(id);
            var removeLitters = _kennelRepo.GetLittersByDogLink(removeDog.DogLink).ToList();
            //var removePuppies = _kennelRepo.GetPuppiesByLitterId(removeLitter.)

            int countOfRemoveDogImages = 0;
            int countOfRemoveLitters = 0;
            int countOfRemovePuppies = 0;
            int countOfRemovePuppyPictures = 0;

            if (removeDog != null)
            {
                if (removeLitters != null)
                {
                    /// usuwanie miotów
                    foreach (var removeLitter in removeLitters)
                    {
                        var removePuppies = _kennelRepo.GetPuppiesByLitterId(removeLitter.Id).ToList();
                        if (removePuppies != null)
                        {
                            /// usuwanie szczeniąt
                            foreach (var removePuppie in removePuppies)
                            {
                                var puppieToRemove = _kennelRepo.GetPuppyById(removePuppie.Id);
                                if (puppieToRemove != null)
                                {
                                    _kennelRepo.RemovePuppy(puppieToRemove);
                                    _kennelRepo.SaveChanges();
                                    countOfRemovePuppies++;

                                    var imagesByPuppy = _kennelRepo.GetPuppyImagesByLitterLinkAndPuppyLink(puppieToRemove.LitterLink, removePuppie.PuppyLink);
                                    if (imagesByPuppy != null)
                                    {
                                        /// usuwanie zdjęć szczeniąt
                                        foreach (var imgToRemove in imagesByPuppy)
                                        {
                                            _kennelRepo.RemovePuppyImage(imgToRemove);
                                            _kennelRepo.SaveChanges();

                                            var filePath = Path.Combine(HostingEnvironment.MapPath("~/Puppies/" + removePuppie.BreedLink + "/" + imgToRemove.PuppyLink + "/" + imgToRemove.ImageFileName));
                                            if (System.IO.File.Exists(filePath) == true)
                                            {
                                                System.IO.File.Delete(filePath);
                                            }

                                            try
                                            {
                                                foreach(var puppyFolders in imagesByPuppy.Where(p => p.DogLink == removeDog.DogLink))
                                                {
                                                    var puppyDirPath = Path.Combine(HostingEnvironment.MapPath("~/Puppies/" + removePuppie.BreedLink + "/" + puppyFolders.PuppyLink));
                                                    if (System.IO.Directory.Exists(puppyDirPath) == true)
                                                    {
                                                        System.IO.Directory.Delete(puppyDirPath);
                                                    }
                                                }                                                
                                            }
                                            catch
                                            {

                                            }

                                            countOfRemovePuppyPictures++;
                                        }

                                        /// usuwanie katalogu szczenięcia
                                        //var puppyDirPath = Path.Combine(HostingEnvironment.MapPath("~/Puppies/" + removePuppie.BreedLink + "/" + removePuppie.PuppyLink));
                                        //if (System.IO.Directory.Exists(puppyDirPath) == true)
                                        //{
                                        //    System.IO.Directory.Delete(puppyDirPath);
                                        //}
                                    }
                                }
                            }
                            /// usuwanie miotu z bazy danych i zdjęcia zapowiedzi oraz katalogu
                            var removeLitterFromDb = _kennelRepo.GetLitterById(removeLitter.Id);
                            if (removeLitterFromDb != null)
                            {
                                /// usuwanie pozycji w zdjęciach miotu
                                var litterImage = _kennelRepo.GetLitterImageByLitterLink(removeLitterFromDb.LitterLink);
                                if (litterImage != null)
                                {
                                    _kennelRepo.RemoveLitterImage(litterImage);
                                    _kennelRepo.SaveChanges();
                                    countOfRemoveLitters++;
                                }
                                
                                /// usuwanie zdjęcia zapowiedzi
                                var filePath = Path.Combine(HostingEnvironment.MapPath("~/Litters/" + litterImage.BreedLink + "/" + litterImage.LitterLink + "/" + litterImage.ImageFileName));
                                if (System.IO.File.Exists(filePath) == true)
                                {
                                    System.IO.File.Delete(filePath);
                                }

                                /// usuwanie miotu z tabeli Litters
                                _kennelRepo.RemoveLitter(removeLitterFromDb);
                                _kennelRepo.SaveChanges();

                                /// usuwanie katalogu miotu
                                var litterDirPath = Path.Combine(HostingEnvironment.MapPath("~/Litters/" + litterImage.BreedLink + "/" + removeLitterFromDb.LitterLink));
                                if (System.IO.Directory.Exists(litterDirPath))
                                {
                                    System.IO.Directory.Delete(litterDirPath);
                                }
                            }
                        }                       
                    }
                }

                /// NA KONIEC - USUWANIE ZDJĘĆ PSA, PSA Z BAZY DANYCH ORAZ KATALOGU
                var dogImages = _kennelRepo.GetImagesByDogLink(removeDog.DogLink);
                if (dogImages != null)
                {
                    foreach (var dogImage in dogImages)
                    {
                        /// usuwanie z tabeli DogImages
                        _kennelRepo.DeleteImage(dogImage);
                        _kennelRepo.SaveChanges();
                        countOfRemoveDogImages++;

                        /// usuwanie pliku ze zdjęciem
                        var filePath = Path.Combine(HostingEnvironment.MapPath("~/Dogs/" + removeDog.BreedLink + "/" + removeDog.DogLink + "/" + dogImage.ImageFileName));
                        if (System.IO.File.Exists(filePath) == true)
                        {
                            System.IO.File.Delete(filePath);
                        }
                    }
                }

                /// usuwanie psa z bazy
                _kennelRepo.RemoveDog(removeDog);
                _kennelRepo.SaveChanges();

                /// usuwanie katalogu psa
                var dirPath = Path.Combine(HostingEnvironment.MapPath("~/Dogs/" + removeDog.BreedLink + "/" + removeDog.DogLink));
                if (System.IO.Directory.Exists(dirPath) == true)
                {
                    System.IO.Directory.Delete(dirPath);
                }
            }

            return RedirectToAction("Index", "Kennel").WithSuccess(this, "Sukces",
                "<br>Zwierzę <b>" + removeDog.DogName + "</b> zostało usunięte z systemu.<br><br>"
                + "<b>Usunięto</b><br>"
                + "Miotów: <b>" + countOfRemoveLitters.ToString() + "</b><br>"
                + "Szczeniąt łącznie: <b>" + countOfRemovePuppies.ToString() + "</b><br>"
                + "Zdjęć szczeniąt: <b>" + countOfRemovePuppyPictures.ToString() + "</b><br>"
                + "Zdjęć usuwanego zwierzęcia: <b>" + countOfRemoveDogImages.ToString() + "</b><br>"
                );
        }

        public void RemoveLitter(int id)
        {

        }

        public void RemovePuppy(int id)
        {

        }

        public ActionResult RemovePost([DefaultValue(0)] int id)
        {
            /*
             * 1. Usuwanie zdjęć postu
             * 2. Usuwanie wpisów zdjęć w bazie
             * 3. Usuwanie wpisu z bazy postów
             */

            if (id != 0)
            {
                var postToRemove = _postRepo.GetPostById(id);
                if (postToRemove != null)
                {
                    var imagesBypost = _postRepo.GetImagesByPost(postToRemove.PostLink);
                    if (imagesBypost != null)
                    {
                        foreach (var imgToRemove in imagesBypost)
                        {
                            var filePath = Path.Combine(HostingEnvironment.MapPath("~/Posts/" + imgToRemove.PostLink + "/" + imgToRemove.ImageFileName));
                            if (System.IO.File.Exists(filePath) == true)
                            {
                                System.IO.File.Delete(filePath);
                            }

                            _postRepo.DeleteImage(imgToRemove);
                            _postRepo.SaveChanges();
                        }

                        /// usuwanie katalogu posta
                        var postDirPath = Path.Combine(HostingEnvironment.MapPath("~/Posts/" + postToRemove.PostLink));
                        if (System.IO.Directory.Exists(postDirPath))
                        {
                            System.IO.Directory.Delete(postDirPath);
                        }

                        _postRepo.DeletePost(postToRemove);
                        _postRepo.SaveChanges();

                        return RedirectToAction("Posts", "Admin").WithSuccess(this, "Sukces", "<br>Post został usunięty!");

                    }
                    else
                    {
                        return RedirectToAction("Posts", "Admin").WithWarning(this, "Błąd", "<br>Wpis nie został usunięty!<br>Nie posiada przypisanych zdjęć. (79845664XBV)");
                    }
                }
                else
                {
                    return RedirectToAction("Posts", "Admin").WithError(this, "Błąd", "<br>Wpis nie został usunięty!<br>Nie znaleziono wpisu o podanym ID. (77384XBV)");
                }
            }
            else
            {
                return RedirectToAction("Posts", "Admin").WithWarning(this, "Błąd", "<brNie podano parametru ID! (23664XC)");
            }                       
        }
    }
}