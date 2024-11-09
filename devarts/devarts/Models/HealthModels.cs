using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace devarts.Models
{
    public class HealthAndVaccinations
    {
        public int Id { get; set; }
        public int DogId { get; set; }
        public string DogName { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Cathegory { get; set; }
        public string Comment { get; set; }
        public string Description { get; set; }
        public string ProgressBar { get; set; }
        public bool IsRepeatable { get; set; }
        public int DaysToRepeat { get; set; }
        public DateTime NextDate { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }

    public class HAV_Dog
    {        
        public Dog Dog { get; set; }
        public IEnumerable<HealthAndVaccinations> HealthAndVaccinationsForDog { get; set; }
    }

    /*
     
    LEA                                   
    - szczepienie przeciwko wściekliźnie, Co 365 dni lub jednorazowo / wynik: szczepienie OK / Niepożądane reakcje
    - badanie wzroku / brak wymaganego okresu / wynik WZROK DOBRY
    - odrobaczenie / 

    # Badanie można kliknąć i zmienić datę, poniżej domyślnie jest zaznaczony CheckBox (dodaj w historii)

    [HealthView]

        * Lea (wyszukaj ostatnie badanie z DANEGO TYPU i wyświetl)
         |-- Lea, Wścieklizna, co rok, przypomnienie: tak, data wykonania, wynik, opis
         |-- Lea, Badanie wzrooku, co rok, przypomnienie: nie, data wykonania, wynik, opis
         |-- Lea, Odrobaczenie, co rok, przypomnienie: tak, data wykonania, wynik, opis

        * Mati (wyszukaj ostatnie badanie z DANEGO TYPU i wyświetl)
         |-- Lea, Wścieklizna, co rok, przypomnienie: tak, data wykonania, wynik, opis
         |-- Lea, Badanie wzrooku, co rok, przypomnienie: nie, data wykonania, wynik, opis

        * Frups (wyszukaj ostatnie badanie z DANEGO TYPU i wyświetl)
         |-- Lea, Wścieklizna, co rok, przypomnienie: tak, data wykonania, wynik, opis

        * Nana (wyszukaj ostatnie badanie z DANEGO TYPU i wyświetl)
         |-- Lea, Wścieklizna, co rok, przypomnienie: tak, data wykonania, wynik, opis
         |-- Lea, Badanie wzrooku, co rok, przypomnienie: nie, data wykonania, wynik, opis
         |-- Lea, Odrobaczenie, co rok, przypomnienie: tak, data wykonania, wynik, opis
    
    [Health]

        1, Lea, Wścieklizna, co rok, przypomnienie: tak, data wykonania, wynik, opis
        2, Lea, Badanie wzrooku, co rok, przypomnienie: nie, data wykonania, wynik, opis
        3, Lea, Odrobaczenie, co rok, przypomnienie: tak, data wykonania, wynik, opis
        4, Mini, Cesarskie cięcie, nie dotyczy, nie dotyczy, data wykonania, wynik, opis
        5, Mini, Wścieklizna, co rok, przypomnienie: tak, data wykonania, wynik, opis
        6, Mati, Badanie wzrooku, co rok, przypomnienie: nie, data wykonania, wynik, opis
        7, Frups, Odrobaczenie, co rok, przypomnienie: tak, data wykonania, wynik, opis
        8, Mati, Cesarskie cięcie, nie dotyczy, nie dotyczy, data wykonania, wynik, opis
        9, Nana, Wścieklizna, co rok, przypomnienie: tak, data wykonania, wynik, opis
        10, Nana, Badanie wzrooku, co rok, przypomnienie: nie, data wykonania, wynik, opis
        11, Buba, Odrobaczenie, co rok, przypomnienie: tak, data wykonania, wynik, opis
        12, Buba, Cesarskie cięcie, nie dotyczy, nie dotyczy, data wykonania, wynik, opis

    */
}