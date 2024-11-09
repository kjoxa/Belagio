using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace devarts.Models
{
    [MetadataType(typeof(DogsLocationMetaData))]
    [Table("DogsLocation")]
    public class DogsLocation
    {        
        public int Id { get; set; }
        public int LitterId { get; set; }
        public string DogLink { get; set; }

        public string LitterName { get; set; }
        public string DogName { get; set; }
        public string DogBornDate { get; set; }
        public string DogDescription { get; set; }
        public bool DogSex { get; set; }
        public string Achievements { get; set; }
        public bool IsReproductor { get; set; }

        public string Owner { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string FullAddress { get; set; }

        public string Place { get; set; }
        public string GeoLong { get; set; }
        public string GeoLat { get; set; }

        public string AddDate { get; set; }
        public string ModifyDate { get; set; }

        public int ShowsCount { get; set; }
        public bool IsHide { get; set; }        

        public string ImageUrl { get; set; }
    }

    public class DogsLocationMetaData
    {
        [Key]
        public int Id { get; set; }

        [Display(Name ="Id miotu")]
        public int LitterId { get; set; }

        [Display(Name = "Link do rodowodu psa")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string DogLink { get; set; }

        [Display(Name = "Nazwa miotu")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string LitterName { get; set; }

        [Display(Name = "Nazwa psa")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Required(ErrorMessage ="Nazwa psa jest wymagana.")]
        public string DogName { get; set; }

        [Display(Name = "Data urodzenia")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Required(ErrorMessage = "Data urodzenia jest wymagana.")]
        public string DogBornDate { get; set; }

        [Display(Name = "Opis w okienku")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Required(ErrorMessage = "Opis jest wymagany - nie za długi.")]
        public string DogDescription { get; set; }

        [Display(Name = "Płeć (zaznaczone - suka, odznaczone - samiec)")]                
        public bool DogSex { get; set; }

        [Display(Name = "Osiągnięcia")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]        
        public string Achievements { get; set; }

        [Display(Name = "Reproduktor")]
        public bool IsReproductor { get; set; }

        [Display(Name = "Właściciel")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Required(ErrorMessage = "Proszę podać właściciela.")]
        public string Owner { get; set; }

        [Display(Name = "Miasto")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Required(ErrorMessage = "Podanie miasta jest wymagane.")]
        public string City { get; set; }

        [Display(Name = "Kraj")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Required(ErrorMessage = "Kraj jest wymagany.")]
        public string Country { get; set; }

        [Display(Name = "Pełny adres")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        //[Required(ErrorMessage = "Pełny adres jest wymagany.")]
        public string FullAddress { get; set; }

        [Display(Name = "Miejsce")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Place { get; set; }

        public string GeoLong { get; set; }
        public string GeoLat { get; set; }

        public string AddDate { get; set; }
        public string ModifyDate { get; set; }

        public int ShowsCount { get; set; }

        [Display(Name = "Nie pokazuj na mapie")]
        public bool IsHide { get; set; }

        [Display(Name = "Adres zdjęcia")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Required(ErrorMessage = "Adres zdjęcia jest wymagany.")]        
        public string ImageUrl { get; set; }
    }

    // lokalizacje psów wzbogacone o wiek psa
    public class DogsLocationViewModel
    {
        public int Id { get; set; }
        public int LitterId { get; set; }
        public string DogLink { get; set; }

        public string LitterName { get; set; }
        public string DogName { get; set; }
        public string DogBornDate { get; set; }
        public string DogDescription { get; set; }
        public bool DogSex { get; set; }
        public string Achievements { get; set; }
        public bool IsReproductor { get; set; }

        public string Owner { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string FullAddress { get; set; }

        public string Place { get; set; }
        public string GeoLong { get; set; }
        public string GeoLat { get; set; }

        public string AddDate { get; set; }
        public string ModifyDate { get; set; }

        public int ShowsCount { get; set; }
        public bool IsHide { get; set; }

        public string ImageUrl { get; set; }

        public string HowOld { get; set; }
    }
}