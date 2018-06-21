using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WWW.Controllers
{
    [App_Code.CheckLogin()] //檢查有沒有重複登入
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index(String ErrorMsg)
        {
            if (Session["LoginName"] != null)
                RedirectToAction("Index", "Home");

            if (ErrorMsg == "Error")
                ViewData["Msg"] = "帳號或密碼錯誤!";

            return View();
        }

        //處理登入
        public ActionResult ValidLoginProcess(Models.LoginInfo li)
        {
            if (li.Acct.ToLower() == "mickey" && li.Pass == "abcd1234")
            {
                 Session.Add("LoginName",li.Acct);
                
                //存取帳號名稱
                HttpCookie loginName = new HttpCookie("LoginName", Session["LoginName"].ToString());
                Response.Cookies.Add(loginName);

                //跳轉頁面
                HttpCookie reUrl = Request.Cookies["ReUrl"];

                HttpCookie cc = Request.Cookies["ReUrl"];
                if (cc == null)
                {
                    //return RedirectToAction("Main", "Member");
                    return RedirectToAction("LoginSuccess");
                }
                else
                {
                    return Redirect(cc.Value);
                }

            }
            else
            {
                ViewData["LoginName"] = Session["LoginName"] = "旅客";
                return RedirectToAction("Index", new { ErrorMsg = "Error" });
            }
        }

        public ActionResult LoginSuccess()
        {
            return View();
        }

    }
}