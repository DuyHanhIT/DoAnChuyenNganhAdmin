using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using WebAdminShoesStoreDACN.Controllers;
using WebAdminShoesStoreDACN.Models;

namespace WebAdminShoesStoreDACN.Areas.Admin.Controllers
{
    public class AccountsController : BaseController
    {
        // GET: Admin/Accounts
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public JsonResult GetAllAccount(string keysearch,int trang)
        {
            if(keysearch == null||keysearch=="")
            {
                keysearch = "=";
            }
            var t = new AccountController().checkToken(GlobalVariables.ExpiredDateTime, GlobalVariables.access_token, GlobalVariables.Username, GlobalVariables.Password);                  
            List<Account> accList = new List<Account>();
            GlobalVariables.WebApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            GlobalVariables.WebApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", t);            
            HttpResponseMessage reponse = GlobalVariables.WebApiClient.GetAsync("Admin/GetAllAccount/"+keysearch.ToString()).Result;
            if (reponse.IsSuccessStatusCode)
            {
                BaseResult s;

                s = reponse.Content.ReadAsAsync<BaseResult>().Result;
                var myJsonResponse = s.data.ToString().Trim().TrimStart('{').TrimEnd('}');
                accList = JsonConvert.DeserializeObject<List<Account>>(myJsonResponse);
                ViewBag.Message = s.Message;
                List<Account> accList1 = new List<Account>();
                accList1.Add(accList[0]);
                var ac = new Account();
                ac = accList[0];
                var pageSize = 3;
                var numPages = accList.Count() % pageSize==0?accList.Count/pageSize:(accList.Count / pageSize) +1;
                accList = accList.OrderBy(i=>i.roleid).ThenByDescending(i=>i.active).ToList();
                    var kqht = accList.Skip((trang - 1) * pageSize).Take(pageSize).ToList();

                    return Json(new BaseJsonResult(s.isSuccess, s.status, s.Message, kqht, numPages), JsonRequestBehavior.AllowGet);
                
                /*var kqht = accList.Skip((trang - 1) * pageSize).Take(pageSize).ToList();

                return Json(new BaseJsonResult(s.isSuccess, s.status, s.Message, kqht, numPages), JsonRequestBehavior.AllowGet);*/



                //return Json(new jsonResult<account>(s.isSuccess, s.status, s.Message, accList), JsonRequestBehavior.AllowGet);
                //return View(accList);

            }
            else
            {
                BaseResult s;
                s = reponse.Content.ReadAsAsync<BaseResult>().Result;
                ViewBag.Message = s.Message;
                ViewBag.count = accList.Count();
                //return View(accList);
                return Json(new BaseResult(s.isSuccess, s.status, s.Message, s.data), JsonRequestBehavior.AllowGet);


            }


        }
        
        [HttpGet]
        public JsonResult GetAccountByAccountId(int accountId)
        {
            var t = new AccountController().checkToken(GlobalVariables.ExpiredDateTime, GlobalVariables.access_token, GlobalVariables.Username, GlobalVariables.Password);
            
            List<Account> accList = new List<Account>();
            GlobalVariables.WebApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            GlobalVariables.WebApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", t);
            
            HttpResponseMessage reponse = GlobalVariables.WebApiClient.GetAsync("Admin/GetAccountByAccountId/" + accountId.ToString()).Result;
            if (reponse.IsSuccessStatusCode)
            {
                BaseResult s;
                s = reponse.Content.ReadAsAsync<BaseResult>().Result;
                var myJsonResponse = s.data.ToString().Trim().TrimStart('{').TrimEnd('}');
                accList = JsonConvert.DeserializeObject<List<Account>>(myJsonResponse);
                ViewBag.Message = s.Message;
                List<Account> accList1 = new List<Account>();
                accList1.Add(accList[0]);
                var ac = new Account();
                ac = accList[0];
                return Json(new BaseResult(s.isSuccess, s.status, s.Message, ac), JsonRequestBehavior.AllowGet);


            }
            else
            {
                BaseResult s;
                s = reponse.Content.ReadAsAsync<BaseResult>().Result;
                ViewBag.Message = s.Message;
                ViewBag.count = accList.Count();
                //return View(accList);
                return Json(new BaseResult(s.isSuccess, s.status, s.Message, s.data), JsonRequestBehavior.AllowGet);


            }


        }
        [HttpPost]
        public JsonResult AddAccount(string username, int? roleid)
        {
            
            var f = new Account();
            f.username = username;
            f.roleid = roleid; 
            GlobalVariables.WebApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsJsonAsync("Admin/AddAccount", f).Result;
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
                return Json(new BaseResult(s.isSuccess, s.status, s.Message, ac), JsonRequestBehavior.AllowGet);
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
                return Json(new BaseResult(s.isSuccess, s.status, s.Message, s.data), JsonRequestBehavior.AllowGet);
                


            }
        }
        [HttpPost]
        public JsonResult EditAccount(int accountId, int roleid, bool active)
        {
            try
            {
                var a = new Account();
                a.accountid = accountId;
                a.roleid = roleid;
                a.active = active;
                GlobalVariables.WebApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = GlobalVariables.WebApiClient.PutAsJsonAsync("Admin/UpdateAccount/" + a.accountid, a).Result;
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
                    return Json(new BaseResult(s.isSuccess, s.status, s.Message, accList1), JsonRequestBehavior.AllowGet);
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
                    return Json(new BaseResult(s.isSuccess, s.status, s.Message, s.data), JsonRequestBehavior.AllowGet);
                }
            }catch(Exception ex)
            {
                return Json(new BaseResult(false, 400, "failed", null), JsonRequestBehavior.AllowGet);

            }
        }
        [HttpPost]
        public JsonResult ChangeStatusAccount(int accountId)
        {
            try
            {
                var a = new Account();
                a.accountid = accountId;
                a.roleid=1;
                GlobalVariables.WebApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = GlobalVariables.WebApiClient.PutAsJsonAsync("Admin/DeleteAccount/" + a.accountid, a).Result;
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
                    return Json(new BaseResult(s.isSuccess, s.status, s.Message, accList1), JsonRequestBehavior.AllowGet);
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
                    return Json(new BaseResult(s.isSuccess, s.status, s.Message, s.data), JsonRequestBehavior.AllowGet);
                }
            }catch(Exception ex)
            {
                return Json(new BaseResult(false, 400, "failed", null), JsonRequestBehavior.AllowGet);

            }
        }
        
        [HttpPost]
        public JsonResult ResetPassword(int accountId)
        {
            try
            {
                var a = new Account();
                a.accountid = accountId;
                a.roleid=1;
                GlobalVariables.WebApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = GlobalVariables.WebApiClient.PutAsJsonAsync("Admin/ResetPassword/" + a.accountid, a).Result;
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
                    return Json(new BaseResult(s.isSuccess, s.status, s.Message, accList1), JsonRequestBehavior.AllowGet);
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
                    return Json(new BaseResult(s.isSuccess, s.status, s.Message, s.data), JsonRequestBehavior.AllowGet);
                }
            }catch(Exception ex)
            {
                return Json(new BaseResult(false, 400, "failed", null), JsonRequestBehavior.AllowGet);

            }
        }
        [HttpGet]
        public JsonResult GetAllAccountNotExistsINEmployee()
        {
           
            var t = new AccountController().checkToken(GlobalVariables.ExpiredDateTime, GlobalVariables.access_token, GlobalVariables.Username, GlobalVariables.Password);

            List<Account> accList = new List<Account>();
            GlobalVariables.WebApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            GlobalVariables.WebApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", t);

            HttpResponseMessage reponse = GlobalVariables.WebApiClient.GetAsync("Admin/GetAllAccountNotExistsINEmployee" ).Result;
            if (reponse.IsSuccessStatusCode)
            {
                BaseResult s;
                s = reponse.Content.ReadAsAsync<BaseResult>().Result;
                var myJsonResponse = s.data.ToString().Trim().TrimStart('{').TrimEnd('}');
                accList = JsonConvert.DeserializeObject<List<Account>>(myJsonResponse);
                var ac = new Account();
                ac = accList[0];
               

                return Json(new BaseResult(s.isSuccess, s.status, s.Message,accList), JsonRequestBehavior.AllowGet);

            }
            else
            {
                BaseResult s;
                s = reponse.Content.ReadAsAsync<BaseResult>().Result;
                return Json(new BaseResult(s.isSuccess, s.status, s.Message, s.data), JsonRequestBehavior.AllowGet);


            }


        }
        [HttpGet]
        public JsonResult GetAllAccountForOrder()
        {

            var t = new AccountController().checkToken(GlobalVariables.ExpiredDateTime, GlobalVariables.access_token, GlobalVariables.Username, GlobalVariables.Password);

            List<Account> accList = new List<Account>();
            GlobalVariables.WebApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            GlobalVariables.WebApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", t);

            HttpResponseMessage reponse = GlobalVariables.WebApiClient.GetAsync("Admin/GetAllAccount/=" ).Result;
            if (reponse.IsSuccessStatusCode)
            {
                BaseResult s;
                s = reponse.Content.ReadAsAsync<BaseResult>().Result;
                var myJsonResponse = s.data.ToString().Trim().TrimStart('{').TrimEnd('}');
                accList = JsonConvert.DeserializeObject<List<Account>>(myJsonResponse);
                var ac = new Account();
                ac = accList[0];


                return Json(new BaseResult(s.isSuccess, s.status, s.Message, accList), JsonRequestBehavior.AllowGet);
            }
            else
            {
                BaseResult s;
                s = reponse.Content.ReadAsAsync<BaseResult>().Result;
                return Json(new BaseResult(s.isSuccess, s.status, s.Message, s.data), JsonRequestBehavior.AllowGet);

            }


        }
        public ActionResult Logout()
        {
            GlobalVariables.access_token = "";
            GlobalVariables.Username = "";
            GlobalVariables.Password = "";
            GlobalVariables.Avatar = "";
            GlobalVariables.accountid = 0;
            GlobalVariables.roleId = 0;
            GlobalVariables.ExpiredDateTime = DateTime.Now;
            return RedirectToAction("Login");
        }

        public ActionResult hihi()
        {
            return View();
        }
    }
}