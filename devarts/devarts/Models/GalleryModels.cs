using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace devarts.Models
{
    public class GalleryModels
    {
        public int Id { get; set; }
        public string ImgUrl { get; set; }
        public string ImgHref { get; set; }
        public string Title { get; set; }
        public string ImgFileSize { get; set; }
    }
}