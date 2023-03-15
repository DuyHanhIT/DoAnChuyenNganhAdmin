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
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System.IO;

namespace WebAdminShoesStoreDACN.Areas.Admin.Controllers
{
    public class ShoesController : BaseController
    {

        // GET: Admin/Shoes
        public ActionResult ShoesIndex()
        {
            return View();
        }
        [HttpGet]
        public JsonResult GetAllShoes(string keysearch, int trang, int brandid)
        {
            if (keysearch == null || keysearch == "")
            {
                keysearch = "=";
            }
            var t = new AccountController().checkToken(GlobalVariables.ExpiredDateTime, GlobalVariables.access_token, GlobalVariables.Username, GlobalVariables.Password);

            List<Shoes> shoesList = new List<Shoes>();
            GlobalVariables.WebApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            GlobalVariables.WebApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", t);


            HttpResponseMessage reponse = GlobalVariables.WebApiClient.GetAsync("Admin/GetAllShoes/" + keysearch.ToString()).Result;
            if (reponse.IsSuccessStatusCode)
            {
                Models.BaseResult s;
                s = reponse.Content.ReadAsAsync<Models.BaseResult>().Result;
                var myJsonResponse = s.data.ToString().Trim().TrimStart('{').TrimEnd('}');
                shoesList = JsonConvert.DeserializeObject<List<Shoes>>(myJsonResponse);
                var pageSize = 8;
                if (brandid == 0)
                {

                    shoesList = shoesList.OrderByDescending(x => x.active).ThenBy(x => x.shoename).ToList();
                    var numPages = shoesList.Count() % pageSize == 0 ? shoesList.Count / pageSize : (shoesList.Count / pageSize) + 1;

                    var kqht = shoesList.Skip((trang - 1) * pageSize).Take(pageSize).ToList();
                    return Json(new BaseJsonResult(s.isSuccess, s.status, s.Message, kqht, numPages), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    shoesList = shoesList.Where(i => i.brandid == brandid).ToList();
                    shoesList = shoesList.OrderByDescending(x => x.active).ThenBy(x => x.shoename).ToList();
                    var numPages = shoesList.Count() % pageSize == 0 ? shoesList.Count / pageSize : (shoesList.Count / pageSize) + 1;

                    var kqht = shoesList.Skip((trang - 1) * pageSize).Take(pageSize).ToList();
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
        public JsonResult GetShoesByShoesId(int shoesId)
        {
            try
            {
                var t = new AccountController().checkToken(GlobalVariables.ExpiredDateTime, GlobalVariables.access_token, GlobalVariables.Username, GlobalVariables.Password);

                List<Shoes> shoesList = new List<Shoes>();
                GlobalVariables.WebApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                GlobalVariables.WebApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", t);

                HttpResponseMessage reponse = GlobalVariables.WebApiClient.GetAsync("Admin/GetShoesByShoesId/" + shoesId.ToString()).Result;
                if (reponse.IsSuccessStatusCode)
                {
                    Models.BaseResult s;
                    s = reponse.Content.ReadAsAsync<Models.BaseResult>().Result;
                    var myJsonResponse = s.data.ToString().Trim().TrimStart('{').TrimEnd('}');
                    shoesList = JsonConvert.DeserializeObject<List<Shoes>>(myJsonResponse);
                    ViewBag.Message = s.Message;
                    var sh = new Shoes();
                    sh = shoesList[0];
                    return Json(new Models.BaseResult(s.isSuccess, s.status, s.Message, sh), JsonRequestBehavior.AllowGet);

                    //return Json(new jsonResult<account>(s.isSuccess, s.status, s.Message, accList), JsonRequestBehavior.AllowGet);
                    //return View(accList);

                }
                else
                {
                    Models.BaseResult s;
                    s = reponse.Content.ReadAsAsync<Models.BaseResult>().Result;
                    //return View(accList);
                    return Json(new Models.BaseResult(s.isSuccess, s.status, s.Message, s.data), JsonRequestBehavior.AllowGet);


                }
            }
            catch (Exception ex)
            {
                return Json(new Models.BaseResult(false, 500, "Server error 1", null), JsonRequestBehavior.AllowGet);

            }


        }

        [HttpPost]
        public JsonResult AddShoes(int? Brandid, int? Categoryid, string Shoename, decimal? Price, string Description,
            bool? Shoenew, string IImg1, string IImg2, string IImg3, string IImg4)
        {

            var f = new Shoes();
            f.brandid = Brandid;
            f.categoryid = Categoryid;
            f.shoename = Shoename;
            f.price = Price;
            f.description = Description;
            f.shoenew = Shoenew;
            f.image1 = IImg1;
            f.image2 = IImg2;
            f.image3 = IImg3;
            f.image4 = IImg4;
            f.rate = 0;
            f.createby = GlobalVariables.accountid;
            f.updateby = GlobalVariables.accountid;
            GlobalVariables.WebApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsJsonAsync("Admin/AddShoes", f).Result;


            if (response.IsSuccessStatusCode)
            {
                Models.BaseResult s;
                s = response.Content.ReadAsAsync<Models.BaseResult>().Result;
               
                return Json(new Models.BaseResult(s.isSuccess, s.status, s.Message, s.data), JsonRequestBehavior.AllowGet);
               
            }
            else
            {
                Models.BaseResult s;
                s = response.Content.ReadAsAsync<Models.BaseResult>().Result;
                return Json(new Models.BaseResult(s.isSuccess, s.status, s.Message, s.data), JsonRequestBehavior.AllowGet);



            }
        }

        [HttpPost]
        public JsonResult ProcessUpload(HttpPostedFileBase file)
        {
            try
            {
                if (file == null)
                {
                    return Json(new Models.BaseResult(false, 404, "Chưa upload ảnh", null), JsonRequestBehavior.AllowGet);
                }
                string file1 = file.FileName;
                List<string> ImageExtensions = new List<string> { ".JPG", ".JPEG", ".JPE", ".BMP", ".GIF", ".PNG" };
                if (ImageExtensions.Contains(Path.GetExtension(file1).ToUpperInvariant()))
                {
                    if (file.ContentLength <=2097152)
                    {
                        var myAccount = new CloudinaryDotNet.Account { ApiKey = "864855319678933", ApiSecret = "W7HQ3lXhjUKqZ0DXXNWmmvQFv5Q", Cloud = "dmzfbmf2b" };
                        Cloudinary cloudinary = new Cloudinary(myAccount);
                        var uploadParams = new ImageUploadParams()
                        {
                            File = new FileDescription(file.FileName, file.InputStream)

                        };

                        var uploadImg = cloudinary.Upload(uploadParams);
                        string response = uploadImg.SecureUri.AbsoluteUri;
                        return Json(new Models.BaseResult(true, 200, "Upload success", response), JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new Models.BaseResult(false, 500, "Kích thước file quá lớn. vui lòng chọn file khác", null), JsonRequestBehavior.AllowGet);
                    }

                }
                else
                {
                    return Json(new Models.BaseResult(false, 404, "file bạn chọn không phải hình ảnh", null), JsonRequestBehavior.AllowGet);
                }


            }
            catch (Exception ex)
            {
                return Json(new Models.BaseResult(false, 500, "Eror server", null), JsonRequestBehavior.AllowGet);
            }


        }

        [HttpPost]
        public JsonResult EditShoes(int shoesid, int Brandid, int Categoryid, string Shoename, decimal? Price, string Description, bool active, bool Shoenew,
            string IImg1, string IImg2, string IImg3, string IImg4)
        {
            try
            {
                var a = new Shoes();
                a.shoeid = shoesid;
                a.brandid = Brandid;
                a.categoryid = Categoryid;
                a.shoename = Shoename;
                a.price = Price;
                a.description = Description;
                a.active = active;
                a.shoenew = Shoenew;
                a.image1 = IImg1.Equals("") ? null : IImg1;
                a.image2 = IImg2.Equals("") ? null : IImg2;
                a.image3 = IImg3.Equals("") ? null : IImg3;
                a.image4 = IImg4.Equals("") ? null : IImg4;
                a.stock = 0;
                a.rate = 0;
                a.purchased = 0;
                a.createby = 1;
                a.updateby = GlobalVariables.accountid;
                a.dateupdate = DateTime.Now;
                a.createdate = DateTime.Now;
                GlobalVariables.WebApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = GlobalVariables.WebApiClient.PutAsJsonAsync("Admin/UpdateShoes/" + a.shoeid, a).Result;
                /* List<Shoes> accList = new List<Shoes>();*/
                if (response.IsSuccessStatusCode)
                {
                    Models.BaseResult s;
                    s = response.Content.ReadAsAsync<Models.BaseResult>().Result;
                    /* var myJsonResponse = s.data.ToString().Trim().TrimStart('{').TrimEnd('}');
                     accList = JsonConvert.DeserializeObject<List<Account>>(myJsonResponse);
                     ViewBag.Message = s.Message;
                     List<Account> accList1 = new List<Account>();
                     accList1.Add(accList[0]);
                     var ac = new Account();
                     ac = accList[0];*/
                    return Json(new Models.BaseResult(s.isSuccess, s.status, s.Message, s.data), JsonRequestBehavior.AllowGet);
                    //return Json(new jsonResult<account>(s.isSuccess, s.status, s.Message, accList), JsonRequestBehavior.AllowGet);
                    // return View(accList);
                }
                else
                {
                    Models.BaseResult s;
                    s = response.Content.ReadAsAsync<Models.BaseResult>().Result;
                    //return View(accList);
                    return Json(new Models.BaseResult(s.isSuccess, s.status, s.Message, s.data), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new Models.BaseResult(false, 400, "failed", null), JsonRequestBehavior.AllowGet);

            }
        }
        [HttpPost]
        public JsonResult InactiveShoes(int shoesid)
        {
            try
            {
                var a = new Shoes();
                a.shoeid = shoesid;
                a.brandid = 1;
                a.categoryid = 1;
                a.shoename = "";
                a.price = 0;
                a.description = "";
                a.active = false;
                a.shoenew = true;
                a.image1 = null;
                a.image2 = null;
                a.image3 = null;
                a.image4 = null;
                a.stock = 0;
                a.rate = 0;
                a.purchased = 0;
                a.createby = 1;
                a.updateby = GlobalVariables.accountid;
                a.dateupdate = DateTime.Now;
                a.createdate = DateTime.Now;
                GlobalVariables.WebApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = GlobalVariables.WebApiClient.PutAsJsonAsync("Admin/InactiveShoes/" + a.shoeid, a).Result;
                /* List<Shoes> accList = new List<Shoes>();*/
                if (response.IsSuccessStatusCode)
                {
                    Models.BaseResult s;
                    s = response.Content.ReadAsAsync<Models.BaseResult>().Result;
                    /* var myJsonResponse = s.data.ToString().Trim().TrimStart('{').TrimEnd('}');
                     accList = JsonConvert.DeserializeObject<List<Account>>(myJsonResponse);
                     ViewBag.Message = s.Message;
                     List<Account> accList1 = new List<Account>();
                     accList1.Add(accList[0]);
                     var ac = new Account();
                     ac = accList[0];*/
                    return Json(new Models.BaseResult(s.isSuccess, s.status, s.Message, s.data), JsonRequestBehavior.AllowGet);
                    //return Json(new jsonResult<account>(s.isSuccess, s.status, s.Message, accList), JsonRequestBehavior.AllowGet);
                    // return View(accList);
                }
                else
                {
                    Models.BaseResult s;
                    s = response.Content.ReadAsAsync<Models.BaseResult>().Result;
                    //return View(accList);
                    return Json(new Models.BaseResult(s.isSuccess, s.status, s.Message, s.data), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new Models.BaseResult(false, 400, "failed", null), JsonRequestBehavior.AllowGet);

            }
        }
    }
}