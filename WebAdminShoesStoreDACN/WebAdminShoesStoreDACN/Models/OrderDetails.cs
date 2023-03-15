using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAdminShoesStoreDACN.Models
{
    public class OrderDetails
    {
        public int? orderid { get; set; }


        public int? shoeid { get; set; }

        public int? quantity { get; set; }

        public int size { get; set; }

        public decimal? price { get; set; }

        public decimal? total { get; set; }
        public string statusname { get; set; }
        public int statusid { get; set; }
        public string shoename { get; set; }
        public string image1 { get; set; }
    }
}