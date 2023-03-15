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
    public class OrderDetailsController : BaseController
    {
        // GET: Admin/OrderDetails
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public JsonResult getOrderDetails(int orderid)
        {
            var t = new AccountController().checkToken(GlobalVariables.ExpiredDateTime, GlobalVariables.access_token, GlobalVariables.Username, GlobalVariables.Password);

            List<OrderDetails> OdList = new List<OrderDetails>();
            GlobalVariables.WebApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            GlobalVariables.WebApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", t);

            HttpResponseMessage reponse = GlobalVariables.WebApiClient.GetAsync("Admin/getOrderDetails/" + orderid.ToString()).Result;
            if (reponse.IsSuccessStatusCode)
            {
                BaseResult s;
                s = reponse.Content.ReadAsAsync<BaseResult>().Result;
                var myJsonResponse = s.data.ToString().Trim().TrimStart('{').TrimEnd('}');
                OdList = JsonConvert.DeserializeObject<List<OrderDetails>>(myJsonResponse);
                return Json(new BaseResult(s.isSuccess, s.status, s.Message, OdList), JsonRequestBehavior.AllowGet);

                //return Json(new jsonResult<account>(s.isSuccess, s.status, s.Message, accList), JsonRequestBehavior.AllowGet);
                //return View(accList);

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