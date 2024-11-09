using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace devarts.Models
{
    /// TABELA ZAWIERAJĄCA POZYCJE MIOTÓW I W OGÓLE OPIS MIOTU
    public class Litters
    {
        public int Id { get; set; }
        // nazwa miotu
        [Display(Name = "Nazwa (bez spacji i pl znaków)")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Required(ErrorMessage = "{0} jest wymagana.")]
        public string LitterName { get; set; }

        [Display(Name = "Wyświetlana nazwa miotu")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Required(ErrorMessage = "{0} jest wymagana.")]
        public string LitterPresentationName { get; set; }

        // rasa
        [Display(Name = "Rasa")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Required(ErrorMessage = "{0} jest wymagana.")]
        public string LitterBreed { get; set; }

        [Display(Name = "Liczba samców")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Required(ErrorMessage = "{0} jest wymagana.")]
        public int MaleCount { get; set; }

        [Display(Name = "Liczba suk")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Required(ErrorMessage = "{0} jest wymagana.")]
        public int FemaleCount { get; set; }

        [Display(Name = "Nazwa matki")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Required(ErrorMessage = "{0} jest wymagany.")]
        public string DogMother { get; set; }

        [Display(Name = "Nazwa ojca")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Required(ErrorMessage = "{0} jest wymagany.")]
        public string DogFather { get; set; }

        [Display(Name = "Link do matki")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string DogMotherLink { get; set; }

        [Display(Name = "Link do ojca")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string DogFatherLink { get; set; }

        [AllowHtml]
        [Display(Name = "Opis miotu")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Column("Description", TypeName = "ntext")]
        [Required(ErrorMessage = "{0} jest wymagany.")]
        public string Description { get; set; }

        [Display(Name = "Data urodzenia")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string BornDate { get; set; }

        // czyli szerokość zapowiedzi - jedna może być duża, inna mała
        [Display(Name = "Szerokość obrazka")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string BootstrapColumn { get; set; }

        public string ImgFileName { get; set; }

        // Administracyjno-statystyczne
        public string Date { get; set; }
        public int ShowsCount { get; set; }

        // współrzędne geograficzne
        //[Display(Name ="Dokładny Aares")]
        //[DisplayFormat(ConvertEmptyStringToNull = false)]
        //public string Address { get; set; }
        //[DisplayFormat(ConvertEmptyStringToNull = false)]
        //public string GeoLat { get; set; }
        //[DisplayFormat(ConvertEmptyStringToNull = false)]
        //public string GeoLong { get; set; }
    }

    /// model widoku dla DataTables

    public class LittersViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Wyświetlana nazwa miotu")]
        public string LitterName { get; set; }

        [Display(Name = "Wyświetlana nazwa miotu")]
        public string LitterPresentationName { get; set; }

        // rasa
        [Display(Name = "Rasa")]
        public string LitterBreed { get; set; }

        [Display(Name = "Liczba samców")]
        public int MaleCount { get; set; }

        [Display(Name = "Liczba suk")]
        public int FemaleCount { get; set; }

        [Display(Name = "Nazwa matki")]
        public string DogMother { get; set; }

        [Display(Name = "Nazwa ojca")]
        public string DogFather { get; set; }

        [Display(Name = "Data urodzenia")]
        public string BornDate { get; set; }

        [Display(Name = "Wyświetlenia")]
        public int ShowsCount { get; set; }

        [Display(Name = "Nazwa pliku")]
        public string ImgFileName { get; set; }
    }

    /// TABELA ZAWIERAJĄCA SZCZEGÓŁY DLA KAŻDEJ POZYCJI Z TABELI "LITTERS"
    public class DogsByLitters
    {
        public int Id { get; set; }
        public int LittersId { get; set; }
        public string LitterName { get; set; }
        //public string LitterPresentationName { get; set; }

        [Display(Name = "Osiągnięcia")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Required(ErrorMessage = "{0} są wymagane.")]
        public string DogAchievments { get; set; }

        [Display(Name = "Kolor")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Required(ErrorMessage = "{0} jest wymagany.")]
        public string DogColour { get; set; }

        [Display(Name = "Kraj")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Required(ErrorMessage = "{0} jest wymagany.")]
        public string Country { get; set; }

        [Display(Name = "Nazwa psa - FOLDER")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Required(ErrorMessage = "{0} jest wymagany.")]
        public string DogName { get; set; }

        [Display(Name = "Nazwa psa - wyświetlana")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Required(ErrorMessage = "{0} jest wymagany.")]
        public string DogPresentationName { get; set; }

        [Display(Name = "Płeć psa")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Required(ErrorMessage = "{0} jest wymagany.")]
        public string DogSex { get; set; }

        [AllowHtml]
        [Display(Name = "Opis (fb)")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Column("Description", TypeName = "ntext")]
        [Required(ErrorMessage = "{0} jest wymagany.")]
        public string Description { get; set; }

        [Display(Name = "Link")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Link { get; set; }

        // czyli szerokość zapowiedzi - jedna może być duża, inna mała
        [Display(Name = "Szerokość obrazka")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string BootstrapColumn { get; set; }

        public string ImgFileName { get; set; }
        public string Date { get; set; }
        public int ShowsCount { get; set; }
    }

    /// INFORMACJE O ZDJĘCIACH PRZYPISANYCH DO KONKRETNYCH PIESEŁKÓW
    public class ImagesForLitterDog
    {
        public int Id { get; set; }
        public int DogsByLittersId { get; set; }
        public string DogName { get; set; }
        public string LitterName { get; set; }
        public string ImageFileSize { get; set; }
        public string ImgFileName { get; set; }

        [Display(Name = "Indeks sortujący")]
        public int SortIndex { get; set; }

        public string Date { get; set; }
    }

    public class ReuploadImage
    {
        public string oldFileName { get; set; }
        public string newFileName { get; set; }
        public string Date { get; set; }
        [Display(Name = "Usuń poprzednie zdjęcie z serwera")]
        public bool removeOriginal { get; set; } = true;
        [Display(Name = "Ustaw jako zdjęcie główne")]
        public bool setAsMainImage { get; set; } = false;
    }

    // widok potrzebny do zmiany indeksu (iamgesForLitterDog) i zdjęcia (reuploadImage)
    public class ReuploadImagesForLitterDog
    {
        public ImagesForLitterDog imagesForLitterDog { get; set; }
        public ReuploadImage reuploadImage { get; set; }
    }

    /// VIEW MODEL

    public class DogLitterAndImagesViewModel
    {
        public Litters litter { get; set; }
        public DogsByLitters dog { get; set; }
        public List<ImagesForLitterDog> imagesForDogs { get; set; }
    }
}