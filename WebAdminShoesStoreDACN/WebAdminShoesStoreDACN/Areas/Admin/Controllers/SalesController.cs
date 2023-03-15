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
    public class SalesController : BaseController
    {
        // GET: Admin/Sales
        public ActionResult SalesIndex()
        {
            return View();
        }
        [HttpGet]
        public JsonResult GetAllSaleForSaleDetails(int typeS)
        {
            try
            {
                var t = new AccountController().checkToken(GlobalVariables.ExpiredDateTime, GlobalVariables.access_token, GlobalVariables.Username, GlobalVariables.Password);

                List<Sale> SaleList = new List<Sale>();
                GlobalVariables.WebApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                GlobalVariables.WebApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", t);

                HttpResponseMessage reponse = GlobalVariables.WebApiClient.GetAsync("Admin/getAllSales/=").Result;
                if (reponse.IsSuccessStatusCode)
                {
                    BaseResult s;
                    s = reponse.Content.ReadAsAsync<BaseResult>().Result;
                    var myJsonResponse = s.data.ToString().Trim().TrimStart('{').TrimEnd('}');
                    SaleList = JsonConvert.DeserializeObject<List<Sale>>(myJsonResponse);
                    if (typeS == 0)//lay cac chuong trinh dang sale
                    {
                        SaleList = SaleList.Where(i => i.startday <= DateTime.Now && i.endday >= DateTime.Now).ToList();
                    }else if (typeS == 1)//lay cac chuong trinh chua sale
                    {
                        SaleList = SaleList.Where(i => i.startday > DateTime.Now).ToList();
                    }
                    else
                    {
                        SaleList = SaleList.Where(i => i.endday < DateTime.Now).ToList();
                    }
                    return Json(new BaseResult(s.isSuccess, s.status, s.Message, SaleList), JsonRequestBehavior.AllowGet);
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
        public JsonResult GetAllSale(string keysearch, int trang, int statusSale)
        {
            if (keysearch == null || keysearch == "")
            {
                keysearch = "=";
            }
            var t = new AccountController().checkToken(GlobalVariables.ExpiredDateTime, GlobalVariables.access_token, GlobalVariables.Username, GlobalVariables.Password);

            List<Sale> saleList = new List<Sale>();
            GlobalVariables.WebApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            GlobalVariables.WebApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", t);

            HttpResponseMessage reponse = GlobalVariables.WebApiClient.GetAsync("Admin/getAllSales/" + keysearch.ToString()).Result;
            if (reponse.IsSuccessStatusCode)
            {
                BaseResult s;
                s = reponse.Content.ReadAsAsync<BaseResult>().Result;
                var myJsonResponse = s.data.ToString().Trim().TrimStart('{').TrimEnd('}');
                saleList = JsonConvert.DeserializeObject<List<Sale>>(myJsonResponse);
                var pageSize = 2;
                if (statusSale == 0)//lay tat ca chouong trinh sale
                {

                    saleList = saleList.ToList();
                    var numPages = saleList.Count() % pageSize == 0 ? saleList.Count / pageSize : (saleList.Count / pageSize) + 1;

                    var kqht = saleList.Skip((trang - 1) * pageSize).Take(pageSize).ToList();
                    return Json(new BaseJsonResult(s.isSuccess, s.status, s.Message, kqht, numPages), JsonRequestBehavior.AllowGet);
                }
                else if (statusSale == 1)//lay cac chuong trinh dang sale
                {
                    saleList = saleList.Where(i => i.startday <= DateTime.Now && i.endday >= DateTime.Now).ToList();
                    saleList = saleList.ToList();
                    var numPages = saleList.Count() % pageSize == 0 ? saleList.Count / pageSize : (saleList.Count / pageSize) + 1;

                    var kqht = saleList.Skip((trang - 1) * pageSize).Take(pageSize).ToList();
                    return Json(new BaseJsonResult(s.isSuccess, s.status, s.Message, kqht, numPages), JsonRequestBehavior.AllowGet);
                }
                else
                if (statusSale == 2)//lay cac chuong trinh chua sale
                {
                    saleList = saleList.Where(i => i.startday > DateTime.Now).ToList();
                    saleList = saleList.ToList();
                    var numPages = saleList.Count() % pageSize == 0 ? saleList.Count / pageSize : (saleList.Count / pageSize) + 1;

                    var kqht = saleList.Skip((trang - 1) * pageSize).Take(pageSize).ToList();
                    return Json(new BaseJsonResult(s.isSuccess, s.status, s.Message, kqht, numPages), JsonRequestBehavior.AllowGet);
                }
                else//lay cac chuong trinh da ket thuc sale
                {
                    saleList = saleList.Where(i => i.endday < DateTime.Now).ToList();
                    saleList = saleList.ToList();
                    var numPages = saleList.Count() % pageSize == 0 ? saleList.Count / pageSize : (saleList.Count / pageSize) + 1;

                    var kqht = saleList.Skip((trang - 1) * pageSize).Take(pageSize).ToList();
                    return Json(new BaseJsonResult(s.isSuccess, s.status, s.Message, kqht, numPages), JsonRequestBehavior.AllowGet);
                }



            }
            else
            {
                Models.BaseResult s;
                s = reponse.Content.ReadAsAsync<Models.BaseResult>().Result;
                //return View(accList);
                return Json(new Models.BaseResult(s.isSuccess, s.status, s.Message, s.data), JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public JsonResult GetShoesByShoesId(int saleid)
        {
            try
            {
                var t = new AccountController().checkToken(GlobalVariables.ExpiredDateTime, GlobalVariables.access_token, GlobalVariables.Username, GlobalVariables.Password);

                List<Sale> SaleList = new List<Sale>();
                GlobalVariables.WebApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                GlobalVariables.WebApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", t);

                HttpResponseMessage reponse = GlobalVariables.WebApiClient.GetAsync("Admin/GetSaleBySaleId/" + saleid.ToString()).Result;
                if (reponse.IsSuccessStatusCode)
                {
                    BaseResult s;
                    s = reponse.Content.ReadAsAsync<BaseResult>().Result;
                    var myJsonResponse = s.data.ToString().Trim().TrimStart('{').TrimEnd('}');
                    SaleList = JsonConvert.DeserializeObject<List<Sale>>(myJsonResponse);
                    var sh = new Sale();
                    sh = SaleList[0];
                    return Json(new BaseResult(s.isSuccess, s.status, s.Message, sh), JsonRequestBehavior.AllowGet);

                }
                else
                {
                    BaseResult s;
                    s = reponse.Content.ReadAsAsync<BaseResult>().Result;
                    //return View(accList);
                    return Json(new BaseResult(s.isSuccess, s.status, s.Message, s.data), JsonRequestBehavior.AllowGet);


                }
            }
            catch (Exception ex)
            {
                return Json(new BaseResult(false, 500, "Server error 1", null), JsonRequestBehavior.AllowGet);

            }


        }
        [HttpPost]
        public JsonResult EditSale(int saleid, string salename, DateTime? startday, DateTime? endday, string content,
            decimal? percent, string imgsale)
        {
            try
            {
                var a = new Sale();
                a.saleid = saleid;
                a.salename = salename;
                a.updateby = GlobalVariables.accountid;
                a.startday = startday;
                a.endday = endday;
                a.content = content;
                a.percent = percent;
                a.imgsale = imgsale.Equals("") ? null : imgsale;
                var t = new AccountController().checkToken(GlobalVariables.ExpiredDateTime, GlobalVariables.access_token, GlobalVariables.Username, GlobalVariables.Password);


                GlobalVariables.WebApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                GlobalVariables.WebApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", t);

                HttpResponseMessage response = GlobalVariables.WebApiClient.PutAsJsonAsync("Admin/UpdateSale/" + a.saleid, a).Result;
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
            catch (Exception ex)
            {
                return Json(new BaseResult(false, 400, "failed", null), JsonRequestBehavior.AllowGet);

            }
        }
        //Admin/AddSale
        [HttpPost]
        public JsonResult AddSale(string salename, DateTime? startday, DateTime? endday, string content,
            decimal? percent, string imgsale)
        {

            var f = new Sale();
            f.salename = salename;
            f.startday = startday;
            f.endday = endday;
            f.content = content;
            f.percent = percent;
            f.imgsale = imgsale;
            f.createby = GlobalVariables.accountid;
            f.updateby = GlobalVariables.accountid;
            var t = new AccountController().checkToken(GlobalVariables.ExpiredDateTime, GlobalVariables.access_token, GlobalVariables.Username, GlobalVariables.Password);
            GlobalVariables.WebApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            GlobalVariables.WebApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", t);

            HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsJsonAsync("Admin/AddSale", f).Result;

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