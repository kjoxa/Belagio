using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace devarts.Models
{
    // statystyki odwiedzin witryny
    [Table("Statistics")]
    public class Statistic
    {
        [Display(Name = "Id")]
        public Int32 Id { get; set; }

        [Display(Name = "Data i czas")]
        public String DateTime { get; set; }

        [Display(Name = "Adres IP")]
        public String IpAddress { get; set; }

        [Display(Name = "Nazwa hosta")]
        public String Name { get; set; }

        [Display(Name = "Typ")]
        public int DeviceType { get; set; }

        [Display(Name = "Kontynent")]
        public String Continent { get; set; }

        [Display(Name = "Kraj")]
        public String Country { get; set; }

        [Display(Name = "GeoLong")]
        public String GeoLong { get; set; }

        [Display(Name = "GeoLat")]
        public String GeoLat { get; set; }
    }

    [Table("Tracing")]
    public class Tracing
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Adres URL")]
        public string Url { get; set; }
        [Display(Name = "Host/IP")]
        public string HostAndIp { get; set; }
        [Display(Name = "Data i czas")]
        public string DateTime { get; set; }
        [Display(Name = "Typ")]
        public int DeviceType { get; set; }
    }

    // AJAXowe parametry DataTables
    public class JQueryDataTableParamModel
    {
        /// <summary>
        /// Request sequence number sent by DataTable, same value must be returned in response
        /// </summary>       
        public string sEcho { get; set; }

        /// <summary>
        /// Text used for filtering
        /// </summary>
        public string sSearch { get; set; }

        /// <summary>
        /// Number of records that should be shown in table
        /// </summary>
        public int iDisplayLength { get; set; }

        /// <summary>
        /// First record that should be shown(used for paging)
        /// </summary>
        public int iDisplayStart { get; set; }

        /// <summary>
        /// Number of columns in table
        /// </summary>
        public int iColumns { get; set; }

        /// <summary>
        /// Number of columns that are used in sorting
        /// </summary>
        public int iSortingCols { get; set; }

        /// <summary>
        /// Comma separated list of column names
        /// </summary>
        public string sColumns { get; set; }

    }
}