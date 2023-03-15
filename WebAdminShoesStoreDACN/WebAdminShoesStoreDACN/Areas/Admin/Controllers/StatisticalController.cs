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
    public class StatisticalController : BaseController
    {
        
        // GET: Admin/Statistical
        public ActionResult StatisticalIndex()
        {
            return View();
        }
        [HttpGet]
        public JsonResult GetStatisticalOrderList(int day, int month, int year, int type)
        {
            var t = new AccountController().checkToken(GlobalVariables.ExpiredDateTime, GlobalVariables.access_token, GlobalVariables.Username, GlobalVariables.Password);
            List<StatisticalOrder> StatisticalOrderList = new List<StatisticalOrder>();
            GlobalVariables.WebApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            GlobalVariables.WebApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", t);

            HttpResponseMessage reponse = GlobalVariables.WebApiClient.GetAsync("Admin/getStatisticalOrder/"+day.ToString()+"/"+month.ToString()+"/"+year.ToString()+"/"+type.ToString() ).Result;
            if (reponse.IsSuccessStatusCode)

            {
                BaseResult s;
                s = reponse.Content.ReadAsAsync<BaseResult>().Result;
                var myJsonResponse = s.data.ToString().Trim().TrimStart('{').TrimEnd('}');
                StatisticalOrderList = JsonConvert.DeserializeObject<List<StatisticalOrder>>(myJsonResponse);                
                return Json(new BaseResult(s.isSuccess, s.status, s.Message, StatisticalOrderList), JsonRequestBehavior.AllowGet);                                
            }
            else
            {
                BaseResult s;
                s = reponse.Content.ReadAsAsync<BaseResult>().Result;
                //return View(accList);
                return Json(new BaseResult(s.isSuccess, s.status, s.Message, s.data), JsonRequestBehavior.AllowGet);
            }
        }                
        [HttpGet]
        public JsonResult getStatisticalBrand(int day, int month, int year, int time)
        {
            var t = new AccountController().checkToken(GlobalVariables.ExpiredDateTime, GlobalVariables.access_token, GlobalVariables.Username, GlobalVariables.Password);
            List<monthlyrevenues> StatisticalOrderList = new List<monthlyrevenues>();
            GlobalVariables.WebApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            GlobalVariables.WebApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", t);

            HttpResponseMessage reponse = GlobalVariables.WebApiClient.GetAsync("Admin/getStatisticalBrand/" + day.ToString()+"/"+month.ToString()+"/"+year.ToString()+"/"+time.ToString() ).Result;
            if (reponse.IsSuccessStatusCode)

            {
                BaseResult s;
                s = reponse.Content.ReadAsAsync<BaseResult>().Result;
                var myJsonResponse = s.data.ToString().Trim().TrimStart('{').TrimEnd('}');
                StatisticalOrderList = JsonConvert.DeserializeObject<List<monthlyrevenues>>(myJsonResponse);
                
                return Json(new BaseResult(s.isSuccess, s.status, s.Message, StatisticalOrderList), JsonRequestBehavior.AllowGet);
                                
            }
            else
            {
                BaseResult s;
                s = reponse.Content.ReadAsAsync<BaseResult>().Result;
                //return View(accList);
                return Json(new BaseResult(s.isSuccess, s.status, s.Message, s.data), JsonRequestBehavior.AllowGet);


            }


        }
        [HttpGet]
        public JsonResult getStatisticalShoes(int day, int month, int year, int time,int brand)
        {
            var t = new AccountController().checkToken(GlobalVariables.ExpiredDateTime, GlobalVariables.access_token, GlobalVariables.Username, GlobalVariables.Password);
            List<monthlyrevenues> StatisticalOrderList = new List<monthlyrevenues>();
            GlobalVariables.WebApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            GlobalVariables.WebApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", t);

            HttpResponseMessage reponse = GlobalVariables.WebApiClient.GetAsync("Admin/getStatisticalShoes/" + day.ToString()+"/"+month.ToString()+"/"+year.ToString()+"/"+time.ToString()+"/"+brand.ToString() ).Result;
            if (reponse.IsSuccessStatusCode)

            {
                BaseResult s;
                s = reponse.Content.ReadAsAsync<BaseResult>().Result;
                var myJsonResponse = s.data.ToString().Trim().TrimStart('{').TrimEnd('}');
                StatisticalOrderList = JsonConvert.DeserializeObject<List<monthlyrevenues>>(myJsonResponse);
                
                return Json(new BaseResult(s.isSuccess, s.status, s.Message, StatisticalOrderList), JsonRequestBehavior.AllowGet);
                                
            }
            else
            {
                BaseResult s;
                s = reponse.Content.ReadAsAsync<BaseResult>().Result;
                //return View(accList);
                return Json(new BaseResult(s.isSuccess, s.status, s.Message, s.data), JsonRequestBehavior.AllowGet);


            }


        }
        [HttpGet]
        public JsonResult getStatisticalMonth(int month, int year,int type)
        {
            var type1 = 1;
            var t = new AccountController().checkToken(GlobalVariables.ExpiredDateTime, GlobalVariables.access_token, GlobalVariables.Username, GlobalVariables.Password);

            List<monthlyrevenues> monthlyrevenuesList = new List<monthlyrevenues>();
            GlobalVariables.WebApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            GlobalVariables.WebApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", t);

            HttpResponseMessage reponse = GlobalVariables.WebApiClient.GetAsync("Admin/getStatisticalMonth/"+month.ToString()+"/"+ year.ToString()+"/"+type.ToString()).Result;
            if (reponse.IsSuccessStatusCode)
            {
                BaseResult s;
                s = reponse.Content.ReadAsAsync<BaseResult>().Result;
                var myJsonResponse = s.data.ToString().Trim().TrimStart('{').TrimEnd('}');
                monthlyrevenuesList = JsonConvert.DeserializeObject<List<monthlyrevenues>>(myJsonResponse);
                var r =monthlyrevenuesList.OrderBy(i => i.month).ToList();
                
                return Json(new BaseResult(s.isSuccess, s.status, s.Message, r), JsonRequestBehavior.AllowGet);

                

            }
            else
            {
                BaseResult s;
                s = reponse.Content.ReadAsAsync<BaseResult>().Result;
                //return View(accList);
                return Json(new BaseResult(s.isSuccess, s.status, s.Message, s.data), JsonRequestBehavior.AllowGet);


            }


        }
        [HttpGet]
        public JsonResult getStatisticalCurrenTime(int type)
        {
            var type1 = 1;
            var t = new AccountController().checkToken(GlobalVariables.ExpiredDateTime, GlobalVariables.access_token, GlobalVariables.Username, GlobalVariables.Password);

            List<monthlyrevenues> monthlyrevenuesList = new List<monthlyrevenues>();
            GlobalVariables.WebApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            GlobalVariables.WebApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", t);

            HttpResponseMessage reponse = GlobalVariables.WebApiClient.GetAsync("Admin/getStatisticalCurrentTime/" +type.ToString()).Result;

            if (reponse.IsSuccessStatusCode)
            {
                BaseResult s;
                s = reponse.Content.ReadAsAsync<BaseResult>().Result;
                var myJsonResponse = s.data.ToString().Trim().TrimStart('{').TrimEnd('}');
                monthlyrevenuesList = JsonConvert.DeserializeObject<List<monthlyrevenues>>(myJsonResponse);
                var r =monthlyrevenuesList.FirstOrDefault();                

                return Json(new BaseResult(s.isSuccess, s.status, s.Message, r), JsonRequestBehavior.AllowGet);
                                

            }
            else
            {
                BaseResult s;
                s = reponse.Content.ReadAsAsync<BaseResult>().Result;
                //return View(accList);
                return Json(new BaseResult(s.isSuccess, s.status, s.Message, s.data), JsonRequestBehavior.AllowGet);


            }


        }
    }
}