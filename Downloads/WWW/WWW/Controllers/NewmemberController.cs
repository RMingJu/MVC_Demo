using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WWW.Controllers
{
    public class NewmemberController : Controller
    {
        // GET: Newmember
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddNewMember(Models.MemberInfo mi)
        {
            NWDB.NWDB myDB = new NWDB.NWDB("SQLAdmin","1234");
            NWDB.Impl_Member im = new NWDB.Impl_Member(myDB.Connection);
            //雜湊用戶的密碼
            System.Security.Cryptography.SHA512Managed sha =
                new System.Security.Cryptography.SHA512Managed();

            //將用戶的原始密碼轉成UTF8編碼的位元陣列
            byte[] oldpass = System.Text.Encoding.UTF8.GetBytes(mi.Pass);
            //加點鹽
            byte[] hashpass = sha.ComputeHash(oldpass);

            im.AddNewMember(mi.Acct, hashpass, mi.LastName, mi.FirstName
                , (byte)mi.Gender.Value, mi.Birth
                , mi.County, mi.Region, mi.Address, mi.Edu, mi.Email);

            return RedirectToAction("AddMemberFinish","Newmember",mi);
        }

        public ActionResult AddMemberFinish(Models.MemberInfo mi)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(mi.Acct + "\r\n").Append(mi.Address);
            ViewData["Member"] = sb.ToString();
            return View();
        }

    }
}