using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace devarts.Helpers
{
    public class DepartmentHelper
    {
        public string GetFullName(string DepartmentId)
        {
            switch (DepartmentId)
            {
                case "WA":
                    return "Wydział Architektury";                    

                case "WCh":
                    return "Wydział Chemiczny";
                case "WETI":
                    return "Wydział Elektroniki, Telekomunikacji i Informatyki";
                case "WEiA":
                    return "Elektrotechniki i Automatyki";
                case "WFTiMS":
                    return "Wydział Fizyki Technicznej i Matematyki Stosowanej";
                case "WILiS":
                    return "Wydział Inżynierii Lądowej i Środowiska";
                case "WM":
                    return "Wydział Mechaniczny";
                case "WOiO":
                    return "Wydział Oceanotechniki i Okrętownictwa";
                case "WZiE":
                    return "Wydział Zarządzania i Ekonomii";

                default:
                    return DepartmentId;                    
            }            
        }
    }
}