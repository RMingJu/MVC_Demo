using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WWW.Controllers
{
    public class ToolsController : Controller
    {
        public String GetAddressXml()
        {
            String path = Server.MapPath("~/App_Data/Address.xml");
            String address = System.IO.File.ReadAllText(path);
            return address;
        }

        public ActionResult GetValidPic()
        {
            ValidNum.ValidNum vn = new ValidNum.ValidNum();

            //將驗證碼存入Session
            Session.Add("ValidAns", vn.ValidAnswer);

            Bitmap bmp = vn.ValidImage;
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            byte[] bb = ms.ToArray();
            return File(bb, "image/png");
        }

        public string GetValidAns()
        {
            return Session["ValidAns"].ToString();
        }

        public string CheckAccount(string acct)
        {
            String result = "";
            NWDB.NWDB mydb = new NWDB.NWDB();
            NWDB.Impl_Member im = new NWDB.Impl_Member(mydb.Connection);
            bool ans = im.CheckAccountDuplicate(acct);

            if (ans)
                result = "t";
            else
                result = "f";

            return result;
        }
        
       // public ActionResult Get

    }
}