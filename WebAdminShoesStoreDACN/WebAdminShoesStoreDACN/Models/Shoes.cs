using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebAdminShoesStoreDACN.Models
{
    public class Shoes
    {
        public int shoeid { get; set; }

        public int? brandid { get; set; }

        public int? categoryid { get; set; }

        [StringLength(100)]
        public string shoename { get; set; }
        public string categoryname { get; set; }
        public string brandname { get; set; }

        public decimal? price { get; set; }

        [Column(TypeName = "ntext")]
        public string description { get; set; }

        public int? stock { get; set; }

        public int? purchased { get; set; }

        public bool? shoenew { get; set; }

        public DateTime? createdate { get; set; }

        public DateTime? dateupdate { get; set; }

        public int? updateby { get; set; }

        public bool active { get; set; }

        public int? createby { get; set; }

        [StringLength(400)]
        public string image1 { get; set; }

        [StringLength(400)]
        public string image2 { get; set; }

        [StringLength(400)]
        public string image3 { get; set; }

        [StringLength(400)]
        public string image4 { get; set; }
        public string createByA { get; set; }
        public string updateByA { get; set; }
        public decimal? rate { get; set; }

    }
}