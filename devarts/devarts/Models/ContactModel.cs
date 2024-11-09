using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace devarts.Models
{
    public class Contact
    {
        [Required(ErrorMessage = "Adres e-mail jest wymagany / E-mail required.")]
        [Display(Name = "E-mail")]
        [StringLength(100, ErrorMessage = "Pole adresu mailowego może zawierać nie więcej niż 100 znaków!")]
        [EmailAddress(ErrorMessage = "Adres e-mail jest niepoprawny / Wrong e-mail address.")]
        [RegularExpression("^[^<>,<|>]+$", ErrorMessage = "Tagi składni HTML nie są dozwolone / HTML tags do not support.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Temat jest wymagany / Topis required.")]
        [Display(Name = "Temat")]
        [StringLength(100, ErrorMessage = "Temat może mieć maksymalnie 50 znaków.")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Treść jest wymagana / Message content required.")]
        [Display(Name = "Treść")]
        [StringLength(1500, ErrorMessage = "Treść może mieć maksymalnie 1500 znaków.")]
        public string Content { get; set; }
    }   
}