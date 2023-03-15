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
    public class EmployeeController : BaseController
    {
        // GET: Admin/Employee
        public ActionResult EmployeeIndex()
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

                List<employee> empList = new List<employee>();
                GlobalVariables.WebApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                GlobalVariables.WebApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", t);

                HttpResponseMessage reponse = GlobalVariables.WebApiClient.GetAsync("Admin/GetAllEmployee/" + keysearch.ToString()).Result;
                if (reponse.IsSuccessStatusCode)
                {
                    BaseResult s;
                    s = reponse.Content.ReadAsAsync<BaseResult>().Result;
                    var myJsonResponse = s.data.ToString().Trim().TrimStart('{').TrimEnd('}');
                    empList = JsonConvert.DeserializeObject<List<employee>>(myJsonResponse);
                    var u = new employee();
                    u = empList[0];
                    var pageSize = 2;
                    var numPages = empList.Count() % pageSize == 0 ? empList.Count / pageSize : (empList.Count / pageSize) + 1;
                    empList = empList.ToList();
                    var kqht = empList.Skip((trang - 1) * pageSize).Take(pageSize).ToList();
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
        [HttpGet]
        public JsonResult GetEmployeeByEmployeeId(int epid)
        {
            var t = new AccountController().checkToken(GlobalVariables.ExpiredDateTime, GlobalVariables.access_token, GlobalVariables.Username, GlobalVariables.Password);
            List<employee> epList = new List<employee>();
            GlobalVariables.WebApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            GlobalVariables.WebApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", t);

            HttpResponseMessage reponse = GlobalVariables.WebApiClient.GetAsync("Admin/GetEmployeeByEmployeeId/" + epid.ToString()).Result;
            if (reponse.IsSuccessStatusCode)
            {
                BaseResult s;
                s = reponse.Content.ReadAsAsync<BaseResult>().Result;
                var myJsonResponse = s.data.ToString().Trim().TrimStart('{').TrimEnd('}');
                epList = JsonConvert.DeserializeObject<List<employee>>(myJsonResponse);

                var ac = new employee();
                ac = epList[0];
                return Json(new BaseResult(s.isSuccess, s.status, s.Message, ac), JsonRequestBehavior.AllowGet);


            }
            else
            {
                BaseResult s;
                s = reponse.Content.ReadAsAsync<BaseResult>().Result;

                return Json(new BaseResult(s.isSuccess, s.status, s.Message, null), JsonRequestBehavior.AllowGet);



            }
        }
        [HttpPost]
        public JsonResult UpdateEmployee(int epid, int accountid, string firstName, string lastName,DateTime birthday, bool gender,string phone, string address, string avatar)
        {
            try
            {
                var a = new employee();
                a.epid = epid;
                a.accountid = accountid;
                a.firstName = firstName;
                a.lastName = lastName ;
                a.birthday = birthday;
                a.gender = gender;
                a.phone = phone;
                a.address = address;
                a.avatar = avatar;
                var t = new AccountController().checkToken(GlobalVariables.ExpiredDateTime, GlobalVariables.access_token, GlobalVariables.Username, GlobalVariables.Password);
                GlobalVariables.WebApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                GlobalVariables.WebApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", t);
                HttpResponseMessage response = GlobalVariables.WebApiClient.PutAsJsonAsync("Admin/UpdateEmployee/" + epid, a).Result;
                if (response.IsSuccessStatusCode)
                {
                    BaseResult s;
                    s = response.Content.ReadAsAsync<BaseResult>().Result;
                    return Json(new BaseResult(s.isSuccess, s.status, s.Message, null), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    BaseResult s;
                    s = response.Content.ReadAsAsync<BaseResult>().Result;
                    return Json(new BaseResult(s.isSuccess, s.status, s.Message, s.data), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new BaseResult(false, 400, "failed", null), JsonRequestBehavior.AllowGet);

            }
        }
        [HttpPost]
        public JsonResult AddEmployye( int? accountid, string firstName, string lastName, DateTime? birthday, bool? gender, string phone, string address, string avatar)
        {

            var f = new employee();
            f.accountid = accountid;
            f.firstName = firstName;
            f.lastName = lastName;
            f.birthday = birthday;
            f.gender = gender;
            f.phone = phone;
            f.address = address;
            f.avatar = avatar;
            var t = new AccountController().checkToken(GlobalVariables.ExpiredDateTime, GlobalVariables.access_token, GlobalVariables.Username, GlobalVariables.Password);
            GlobalVariables.WebApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            GlobalVariables.WebApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", t);
            HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsJsonAsync("Admin/AddEmployee", f).Result;

            if (response.IsSuccessStatusCode)
            {
                BaseResult s;
                s = response.Content.ReadAsAsync<BaseResult>().Result;
                return Json(new BaseResult(s.isSuccess, s.status, s.Message, s.data), JsonRequestBehavior.AllowGet);
            }
            else
            {
                BaseResult s;
                s = response.Content.ReadAsAsync<BaseResult>().Result;
                return Json(new BaseResult(s.isSuccess, s.status, s.Message, s.data), JsonRequestBehavior.AllowGet);



            }
        }
    }
}