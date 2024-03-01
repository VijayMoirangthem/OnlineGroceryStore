using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GrocerySysWeb_DAL.Models
{
    public class InventoryModel
    {
        public string ProductID { get; set; }
        public string ProductName { get; set; }
        public string ProductType { get; set; }
        public int ProductQuantity { get; set; }
        public string ProductBrand { get; set; }
        public int ProductPrice { get; set; }
        public byte[] ProductImage { get; set; }
    }
}