using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebAdminShoesStoreDACN.Models
{
    public class employee
    {
        public int epid { get; set; }

        public int? accountid { get; set; }

        [StringLength(200)]
        public string firstName { get; set; }

        public DateTime? birthday { get; set; }

        public bool? gender { get; set; }

        [StringLength(11)]
        public string phone { get; set; }

        [StringLength(200)]
        public string lastName { get; set; }

        [StringLength(200)]
        public string address { get; set; }

        [StringLength(400)]
        public string avatar { get; set; }
        public string username { get; set; }

    }
}