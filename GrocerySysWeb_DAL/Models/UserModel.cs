using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GrocerySysWeb_DAL.Models
{
    public class UserModel
    {
        public string UserEmail { get; set; }
        public string UserName { get; set; }
        public string UserMobile { get; set; }
        public string UserPassword { get; set; }
        public string UserAddress { get; set; }
    }
}