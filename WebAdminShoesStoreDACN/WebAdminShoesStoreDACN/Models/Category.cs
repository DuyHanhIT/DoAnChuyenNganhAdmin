using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebAdminShoesStoreDACN.Models
{
    public class Category
    {
        public int categoryid { get; set; }

        [StringLength(100)]
        public string categoryname { get; set; }
    }
}