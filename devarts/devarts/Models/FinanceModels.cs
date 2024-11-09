using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace devarts.Models
{
    // wszystkie wydatki i koszty
    [Table("Finances")]
    [MetadataType(typeof(FinanceMetaData))]
    public class Finance
    {
        [Key]
        public int Id { get; set; }
        public int DocumentId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }       
        public string Cathegory { get; set; }                
        public decimal Amount { get; set; }
        public string CurrencyName { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool IsExpense { get; set; }
        public bool IncludeFinance { get; set; }
        public bool Visibility { get; set; }
    }

    public class FinanceMetaData
    {
        [Display(Name = "Nazwa")]
        [Required(ErrorMessage = "{0} jest wymagana.")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Name { get; set; }
        [Display(Name = "Opis")]
        [Required(ErrorMessage = "{0} jest wymagany.")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Description { get; set; }

        [Display(Name = "Data od")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "{0} jest wymagana.")]
        public DateTime DateFrom { get; set; }

        [Display(Name = "Data do")]
        [Required(ErrorMessage = "{0} jest wymagana.")]
        [DataType(DataType.Date)]
        public DateTime DateTo { get; set; }

        [Display(Name = "Kategoria")]
        [Required(ErrorMessage = "{0} jest wymagana.")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Cathegory { get; set; }
        [Display(Name = "Kwota")]
        [Required(ErrorMessage = "{0} jest wymagana.")]
        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }
        [Display(Name = "Waluta")]
        [Required(ErrorMessage = "{0} jest wymagany.")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string CurrencyName { get; set; }

        [Display(Name = "Uwzględnij w finansach")]
        public bool IncludeFinance { get; set; }
        [Display(Name = "Widoczność")]
        [Required(ErrorMessage = "{0} jest wymagany.")]
        public bool Visibility { get; set; }
        [Display(Name = "To jest wydatek")]
        [Required(ErrorMessage = "{0} jest wymagany.")]
        public bool IsExpense { get; set; }
    }

    // kategorie finansów
    [Table("FinanceCathegoryNames")]
    public class FinanceCathegoryName
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsExpense { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool Visibility { get; set; }
    }

    // lista dodawanych wydatków
    [Table("FinanceFovouritesNames")]
    public class FinanceFavouritesName
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool Visibility { get; set; }
    }
}