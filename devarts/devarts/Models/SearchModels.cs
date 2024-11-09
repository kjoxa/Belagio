using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace devarts.Models
{
    public class Search
    {
        public string cathegory { get; set; }
        public string content { get; set; }
        public string Other { get; set; }
    }

    public class SearchWithNews
    {
        public IPagedList<PostWithMainImageUrl> Post { get; set; }
        public Search Search { get; set; }
    }
}