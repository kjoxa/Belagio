using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace devarts.Models
{
    // REZERWACJE PSÓW
    [Table("Reservations")]
    public class Reservation
    {
        [Key]
        public int Id { get; set; }
        // dodane przez klienta - true / przez hodowcę - false
        [Display(Name = "Dodane przez internautę")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public bool CreateByClient { get; set; }

        [Display(Name = "E-mail")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Required(ErrorMessage = "{0} jest wymagany / E-mail is required")]
        [StringLength(250, ErrorMessage = "Pole adresu mailowego może zawierać nie więcej niż 250 znaków!")]
        [EmailAddress(ErrorMessage = "Podany adres e-mail jest nieprawidłowy")]
        [RegularExpression("^[^<>,<|>]+$", ErrorMessage = "Tagi składni HTML nie są dozwolone!")]
        public string Email { get; set; }

        [Display(Name = "Imię")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Required(ErrorMessage = "{0} jest wymagane / First name is required")]
        public string FirstName { get; set; }

        [Display(Name = "Nazwisko")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Required(ErrorMessage = "{0} jest wymagane / Surname is required")]
        public string SurName { get; set; }

        [Display(Name = "Kraj")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Required(ErrorMessage = "{0} jest wymagany / Country is required")]
        public string Country { get; set; }

        [Display(Name = "Miejscowość")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Required(ErrorMessage = "{0} jest wymagana / City is required")]
        public string City { get; set; }

        [Display(Name = "Nr. tel.")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Required(ErrorMessage = "{0} jest wymagany / Phone number is required")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Rasa")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        //[Required(ErrorMessage = "{0} jest wymagana")]
        public string Breed { get; set; }

        [Display(Name = "Preferowana płeć")]
        [Required(ErrorMessage = "{0} jest wymagana / Sex is required")]
        public string Sex { get; set; }

        [Display(Name = "Preferowane umaszczenie")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        //[Required(ErrorMessage = "{0} jest wymagane")]
        public string Colour { get; set; }

        [Display(Name = "Preferowany poziom energii")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        //[Required(ErrorMessage = "{0} jest wymagany / Energy level is required")]
        public string EnergyLevel { get; set; }

        [Display(Name = "Preferowany rozmiar")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        //[Required(ErrorMessage = "{0} jest wymagany / Size is required")]
        public string DogSize { get; set; }

        [Display(Name = "Do hodowli")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public bool DogForKennel { get; set; }

        [Display(Name = "Wyścigi")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public bool DogForSport { get; set; }

        [Display(Name = "Preferowana matka (opcjonalnie)")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string PreferredMother { get; set; }

        [Display(Name = "Preferowany ojciec (opcjonalnie)")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string PreferredFather { get; set; }

        [Display(Name = "Gotowość na odbiór")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Required(ErrorMessage = "{0} jest wymagana")]
        public string ReadyToDog { get; set; }

        [Display(Name = "Dodatkowi mieszkańcy (dzieci, zwierzęta / opcjonalnie)")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string AdditionalResidents { get; set; }

        [Display(Name = "Alergie (opcjonalnie)")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Alergies { get; set; }

        [Display(Name = "Uwagi")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string ClientComments { get; set; }

        // ----------- BACKEND ------------------------
        [Display(Name = "Priorytet")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Priority { get; set; }

        [Display(Name = "Moje uwagi")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string BackendComments { get; set; }

        [Display(Name = "Status płatności")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string PaymentStatus { get; set; }

        [Display(Name = "Sposób dostawy")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string DeliveryMethod { get; set; }

        [Display(Name = "Data dodania")]
        public DateTime CreateDate { get; set; }

        [Display(Name = "Data edycji")]
        public DateTime ModifiedDate { get; set; }

        [Required(ErrorMessage = "Bez zgody na przetwarzanie danych osobowych rezerwacja nie może zostać przyjęta")]
        [Display(Name = "Zgoda na przetwarzanie danych osobowych")]
        public bool Rodo { get; set; }

        public bool IsReaded { get; set; }
        public bool IsClosed { get; set; }

        // jeśli większe od 0, to znaczy, że już ten ktoś dostał info o dostępnym szczenięciu
        //public int SendMessageCount { get; set; }
        public string Password { get; set; }

        [Display(Name = "Ogon / Tail")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string TailLength { get; set; }
    }

    public class ReservationAndSendViewModel
    {
        public Contact contact { get; set; }
        public Reservation reservation { get; set; }
    }
}