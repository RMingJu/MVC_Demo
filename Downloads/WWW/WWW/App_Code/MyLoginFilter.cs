using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WWW.App_Code
{
    public class MyLoginFilter:ActionFilterAttribute
    {
        //進商城或後台管理要先登入，沒登入踢去登入
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //@Url在這邊要這樣寫
            UrlHelper uu = new UrlHelper(filterContext.HttpContext.Request.RequestContext);

            //如果沒登入踢去登入畫面
            if (filterContext.HttpContext.Session["LoginName"] == null
                ||String.IsNullOrEmpty(filterContext.HttpContext.Session["LoginName"].ToString())
                 )
            {
                //Cookie存取目前想去畫面
                String location = filterContext.HttpContext.Request.RawUrl;
                HttpCookie cc = new HttpCookie("ReUrl");
                cc.Value = location;
                
                //把cookie加回去
                filterContext.HttpContext.Response.Cookies.Add(cc);

                //踢去登入
                filterContext.HttpContext.Response.Redirect(uu.Action("Index", "Login"));
            }


        }
    }
}