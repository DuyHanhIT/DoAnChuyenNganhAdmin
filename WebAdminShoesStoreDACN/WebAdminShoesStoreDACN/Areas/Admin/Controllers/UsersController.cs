using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using WebAdminShoesStoreDACN.Models;
using WebAdminShoesStoreDACN.Controllers;

namespace WebAdminShoesStoreDACN.Areas.Admin.Controllers
{
    public class UsersController : BaseController
    {
        // GET: Admin/Users
        public ActionResult UserIndex()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetAllUser(string keysearch, int trang)
        {

            try
            {
                if (keysearch == null || keysearch == "")
                {
                    keysearch = "=";
                }
                var t = new AccountController().checkToken(GlobalVariables.ExpiredDateTime, GlobalVariables.access_token, GlobalVariables.Username, GlobalVariables.Password);

                List<User> userList = new List<User>();
                GlobalVariables.WebApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                GlobalVariables.WebApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", t);

                HttpResponseMessage reponse = GlobalVariables.WebApiClient.GetAsync("Admin/GetAllUser/" + keysearch.ToString()).Result;
                if (reponse.IsSuccessStatusCode)
                {
                    BaseResult s;
                    s = reponse.Content.ReadAsAsync<BaseResult>().Result;
                    var myJsonResponse = s.data.ToString().Trim().TrimStart('{').TrimEnd('}');
                    userList = JsonConvert.DeserializeObject<List<User>>(myJsonResponse);
                    var u = new User();
                    u = userList[0];
                    var pageSize = 2;
                    var numPages = userList.Count() % pageSize == 0 ? userList.Count / pageSize : (userList.Count / pageSize) + 1;
                    userList = userList.ToList();
                    var kqht = userList.Skip((trang - 1) * pageSize).Take(pageSize).ToList();
                    return Json(new BaseJsonResult(s.isSuccess, s.status, s.Message, kqht, numPages), JsonRequestBehavior.AllowGet);

                }
                else
                {
                    BaseResult s;
                    s = reponse.Content.ReadAsAsync<BaseResult>().Result;
                    return Json(new BaseResult(s.isSuccess, s.status, s.Message, s.data), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new BaseResult(false, 500, "Server error", null), JsonRequestBehavior.AllowGet);
            }
        }
        public User GetUserByAccountID(int accountId)
        {
            var t = new AccountController().checkToken(GlobalVariables.ExpiredDateTime,GlobalVariables.access_token,GlobalVariables.Username,GlobalVariables.Password);
            List<User> userList = new List<User>();
            GlobalVariables.WebApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            GlobalVariables.WebApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", t);
            
            HttpResponseMessage reponse = GlobalVariables.WebApiClient.GetAsync("getUserByAccountId/" + accountId.ToString()).Result;
            if (reponse.IsSuccessStatusCode)
            {
                BaseResult s;
                s = reponse.Content.ReadAsAsync<BaseResult>().Result;
                var myJsonResponse = s.data.ToString().Trim().TrimStart('{').TrimEnd('}');
                userList = JsonConvert.DeserializeObject<List<User>>(myJsonResponse);
                ViewBag.Message = s.Message;
                
                var ac = new User();
                ac = userList[0];
                return ac;

                //return Json(new jsonResult<account>(s.isSuccess, s.status, s.Message, accList), JsonRequestBehavior.AllowGet);
                //return View(accList);

            }
            else
            {
                BaseResult s;
                s = reponse.Content.ReadAsAsync<BaseResult>().Result;
                ViewBag.Message = s.Message;
                ViewBag.count = userList.Count();
                //return View(accList);
                var ac = new User();
                
                return ac;

            } 
        }
        [HttpGet]
        public JsonResult GetUserByUserId(int userid)
        {
            var t = new AccountController().checkToken(GlobalVariables.ExpiredDateTime,GlobalVariables.access_token,GlobalVariables.Username,GlobalVariables.Password);
            List<User> userList = new List<User>();
            GlobalVariables.WebApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            GlobalVariables.WebApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", t);
            
            HttpResponseMessage reponse = GlobalVariables.WebApiClient.GetAsync("Admin/GetUserByUserId/" + userid.ToString()).Result;
            if (reponse.IsSuccessStatusCode)
            {
                BaseResult s;
                s = reponse.Content.ReadAsAsync<BaseResult>().Result;
                var myJsonResponse = s.data.ToString().Trim().TrimStart('{').TrimEnd('}');
                userList = JsonConvert.DeserializeObject<List<User>>(myJsonResponse);
                ViewBag.Message = s.Message;
                
                var ac = new User();
                ac = userList[0];
                return Json(new BaseResult(s.isSuccess, s.status, s.Message, ac), JsonRequestBehavior.AllowGet);


            }
            else
            {
                BaseResult s;
                s = reponse.Content.ReadAsAsync<BaseResult>().Result;
                ViewBag.Message = s.Message;
                ViewBag.count = userList.Count();
                //return View(accList);
                var ac = new User();

                return Json(new BaseResult(false, 500, "Server error 1", null), JsonRequestBehavior.AllowGet);



            }
        }
        

        // GET: Admin/Users/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Users/Create
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

        // GET: Admin/Users/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/Users/Edit/5
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

        // GET: Admin/Users/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/Users/Delete/5
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
