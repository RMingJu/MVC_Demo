using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WWW.Models
{
    public class ProductInfo
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int SupplyID { get; set; }
        public string SupplyName { get; set; }
        public int TypeID { get; set; }
        public string TypeName { get; set; }
        public double Price { get; set; }
        public string Unit { get; set; }
        public int Stock { get; set; }
        public bool? IsSellOrNot { get; set; }
        public byte[] Pic { get; set; }
    }
}