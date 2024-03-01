using GrocerySysWeb_DAL.Models;
using GrocerySysWeb_DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GrocerySysWeb_WebApi.Controllers
{
    
    public class UserApiController : ApiController
    {
        UserRepository UserREPO = new UserRepository();

        [HttpGet]
        public UserModel verifyUser(string Email, string Password)
        {
            if(UserREPO.verifyUser(Email , Password) != null)
            {
                return UserREPO.verifyUser(Email, Password);
            }
            return null;
        }

        [HttpPost]
        public void insertUser(UserModel User)
        {
            UserREPO.insertUser(User);  
        }
    }
}
