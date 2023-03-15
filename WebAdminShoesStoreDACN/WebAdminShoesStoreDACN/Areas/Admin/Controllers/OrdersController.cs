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
    public class OrdersController : BaseController
    {
        // GET: Admin/Orders
        public ActionResult OrderIndex()
        {
            return View();
        }
        [HttpGet]
        public JsonResult GetAllOrders(string keysearch, int trang,int stid)
        {
            if (keysearch == null || keysearch == "")
            {
                keysearch = "=";
            }



            var t = new AccountController().checkToken(GlobalVariables.ExpiredDateTime, GlobalVariables.access_token, GlobalVariables.Username, GlobalVariables.Password);

            List<Order> orderList = new List<Order>();
            GlobalVariables.WebApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            GlobalVariables.WebApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", t);

            HttpResponseMessage reponse = GlobalVariables.WebApiClient.GetAsync("Admin/GetAllOrders/" + keysearch.ToString()).Result;
            if (reponse.IsSuccessStatusCode)
            {
                BaseResult s;
                s = reponse.Content.ReadAsAsync<BaseResult>().Result;
                var myJsonResponse = s.data.ToString().Trim().TrimStart('{').TrimEnd('}');
                orderList = JsonConvert.DeserializeObject<List<Order>>(myJsonResponse);
                var pageSize = 7;
                if (stid == 0)
                {
                    orderList = orderList.ToList();
                    var numPages = orderList.Count() % pageSize == 0 ? orderList.Count / pageSize : (orderList.Count / pageSize) + 1;                    
                    var kqht = orderList.Skip((trang - 1) * pageSize).Take(pageSize).ToList();

                    return Json(new BaseJsonResult(s.isSuccess, s.status, s.Message, kqht, numPages), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    orderList = orderList.Where(i=>i.statusid== stid).ToList();
                    var numPages = orderList.Count() % pageSize == 0 ? orderList.Count / pageSize : (orderList.Count / pageSize) + 1;
                    var kqht = orderList.Skip((trang - 1) * pageSize).Take(pageSize).ToList();
                    return Json(new BaseJsonResult(s.isSuccess, s.status, s.Message, kqht, numPages), JsonRequestBehavior.AllowGet);
                }                
            }
            else
            {


                BaseResult s;
                s = reponse.Content.ReadAsAsync<BaseResult>().Result;
                return Json(new BaseResult(s.isSuccess, s.status, s.Message, s.data), JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public JsonResult GetOrderByOrderId(int orderid)
        {
            try
            {
                var t = new AccountController().checkToken(GlobalVariables.ExpiredDateTime, GlobalVariables.access_token, GlobalVariables.Username, GlobalVariables.Password);
                List<Order> OrderList = new List<Order>();
                GlobalVariables.WebApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                GlobalVariables.WebApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", t);

                HttpResponseMessage reponse = GlobalVariables.WebApiClient.GetAsync("Admin/GetOrderByOrderId/" + orderid.ToString()).Result;
                if (reponse.IsSuccessStatusCode)
                {

                    BaseResult s;
                    s = reponse.Content.ReadAsAsync<BaseResult>().Result;
                    var myJsonResponse = s.data.ToString().Trim().TrimStart('{').TrimEnd('}');
                    OrderList = JsonConvert.DeserializeObject<List<Order>>(myJsonResponse);
                    var od = new Order();
                    od = OrderList[0];
                    return Json(new BaseResult(s.isSuccess, s.status, s.Message, od), JsonRequestBehavior.AllowGet);

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
        //Admin/UpdateOrder/{orderid}
        [HttpPost]
        public JsonResult UpdateOrder(int orderid, int? statusid)
        {
            try
            {
                var a = new Order();
                a.orderid = orderid;
                a.statusid = statusid;
                a.createdate = DateTime.Now;
               
                var t = new AccountController().checkToken(GlobalVariables.ExpiredDateTime, GlobalVariables.access_token, GlobalVariables.Username, GlobalVariables.Password);
                GlobalVariables.WebApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                GlobalVariables.WebApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", t);
                HttpResponseMessage response = GlobalVariables.WebApiClient.PutAsJsonAsync("Admin/UpdateOrder/" + orderid, a).Result;
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
    }
}