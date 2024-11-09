using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace devarts.Models
{   
    /// DRZEWA GENEALOGICZNE
    [Table("Trees")]
    [MetadataType(typeof(TreeMetaData))]
    public class Tree
    {
        [Key]
        public int Id { get; set; }
        public int DogId { get; set; }
        public int LitterId { get; set; }
        public string DogLink { get; set; }
        public string LitterLink { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime EditDate { get; set; }

        // Wybrany
        public string DogTreeBox    { get; set; }
        public string DogTreeTooltip { get; set; }
        public string DogTreeTooltip_Image { get; set; }

        // Rodzice
        public string A_DogTreeBox_Father_1 { get; set; }
        public string A_DogTreeTooltip_Father_1 { get; set; }
        public string A_DogTreeImage_Father_1 { get; set; }      
        
        public string A_DogTreeBox_Mother_2 { get; set; }
        public string A_DogTreeTooltip_Mother_2 { get; set; }
        public string A_DogTreeImage_Mother_2 { get; set; }

        // Dziadkowie
        public string B_DogTreeBox_Father_3 { get; set; }
        public string B_DogTreeTooltip_Father_3 { get; set; }
        public string B_DogTreeImage_Father_3 { get; set; }

        public string B_DogTreeBox_Mother_4 { get; set; }
        public string B_DogTreeTooltip_Mother_4 { get; set; }
        public string B_DogTreeImage_Mother_4 { get; set; }

        public string B_DogTreeBox_Father_5 { get; set; }
        public string B_DogTreeTooltip_Father_5 { get; set; }
        public string B_DogTreeImage_Father_5 { get; set; }

        public string B_DogTreeBox_Mother_6 { get; set; }
        public string B_DogTreeTooltip_Mother_6 { get; set; }
        public string B_DogTreeImage_Mother_6 { get; set; }

        // Pradziadkowie
        public string C_DogTreeBox_Father_7 { get; set; }
        public string C_DogTreeTooltip_Father_7 { get; set; }
        public string C_DogTreeImage_Father_7 { get; set; }

        public string C_DogTreeBox_Mother_8 { get; set; }
        public string C_DogTreeTooltip_Mother_8 { get; set; }
        public string C_DogTreeImage_Mother_8 { get; set; }

        public string C_DogTreeBox_Father_9 { get; set; }
        public string C_DogTreeTooltip_Father_9 { get; set; }
        public string C_DogTreeImage_Father_9 { get; set; }

        public string C_DogTreeBox_Mother_10 { get; set; }
        public string C_DogTreeTooltip_Mother_10 { get; set; }
        public string C_DogTreeImage_Mother_10 { get; set; }

        public string C_DogTreeBox_Father_11 { get; set; }
        public string C_DogTreeTooltip_Father_11 { get; set; }
        public string C_DogTreeImage_Father_11 { get; set; }

        public string C_DogTreeBox_Mother_12 { get; set; }
        public string C_DogTreeTooltip_Mother_12 { get; set; }
        public string C_DogTreeImage_Mother_12 { get; set; }

        public string C_DogTreeBox_Father_13 { get; set; }
        public string C_DogTreeTooltip_Father_13 { get; set; }
        public string C_DogTreeImage_Father_13 { get; set; }

        public string C_DogTreeBox_Mother_14 { get; set; }
        public string C_DogTreeTooltip_Mother_14 { get; set; }
        public string C_DogTreeImage_Mother_14 { get; set; }

        // tu wybieramy ile chcemy widzieć generacji, domyślmnie w Db = 3
        public int GenerationsCount { get; set; }
        public bool Visible { get; set; }
        public bool IsLitter { get; set; }
    }

    public class TreeMetaData
    {
        [Key]
        public int Id { get; set; }
        
    }
}