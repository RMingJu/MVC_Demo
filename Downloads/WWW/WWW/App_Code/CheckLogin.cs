using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WWW.App_Code
{
    public class CheckLogin:ActionFilterAttribute
    {
        //進登入畫面前，檢查有沒有重複登入
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            
            //有登入的話踢回去原本預覽的畫面
            if (filterContext.HttpContext.Session["LoginName"] != null)
            {
                UrlHelper uu = new UrlHelper(filterContext.HttpContext.Request.RequestContext);
                String url = filterContext.HttpContext.Request.RawUrl;
                //跳轉
                filterContext.HttpContext.Response.Redirect(uu.Action("Index", "Home"));
            }

        }
    }
}