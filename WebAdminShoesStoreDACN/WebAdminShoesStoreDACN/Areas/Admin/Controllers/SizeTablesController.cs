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
    public class SizeTablesController : BaseController
    {
        // GET: Admin/SizeTables
        public ActionResult SizeTableIndex()
        {
            return View();
        }
        [HttpGet]
        public JsonResult GetAllSizeTable(string keysearch, int trang)
        {
            if (keysearch == null || keysearch == "")
            {
                keysearch = "=";
            }
            var t = new AccountController().checkToken(GlobalVariables.ExpiredDateTime, GlobalVariables.access_token, GlobalVariables.Username, GlobalVariables.Password);
            List<SizeTable> STList = new List<SizeTable>();
            GlobalVariables.WebApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            GlobalVariables.WebApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", t);
            HttpResponseMessage reponse = GlobalVariables.WebApiClient.GetAsync("Admin/GetAllSizeTable/" + keysearch.ToString()).Result;
            if (reponse.IsSuccessStatusCode)
            {
                BaseResult s;
                s = reponse.Content.ReadAsAsync<BaseResult>().Result;
                var myJsonResponse = s.data.ToString().Trim().TrimStart('{').TrimEnd('}');
                STList = JsonConvert.DeserializeObject<List<SizeTable>>(myJsonResponse);
                var ac = new SizeTable();
                ac = STList[0];
                var pageSize = 9;
                var numPages = STList.Count() % pageSize == 0 ? STList.Count / pageSize : (STList.Count / pageSize) + 1;
                STList = STList.ToList();
                var kqht = STList.Skip((trang - 1) * pageSize).Take(pageSize).ToList();
                return Json(new BaseJsonResult(s.isSuccess, s.status, s.Message, kqht, numPages), JsonRequestBehavior.AllowGet);
            }
            else
            {
                BaseResult s;
                s = reponse.Content.ReadAsAsync<BaseResult>().Result;
                return Json(new BaseResult(s.isSuccess, s.status, s.Message, s.data), JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public JsonResult GetSizeTableBySizeId(int stid)
        {
            try
            {
                var t = new AccountController().checkToken(GlobalVariables.ExpiredDateTime, GlobalVariables.access_token, GlobalVariables.Username, GlobalVariables.Password);
                List<SizeTable> STList = new List<SizeTable>();
                GlobalVariables.WebApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                GlobalVariables.WebApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", t);
                HttpResponseMessage reponse = GlobalVariables.WebApiClient.GetAsync("Admin/GetSizeTableBySizeId/" + stid.ToString()).Result;
                if (reponse.IsSuccessStatusCode)
                {
                    BaseResult s;
                    s = reponse.Content.ReadAsAsync<BaseResult>().Result;
                    var myJsonResponse = s.data.ToString().Trim().TrimStart('{').TrimEnd('}');
                    STList = JsonConvert.DeserializeObject<List<SizeTable>>(myJsonResponse);
                    var ST = new SizeTable();
                    ST = STList[0];
                    return Json(new BaseResult(s.isSuccess, s.status, s.Message, ST), JsonRequestBehavior.AllowGet);
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
        public JsonResult EditSizeTable(int stid, int s38, int s39, int s40, int s41, int s42, int s43, int s44, int s45, int s46, int s47, int s48)
        {
            try
            {
                var a = new SizeTable();
                a.stid = stid;
                a.shoeid = 1;
                a.s38 = s38;
                a.s39 = s39;
                a.s40 = s40;
                a.s41 = s41;
                a.s42 = s42;
                a.s43 = s43;
                a.s44 = s44;
                a.s45 = s45;
                a.s46 = s46;
                a.s47 = s47;
                a.s48 = s48;
                GlobalVariables.WebApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = GlobalVariables.WebApiClient.PutAsJsonAsync("Admin/UpdateSizeTable/" + a.stid, a).Result;
                if (response.IsSuccessStatusCode)
                {
                    BaseResult s;
                    s = response.Content.ReadAsAsync<Models.BaseResult>().Result;
                    
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
    }
}