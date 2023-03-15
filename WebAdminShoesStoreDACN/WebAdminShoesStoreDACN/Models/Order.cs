using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebAdminShoesStoreDACN.Models
{
    public class Order
    {
        public int orderid { get; set; }

        public int? accountid { get; set; }

        public int? statusid { get; set; }

        public DateTime? createdate { get; set; }

        public DateTime? deliverydate { get; set; }

        [StringLength(200)]
        public string firstName { get; set; }

        [StringLength(11)]
        public string phone { get; set; }

        [StringLength(150)]
        public string email { get; set; }

        public string note { get; set; }

        public decimal? total { get; set; }

        public bool? payment { get; set; }

        public string momo { get; set; }

        [StringLength(200)]
        public string lastName { get; set; }

        public string address { get; set; }
        public string statusname { get; set; }
        public string username { get; set; }

    }
}