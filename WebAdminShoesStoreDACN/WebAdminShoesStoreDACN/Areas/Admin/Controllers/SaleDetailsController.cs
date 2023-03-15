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
    public class SaleDetailsController : BaseController
    {
        // GET: Admin/SaleDetails
        public ActionResult SaleDetailsIndex()
        {
            return View();
        }
        [HttpGet]
        public JsonResult GetSaleDetailsBySaleId(string keysearch, int trang, int saleid)
        {
            if (keysearch == null || keysearch == "")
            {
                keysearch = "=";
            }
            var t = new AccountController().checkToken(GlobalVariables.ExpiredDateTime, GlobalVariables.access_token, GlobalVariables.Username, GlobalVariables.Password);
            List<SaleDetails> SDList = new List<SaleDetails>();
            GlobalVariables.WebApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            GlobalVariables.WebApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", t);


            HttpResponseMessage reponse = GlobalVariables.WebApiClient.GetAsync("Admin/GetSaleDetailsBySaleId/"+ saleid .ToString()+ "/" + keysearch.ToString()).Result;
            if (reponse.IsSuccessStatusCode)
            {
                BaseResult s;
                s = reponse.Content.ReadAsAsync<BaseResult>().Result;
                var myJsonResponse = s.data.ToString().Trim().TrimStart('{').TrimEnd('}');
                SDList = JsonConvert.DeserializeObject<List<SaleDetails>>(myJsonResponse);
                var pageSize = 3;                

                SDList = SDList.Where(i => i.saleid == saleid).ToList();
                
                var numPages = SDList.Count() % pageSize == 0 ? SDList.Count / pageSize : (SDList.Count / pageSize) + 1;

                var kqht = SDList.Skip((trang - 1) * pageSize).Take(pageSize).ToList();
                return Json(new BaseJsonResult(s.isSuccess, s.status, s.Message, kqht, numPages), JsonRequestBehavior.AllowGet);
                



            }
            else
            {
                BaseResult s;
                s = reponse.Content.ReadAsAsync<BaseResult>().Result;
                //return View(accList);
                return Json(new BaseResult(s.isSuccess, s.status, s.Message, s.data), JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult DeleteSaleDetailsByShoesIdAndSaleid(int saleid, int shoeid)
        {
            var t = new AccountController().checkToken(GlobalVariables.ExpiredDateTime, GlobalVariables.access_token, GlobalVariables.Username, GlobalVariables.Password);

            List<SaleDetails> SDList = new List<SaleDetails>();
            GlobalVariables.WebApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            GlobalVariables.WebApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", t);


            HttpResponseMessage reponse = GlobalVariables.WebApiClient.DeleteAsync("Admin/DeleteSaleDetailsByShoesIdAndSaleid/" + saleid .ToString()+ "/" + shoeid.ToString()).Result;
            if (reponse.IsSuccessStatusCode)
            {
                BaseResult s;
                s = reponse.Content.ReadAsAsync<BaseResult>().Result;
                return Json(new BaseResult(s.isSuccess, s.status, s.Message, s.data), JsonRequestBehavior.AllowGet);




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
        public JsonResult GetAllShoesNotExistsInSaledetails(string keysearch1, int trang1)
        {
            if (keysearch1 == null || keysearch1 == "")
            {
                keysearch1 = "=";
            }
            var t = new AccountController().checkToken(GlobalVariables.ExpiredDateTime, GlobalVariables.access_token, GlobalVariables.Username, GlobalVariables.Password);

            List<Shoes> SDList = new List<Shoes>();
            GlobalVariables.WebApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            GlobalVariables.WebApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", t);


            HttpResponseMessage reponse = GlobalVariables.WebApiClient.GetAsync("Admin/GetAllShoesNotExistsInSaledetails/" + keysearch1.ToString()).Result;
            if (reponse.IsSuccessStatusCode)
            {
                BaseResult s;
                s = reponse.Content.ReadAsAsync<BaseResult>().Result;
                var myJsonResponse = s.data.ToString().Trim().TrimStart('{').TrimEnd('}');
                SDList = JsonConvert.DeserializeObject<List<Shoes>>(myJsonResponse);
                var pageSize = 4;


                var numPages = SDList.Count() % pageSize == 0 ? SDList.Count / pageSize : (SDList.Count / pageSize) + 1;

                var kqht = SDList.Skip((trang1 - 1) * pageSize).Take(pageSize).ToList();
                return Json(new BaseJsonResult(s.isSuccess, s.status, s.Message, kqht, numPages), JsonRequestBehavior.AllowGet);




            }
            else
            {
                BaseResult s;
                s = reponse.Content.ReadAsAsync<BaseResult>().Result;
                //return View(accList);
                return Json(new BaseResult(s.isSuccess, s.status, s.Message, s.data), JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult AddShoesForSaleDetails(int saleid, List<int> shoesid)
        {
            var lsd = new List<SaleDetails>();
            foreach(var y in shoesid)
            {
                var sd = new SaleDetails();
                sd.saleid = saleid;
                sd.shoeid = y;
                sd.updateby = GlobalVariables.accountid;
                lsd.Add(sd);

            }
            

            var t = new AccountController().checkToken(GlobalVariables.ExpiredDateTime, GlobalVariables.access_token, GlobalVariables.Username, GlobalVariables.Password);

            
            GlobalVariables.WebApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            GlobalVariables.WebApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", t);


            HttpResponseMessage reponse = GlobalVariables.WebApiClient.PostAsJsonAsync("Admin/AddShoesToSaleDetails", lsd).Result;
            if (reponse.IsSuccessStatusCode)
            {
                BaseResult s;
                s = reponse.Content.ReadAsAsync<BaseResult>().Result;
                return Json(new BaseResult(s.isSuccess, s.status, s.Message, s.data), JsonRequestBehavior.AllowGet);




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