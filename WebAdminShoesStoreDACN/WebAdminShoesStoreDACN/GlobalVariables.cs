using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace WebAdminShoesStoreDACN
{
    public class GlobalVariables
    {
        public static string access_token ="";
        public static string Username = "";
        public static string Password = "";
        public static string Avatar = "";
        public static int accountid ;
        public static int roleId = 0;
        public static DateTime ExpiredDateTime = DateTime.Now;

        public static HttpClient WebApiClient = new HttpClient();
        static GlobalVariables()
        {
            
            WebApiClient.BaseAddress = new Uri("http://10.14.116.143/ShoesStore.com/api/");
            WebApiClient.DefaultRequestHeaders.Clear();
            
        }
    }
}