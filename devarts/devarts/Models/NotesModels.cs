using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace devarts.Models
{
    /// MODEL NOTATEK HODOWLI
    public class NotesModels
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public string CreateDate { get; set; }
        public string ModifyDate { get; set; }

        public string Colour { get; set; }
        public string Priority { get; set; }

        public bool Visibility { get; set; }
    }
}