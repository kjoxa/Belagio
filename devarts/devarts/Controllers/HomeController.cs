using PagedList;
using devarts.Helpers;
using devarts.Models;
using devarts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;
using WebMatrix.WebData;
using NLog;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using A = DocumentFormat.OpenXml.Drawing;
using DW = DocumentFormat.OpenXml.Drawing.Wordprocessing;
using PIC = DocumentFormat.OpenXml.Drawing.Pictures;
using System.IO;
using OpenXmlPowerTools;
using System.Globalization;

/// POLO
/// https://themeforest.net/item/polo-responsive-multipurpose-html5-template/13708923

/// SPANISH WATER DOG - SWD
/// Hiszpański Pies Dowodny
/// https://fonts.google.com/specimen/Great+Vibes?preview.text=Belagio&preview.text_type=custom
/// https://fonts.google.com/specimen/Tangerine?preview.text=Belagio&preview.text_type=custom

namespace devarts.Controllers
{
    //[LayoutInjecter("_Layout")]    
    public class HomeController : LanguageController
    {
        private PostRepository _postRepo;
        private AdminRepository _adminRepo;
        private KennelRepository _kennelRepo;
        private AssistantRepository _assistRepo;
        private static Logger nLog = LogManager.GetCurrentClassLogger();

        public HomeController()
        {
            _postRepo = new PostRepository();
            _adminRepo = new AdminRepository();
            _kennelRepo = new KennelRepository();
            _assistRepo = new AssistantRepository();
        }

        public List<SelectListItem> PostCathegories()
        {
            List<SelectListItem> category = new List<SelectListItem>();
            category.Add(new SelectListItem { Text = "Wszystkie kategorie", Value = "All" });
            category.Add(new SelectListItem { Text = "Szczenięta", Value = "Szczenięta" });
            category.Add(new SelectListItem { Text = "Wystawy", Value = "Wystawy" });
            category.Add(new SelectListItem { Text = "Hodowla", Value = "Hodowla" });
            category.Add(new SelectListItem { Text = "Wydarzenia", Value = "Wydarzenia" });
            category.Add(new SelectListItem { Text = "Inne", Value = "Inne" });

            return category;
        }

        #region PRZYKŁADOWA OBSŁUGA DOKUMENTÓW

        string path = @"D:\Data\Umowa.docx";
        string path2 = @"D:\Data\Umowa2.docx";
        string path3 = @"D:\Data\Umowa3.docx";
        string searchKeyWord = "Java";

        // do obsługi tej metody wymagany jest DocumentFormat.OpenXml -Version 2.14.0
        // a do SearchAndReplace OpenXmlPowerTools -Version 4.5.3.2, który zawiera starszą wersję DocumentFormat.OpenXml -Version 2.7.2
        private bool SearchWordIsMatched(string path, string searchKeyWord)
        {
            try
            {
                using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(path, true))
                {
                    var text = wordDoc.MainDocumentPart.Document.InnerText;
                    if (text.Contains(searchKeyWord))
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected byte[] SearchAndReplace(byte[] file, IDictionary<string, string> translations)
        {
            WmlDocument doc = new WmlDocument(file.Length.ToString(), file);

            foreach (var translation in translations)
                doc = doc.SearchAndReplace(translation.Key, translation.Value, true);

            return doc.DocumentByteArray;
        }

        public void ReplateText(string sourceText, string destinationText)
        {
            var templateDoc = System.IO.File.ReadAllBytes(path);
            var generatedDoc = SearchAndReplace(templateDoc, new Dictionary<string, string>(){
            {"{Imie}", "Marcin"},
            {"{Nazwisko}", "Gadziejewski"},
            {"{PESEL}", "88062708450"},
            {"{Ubezpieczyciel}", "PZU"},
            {"Java", "C#"},
            {"{Adres}", "Trójmiasto!"},
            });
            System.IO.File.WriteAllBytes(path2, generatedDoc);
        }

        /// =====================================================================================================================

        private static void AddImageToCell(DocumentFormat.OpenXml.Wordprocessing.TableCell cell, string relationshipId)
        {
            var element =
              new DocumentFormat.OpenXml.Wordprocessing.Drawing(
                new DW.Inline(
                  new DW.Extent() { Cx = 990000L, Cy = 792000L },
                  new DW.EffectExtent()
                  {
                      LeftEdge = 0L,
                      TopEdge = 0L,
                      RightEdge = 0L,
                      BottomEdge = 0L
                  },
                  new DW.DocProperties()
                  {
                      Id = (UInt32Value)1U,
                      Name = "Picture 1"
                  },
                  new DW.NonVisualGraphicFrameDrawingProperties(
                      new A.GraphicFrameLocks() { NoChangeAspect = true }),
                  new A.Graphic(
                    new A.GraphicData(
                      new PIC.Picture(
                        new PIC.NonVisualPictureProperties(
                          new PIC.NonVisualDrawingProperties()
                          {
                              Id = (UInt32Value)0U,
                              Name = "New Bitmap Image.jpg"
                          },
                          new PIC.NonVisualPictureDrawingProperties()),
                        new PIC.BlipFill(
                          new A.Blip(
                            new A.BlipExtensionList(
                              new A.BlipExtension()
                              {
                                  Uri = "{28A0092B-C50C-407E-A947-70E740481C1C}"
                              })
                           )
                          {
                              Embed = relationshipId,
                              CompressionState =
                              A.BlipCompressionValues.Print
                          },
                          new A.Stretch(
                            new A.FillRectangle())),
                          new PIC.ShapeProperties(
                            new A.Transform2D(
                              new A.Offset() { X = 0L, Y = 0L },
                              new A.Extents() { Cx = 990000L, Cy = 792000L }),
                            new A.PresetGeometry(
                              new A.AdjustValueList()
                            )
                            { Preset = A.ShapeTypeValues.Rectangle }))
                    )
                    { Uri = "http://schemas.openxmlformats.org/drawingml/2006/picture" })
                )
                {
                    DistanceFromTop = (UInt32Value)0U,
                    DistanceFromBottom = (UInt32Value)0U,
                    DistanceFromLeft = (UInt32Value)0U,
                    DistanceFromRight = (UInt32Value)0U
                });

            cell.Append(new Paragraph(new Run(element)));
        }

        private string AddGraph(WordprocessingDocument wpd, string filepath)
        {
            ImagePart ip = wpd.MainDocumentPart.AddImagePart(ImagePartType.Jpeg);
            using (FileStream fs = new FileStream(filepath, FileMode.Open))
            {
                if (fs.Length == 0) return string.Empty;
                ip.FeedData(fs);
            }

            return wpd.MainDocumentPart.GetIdOfPart(ip);
        }

        public void AddImage()
        {
            string file = @"D:\Data\Umowa3.docx";
            string imageFile = @"D:\Data\myimage.jpeg";
            string labelText = "PersonMainPhoto";

            using (var document = WordprocessingDocument.Open(file, isEditable: true))
            {
                var mainPart = document.MainDocumentPart;
                var table = mainPart.Document.Body.Descendants<DocumentFormat.OpenXml.Wordprocessing.Table>().First();

                var pictureCell = table.Descendants<DocumentFormat.OpenXml.Wordprocessing.TableCell>().First(c => c.InnerText == labelText);

                ImagePart imagePart = mainPart.AddImagePart(ImagePartType.Jpeg);

                using (FileStream stream = new FileStream(imageFile, FileMode.Open))
                {
                    imagePart.FeedData(stream);
                }

                pictureCell.RemoveAllChildren();
                AddImageToCell(pictureCell, mainPart.GetIdOfPart(imagePart));
                mainPart.Document.Save();
                //document.Close();
            }
        }

        public ActionResult Sign()
        {
            AddImage();
            ReplateText("", "");
            return View();
        }

        #endregion        

        //https://www.freetimelearning.com/bootstrap-snippets/bootstrap-3-main-plugins.php?Bootstrap-Family-Tree-Vertical-Family-Tree----Free-Time-Learning&id=58
        public ActionResult Tree() => View();

        public ActionResult ItalianGreyhoundWorld()
        {
            var dogsList = _kennelRepo.GetAllDogs();
            var littersList = _kennelRepo.GetAllLitters();

            string info = "";
            string borndate = "";
            string dateBuilder = "";
            int years = 0;

            DateTime daysAndMonthNow = DateTime.Now;
            DateTime daysAndMonthPlusYear = DateTime.Now;

            List<Birthdays> birthdays = new List<Birthdays>();

            // urodziny psów
            foreach (var dog in dogsList)
            {
                try
                {
                    dateBuilder = DateTime.Now.Day.ToString("00") + "." + DateTime.Now.Month.ToString("00") + "." + (DateTime.Now.Year).ToString();
                    daysAndMonthNow = DateTime.ParseExact(dateBuilder, "dd.MM.yyyy", CultureInfo.CurrentCulture);

                    dateBuilder = dog.BornDateDateTime.Day.ToString("00") + "." + dog.BornDateDateTime.Month.ToString("00") + "." + (DateTime.Now.Year).ToString();
                    daysAndMonthPlusYear = DateTime.ParseExact(dateBuilder, "dd.MM.yyyy", CultureInfo.CurrentCulture);
                    years = daysAndMonthPlusYear.Year - dog.BornDateDateTime.Year;                    

                    if (daysAndMonthPlusYear > daysAndMonthNow)
                    {
                        if ((daysAndMonthPlusYear - daysAndMonthNow).Days == 1)
                        {
                            birthdays.Add(new Birthdays
                            {
                                dog = dog,
                                Month = daysAndMonthPlusYear.Month,
                                BornDateInThisYear = daysAndMonthPlusYear.ToString("dd.MM.yyyy"),
                                IsPast = false,
                                RemainingDays = "<b>" + (daysAndMonthPlusYear - daysAndMonthNow).Days + "</b><small> dni</small>"
                            });
                        }
                        else
                        {
                            birthdays.Add(new Birthdays
                            {
                                dog = dog,
                                Month = daysAndMonthPlusYear.Month,
                                BornDateInThisYear = daysAndMonthPlusYear.ToString("dd.MM.yyyy"),
                                IsPast = false,
                                RemainingDays = "<b>" + (daysAndMonthPlusYear - daysAndMonthNow).Days + "</b><small> dni</small>"
                            });
                        }
                    }
                    // wydarzenie odbyło się
                    else
                    {
                        birthdays.Add(new Birthdays
                        {
                            dog = dog,
                            Month = daysAndMonthPlusYear.Month,
                            BornDateInThisYear = daysAndMonthPlusYear.ToString("dd.MM.yyyy"),
                            IsPast = true,
                            RemainingDays = daysAndMonthPlusYear.ToString("dd.MM.yyyy")
                        });
                    }                    
                }
                catch (Exception ex)
                {
                    nLog.Error(ex.ToString());
                }                       
            }

            // urodziny miotów
            foreach (var litter in littersList)
            {
                try
                {
                    dateBuilder = DateTime.Now.Day.ToString("00") + "." + DateTime.Now.Month.ToString("00") + "." + (DateTime.Now.Year).ToString();
                    daysAndMonthNow = DateTime.ParseExact(dateBuilder, "dd.MM.yyyy", CultureInfo.CurrentCulture);

                    dateBuilder = litter.BornDateOkay.Day.ToString("00") + "." + litter.BornDateOkay.Month.ToString("00") + "." + (DateTime.Now.Year).ToString();
                    daysAndMonthPlusYear = DateTime.ParseExact(dateBuilder, "dd.MM.yyyy", CultureInfo.CurrentCulture);
                    years = daysAndMonthPlusYear.Year - litter.BornDateOkay.Year;

                    if (daysAndMonthPlusYear > daysAndMonthNow)
                    {
                        if ((daysAndMonthPlusYear - daysAndMonthNow).Days == 1)
                        {
                            birthdays.Add(new Birthdays
                            {
                                litter = litter,
                                Month = daysAndMonthPlusYear.Month,
                                BornDateInThisYear = daysAndMonthPlusYear.ToString("dd.MM.yyyy"),
                                IsPast = false,
                                RemainingDays = "<b>" + (daysAndMonthPlusYear - daysAndMonthNow).Days + "</b><small> dni</small>"
                            });
                        }
                        else
                        {
                            birthdays.Add(new Birthdays
                            {
                                litter = litter,
                                Month = daysAndMonthPlusYear.Month,
                                BornDateInThisYear = daysAndMonthPlusYear.ToString("dd.MM.yyyy"),
                                IsPast = false,
                                RemainingDays = "<b>" + (daysAndMonthPlusYear - daysAndMonthNow).Days + "</b><small> dni</small>"
                            });
                        }
                    }
                    else
                    {
                        birthdays.Add(new Birthdays
                        {
                            litter = litter,
                            Month = daysAndMonthPlusYear.Month,
                            BornDateInThisYear = daysAndMonthPlusYear.ToString("dd.MM.yyyy"),
                            IsPast = true,
                            RemainingDays = "Odbyły się " + daysAndMonthPlusYear.ToString("dd.MM.yyyy")
                        });
                    }
                }
                catch (Exception ex)
                {
                    nLog.Error(ex.ToString());
                }
            }         

            // służy do wykrywania, który miesiąc ma być rozwinięty w Accordionie (Collapse in)
            int thisMonth = DateTime.Now.Month;
            ViewBag.January = thisMonth == 1 ? "in" : "";
            ViewBag.February = thisMonth == 2 ? "in" : "";
            ViewBag.March = thisMonth == 3 ? "in" : "";
            ViewBag.April = thisMonth == 4 ? "in" : "";
            ViewBag.May = thisMonth == 5 ? "in" : "";
            ViewBag.June = thisMonth == 6 ? "in" : "";
            ViewBag.July = thisMonth == 7 ? "in" : "";
            ViewBag.August = thisMonth == 8 ? "in" : "";
            ViewBag.September = thisMonth == 9 ? "in" : "";
            ViewBag.October = thisMonth == 10 ? "in" : "";
            ViewBag.November = thisMonth == 11 ? "in" : "";
            ViewBag.December = thisMonth == 12 ? "in" : "";

            return View(birthdays.OrderBy(b => b.RemainingDays.Length).ThenBy(b => b.RemainingDays));
        }

        public ActionResult Index(int? page, Search model)
        {
            //var token = WebSecurity.GeneratePasswordResetToken("Administrator");
            //WebSecurity.ResetPassword(token, "Ladyvet2019!");            

            int pageSize = 6;
            int pageNumber = (page ?? 1);

            var postsList = _postRepo.GetAllPosts().Where(p => p.IsPublished == true && p.Type != "Blog");
            ViewBag.Cathegory = PostCathegories();

            if (!string.IsNullOrEmpty(model.cathegory))
            {
                if (model.cathegory == "All")
                {
                    postsList = _postRepo.GetAllPosts().Where(p => p.IsPublished == true);
                }
                else
                {
                    postsList = postsList.Where(p => p.CategoryName.Contains(model.cathegory));
                }                
            }

            if (!string.IsNullOrEmpty(model.content))
            {
                postsList = postsList.Where(p => p.PostContent.Contains(model.content) || p.PostName.Contains(model.content) || p.PostNameEng.Contains(model.content));
            }

            List<PostWithMainImageUrl> postWithImgUrl = new List<PostWithMainImageUrl>();
            foreach (var item in postsList)
            {
                PostWithMainImageUrl addItem = new PostWithMainImageUrl();
                addItem.Id = item.Id;
                addItem.ImageId = item.ImageId;
                addItem.AuthorName = item.AuthorName;
                addItem.PostName = item.PostName;
                addItem.PostNameEng = item.PostNameEng;
                addItem.PostContentShort = item.PostContentShort;
                addItem.PostContentShortEng = item.PostContentShortEng;
                addItem.CategoryName = item.CategoryName;
                addItem.Type = item.Type;
                addItem.PostContent = item.PostContent;
                addItem.PostLink = item.PostLink;
                addItem.Tags = item.Tags;
                addItem.PostDate = item.PostDate;
                addItem.PostDateOkay = item.PostDateOkay;
                addItem.PostRate = item.PostRate;
                addItem.PostShow = item.PostShow;
                addItem.IsPublished = item.IsPublished;
                try
                {
                    addItem.mainImage = _postRepo.GetPostImageById(item.ImageId).ImageFileName;
                }
                catch
                {
                    addItem.mainImage = "empty.jpg";
                }                
                postWithImgUrl.Add(addItem);
            }

            postWithImgUrl = postWithImgUrl.OrderByDescending(p => p.PostDateOkay).ToList();

            SearchWithNews SWN = new SearchWithNews
            {
                Post = postWithImgUrl.ToPagedList(pageNumber, pageSize),
                Search = model
            };
            //return RedirectToAction("Login", "Account");
            return View(SWN);
        }

        public ActionResult Blog(int? page, Search model)
        {
            //var token = WebSecurity.GeneratePasswordResetToken("Administrator");
            //WebSecurity.ResetPassword(token, "Ladyvet2019!");            

            int pageSize = 6;
            int pageNumber = (page ?? 1);

            var postsList = _postRepo.GetAllPosts().Where(p => p.IsPublished == true && p.Type == "Blog");
            ViewBag.Cathegory = PostCathegories();

            if (!string.IsNullOrEmpty(model.content))
            {
                postsList = postsList.Where(p => p.PostContent.Contains(model.content) || p.PostName.Contains(model.content) || p.PostNameEng.Contains(model.content));
            }

            List<PostWithMainImageUrl> postWithImgUrl = new List<PostWithMainImageUrl>();
            foreach (var item in postsList)
            {

                PostWithMainImageUrl addItem = new PostWithMainImageUrl();
                addItem.Id = item.Id;
                addItem.ImageId = item.ImageId;
                addItem.AuthorName = item.AuthorName;
                addItem.PostName = item.PostName;
                addItem.PostNameEng = item.PostNameEng;
                addItem.PostContentShort = item.PostContentShort;
                addItem.PostContentShortEng = item.PostContentShortEng;
                addItem.CategoryName = item.CategoryName;
                addItem.Type = item.Type;
                addItem.PostContent = item.PostContent;
                addItem.PostLink = item.PostLink;
                addItem.Tags = item.Tags;
                addItem.PostDate = item.PostDate;
                addItem.PostDateOkay = item.PostDateOkay;
                addItem.PostRate = item.PostRate;
                addItem.PostShow = item.PostShow;
                addItem.IsPublished = item.IsPublished;

                try
                {
                    addItem.mainImage = _postRepo.GetPostImageById(item.ImageId).ImageFileName;
                }
                catch
                {
                    addItem.mainImage = "empty.jpg";
                }
                postWithImgUrl.Add(addItem);
            }

            postWithImgUrl = postWithImgUrl.OrderByDescending(p => p.PostDateOkay).ToList();

            SearchWithNews SWN = new SearchWithNews
            {
                Post = postWithImgUrl.ToPagedList(pageNumber, pageSize),
                Search = model
            };
            //return RedirectToAction("Login", "Account");
            return View(SWN);
        }

        public ActionResult Post(string postLink)
        {
            var postView = _postRepo.GetPostByPostLink(postLink);
            if (postView != null)
            {
                postView.PostShow++;
                _postRepo.SaveChanges();

                return View(postView);
            }
            else
            {
                return View("Index").WithWarning(this, "Błąd", "<br>Nie znaleziono wpisu o podanym adresie");
            }            
        }

        public ActionResult BlogPost(string postLink)
        {
            var postView = _postRepo.GetPostByPostLink(postLink);
            if (postView != null)
            {
                postView.PostShow++;
                _postRepo.SaveChanges();

                return View(postView);
            }
            else
            {
                return View("Index").WithWarning(this, "Błąd", "<br>Nie znaleziono wpisu o podanym adresie");
            }
        }

        [ValidateInput(false)]
        public ActionResult Dog([DefaultValue("")] string dogLink)
        {
            var dog = _kennelRepo.GetDogByDogLink(dogLink);
            if (dog != null)
            {
                var imagesForDog = _kennelRepo.GetImagesByDogLink(dogLink);
                var littersForDog = _kennelRepo.GetLittersByDogLink(dogLink);
                var tree = _kennelRepo.GetDogTreeByDogLink(dogLink);
                if (imagesForDog != null)
                {
                    var dogAndImages = new DogWithImages
                    {
                        dog = dog,
                        images = imagesForDog.AsQueryable(),
                        litters = littersForDog,
                        tree = tree
                    };
                    return View(dogAndImages);
                }

                return View(dog);
            }
            else
            {
                return RedirectToAction("Index").WithWarning(this, "Błąd", "<br>Nie znaleziono zwierzęcia o podanym Id Linku.");
            }
        }

        public ActionResult ItalianGreyhoundLitters()
        {
            var litters = _kennelRepo.GetAllLitters();

            return View(litters);
        }

        public ActionResult BorzoiLitters()
        {
            var litters = _kennelRepo.GetAllLitters();

            return View(litters);
        }

        public ActionResult Litters()
        {
            var litters = _kennelRepo.GetAllLitters();

            return View(litters);
        }

        public ActionResult Litter(string litterLink)
        {
            if (litterLink != null)
            {
                var litter = _kennelRepo.GetLitterByLitterLink(litterLink);
                if (litter != null)
                {
                    var dog = _kennelRepo.GetDogByDogLink(litter.DogLink);
                    var litterImg = _kennelRepo.GetLitterImagesByLitterLink(litterLink);
                    var puppies = _kennelRepo.GetPuppiesByLitterLink(litterLink);
                    var tree = _kennelRepo.GetLitterTreeByLitterLink(litterLink);
                    var modelView = new DogAndLitterAndImagesAndPuppies
                    {
                        dog = dog,
                        Litter = litter,
                        LitterImages = litterImg,
                        Puppies = puppies,
                        tree = tree
                    };
                    return View(modelView);
                }
                else
                {
                    return RedirectToAction("Index", "Home").WithError(this, "Błąd", "<br>Nie znaleziono miotu o podanej nazwie. L(01Err)");
                }
            }
            return View();
        }

        public ActionResult Puppy(string puppyLink)
        {
            //try
            //{
            //    if (puppyLink != null)
            //    {
            //        try
            //        {
            //            var previewPuppy = _kennelRepo.GetPuppyByPuppyLink(puppyLink);
            //            var imagesForPuppy = _kennelRepo.GetImagesByPuppyLink(puppyLink);
            //            var litter = _kennelRepo.GetLitterByLitterLink(previewPuppy.LitterLink);
            //            var dog = _kennelRepo.GetDogByDogLink(litter.DogLink);

            //            if (previewPuppy != null)
            //            {
            //                var puppyAndImages = new PuppyAndImages
            //                {
            //                    puppy = previewPuppy,
            //                    puppyImages = imagesForPuppy,
            //                    litter = litter,
            //                    dog = dog
            //                };

            //                return View(puppyAndImages);
            //            }
            //            else
            //            {
            //                return RedirectToAction("Index", "Home").WithError(this, "Błąd", "<br>Nie znaleziono szczenięcia o podanym ID. (0PErr)");
            //            }

            //        }
            //        catch
            //        {
            //            return RedirectToAction("Index", "Home").WithError(this, "Błąd", "<br>Nie znaleziono szczenięcia o podanym ID. (1PErr)");
            //        }                                      
            //    }
            //    else
            //    {
            //        return RedirectToAction("Index", "Home").WithError(this, "Błąd", "<br>Nie znaleziono szczenięcia o podanym ID. (2PErr)");
            //    }
            //}
            //catch
            //{
            //    return RedirectToAction("Index", "Home").WithError(this, "Błąd", "<br>Nie znaleziono szczenięcia o podanym ID. (3PErr)");
            //}
            return View();
        }

        public ActionResult Puppies()
        {
            return View();
        }

        public ActionResult AllPuppies(string breedLink)
        {
            ViewBag.BreedLink = breedLink;
            if (breedLink != null)
            {
                ViewBag.BreedLink = breedLink;
                var litters = _kennelRepo.GetAllLitters().Where(l => l.BreedLink == breedLink);
                return View(litters);
            }
            return RedirectToAction("Index", "Home").WithError(this, "Błąd", "Nie znaleziono miotów dla podanej rasy.");
        }
        
        public ActionResult InMemories()
        {
            return View();
        }

        public ActionResult UploadGallery(int? page)
        {

            //int pageSize = 12;
            //int pageNumber = (page ?? 1);

            //ViewBag.Count = _adminRepo.GetCountRecords();

            //List<GalleryModels> joinedGallery = new List<GalleryModels>();
            ////GalleryModels oneItem = new GalleryModels();

            //// 1. Odczytanie zdjęć z Newsów 
            //var allGallery = _adminRepo.GetAllImages();

            //// 2. Odczytanie zdjęć z Miotów
            //var dogsByLittersImg = _kennelRepo.GetDogsByLittersList();

            //// 3. Przemodelowanie Newsów na Galerię
            //foreach (var item in allGallery)
            //{
            //    joinedGallery.Add(new GalleryModels()
            //    {
            //        ImgUrl = "/UploadImages/" + item.EntryLink + "/" + item.ImageFileName,
            //        ImgHref = "/Home/News" + item.EntryLink,
            //        Title = item.EntryLink,
            //        Id = item.Id,
            //    });
            //}

            //// 3. Przemodelowanie Miotów na Galerię
            //foreach (var item in dogsByLittersImg)
            //{
            //    joinedGallery.Add(new GalleryModels()
            //    {
            //        ImgUrl = "/UploadLitters/" + item.LitterName + "/" + item.DogName + "/" + item.ImgFileName,
            //        ImgHref = "/Dog/" + item.LitterName + "/" + item.DogName,
            //        Title = item.DogPresentationName,
            //        Id = item.Id
            //    });
            //}

            //return View(joinedGallery.ToPagedList(pageNumber, pageSize));
            return View();
        }

        public ActionResult Movies()
        {
            return View();
        }

        public ActionResult AboutUs()
        {
            return View();
        }

        public ActionResult AboutKennel()
        {
            return View();
        }

        public ActionResult RenderNewsletter()
        {
            return PartialView("_subscriberSaveForm");
        }

        
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult SubmitNewsletter(Subscriber model)
        {
            if (ModelState.IsValid)
            {
                var subscribers = _adminRepo.GetAllSubscribers();

                if (subscribers.Where(s => s.Email.ToLower().Contains(model.Email.ToLower())).Count() > 0)
                {
                    return PartialView("_subscriberExist", model);
                }

                try
                {
                    Subscriber newSubscriber = new Subscriber();
                    newSubscriber.Email = model.Email.ToLower();
                    newSubscriber.CreateDate = DateTime.Now.ToString();
                    newSubscriber.IsActive = true;

                    _adminRepo.AddSubscriber(newSubscriber);
                    _adminRepo.SaveChanges();
                }
                catch
                {
                    return PartialView("_subscriberSaveError");
                }

                return PartialView("_subscriberSaveSuccess", model);
            }
            return PartialView("_subscriberSaveError");
        }

        public JsonResult CheckPassword(string password)
        {
            if (password == "rezerwacja")
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult CreateReservation()
        {
            TempData["Eyfruntopas"] = DateTime.Now;

            ReservationController rec = new ReservationController();
            ViewBag.DropdownBreed = rec.Breed();
            ViewBag.Taillen = rec.Taillen();
            ViewBag.DropdownSex = rec.Sex();
            ViewBag.DropdownColour = rec.Colour();
            ViewBag.PrefferedEnergyLevel = rec.PrefferedEnergyLevel();
            ViewBag.PrefferedSize = rec.PrefferedSize();
            ViewBag.ReadyForDog = rec.ReadyForDog();
            ViewBag.Priority = rec.Priority();
            ViewBag.ForBreeding = rec.ForBreeding();
            return View();
        }

        [HttpPost]
        public ActionResult CreateReservation(Reservation model)
        {
            try
            {
                double secondsToFill = 0;

                if (TempData["Eyfruntopas"] != null)
                {
                    DateTime formStartTime = (DateTime)TempData["Eyfruntopas"];
                    DateTime formEndTime = DateTime.Now;

                    // Oblicz różnicę czasu w sekundach
                    TimeSpan timeToFill = formEndTime - formStartTime;
                    secondsToFill = timeToFill.TotalSeconds;
                }

                ReservationController rec = new ReservationController();
                ViewBag.DropdownBreed = rec.Breed();
                ViewBag.Taillen = rec.Taillen();
                ViewBag.DropdownSex = rec.Sex();
                ViewBag.DropdownColour = rec.Colour();
                ViewBag.PrefferedEnergyLevel = rec.PrefferedEnergyLevel();
                ViewBag.PrefferedSize = rec.PrefferedSize();
                ViewBag.ReadyForDog = rec.ReadyForDog();
                ViewBag.Priority = rec.Priority();
                ViewBag.ForBreeding = rec.ForBreeding();

                if (ModelState.IsValid && secondsToFill >= 30)
                {
                    Reservation res = new Reservation();
                    res.FirstName = model.FirstName;
                    res.SurName = model.SurName;
                    res.Country = model.Country;
                    res.City = model.City;
                    res.Email = model.Email;
                    res.PhoneNumber = model.PhoneNumber;
                    res.Breed = model.Breed;
                    res.Sex = model.Sex;
                    res.Colour = model.Colour;
                    res.DogSize = model.DogSize;
                    res.EnergyLevel = model.EnergyLevel;
                    res.ReadyToDog = model.ReadyToDog;
                    //res.AdditionalResidents = model.AdditionalResidents;
                    res.Alergies = model.Alergies;
                    res.PreferredFather = model.PreferredFather;
                    res.PreferredMother = model.PreferredMother;
                    res.ClientComments = model.ClientComments;

                    // Additional Residents jest wykorzystane do zadecydowania czy do hodowli czy nie
                    if(model.AdditionalResidents == "0")
                    {
                        res.DogForKennel = false;
                    }
                    else
                    {
                        res.DogForKennel = true;
                    }

                    res.DogForSport = model.DogForSport;

                    res.CreateDate = DateTime.Now;
                    res.ModifiedDate = DateTime.Now;

                    _assistRepo.AddReservation(res);
                    _assistRepo.SaveChanges();

                    // Wykorzystanie helpera do wysyłania e-maili
                    string subject = "Potwierdzenie otrzymania rezerwacji";
                    string contentForMe;
                    contentForMe = "<h3 style='color: darkcyan; margin:0'><b>Nowa rezerwacja!</b></h3>" +
                              "Została dodana nowa rezerwacja od <b>" + res.FirstName + " " + res.SurName + "</b>!<br><br>" +
                              "<b><h4 style='margin:0'><b>Zapisana rezerwacja: </b></h5></b>" +
                              "<hr style='max-width: 100px; margin-left:0'>" +
                              "<b>Imię: </b>" + res.FirstName + "<br>" +
                              "<b>Nazwisko: </b>" + res.SurName + "<br>" +
                              "<b>Kraj: </b>" + res.Country + "<br>" +
                              "<b>Miasto: </b>" + res.City + "<br>" +
                              "<b>E-mail: </b>" + res.Email + "<br>" +
                              "<b>Numer telefonu: </b>" + res.PhoneNumber +
                              "<hr style='max-width: 100px; margin-left:0'>" +
                              //"<b>Rasa (breed): </b>" + res.Breed + "<br>" +
                              "<b>Płeć (sex): </b>" + res.Sex + "<br>" +
                              "<b>Kolor (colour): </b>" + res.Colour + "<br>" +
                              //"<b>Rozmiar (size): </b>" + res.DogSize + "<br>" +
                              //"<b>Poziom enegrii (energy level ): </b>" + res.EnergyLevel + "<br>" +
                              "<b>Gotowość na zwierzę (ready for the dog): </b>" + res.ReadyToDog + "<br>" +
                              //"<b>Dodatkowi mieszkańcy (additional residents): </b>" + res.AdditionalResidents + "<br>" +
                              //"<b>Alergie (allergies): </b>" + res.Alergies + "<br>" +
                              "<b>Preferowana matka (preffered mother): </b>" + res.PreferredMother + "<br>" +
                              "<b>Długość ogona (preffered tail size): </b>" + res.TailLength + "<br>" +
                              //"<b>Preferowany ojciec (preffered father): </b>" + res.PreferredFather + "<br>" +
                              "<b>Uwagi (notes):</b>" + res.ClientComments + "<br>" +
                              "<b>Do hodowli (for kennel): </b>" + ((res.DogForKennel == true) ? "Tak" : "Nie") + "<br>" +
                              "<b>Do sportu (for sport): </b>" + ((res.DogForSport == true) ? "Tak" : "Nie") + "<br>" +
                              "<hr style='max-width: 100px; margin-left:0'>" +
                              "Zajrzyj do panelu zarządzania, aby zarządzać tą i innymi rezerwacjami!" +
                              "<h3 style='color: darkcyan; margin:0'>PureBreed FCI</h3><br>";

                    string content;
                    content = "<h3 style='color: darkcyan; margin:0'><b>Szanowni Państwo!</b></h3>" +
                              "Dokonana przez Państwa rezerwacja została zapisana!<br><br>" +
                              "<b><h5 style='margin:0'><b>Zapisana rezerwacja: </b></h5></b>" +
                              "<hr style='max-width: 100px; margin-left:0'>" +
                              //"<b>Rasa (breed): </b>" + res.Breed + "<br>" +
                              "<b>Płeć (sex): </b>" + res.Sex + "<br>" +
                              "<b>Kolor (colour): </b>" + res.Colour + "<br>" +
                              //"<b>Rozmiar (size): </b>" + res.DogSize + "<br>" +
                              //"<b>Poziom enegrii (energy level ): </b>" + res.EnergyLevel + "<br>" +
                              "<b>Gotowość na zwierzę (ready for the dog): </b>" + res.ReadyToDog + "<br>" +
                              //"<b>Dodatkowi mieszkańcy (additional residents): </b>" + res.AdditionalResidents + "<br>" +
                              //"<b>Alergie (allergies): </b>" + res.Alergies + "<br>" +
                              "<b>Preferowana matka (preffered mother): </b>" + res.PreferredMother + "<br>" +
                              //"<b>Preferowany ojciec (preffered father): </b>" + res.PreferredFather + "<br>" +
                              "<b>Długość ogona (preffered tail size): </b>" + res.TailLength + "<br>" +
                              "<b>Uwagi (notes): </b>" + res.ClientComments + "<br>" +
                              "<b>Do hodowli (for kennel): </b>" + ((res.DogForKennel == true) ? "Tak" : "Nie") + "<br>" +
                              "<b>Do sportu (for sport): </b>" + ((res.DogForSport == true) ? "Tak" : "Nie") + "<br><br>" +
                              "Skontaktujemy się z Państwem, gdy dostępne będzie szczenię spełniające oczekiwania." +
                              "<hr style='max-width: 100px; margin-left:0'>" +
                              "<h3 style='color: darkcyan; margin:0'>Z wyrazami szacunku</h3>" +
                              "<b>Dominika Kania</b>";

                    MailHelper.SendEmailReservation(model.Email, subject, contentForMe);
                    MailHelper.SendToRecepientEmailReservation(model.Email, subject, content);

                    return View("ReservationSuccess");
                }
                else
                {
                    var message = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                    nLog.Error(message);
                    return View(model).WithError(this, "Błąd", "Proszę uzupełnić lub poprawić pola w formularzu!");
                }
            }
            catch (Exception ex)
            {
                nLog.Error(ex.ToString());
                return RedirectToAction("Index","Home");
            }            
        }

        public ActionResult ReservationSuccess()
        {
            //if (ModelState.IsValid)
            //{
            //    Reservation res = new Reservation();
            //    res.FirstName = "Marcin";
            //    res.SurName = "Gadziejewski";
            //    res.Country = "Polska";
            //    res.City = "Charzykowy";
            //    res.Email = "mgadziejewski@gmail.com";
            //    res.PhoneNumber = "504430997";
            //    res.Breed = "Charzik Włoski";
            //    res.Sex = "Pies";
            //    res.Colour = "Czarny";
            //    res.DogSize = "Średni";
            //    res.EnergyLevel = "Średni";
            //    res.ReadyToDog = "Od zaraz";
            //    res.AdditionalResidents = "Komary";
            //    res.Alergies = "Pszenica";
            //    res.PreferredFather = "Nieistotne";
            //    res.PreferredMother = "Nieistotne";
            //    res.ClientComments = "Fajnie jak by piesio miał super charakter!";
            //    res.DogForKennel = true;
            //    res.DogForSport = false;

            //    res.CreateDate = DateTime.Now;
            //    res.ModifiedDate = DateTime.Now;

            //    //_assistRepo.AddReservation(res);
            //    //_assistRepo.SaveChanges();

            //    // Wykorzystanie helpera do wysyłania e-maili
            //    string subject = "Potwierdzenie otrzymania rezerwacji";
            //    string contentForMe;
            //    contentForMe = "<h3 style='color: darkcyan; margin:0'><b>Nowa rezerwacja!</b></h3>" +
            //              "Została dodana nowa rezerwacja od <b>" + res.FirstName + " " + res.SurName + "</b>!<br><br>" +
            //              "<b><h4 style='margin:0'><b>Zapisana rezerwacja: </b></h5></b>" +
            //              "<hr style='max-width: 100px; margin-left:0'>" +
            //              "<b>Imię: </b>" + res.FirstName + "<br>" +
            //              "<b>Nazwisko: </b>" + res.SurName + "<br>" +
            //              "<b>Kraj: </b>" + res.Country + "<br>" +
            //              "<b>Miasto: </b>" + res.City + "<br>" +
            //              "<b>E-mail: </b>" + res.Email + "<br>" +
            //              "<b>Numer telefonu: </b>" + res.PhoneNumber + 
            //              "<hr style='max-width: 100px; margin-left:0'>" +
            //              "<b>Płeć (sex): </b>" + res.Sex + "<br>" +
            //              "<b>Kolor (colour): </b>" + res.Colour + "<br>" +
            //              "<b>Rozmiar (size): </b>" + res.DogSize + "<br>" +
            //              "<b>Poziom enegrii (energy level ): </b>" + res.EnergyLevel + "<br>" +
            //              "<b>Gotowość na zwierzę (ready for the dog): </b>" + res.ReadyToDog + "<br>" +
            //              "<b>Dodatkowi mieszkańcy (additional residents): </b>" + res.AdditionalResidents + "<br>" +
            //              "<b>Alergie (allergies): </b>" + res.Alergies + "<br>" +
            //              "<b>Preferowana matka (preffered mother): </b>" + res.PreferredMother + "<br>" +
            //              "<b>Preferowany ojciec (preffered father): </b>" + res.PreferredFather + "<br>" +
            //              "<b>Uwagi (notes):</b>" + res.ClientComments + "<br>" +
            //              "<b>Do hodowli (for kennel): </b>" + ((res.DogForKennel == true) ? "Tak" : "Nie") + "<br>" +
            //              "<b>Do sportu (for sport): </b>" + ((res.DogForSport == true) ? "Tak" : "Nie") + "<br>" +
            //              "<hr style='max-width: 100px; margin-left:0'>" +
            //              "Zajrzyj do panelu zarządzania, aby zarządzać tą i innymi rezerwacjami!" +
            //              "<h3 style='color: darkcyan; margin:0'>PureBreed FCI</h3><br>";

            //    string content;
            //    content = "<h3 style='color: darkcyan; margin:0'><b>Szanowni Państwo!</b></h3>" +
            //              "Dokonana przez Państwa rezerwacja została zapisana!<br><br>" +
            //              "<b><h5 style='margin:0'><b>Zapisana rezerwacja: </b></h5></b>" +
            //              "<hr style='max-width: 100px; margin-left:0'>" +
            //              "<b>Rasa (breed): </b>" + res.Breed + "<br>" +
            //              "<b>Płeć (sex): </b>" + res.Sex + "<br>" +
            //              "<b>Kolor (colour): </b>" + res.Colour + "<br>" +
            //              "<b>Rozmiar (size) :</b>" + res.DogSize + "<br>" +
            //              "<b>Poziom enegrii (energy level ): </b>" + res.EnergyLevel + "<br>" +
            //              "<b>Gotowość na zwierzę (ready for the dog): </b>" + res.ReadyToDog + "<br>" +
            //              "<b>Dodatkowi mieszkańcy (additional residents): </b>" + res.AdditionalResidents + "<br>" +
            //              "<b>Alergie (allergies): </b>" + res.Alergies + "<br>" +
            //              "<b>Preferowana matka (preffered mother): </b>" + res.PreferredMother + "<br>" +
            //              "<b>Preferowany ojciec (preffered father): </b>" + res.PreferredFather + "<br>" +
            //              "<b>Uwagi (notes): </b>" + res.ClientComments + "<br>" +
            //              "<b>Do hodowli (for kennel): </b>" + ((res.DogForKennel == true) ? "Tak" : "Nie") + "<br>" +
            //              "<b>Do sportu (for sport): </b>" + ((res.DogForSport == true) ? "Tak" : "Nie") + "<br><br>" +
            //              "Skontaktujemy się z Państwem, gdy dostępne będzie szczenię spełniające oczekiwania." +
            //              "<hr style='max-width: 100px; margin-left:0'>" +
            //              "<h3 style='color: darkcyan; margin:0'>Z wyrazami szacunku</h3>" +
            //              "<b>Anna Szkudlarz</b>";

            //    MailHelper.SendEmailReservation(res.Email, subject, contentForMe);
            //    MailHelper.SendToRecepientEmailReservation(res.Email, subject, contentForMe);

            //    return View("ReservationSuccess");
            //}
            
            return View();
        }

        public ActionResult FAQ()
        {
            return View();
        }

        public ActionResult ExGal()
        {
            return View();
        }

        public ActionResult ExGalSec()
        {
            return View();
        }

        public ActionResult ChangeLanguage(string lang)
        {
            new LanguageMang().SetLanguage(lang);
            return RedirectToAction("Index", "Home");
        }
    }
}