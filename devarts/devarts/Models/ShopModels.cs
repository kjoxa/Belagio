using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace devarts.Models
{
    //https://iprogramista.pl/code/przelewy24-sdk

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Order
    {
        public int Id { get; set; }
        public string OrderId = Guid.NewGuid().ToString();
        public List<Product> Products { get; set; }
    }
}