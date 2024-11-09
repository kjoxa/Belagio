using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace devarts.Models
{
    // dokumenty, zaświadczenia i kreator umów
    [Table("Documents")]
    public class Document
    {
        [Key]
        public int Id { get; set; }
        public string DocumentName { get; set; }        
        public string Description { get; set; }
        public string Type { get; set; }
        public string Url { get; set; }
        public string DocumentDate { get; set; }
        public string DocumentVersion { get; set; }
        public string UploadDate { get; set; }        
        public bool IsActual { get; set; }
    }

    // badania i szczepienie
    [Table("Medicals")]
    public class Medical
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }       
        public string Price { get; set; }
    }

    // CIECZKI SUK
    [Table("Reproduction")]
    public class Reproduction
    {
        [Key]
        public int Id { get; set; }
        public int DogId { get; set; } // jeśli pies będzie podpinany z istniejących już psów w systemie

        [Display(Name = "Nazwa psa")]
        [Required(ErrorMessage = "Nazwa psa jest wymagana.")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string DogName { get; set; }
        
        [Display(Name = "Rozpoczęcie cieczki")]
        [Required(ErrorMessage = "Data rozpoczęcia cieczki jest wymagana.")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime EstrusStartDate { get; set; } // cieczka        
        public DateTime? EstrusEndDate { get; set; }

        [Display(Name = "Data rozpoczęcia rui")]               
        public DateTime? RutStartDate { get; set; }
        public DateTime? RutEndDate { get; set; }

        [Display(Name = "Najlepszy dzień (cieczki) na krycie")]
        public string ProgessteronBestDay { get; set; }
        [Display(Name = "Poziom (najlepszy) progesteronu dla suki")]
        public string ProgessteronBestVal { get; set; }

        [Display(Name = "Nazwa reproduktora")]
        [Required(ErrorMessage = "Nazwa ojca jest wymagana.")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string FatherName { get; set; }
        [Display(Name = "Nazwa hodowli reproduktora")]
        public string FatherBreederName { get; set; }
        // ojciec jest z mojej hodowli
        [Display(Name = "Własny reproduktor")]
        public bool FatherFromMyHome { get; set; }

        [Display(Name = "Data pierwszego krycia")]
        public DateTime? MatingDate_First { get; set; }
        [Display(Name = "Data drugiego krycia")]
        public DateTime? MatingDate_Second { get; set; }
        [Display(Name = "Data trzeciego krycia")]
        public DateTime? MatingDate_Third { get; set; }

        public DateTime? EstimationBornDate { get; set; }
        public DateTime? NextEstrusDate { get; set; }
        // data urodzenia - dni od krycia do porodu się obliczy
        public DateTime? DateOfBorn { get; set; }

        // nie wiadomo czy się przyda
        [Display(Name = "Waga przed porodem")]
        public int WeightBeforeBorn { get; set; }
        [Display(Name = "Liczba urodzonych szczeniąt")]
        public int CountOfBornPuppies { get; set; }

        // dane do korygowania obliczeń
        [Display(Name = "Korekta dni następnej cieczki (domyślnie 180 dni)")]
        public int CorrectDaysToEstrus { get; set; }
        public int CorrectDaysToRut { get; set; }
        [Display(Name = "Korekta dni trwania ciąży (domyślnie 61 dni)")]
        public int CorrectDaysToPregnancy { get; set; }

        [Display(Name = "Komentarz")]
        public string Comment { get; set; }
        public string CommentDate { get; set; }
        public string CommentForBorn { get; set; }
        [Display(Name = "Poród przez cesarskie cięcie")]
        public bool Caesarean { get; set; }
        [Display(Name = "Poród zakończony sukcesem")]
        public bool IsSuccess { get; set; }
        [Display(Name = "Zatrzymaj obliczanie")]
        public bool CalculateDone { get; set; }
    }

    // model wykorzystywany przy widoku kafelków
    public class ReproductionStatisticView
    {
        public Reproduction Reproduction { get; set; }
        public List<ReproductionAndDog> ReproductionAndDog { get; set; }
    }

    // model wykorzystywany przy widoku kafelków
    public class ReproductionAndDog
    {
        public Reproduction Reproduction { get; set; }
        public Dog Dog { get; set; }        
    }

    // POJEDYŃCZY SZCZENIAK Z MIOTU DOPISANY DO SUKI
    public class PuppieOfBitch
    {
        [Key]
        // POWIĄZANE POLA
        public int Id { get; set; }
        public int BitchId { get; set; }
        public string BitchName { get; set; }
        public string Comment { get; set; }

        // DATA POMIARU
        public string MeasurementDate { get; set; }
        public string Weight { get; set; }
        public string Height { get; set; }
        public string Width { get; set; }

        // ZACHOWANIE: CICHY, SPOKOJNY, AKTYWNY, GŁOŚNY
        public string Behaviour { get; set; } 
        public string Attributes { get; set; }

        // SZCZENIAK UMARŁ
        public bool IsDeath { get; set; }
    }
}