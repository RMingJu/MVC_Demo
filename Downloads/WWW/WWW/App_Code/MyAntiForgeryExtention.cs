using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;

namespace WWW.App_Code
{
    public class MyAntiForgeryExtention : IAntiForgeryAdditionalDataProvider
    {
        //要驗證的資料
        public string GetAdditionalData(HttpContextBase context)
        {
            return DateTime.Now.Ticks.ToString();
        }

        //要驗證的資料的驗證方法
        public bool ValidateAdditionalData(HttpContextBase context, string additionalData)
        {
            //如果時間小於60秒回傳true
            bool ans = false;
            if (String.IsNullOrEmpty(additionalData)) return false;

            long a = long.Parse(additionalData);
            long b = DateTime.Now.Ticks;
            TimeSpan ts = new TimeSpan(b - a);
            if (ts.TotalSeconds <= 60)
                ans = true;
            else
                ans = false;
            return ans;

        }
    }
}