using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace devarts.Models
{
    [Table("Litters")]
    [MetadataType(typeof(LitterMetaData))]
    public class Litter
    {
        public int    Id { get; set; }
        public int    BreedId { get; set; }
        public int    DogId { get; set; }

        public string BreedLink { get; set; }
        public string DogLink { get; set; }
        public string LitterLink { get; set; }

        public string Letter { get; set; }
        public string MainPicture { get; set; }  
        
        public string LitterPresentationName { get; set; }                
        public int    MaleCount { get; set; }       
        public int    FemaleCount { get; set; }             
        public string DogFather { get; set; }
        public string DogFatherDegree { get; set; }
        public string DogFatherLink { get; set; }        
        public string Description { get; set; }        
        public string BornDate { get; set; }
        public DateTime BornDateOkay { get; set; }

        public string Tags { get; set; }
        public string BootstrapColumn { get; set; }               
        public string CreateDate { get; set; }
        public string ModifiedDate { get; set; }
        public int    ShowsCount { get; set; }

        public bool   Visibility { get; set; }
        public string NavbarLitterName { get; set; }
        public string NavbarLitterNameEng { get; set; }
        public bool   NavbarEnabled { get; set; }
        public bool   NavbarVisibility { get; set; }

        // 09.11.2022 Dodanie właściwości miotów
        public string MotherLitterDescription { get; set; }
        public string FatherLitterDescription { get; set; }
 
        // czy przydomek ma być z przodu czy z tyłu
        public bool DogFatherKennelNameFirst { get; set; }
        public bool DogMotherKennelNameFirst { get; set; }                
        public string FatherPictureName { get; set; }
    }

    public class LitterMetaData
    {
        public int Id { get; set; }
        public int BreedId { get; set; }
        public int DogId { get; set; }

        [Display(Name = "Pierwsza litera miotu")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Required(ErrorMessage = "{0} jest wymagana.")]
        public string Letter { get; set; }
        
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Required(ErrorMessage = "{0} jest wymagana.")]
        [StringLength(40, ErrorMessage = "Nazwa folderu miotu nie może być dłuższa niż 40 znaków!")]
        [Display(Name = "Nazwa folderu miotu (bez spacji, poskich znaków, z dozwolonym podkreślnikiem oraz myślnikiem)")]
        [RegularExpression(@"^[a-zA-Z0-9-_]+$", ErrorMessage = "Dozwolone litery, cyfry, podkreślnik i myślnik bez spacji i polskich znaków - do 40 znaków!")]
        public string LitterLink { get; set; }

        [Display(Name = "Wyświetlana nazwa miotu")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Required(ErrorMessage = "{0} jest wymagana.")]
        public string LitterPresentationName { get; set; }

        [Display(Name = "Liczba samców")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Required(ErrorMessage = "{0} jest wymagana.")]
        public int MaleCount { get; set; }

        [Display(Name = "Liczba suk")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Required(ErrorMessage = "{0} jest wymagana.")]
        public int FemaleCount { get; set; }

        [Display(Name = "Nazwa ojca")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Required(ErrorMessage = "{0} jest wymagana.")]
        public string DogFather { get; set; }

        [Display(Name = "Osiągnięcia ojca")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]        
        public string DogFatherDegree { get; set; }

        [Display(Name = "Link do ojca")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string DogFatherLink { get; set; }

        [AllowHtml]
        [Display(Name = "Opis miotu")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Column("Description", TypeName = "ntext")]
        //[Required(ErrorMessage = "{0} jest wymagany.")]
        public string Description { get; set; }

        [Display(Name = "Data urodzenia")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Required(ErrorMessage = "{0} jest wymagana.")]
        public string BornDate { get; set; }

        // czyli szerokość zapowiedzi - jedna może być duża, inna mała
        [Display(Name = "Szerokość zapowiedzi")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string BootstrapColumn { get; set; }

        [Display(Name = "Tagi")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Tags { get; set; }

        [AllowHtml]
        [Display(Name = "Opis ojca miotu")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]        
        public string FatherLitterDescription { get; set; }

        [AllowHtml]
        [Display(Name = "Opis matki miotu")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]        
        public int MotherLitterDescription { get; set; }
    }

    [Table("LitterImages")]
    public class LitterImages
    {
        public int Id { get; set; }
        public int BreedId { get; set; }
        public int DogId { get; set; }        
        public int LitterId { get; set; }
        public string BreedLink { get; set; }
        public string DogLink { get; set; }
        public string LitterLink { get; set; }
        public string ImageFileName { get; set; }
        public string ImageFileSize { get; set; }
        public string ImageDate { get; set; }
        public bool IsPublished { get; set; }

        public int ImageIndex { get; set; }
        public string ImageAlt { get; set; }
    }

    public class DogAndLitterAndImagesAndPuppies
    {
        public Dog dog { get; set; }
        public Litter Litter { get; set; }
        public IEnumerable<LitterImages> LitterImages { get; set; }
        public IEnumerable<Puppy> Puppies { get; set; }
        public Tree tree { get; set; }
    }

    public class LitterWithImagesAndDog
    {
        public Litter litter { get; set; }
        public IQueryable<LitterImages> litterImages { get; set; }
        public Dog dog { get; set;}
    }
}