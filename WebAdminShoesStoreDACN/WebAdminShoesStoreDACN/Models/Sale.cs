using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebAdminShoesStoreDACN.Models
{
    public class Sale
    {
        public int saleid { get; set; }

        public string salename { get; set; }

        public DateTime? createdate { get; set; }

        public int? createby { get; set; }

        public int? updateby { get; set; }

        public DateTime? startday { get; set; }

        public DateTime? endday { get; set; }

        public string content { get; set; }

        public decimal? percent { get; set; }
        public string imgsale { get; set; }
        public string createByA { get; set; }
        public string updateByA { get; set; }
    }
}