using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.Mvc;

namespace WWW.Controllers
{
    [App_Code.MyLoginFilter()]
    public class ShopController : Controller
    {
        // GET: Shop
        public ActionResult Index(int?page)
        {
            //如果亂打直接定義為第1頁
            if (page == null) page = 1;

            //從DB查出資料
            NWDB.NWDB mydb = new NWDB.NWDB("SQLAdmin","1234");
            NWDB.Impl_Product ip = new NWDB.Impl_Product(mydb.Connection);


            //總頁數
            int totalPage = 0;
            DataTable tt = ip.GetProductListByPage(page.Value, out totalPage);


            //要傳入前端的Models先用List裝起來，之後一起傳回去
            List<Models.ProductInfo> ps = new List<Models.ProductInfo>();
            Models.ProductInfo pi = null;
            

            //產品資料
            foreach (DataRow r in tt.Rows)
            {
                pi = new Models.ProductInfo();
                pi.ID = int.Parse(r["產品編號"].ToString());
                pi.Name = r["產品"].ToString();
                pi.Price = double.Parse(r["單價"].ToString());
                pi.Unit = r["單位數量"].ToString();
                pi.Stock = int.Parse(r["庫存量"].ToString());
                //pi.SupplyID = int.Parse(r["供應商編號"].ToString());
                pi.SupplyName = r["供應商"].ToString();
                pi.Pic = (byte[])r["產品圖片"];
                ps.Add(pi);
            }

            //處理頁碼
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            int p =Convert.ToInt32(Math.Ceiling(totalPage / 10.0));
            for (int i = 1; i <= p; i++)
            {
                if (i == page.Value)
                    sb.Append(i+ "&nbsp;&nbsp;");
                else
                    sb.Append(String.Format("<a href='{0}'>{1}</a>&nbsp;&nbsp;", Url.Action("Index", new { page = i }), i));
            }
            ViewData["pageLink"] = sb.ToString();

            return View(ps);
        }


        public ActionResult AddToCart(int? pid)
        {
            if (pid == null)
                return RedirectToAction("Index");

            HashSet<Models.CartItem> myCart = null;

            //若無購物車，New新的；若Session已有購物車，直接取出
            if (Session["Cart"] == null)
                myCart = new HashSet<Models.CartItem>();
            else
                myCart = Session["Cart"] as HashSet<Models.CartItem>;


            //先New一個CartItem
            Models.CartItem ci = new Models.CartItem();
            ci.Id = pid.Value;

            //若購物車已有該產品，直接前往購物車清單頁面
            if (myCart.Contains(ci)) return RedirectToAction("MyShoppingCart");


            //若購物車無該項目，則資料庫查詢，查詢該產品相關資料
            NWDB.NWDB mydb = new NWDB.NWDB();
            NWDB.IProduct ip = new NWDB.Impl_Product(mydb.Connection);
            Dictionary<string, object> pp = ip.GetSingleProduct(pid.Value);

            if (pp == null)    //無此產品
                return RedirectToAction("Index");

            //若查到改產品項目
            ci.Id = Convert.ToInt32(pp["產品編號"]);
            ci.Name = pp["產品"].ToString();
            ci.Unit = pp["單位數量"].ToString();
            ci.Stock = Convert.ToInt32(pp["庫存量"]);
            ci.Quantity = 1;
            ci.Price = Convert.ToDouble(pp["單價"]);
            myCart.Add(ci);
            Session["Cart"] = myCart;       //回存購物車至Session                        
            return RedirectToAction("MyShoppingCart");
        }

        public ActionResult MyShoppingCart()
        {
            if (Session["Cart"] == null) return RedirectToAction("Index");

            HashSet<Models.CartItem> myCart =
                Session["Cart"] as HashSet<Models.CartItem>;

            int shipping = 0;
            double amount = 0;
            int total = 0;

            foreach (Models.CartItem ii in myCart)
            {
                amount += ii.SubTotal;
            }
            
            //運費計算
            if (amount < 1000)
                shipping = 150;
            else if (amount < 2000)
                shipping = 80;

            total = (int)Math.Round(amount) + shipping;

            ViewData["Shipping"] = shipping;
            ViewData["Amount"] = (int)Math.Round(amount);
            ViewData["Total"] = total;

            return View();
        }


        public ActionResult ClearCart()
        {
            Session["Cart"] = null;
            Session.Remove("Cart");
            return RedirectToAction("Index");
        }

        public ActionResult UpdateCart()
        {
            if (Session["Cart"] == null) return RedirectToAction("Index");

            HashSet<Models.CartItem> myCart =
                Session["Cart"] as HashSet<Models.CartItem>;

            foreach (Models.CartItem ci in myCart)
            {
                int num = int.Parse(Request.Form["qq_" + ci.Id]);
                ci.Quantity = num;
            }

            Session["Cart"] = myCart;       //回存購物車至Session 
            return RedirectToAction("MyShoppingCart");
        }


    }
}