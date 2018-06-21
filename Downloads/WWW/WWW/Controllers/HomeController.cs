using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WWW.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            //印出瀏覽網站累積人數
            String path = Server.MapPath("~/APP_Data/VistCount.txt");
            String Vistcount = System.IO.File.ReadAllText(path);
            int cc = int.Parse(Vistcount) + 1;
            System.IO.File.WriteAllText(path, cc.ToString());

            var sb = new System.Text.StringBuilder();

            //先補零
            for (int i = 1; i <= 8 - cc.ToString().Length; i++)
            {
                sb.Append("<img src='~/img/num2/0.gif' />");
            }
            //放數字
            for (int i = 0; i < cc.ToString().Length; i++)
            {
                sb.Append(String.Format("<img src='~/img/num2/{0}.gif' />", cc.ToString().Substring(i, 1)));
                
            }
            

            ViewData["VistCount"] = sb.ToString();
            
            return View();
        }
    }
}