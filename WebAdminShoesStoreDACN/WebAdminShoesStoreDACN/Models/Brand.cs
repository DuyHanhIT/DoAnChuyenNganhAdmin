using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebAdminShoesStoreDACN.Models
{
    public class Brand
    {
        public int brandid { get; set; }

        [StringLength(100)]
        public string brandname { get; set; }

        [Column(TypeName = "ntext")]
        public string information { get; set; }

        [Column(TypeName = "text")]
        public string logo { get; set; }
    }
}