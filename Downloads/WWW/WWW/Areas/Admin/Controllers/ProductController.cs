using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WWW.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        //新增產品
        public ActionResult AddNewProduct()
        {
            return View();
        }
        //修改產品
        public ActionResult MaintainProduct(int? pid)
        {
            return View();
        }
        
    }
}