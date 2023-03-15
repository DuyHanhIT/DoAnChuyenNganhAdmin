using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAdminShoesStoreDACN.Models
{
    public class Token
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
        public string error_description { get; set; }

    }
}