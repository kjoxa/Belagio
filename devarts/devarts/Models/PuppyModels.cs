using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace devarts.Models
{
    [Table("Puppies")]
    [MetadataType(typeof(PuppyMetaData))]
    public class Puppy
    {
        public int Id { get; set; }
        public int LitterId { get; set; }
        public int BreedId { get; set; }
        public int DogId { get; set; }

        public string BreedLink { get; set; }
        public string DogLink { get; set; }
        public string LitterLink { get; set; }
        public string PuppyLink { get; set; }

        public string MainPicture { get; set; }

        public string PuppyName { get; set; }
        public string PuppyAchievements { get; set; }
        public string PuppyColour { get; set; }
        public bool PuppySex { get; set; }

        public bool IsForBreeding { get; set; }
        public bool IsForSale { get; set; }
        public bool IsReproductor { get; set; }

        public string Defects { get; set; }
        public string SpecialSigns { get; set; }

        /// BornDate -> bez sensu, ale już dodane - może się przyda
        public string BornDate { get; set; }
        public int BornWeight { get; set; }
        public string DeathDate { get; set; }
        public string CauseOfDeath { get; set; }

        public int AdultWeight { get; set; }
        public int AdultHeight { get; set; }

        public string Country { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string FirstName { get; set; }
        public string SurName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public string Description { get; set; }
        public string DescriptionEng { get; set; }

        public string Url { get; set; }
        public string BootstrapColumn { get; set; }
        public string Tags { get; set; }

        public string CreateDate { get; set; }
        public string ModifiedDate { get; set; }
        public int ShowsCount { get; set; }

        public bool Visibility { get; set; }
        public string NavbarPuppyName { get; set; }
        public string NavbarPuppyNameEng { get; set; }
        public bool NavbarEnabled { get; set; }
        public bool NavbarVisibility { get; set; }
        public string AvailableStatus { get; set; }

    }

    public class PuppyMetaData
    {
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Required(ErrorMessage = "{0} jest wymagana.")]
        [StringLength(40, ErrorMessage = "Nazwa folderu szczenięcia nie może być dłuższa niż 40 znaków!")]
        [Display(Name = "Nazwa folderu szczenięcia (bez spacji, poskich znaków, z dozwolonym podkreślnikiem oraz myślnikiem)")]
        [RegularExpression(@"^[a-zA-Z0-9-_]+$", ErrorMessage = "Dozwolone litery, cyfry, podkreślnik i myślnik bez spacji i polskich znaków - do 40 znaków!")]
        public string PuppyLink { get; set; }

        [Display(Name = "Nazwa szczenięcia")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Required(ErrorMessage = "{0} jest wymagana.")]
        public string PuppyName { get; set; }

        [AllowHtml]
        [Display(Name = "Osiągnięcia")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string PuppyAchievements { get; set; }

        [Display(Name = "Kolor")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Required(ErrorMessage = "{0} jest wymagany.")]
        public string PuppyColour { get; set; }

        [Display(Name = "Płeć")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public bool PuppySex { get; set; }

        [Display(Name = "Szczenie hodowlane")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public bool IsForBreeding { get; set; }

        [Display(Name = "Dostępny (sprzedaż)")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public bool IsForSale { get; set; }

        [Display(Name = "Reproduktor")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public bool IsReproductor { get; set; }

        [Display(Name = "Wady")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Defects { get; set; }

        [Display(Name = "Znaki szczególne")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string SpecialSigns { get; set; }

        [Display(Name = "Data urodzenia")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string BornDate { get; set; }

        [Display(Name = "Masa po urodzeniu (gramy)")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int BornWeight { get; set; }

        [Display(Name = "Data śmierci")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string DeathDate { get; set; }

        [Display(Name = "Przyczyna śmierci")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string CauseOfDeath { get; set; }

        [Display(Name = "Waga (dojrzały) - gramy")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int AdultWeight { get; set; }

        [Display(Name = "Wzrost (dojrzały) - centymetry")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int AdultHeight { get; set; }

        [Display(Name = "Kraj")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Country { get; set; }

        [Display(Name = "Miasto")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string City { get; set; }

        [Display(Name = "Adres")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Address { get; set; }

        [Display(Name = "Imię")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string FirstName { get; set; }

        [Display(Name = "Nazwisko")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string SurName { get; set; }

        [Display(Name = "Numer telefonu")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Phone { get; set; }

        [Display(Name = "Email")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Email { get; set; }

        [AllowHtml]
        [Display(Name = "Opis")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Column("Description", TypeName = "ntext")]
        public string Description { get; set; }

        [AllowHtml]
        [Display(Name = "Opis po angielsku")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Column("DescriptionEng", TypeName = "ntext")]
        public string DescriptionEng { get; set; }

        [Display(Name = "Odnośnik (jeśli istnieje)")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Url { get; set; }

        [Display(Name = "Szerokość zdjęcia głównego")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string BootstrapColumn { get; set; }

        public int ShowsCount { get; set; }

        [Display(Name = "Status dostępności (pod zdjęciem na stronie głównej)")]
        public string AvailableStatus { get; set; }

    }

    [Table("PuppyImages")]
    public class PuppyImages
    {
        public int Id { get; set; }
        public int BreedId { get; set; }
        public int DogId { get; set; }
        public int LitterId { get; set; }
        public string BreedLink { get; set; }
        public string DogLink { get; set; }
        public string LitterLink { get; set; }
        public string PuppyLink { get; set; }
        public string ImageFileName { get; set; }
        public string ImageFileSize { get; set; }
        public string ImageDate { get; set; }
        public bool IsPublished { get; set; }

        public int ImageIndex { get; set; }
        public string ImageAlt { get; set; }
    }

    public class PuppyAndImages
    {
        public Puppy puppy { get; set; }
        public IQueryable<PuppyImages> puppyImages { get; set; }
        public Litter litter { get; set; }
        public Dog dog { get; set; }
    }

    public class PuppyOrders
    {
        [Key]
        public int Id { get; set; }
        public string Email { get; set; }
        public string Colour { get; set; }
        public string Sex { get; set; }
        public string DogShow { get; set; }
        public string CoursingDog { get; set; }

        public string House { get; set; }
        public string WorkType { get; set; }
        public string HoursAlone { get; set; }
        public string Children { get; set; }

        public string Description { get; set; }
    }
}