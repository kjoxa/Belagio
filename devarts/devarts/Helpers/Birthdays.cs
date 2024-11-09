using devarts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devarts.Helpers
{
    public class Birthdays
    {
        public int Month { get; set; }
        public Dog dog { get; set; }
        public Litter litter { get; set; }
        public Puppy puppy { get; set; }
        public string RemainingDays { get; set; }
        public string BornDateInThisYear { get; set; }
        public bool IsPast { get; set; }  
        public string Collapse { get; set; }
    }
}
