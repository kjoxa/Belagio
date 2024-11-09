using devarts.Models;
using devarts.Repositories;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace devarts.Helpers
{
    public class PrepareDogTrees
    {
        KennelRepository _kennelRepo;
        private static Logger nLog = LogManager.GetCurrentClassLogger();

        public PrepareDogTrees()
        {
            _kennelRepo = new KennelRepository();
        }

        /// DOG
        //  TWORZENIE DOMYŚLNEGO DRZEWA GENEALOGICZNEGO
        public bool CreateDogGenealogicTree(string dogLink, int dogId, string litterLink, int litterId, bool isLitter, string dogTreeTooltipImagePath)
        {
            try
            {
                Tree newTree = new Tree
                {
                    CreateDate = DateTime.Now,
                    EditDate = DateTime.Now,
                    DogLink = dogLink,
                    LitterLink = litterLink,
                    DogId = dogId,
                    LitterId = litterId,
                    IsLitter = isLitter,
                    DogTreeBox = "WW, CH PL",
                    DogTreeTooltip = "<br><b>WW, CH PL<br>22.08.2018<br></b>Krótki tekst bądź jego brak.<br>Możliwe jest dodanie adnotacji poniżej - jak w tym przykładzie.",
                    DogTreeTooltip_Image = dogTreeTooltipImagePath,

                    A_DogTreeBox_Father_1 = "WW, C.I.B, CH PL",
                    A_DogTreeTooltip_Father_1 = "<br><b>WW, CH PL<br>22.08.2018<br></b>Krótki tekst bądź jego brak.<br>Możliwe jest dodanie adnotacji poniżej - jak w tym przykładzie.",
                    A_DogTreeImage_Father_1 = "https://italiangreyhound.breedarchive.com/resource/1/311/photo.bd871682d7d3bc81_s.jpg",

                    A_DogTreeBox_Mother_2 = "WW, C.I.B, CH PL",
                    A_DogTreeTooltip_Mother_2 = "<br><b>WW, CH PL<br>22.08.2018<br></b>Krótki tekst bądź jego brak.<br>Możliwe jest dodanie adnotacji poniżej - jak w tym przykładzie.",
                    A_DogTreeImage_Mother_2 = "https://italiangreyhound.breedarchive.com/resource/1/311/photo.bd871682d7d3bc81_s.jpg",

                    B_DogTreeBox_Father_3 = "WW, C.I.B, CH PL",
                    B_DogTreeTooltip_Father_3 = "<br><b>WW, CH PL<br>22.08.2018<br></b>Krótki tekst bądź jego brak.<br>Możliwe jest dodanie adnotacji poniżej - jak w tym przykładzie.",
                    B_DogTreeImage_Father_3 = "https://italiangreyhound.breedarchive.com/resource/1/311/photo.bd871682d7d3bc81_s.jpg",
                    B_DogTreeBox_Mother_4 = "WW, C.I.B, CH PL",
                    B_DogTreeTooltip_Mother_4 = "<br><b>WW, CH PL<br>22.08.2018<br></b>Krótki tekst bądź jego brak.<br>Możliwe jest dodanie adnotacji poniżej - jak w tym przykładzie.",
                    B_DogTreeImage_Mother_4 = "https://italiangreyhound.breedarchive.com/resource/1/311/photo.bd871682d7d3bc81_s.jpg",
                    B_DogTreeBox_Father_5 = "WW, C.I.B, CH PL",
                    B_DogTreeTooltip_Father_5 = "<br><b>WW, CH PL<br>22.08.2018<br></b>Krótki tekst bądź jego brak.<br>Możliwe jest dodanie adnotacji poniżej - jak w tym przykładzie.",
                    B_DogTreeImage_Father_5 = "https://italiangreyhound.breedarchive.com/resource/1/311/photo.bd871682d7d3bc81_s.jpg",
                    B_DogTreeBox_Mother_6 = "WW, C.I.B, CH PL",
                    B_DogTreeTooltip_Mother_6 = "<br><b>WW, CH PL<br>22.08.2018<br></b>Krótki tekst bądź jego brak.<br>Możliwe jest dodanie adnotacji poniżej - jak w tym przykładzie.",
                    B_DogTreeImage_Mother_6 = "https://italiangreyhound.breedarchive.com/resource/1/311/photo.bd871682d7d3bc81_s.jpg",

                    C_DogTreeBox_Father_7 = "WW, C.I.B, CH PL",
                    C_DogTreeTooltip_Father_7 = "<br><b>WW, CH PL<br>22.08.2018<br></b>Krótki tekst bądź jego brak.<br>Możliwe jest dodanie adnotacji poniżej - jak w tym przykładzie.",
                    C_DogTreeImage_Father_7 = "https://italiangreyhound.breedarchive.com/resource/1/311/photo.bd871682d7d3bc81_s.jpg",
                    C_DogTreeBox_Mother_8 = "WW, C.I.B, CH PL",
                    C_DogTreeTooltip_Mother_8 = "<br><b>WW, CH PL<br>22.08.2018<br></b>Krótki tekst bądź jego brak.<br>Możliwe jest dodanie adnotacji poniżej - jak w tym przykładzie.",
                    C_DogTreeImage_Mother_8 = "https://italiangreyhound.breedarchive.com/resource/1/311/photo.bd871682d7d3bc81_s.jpg",
                    C_DogTreeBox_Father_9 = "WW, C.I.B, CH PL",
                    C_DogTreeTooltip_Father_9 = "<br><b>WW, CH PL<br>22.08.2018<br></b>Krótki tekst bądź jego brak.<br>Możliwe jest dodanie adnotacji poniżej - jak w tym przykładzie.",
                    C_DogTreeImage_Father_9 = "https://italiangreyhound.breedarchive.com/resource/1/311/photo.bd871682d7d3bc81_s.jpg",
                    C_DogTreeBox_Mother_10 = "WW, C.I.B, CH PL",
                    C_DogTreeTooltip_Mother_10 = "<br><b>WW, CH PL<br>22.08.2018<br></b>Krótki tekst bądź jego brak.<br>Możliwe jest dodanie adnotacji poniżej - jak w tym przykładzie.",
                    C_DogTreeImage_Mother_10 = "https://italiangreyhound.breedarchive.com/resource/1/311/photo.bd871682d7d3bc81_s.jpg",
                    C_DogTreeBox_Father_11 = "WW, C.I.B, CH PL",
                    C_DogTreeTooltip_Father_11 = "<br><b>WW, CH PL<br>22.08.2018<br></b>Krótki tekst bądź jego brak.<br>Możliwe jest dodanie adnotacji poniżej - jak w tym przykładzie.",
                    C_DogTreeImage_Father_11 = "https://italiangreyhound.breedarchive.com/resource/1/311/photo.bd871682d7d3bc81_s.jpg",
                    C_DogTreeBox_Mother_12 = "WW, C.I.B, CH PL",
                    C_DogTreeTooltip_Mother_12 = "<br><b>WW, CH PL<br>22.08.2018<br></b>Krótki tekst bądź jego brak.<br>Możliwe jest dodanie adnotacji poniżej - jak w tym przykładzie.",
                    C_DogTreeImage_Mother_12 = "https://italiangreyhound.breedarchive.com/resource/1/311/photo.bd871682d7d3bc81_s.jpg",
                    C_DogTreeBox_Father_13 = "WW, C.I.B, CH PL",
                    C_DogTreeTooltip_Father_13 = "<br><b>WW, CH PL<br>22.08.2018<br></b>Krótki tekst bądź jego brak.<br>Możliwe jest dodanie adnotacji poniżej - jak w tym przykładzie.",
                    C_DogTreeImage_Father_13 = "https://italiangreyhound.breedarchive.com/resource/1/311/photo.bd871682d7d3bc81_s.jpg",
                    C_DogTreeBox_Mother_14 = "WW, C.I.B, CH PL",
                    C_DogTreeTooltip_Mother_14 = "<br><b>WW, CH PL<br>22.08.2018<br></b>Krótki tekst bądź jego brak.<br>Możliwe jest dodanie adnotacji poniżej - jak w tym przykładzie.",
                    C_DogTreeImage_Mother_14 = "https://italiangreyhound.breedarchive.com/resource/1/311/photo.bd871682d7d3bc81_s.jpg",

                    GenerationsCount = 3,                    
                    Visible = false
                };

                var isExistTreeByDogLink = _kennelRepo.GetDogTreeByDogLink(dogLink);
                var isExistTreeByLitterLink = _kennelRepo.GetLitterTreeByLitterLink(litterLink);
                if (isExistTreeByDogLink == null || isExistTreeByLitterLink == null)
                {
                    _kennelRepo.AddTree(newTree);
                    _kennelRepo.SaveChanges();

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                nLog.Error(ex.ToString());
                return false;
            }                                    
        }
    }
}