using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebAdminShoesStoreDACN.Models
{
    public class Role
    {
        public int roleid { get; set; }

        [StringLength(100)]
        public string rolename { get; set; }
    }
}