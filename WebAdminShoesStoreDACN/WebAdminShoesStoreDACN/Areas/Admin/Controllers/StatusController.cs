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
    public class StatusController : BaseController
    {
        // GET: Admin/Status
        public ActionResult Index()
        {
            return View();
        }
        //Admin/getAllStatus
        [HttpGet]
        public JsonResult GetAllStatus()
        {
            try
            {
               
                var t = new AccountController().checkToken(GlobalVariables.ExpiredDateTime, GlobalVariables.access_token, GlobalVariables.Username, GlobalVariables.Password);

                List<Status> statusList = new List<Status>();
                GlobalVariables.WebApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                GlobalVariables.WebApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", t);

                HttpResponseMessage reponse = GlobalVariables.WebApiClient.GetAsync("Admin/getAllStatus/=" ).Result;
                if (reponse.IsSuccessStatusCode)
                {
                    BaseResult s;
                    s = reponse.Content.ReadAsAsync<BaseResult>().Result;
                    var myJsonResponse = s.data.ToString().Trim().TrimStart('{').TrimEnd('}');
                    statusList = JsonConvert.DeserializeObject<List<Status>>(myJsonResponse);
                    var r = new Status();
                    r = statusList[0];
                    return Json(new BaseResult(s.isSuccess, s.status, s.Message, statusList), JsonRequestBehavior.AllowGet);
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
        public List<Status> GetListStatus()
        {
            List<Status> statusList = new List<Status>();
            try
            {
               
                var t = new AccountController().checkToken(GlobalVariables.ExpiredDateTime, GlobalVariables.access_token, GlobalVariables.Username, GlobalVariables.Password);

                
                GlobalVariables.WebApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                GlobalVariables.WebApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", t);

                HttpResponseMessage reponse = GlobalVariables.WebApiClient.GetAsync("Admin/getAllStatus/=" ).Result;
                if (reponse.IsSuccessStatusCode)
                {
                    BaseResult s;
                    s = reponse.Content.ReadAsAsync<BaseResult>().Result;
                    var myJsonResponse = s.data.ToString().Trim().TrimStart('{').TrimEnd('}');
                    statusList = JsonConvert.DeserializeObject<List<Status>>(myJsonResponse);
                    var r = new Status();
                    r = statusList[0];
                    return statusList;
                }
                else
                {
                    BaseResult s;
                    s = reponse.Content.ReadAsAsync<BaseResult>().Result;
                    return statusList;

                }
            }
            catch (Exception ex)
            {
                return statusList;
            }
        }
    }
}