using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace devarts.Models
{
    /// RASA
    [Table("DogBreeds")]
    public class DogBreed
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} jest wymagana.")]
        [Display(Name = "Dokładna nazwa rasy")]
        public string BreedSpeciesName { get; set; }

        [Required(ErrorMessage = "{0} jest wymagana.")]
        [Display(Name = "Wyświetlana nazwa w menu")]
        public string BreedSpeciesDisplayName { get; set; }

        [Required(ErrorMessage = "{0} jest wymagana.")]
        [Display(Name = "Wyświetlana nazwa w menu po angielsku")]
        public string BreedSpeciesDisplayNameEng { get; set; }

        [Required(ErrorMessage = "{0} jest wymagany.")]
        [Display(Name = "Krótki opis rasy")]
        public string BreedDescription { get; set; }

        [Required(ErrorMessage = "{0} jest wymagana.")]
        [StringLength(40, ErrorMessage = "Nazwa folderu rasy nie może być dłuższa niż 40 znaków!")]
        [Display(Name = "Nazwa folderu rasy (bez spacji, poskich znaków, z dozwolonym podkreślnikiem oraz myślnikiem)")]        
        [RegularExpression(@"^[a-zA-Z0-9-_]+$", ErrorMessage = "Dozwolone litery, cyfry, podkreślnik i myślnik bez spacji i polskich znaków - do 40 znaków!")]        
        public string BreedLink { get; set; }    
        
        public string CreateDate { get; set; }
        public bool Visibility { get; set; }
    }

    /// PIES W HODOWLI
    [Table("Dogs")]
    [MetadataType(typeof(DogMetaData))]
    public class Dog
    {        
        [Key]
        public int Id { get; set; }
        public int SpeciesId { get; set; }
        public int ImageId { get; set; }
        public string MainPicture { get; set; }
        public string BreedLink { get; set; }
        public string DogLink { get; set; }

        // STANDARDOWE DANE
        public string DogName { get; set; } 
        public string DogPetName { get; set; }
        public string BornDate { get; set; }
        public DateTime BornDateDateTime { get; set; }
        public string DeathDate { get; set; }
        public string CauseOfDeath { get; set; }
        public string DogColour { get; set; }
        public bool DogSex { get; set; }
        public string DogDescription { get; set; }

        // DANE HODOWLANE
        public string Height { get; set; }
        public string Weight { get; set; }
        public string CoursingLicenceNumber { get; set; }
        public string BloodlineNumber { get; set; }
        public string DepartmentNumber { get; set; }
        public string ChipNumber { get; set; }
        
        // POCHODZENIE PSA
        public string BreedName { get; set; }
        public string OwnerName { get; set; }
        public string OwnerContact { get; set; }
        public string BreedArchiveUrl { get; set; }
        public string DogFatherName { get; set; }        
        public string DogMotherName { get; set; }
        public string DogFatherBreedName { get; set; }
        public string DogMotherBreedName { get; set; }
        public string DogFatherUrl { get; set; }
        public string DogMotherUrl { get; set; }
        public string DogFatherCountry { get; set; }
        public string DogMotherCountry { get; set; }
        public string DogFatherCity { get; set; }
        public string DogMotherCity { get; set; }

        // BADANIA
        public string DogMedicalDescription { get; set; }

        // OSIĄGNIĘCIA
        public string DegreeDescription { get; set; }        
        public string AchievementsDescription { get; set; }        

        // WŁAŚCIWOŚCI        
        public bool Visibility { get; set; }

        // jest na psiej emeryturze
        public bool IsRetired { get; set; }
        public bool IsForBreeding { get; set; }
        public bool IsReproductor { get; set; }
        
        public string CreateDate { get; set; }
        public int ShowCount { get; set; }
        public string Tags { get; set; }

        // NAVBAR
        public string NavbarDogName { get; set; }
        public string NavbarDogNameEng { get; set; }
        public bool NavbarEnabled { get; set; }
        public bool NavbarVisible { get; set; }
        
        public string ModifiedDate { get; set; }

        /// 06.07.2021
        // WSPÓŁWŁASNOŚĆ
        public bool Coowner { get; set; }
        public string CoownerFirstName { get; set; }
        public string CoownerSurName { get; set; }
        public string CoownerAddress { get; set; }
        public string CoownerContact { get; set; }

        /// 09.11.2022
        public bool DogKennelNameFirst { get; set; }
        public bool DogFatherKennelNameFirst { get; set; }
        public bool DogMotherKennelNameFirst { get; set; }
    }  
    
    public class DogMetaData
    {
        [Key]
        public int Id { get; set; }
        public int SpeciesId { get; set; }
        public int ImageId { get; set; }
        public string MainPicture { get; set; }

        [Display(Name = "Folder rasy")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Required(ErrorMessage = "{0} jest wymagany.")]
        public string BreedLink { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Required(ErrorMessage = "{0} jest wymagana.")]
        [StringLength(40, ErrorMessage = "Nazwa folderu zwierzęcia nie może być dłuższa niż 40 znaków!")]
        [Display(Name = "Nazwa folderu zwierzęcia (bez spacji, poskich znaków, z dozwolonym podkreślnikiem oraz myślnikiem)")]
        [RegularExpression(@"^[a-zA-Z0-9-_]+$", ErrorMessage = "Dozwolone litery, cyfry, podkreślnik i myślnik bez spacji i polskich znaków - do 40 znaków!")]
        public string DogLink { get; set; }        

        /// STANDARDOWE DANE
        [Display(Name = "Imię zwierzęcia (bez przydomka)")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Required(ErrorMessage = "{0} jest wymagana.")]
        public string DogName { get; set; }

        [Display(Name = "Nazwa domowa zwierzęcia")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string DogPetName { get; set; }

        [Display(Name = "Data urodzenia (DD.MM.RRRR)")]
        //[DisplayFormat(ConvertEmptyStringToNull = false)]
        [Required(ErrorMessage = "{0} jest wymagana.")]
        [DataType(DataType.Date)]
        public string BornDate { get; set; }

        [Display(Name = "Data śmierci zwierzęcia")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]        
        public string DeathDate { get; set; }

        [Display(Name = "Przyczyna śmierci zwierzęcia")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string CauseOfDeath { get; set; }

        [Display(Name = "Umaszczenie zwierzęcia")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Required(ErrorMessage = "{0} jest wymagany.")]
        public string DogColour { get; set; }

        [Display(Name = "Płeć")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Required(ErrorMessage = "{0} jest wymagana.")]
        public bool DogSex { get; set; }

        [AllowHtml]
        [Display(Name = "Opis zwierzęcia")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Required(ErrorMessage = "{0} jest wymagany.")]
        [DataType(DataType.MultilineText)]
        [Column("DogDescription", TypeName = "ntext")]
        public string DogDescription { get; set; }

        /// HODOWLANE DANE
        [Display(Name = "Wzrost")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Required(ErrorMessage = "{0} jest wymagana.")]
        public string Height { get; set; }

        [Display(Name = "Masa ciała - w kilogramach")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Required(ErrorMessage = "{0} jest wymagana.")]
        public string Weight { get; set; }

        [Display(Name = "Numer licencji coursingowej")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]        
        public string CoursingLicenceNumber { get; set; }

        [Display(Name = "Numer rodowodu")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Required(ErrorMessage = "{0} jest wymagany.")]
        public string BloodlineNumber { get; set; }

        [Display(Name = "Numer rej. oddziałowej")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Required(ErrorMessage = "{0} jest wymagana.")]
        public string DepartmentNumber { get; set; }

        [Display(Name = "Numer mikrochipa lub tatuażu")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Required(ErrorMessage = "{0} jest wymagany.")]
        public string ChipNumber { get; set; }

        /// POCHODZENIE PSA
        [Display(Name = "Nazwa hodowli (przydomek)")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Required(ErrorMessage = "{0} jest wymagana.")]
        public string BreedName { get; set; }

        [Display(Name = "Hodowca")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Required(ErrorMessage = "Podanie hodowcy jest wymagane.")]
        public string OwnerName { get; set; }

        [Display(Name = "Kontakt do hodowcy")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]        
        public string OwnerContact { get; set; }

        [Display(Name = "Link do portalu breedarchive")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string BreedArchiveUrl { get; set; }

        [Display(Name = "Nazwa ojca zwierzęcia")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Required(ErrorMessage = "{0} jest wymagana.")]
        public string DogFatherName { get; set; }

        [Display(Name = "Nazwa matki zwierzęcia")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Required(ErrorMessage = "{0} jest wymagana.")]
        public string DogMotherName { get; set; }

        [Display(Name = "Hodowla ojca zwierzęcia")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Required(ErrorMessage = "{0} jest wymagana.")]
        public string DogFatherBreedName { get; set; }

        [Display(Name = "Hodowla matki zwierzęcia")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Required(ErrorMessage = "{0} jest wymagana.")]
        public string DogMotherBreedName { get; set; }

        [Display(Name = "Link do ojca")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]        
        public string DogFatherUrl { get; set; }

        [Display(Name = "Link do matki")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string DogMotherUrl { get; set; }

        [Display(Name = "Kraj pochodzenia ojca")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string DogFatherCountry { get; set; }

        [Display(Name = "Kraj pochodzenia matki")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string DogMotherCountry { get; set; }

        [Display(Name = "Miasto pochodzenia ojca")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string DogFatherCity { get; set; }

        [Display(Name = "Miasto pochodzenia matki")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string DogMotherCity { get; set; }

        // BADANIA
        [AllowHtml]
        [Display(Name = "Badania")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [DataType(DataType.MultilineText)]
        [Column("DogMedicalDescription", TypeName = "ntext")]
        public string DogMedicalDescription { get; set; }

        // OSIĄGNIĘCIA
        [AllowHtml]
        [Display(Name = "Tytuły zwierzęcia")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [DataType(DataType.MultilineText)]
        [Column("DegreeDescription", TypeName = "ntext")]
        public string DegreeDescription { get; set; }

        [AllowHtml]
        [Display(Name = "Osiągnięcia zwierzęcia")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [DataType(DataType.MultilineText)]
        [Column("AchievementsDescription", TypeName = "ntext")]
        public string AchievementsDescription { get; set; }

        // WŁAŚCIWOŚCI        
        public bool Visibility { get; set; }

        [Display(Name = "Psia emerytura/Weteran")]
        public bool IsRetired { get; set; }

        // NAVBAR
        public string NavbarDogName { get; set; }
        public string NavbarDogNameEng { get; set; }
        public bool NavbarEnabled { get; set; }
        public bool NavbarVisible { get; set; }

        // hodowlany czy nie?
        [Display(Name = "Suka hodowlana")]
        public bool IsForBreeding { get; set; }

        [Display(Name = "Reproduktor")]
        public bool IsReproductor { get; set; }

        [Display(Name = "Współwłasność")]
        public string Coowner { get; set; }

        [Display(Name = "Imię współwłaściciela")]
        public string CoownerFirstName { get; set; }

        [Display(Name = "Nazwisko współwłaściciela")]
        public string CoownerSurName { get; set; }

        [Display(Name = "Adres współwłaściciela")]
        public string CoownerAddress { get; set; }

        [Display(Name = "Kontakt do współwłaściciela")]
        public string CoownerContact { get; set; }

        /// 09.11.2022
        [Display(Name = "Przydomek psa jako pierwszy")]
        public bool DogKennelNameFirst { get; set; }
        [Display(Name = "Przydomek ojca jako pierwszy")]
        public bool DogFatherKennelNameFirst { get; set; }
        [Display(Name = "Przydomek matki jako pierwszy")]
        public bool DogMotherKennelNameFirst { get; set; }
    }

    public class BreedDogLitterViewModel
    {
        public IQueryable<DogBreed> dogBreeds { get; set; }
        public IQueryable<Dog> dogs { get; set; }
        public IQueryable<Litter> litters { get; set; }
    }

    [Table("DogImages")]
    public class DogImages
    {
        public int Id { get; set; }
        public int DogId { get; set; }
        public string DogLink { get; set; }
        public string ImageFileName { get; set; }
        public string ImageFileSize { get; set; }
        public string ImageDate { get; set; }
        public bool IsPublished { get; set; }

        public int ImageIndex { get; set; }
        public string ImageAlt { get; set; }
    }

    public class DogWithImages
    {
        public Dog dog { get; set; }
        public IQueryable<DogImages> images { get; set; }
        public IQueryable<Litter> litters { get; set; }
        public Tree tree { get; set; }
    }

    /// ATRYBUTY PRZYPISANE DO PSA
    [Table("DogsProperties")]
    public class DogsProperty
    {
        [Key]
        public int Id { get; set; }
        public int DogId { get; set; }

        public string PropertyName { get; set; }
        public string PropertyContent { get; set; }
        public string PropertyDescription { get; set; }

        // COURSING, WYSTAWA, MIOT, POST
        public string PropertyType { get; set; }
    }
}