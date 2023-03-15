using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using WebAdminShoesStoreDACN.Areas.Admin.Controllers;
using WebAdminShoesStoreDACN.Models;
namespace WebAdminShoesStoreDACN.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }
        [HttpGet]
        public ActionResult Login()
        {
            if (!GlobalVariables.access_token.Equals("") || GlobalVariables.access_token == null)
            {
                return RedirectToAction("OrderIndex", "Admin/Orders");
            }
            else
            {
                return View();

            }
            // return View();


        }
        [HttpPost]
        public ActionResult Login(FormCollection userlog)
        {
            string username = userlog["username"].ToString();
            string password = userlog["password"].ToString();
            try
            {
                int g = 0;
                var acc = new Account();
                
                acc.username = username;
                acc.password = GetMD5(password);
                GlobalVariables.WebApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsJsonAsync("Admin/Login", acc).Result;
                List<Account> accList = new List<Account>();
                if (response.IsSuccessStatusCode)
                {

                    BaseResult s;
                    s = response.Content.ReadAsAsync<BaseResult>().Result;
                    var myJsonResponse = s.data.ToString().Trim().TrimStart('{').TrimEnd('}');
                    accList = JsonConvert.DeserializeObject<List<Account>>(myJsonResponse);
                    ViewBag.Message = s.Message;
                    List<Account> accList1 = new List<Account>();
                    accList1.Add(accList[0]);
                    var ac = new Account();
                    ac = accList[0];
                    var m = getToken(username, password);
                    // experitime,now
                    GlobalVariables.Username = acc.username;
                    GlobalVariables.Password = acc.password;
                    GlobalVariables.roleId = (int)ac.roleid;
                    GlobalVariables.accountid = ac.accountid;
                    GlobalVariables.Avatar = ac.avatar;

                    if (ac.roleid == 1 )
                    {
                        
                        Session["Staff"] = ac;
                        ViewData["ER"] = "";
                        return RedirectToAction("StatisticalIndex", "Admin/Statistical");
                    }
                    else
                    if (ac.roleid == 2)
                    {
                        ViewData["ER"] = "";
                        return RedirectToAction("OrderIndex", "Admin/Orders");
                    }
                    else
                    {
                        GlobalVariables.Username = "";
                        GlobalVariables.Password = "";
                        ViewData["ER"] = "Bạn phải có quyền là admin hoặc staff";
                        acc.password = "";
                        return View(acc);
                    }
                    //return Json(new BaseResult(s.isSuccess, s.status, s.Message, ac), JsonRequestBehavior.AllowGet);

                    //return Json(new jsonResult<account>(s.isSuccess, s.status, s.Message, accList), JsonRequestBehavior.AllowGet);
                    // return View(accList);


                }
                else
                {
                    BaseResult s;
                    s = response.Content.ReadAsAsync<BaseResult>().Result;
                    ViewBag.Message = s.Message;
                    ViewBag.count = accList.Count();
                    //return View(accList);
                    //return Json(new BaseResult(s.isSuccess, s.status, s.Message, s.data), JsonRequestBehavior.AllowGet);
                    ViewData["ER"] = s.Message;
                    acc.password = "";
                    return View(acc);


                }
               /* int g = 0;
                var acc = new Account();
                if (username.Length == 0)
                {
                    ViewData["ER"] = "Bạn không được để trống tên đăng nhập";
                    acc.password = "";
                    return View(acc);
                }else if (password.Length == 0)
                {
                    ViewData["ER"] = "Bạn không được để trống mật khẩu";
                    acc.password = "";
                    return View(acc);
                }
                else {
                    acc.username = username;
                    acc.password = GetMD5(password);
                    GlobalVariables.WebApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsJsonAsync("Admin/Login", acc).Result;
                    List<Account> accList = new List<Account>();
                    if (response.IsSuccessStatusCode)
                    {

                        BaseResult s;
                        s = response.Content.ReadAsAsync<BaseResult>().Result;
                        var myJsonResponse = s.data.ToString().Trim().TrimStart('{').TrimEnd('}');
                        accList = JsonConvert.DeserializeObject<List<Account>>(myJsonResponse);
                        ViewBag.Message = s.Message;
                        List<Account> accList1 = new List<Account>();
                        accList1.Add(accList[0]);
                        var ac = new Account();
                        ac = accList[0];
                        var m = getToken(username, password);
                        // experitime,now
                        GlobalVariables.Username = acc.username;
                        GlobalVariables.Password = acc.password;
                        GlobalVariables.roleId = (int)ac.roleid;
                        GlobalVariables.accountid = ac.accountid;
                        GlobalVariables.Avatar = ac.avatar;

                        if (ac.roleid == 1 *//*|| islogin.MaQuyen == 2*//*)
                        {
                            *//* Session["Admin"] = ac;
                             Session["Staff"] = ac;*//*
                            ViewData["ER"] = "";
                            return RedirectToAction("StatisticalIndex", "Admin/Statistical");
                        }
                        else
                        if (ac.roleid == 2)
                        {
                            *//*Session["Staff"] = ac;*//*
                            ViewData["ER"] = "";
                            return RedirectToAction("OrderIndex", "Admin/Orders");
                        }
                        else
                        {
                            GlobalVariables.Username = "";
                            GlobalVariables.Password = "";
                            ViewData["ER"] = "Bạn phải có quyền là admin hoặc staff";
                            acc.password = "";
                            return View(acc);
                        }
                        //return Json(new BaseResult(s.isSuccess, s.status, s.Message, ac), JsonRequestBehavior.AllowGet);

                        //return Json(new jsonResult<account>(s.isSuccess, s.status, s.Message, accList), JsonRequestBehavior.AllowGet);
                        // return View(accList);


                    }
                    else
                    {
                        BaseResult s;
                        s = response.Content.ReadAsAsync<BaseResult>().Result;
                        ViewBag.Message = s.Message;
                        ViewBag.count = accList.Count();
                        //return View(accList);
                        //return Json(new BaseResult(s.isSuccess, s.status, s.Message, s.data), JsonRequestBehavior.AllowGet);
                        ViewData["ER"] = s.Message;
                        acc.password = "";
                        return View(acc);


                    }
                }*/


            }
            catch (Exception ex)
            {
                ViewData["ER"] = "Đăng nhập thất bại 1";
                var acc1 = new Account();
                acc1.username = username;
                acc1.password = "";
                return View(acc1);
            }
        }
        [HttpPost]
        public Token getToken(string username, string password)
        {
            var t = new Token();
            string userName = username;
            string passWord = GetMD5(password);
            var strEncode = EncodeTo64($"{userName}:{passWord}");
            GlobalVariables.WebApiClient.DefaultRequestHeaders.Clear();
            GlobalVariables.WebApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
            GlobalVariables.WebApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", strEncode);
            var RequestBody = new Dictionary<string, string>
            {
            {"grant_type", "password"},
            {"username", userName},
            {"password", passWord},
            };
            HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsync("token", new FormUrlEncodedContent(RequestBody)).Result;

            if (response.IsSuccessStatusCode)
            {
                var JsonContent = response.Content.ReadAsStringAsync().Result;
                t = JsonConvert.DeserializeObject<Token>(JsonContent);
                t.error_description = null;
                GlobalVariables.access_token = t.access_token;
                GlobalVariables.ExpiredDateTime = DateTime.Now.AddSeconds(t.expires_in);


            }
            else
            {

                var JsonContent = response.Content.ReadAsStringAsync().Result;
                t = JsonConvert.DeserializeObject<Token>(JsonContent);
                GlobalVariables.access_token = "";
                GlobalVariables.ExpiredDateTime = DateTime.Now;
                //t.error_description = "GetAccessToken failed likely due to an invalid client ID, client secret, or invalid usrename and password";
            }
            return t;
            /*WebApiClient.BaseAddress = new Uri("http://192.168.0.21/ShoesStore.com/api/");
            WebApiClient.DefaultRequestHeaders.Clear();
            WebApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));*/





        }
        static public string EncodeTo64(string toEncode)
        {
            byte[] toEncodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(toEncode);
            string returnValue = System.Convert.ToBase64String(toEncodeAsBytes);
            return returnValue;
        }
        public double daysBetween(DateTime StartDate, DateTime EndDate)
        {
            DateTime d = EndDate;
            DateTime g = StartDate;
            return (EndDate - StartDate).TotalDays;
        }
        public string checkToken(DateTime ExpiredDateTime, string token1, string userName, string passWord)
        {

            double difference = daysBetween(DateTime.Now, ExpiredDateTime);
            /*Thread.Sleep(1000);*/
            if (userName == "" || passWord == "")
            {
                GlobalVariables.access_token = "";
            }
            else if (token1 != "" && difference > 0)
            {
                string access_token1 = GlobalVariables.access_token;
                GlobalVariables.access_token = access_token1;
            }
            else if (token1 != "" && difference <= 0)
            {
                getToken(userName, passWord);
                string access_token2 = GlobalVariables.access_token;
                GlobalVariables.access_token = access_token2;
            }
            else if (userName != "" && passWord != "" && token1 == "")
            {
                getToken(userName, passWord);
                string access_token = GlobalVariables.access_token;
                GlobalVariables.access_token = access_token;
            }
            else
            {
                GlobalVariables.access_token = "";
            }
            return GlobalVariables.access_token;
        }
        /*

         //check token
         Future<String> checkToken(DateTime ExpiredDateTime, String token1,
             String? userName, String? passWord) async {
           final expiredDateTime = ExpiredDateTime;
           final dateNow = DateTime.now();
           int difference = daysBetween(dateNow, expiredDateTime);
           await Future.delayed(Duration(seconds: 1));
           if (userName == null || passWord == null) {
             return token = '';
           } else if (token1 != '' && difference > 0) {
             return token = token;
           } else if (token1 != '' && difference <= 0) {
             getToken(userName, passWord);
             return token = token;
           } else if (userName != null && passWord != null && token1 == '') {
             getToken(userName, passWord);
             return token = token;
           } else {
             return token = "";
           }
         }

         
                    */

        public ActionResult Index()
        {
            if (Session["Staff"] == null)
            {
                ViewData["ER"] = "";
                return RedirectToAction("Login");

            }
            else
            {
                return View();
            }
        }

        // GET: Account/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Account/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Account/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Account/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Account/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Account/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Account/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
