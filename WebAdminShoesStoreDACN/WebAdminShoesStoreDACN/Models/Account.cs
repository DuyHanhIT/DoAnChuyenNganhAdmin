using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebAdminShoesStoreDACN.Models
{
    public class Account
    {
        public int accountid { get; set; }

        public int? roleid { get; set; }

        [StringLength(100)]
        public string username { get; set; }

        [StringLength(100)]
        public string password { get; set; }

        public DateTime? createdate { get; set; }

        public bool active { get; set; }
        [StringLength(100)]
        public string rolename { get; set; }
        public string avatar { get; set; }

    }
}