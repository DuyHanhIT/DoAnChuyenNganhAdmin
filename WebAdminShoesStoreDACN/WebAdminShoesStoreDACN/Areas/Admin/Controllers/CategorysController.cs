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
    public class CategorysController : BaseController
    {
        // GET: Admin/Categorys
        public ActionResult CategoryIndex()
        {
            return View();
        }
        [HttpGet]
        public JsonResult GetAllCategory(string keysearch)
        {
            try
            {
                if (keysearch == null || keysearch == "")
                {
                    keysearch = "=";
                }
                var t = new AccountController().checkToken(GlobalVariables.ExpiredDateTime, GlobalVariables.access_token, GlobalVariables.Username, GlobalVariables.Password);
                List<Category> cateList = new List<Category>();
                GlobalVariables.WebApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                GlobalVariables.WebApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", t);
                HttpResponseMessage reponse = GlobalVariables.WebApiClient.GetAsync("Admin/getAllCategorys/" + keysearch.ToString()).Result;
                if (reponse.IsSuccessStatusCode)
                {
                    BaseResult s;
                    s = reponse.Content.ReadAsAsync<BaseResult>().Result;
                    var myJsonResponse = s.data.ToString().Trim().TrimStart('{').TrimEnd('}');
                    cateList = JsonConvert.DeserializeObject<List<Category>>(myJsonResponse);
                    var r = new Category();
                    r = cateList[0];
                    return Json(new BaseResult(s.isSuccess, s.status, s.Message, cateList), JsonRequestBehavior.AllowGet);
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
        public JsonResult GetCategoryByCategoryId(int categoryid)
        {
            try
            {
                var t = new AccountController().checkToken(GlobalVariables.ExpiredDateTime, GlobalVariables.access_token, GlobalVariables.Username, GlobalVariables.Password);

                List<Category> cateList = new List<Category>();
                GlobalVariables.WebApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                GlobalVariables.WebApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", t);

                HttpResponseMessage reponse = GlobalVariables.WebApiClient.GetAsync("Admin/GetCategoryByCategoryId/" + categoryid.ToString()).Result;
                if (reponse.IsSuccessStatusCode)
                {
                    BaseResult s;
                    s = reponse.Content.ReadAsAsync<BaseResult>().Result;
                    var myJsonResponse = s.data.ToString().Trim().TrimStart('{').TrimEnd('}');
                    cateList = JsonConvert.DeserializeObject<List<Category>>(myJsonResponse);
                    ViewBag.Message = s.Message;
                    var sh = new Category();
                    sh = cateList[0];
                    return Json(new BaseResult(s.isSuccess, s.status, s.Message, sh), JsonRequestBehavior.AllowGet);


                }
                else
                {
                    BaseResult s;
                    s = reponse.Content.ReadAsAsync<Models.BaseResult>().Result;
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
        public JsonResult EditCategory(int categoryid, string categoryname)
        {
            try
            {
                var a = new Category();
                a.categoryid = categoryid;
                a.categoryname = categoryname;
                
                GlobalVariables.WebApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = GlobalVariables.WebApiClient.PutAsJsonAsync("Admin/UpdateCategory/" + a.categoryid, a).Result;
                
                if (response.IsSuccessStatusCode)
                {
                    BaseResult s;
                    s = response.Content.ReadAsAsync<BaseResult>().Result;
                    return Json(new BaseResult(s.isSuccess, s.status, s.Message, s.data), JsonRequestBehavior.AllowGet);
                   
                }
                else
                {
                    BaseResult s;
                    s = response.Content.ReadAsAsync<Models.BaseResult>().Result;
                    //return View(accList);
                    return Json(new BaseResult(s.isSuccess, s.status, s.Message, s.data), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new BaseResult(false, 400, "failed", null), JsonRequestBehavior.AllowGet);

            }
        }
        [HttpPost]
        public JsonResult AddCategory(string categoryname)
        {
            var f = new Category();
            f.categoryname = categoryname;
            GlobalVariables.WebApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsJsonAsync("Admin/AddCategory", f).Result;
            

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
                //return View(accList);
                return Json(new BaseResult(s.isSuccess, s.status, s.Message, s.data), JsonRequestBehavior.AllowGet);



            }
        }
    }
}