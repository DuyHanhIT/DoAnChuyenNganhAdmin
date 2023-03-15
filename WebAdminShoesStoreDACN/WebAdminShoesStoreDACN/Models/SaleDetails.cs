using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAdminShoesStoreDACN.Models
{
    public class SaleDetails
    {
        public int saleid { get; set; }

        public int shoeid { get; set; }

        public decimal? saleprice { get; set; }

        public int? updateby { get; set; }

        public bool active { get; set; }
        public string shoename { get; set; }
        public string image1 { get; set; }
        public decimal? price { get; set; }
        public decimal? percent { get; set; }


    }
}