using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace devarts.Models
{
    public class MailModel
    {
        //[Display(Name = "E-mail nadawcy")]
        //[Required(ErrorMessage = "{0} jest wymagany.")]
        //public string To { get; set; }

        [Display(Name = "Temat")]
        [Required(ErrorMessage = "{0} jest wymagany.")]
        public string Subject { get; set; }

        [AllowHtml]
        [Display(Name = "Treść")]
        [Required(ErrorMessage = "{0} jest wymagana.")]
        public string Body { get; set; }
    }

    public class Newsletter
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Adres e-mail jest wymagany / E-mail required.")]
        [Display(Name = "E-mail")]
        [StringLength(100, ErrorMessage = "Pole adresu mailowego może zawierać nie więcej niż 100 znaków!")]
        [EmailAddress(ErrorMessage = "Adres e-mail jest niepoprawny / Wrong e-mail address.")]
        [RegularExpression("^[^<>,<|>]+$", ErrorMessage = "Tagi składni HTML nie są dozwolone / HTML tags do not support.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Temat jest wymagany / Topis required.")]
        [Display(Name = "E-mail")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Treść jest wymagana / Message content required.")]
        [Display(Name = "E-mail")]
        public string Content { get; set; }

        [Display(Name = "Data zapisania")]
        public string CreateDate { get; set; }

        [Display(Name = "Wysłano")]
        public string SendDate { get; set; }

        [Display(Name = "Aktywny")]
        public bool IsActive { get; set; }
    }

    public class Subscriber
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Adres e-mail jest wymagany / E-mail required.")]
        [Display(Name = "E-mail")]
        [StringLength(100, ErrorMessage = "Pole adresu mailowego może zawierać nie więcej niż 100 znaków!")]
        [EmailAddress(ErrorMessage = "Adres e-mail jest niepoprawny! / E-mail is not valid!")]
        [RegularExpression("^[^<>,<|>]+$", ErrorMessage = "Tagi składni HTML nie są dozwolone!")]
        public string Email { get; set; }

        [Display(Name = "Data zapisania")]
        public string CreateDate { get; set; }

        [Display(Name = "Aktywny")]
        public bool IsActive { get; set; }

    }
}