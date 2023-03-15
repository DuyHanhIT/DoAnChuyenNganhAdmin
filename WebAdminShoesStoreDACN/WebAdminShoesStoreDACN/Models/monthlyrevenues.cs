using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAdminShoesStoreDACN.Models
{
    public class monthlyrevenues
    {
        public string month { get; set; }

        public string year { get; set; }
        public string day { get; set; }
        public string brandname { get; set; }
        public string shoename { get; set; }
        public int quarter { get; set; }

        public int? sellnumber { get; set; }

        public decimal? turnover { get; set; }
    }
}