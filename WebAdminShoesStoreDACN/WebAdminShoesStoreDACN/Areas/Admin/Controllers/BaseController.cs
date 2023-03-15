using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAdminShoesStoreDACN.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        // GET: Admin/Base
        public BaseController()
        {
            /*if (System.Web.HttpContext.Current.Session["Staff"] == null )
            {
                System.Web.HttpContext.Current.Response.Redirect("~/Account/Login");
            }*/
            if (GlobalVariables.access_token == "")
            {
                System.Web.HttpContext.Current.Response.Redirect("~/Account/Login");
            }


        }
        public void BlockStaff()
        {
            if (GlobalVariables.roleId == 0  )
            {
                System.Web.HttpContext.Current.Response.Redirect("~/Admin/DonDatHang");
            }
        }
    }
}