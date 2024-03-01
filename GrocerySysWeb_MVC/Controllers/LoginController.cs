using GrocerySysWeb_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Mail;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace GrocerySysWeb_MVC.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login

        string apiURL = "https://localhost:44390/api";
        public ActionResult Login()
        {

            return View();
        }


        [HttpPost]
        public ActionResult verifyLogin(string UserEmail, string UserPassword)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"{apiURL}/UserApi?Email={UserEmail}&Password={UserPassword}");
                HttpResponseMessage response = client.SendAsync(request).Result;
                
                UserModel isSuccess = (new JavaScriptSerializer()).Deserialize<UserModel>(response.Content.ReadAsStringAsync().Result);

                string DBEmail = isSuccess.UserEmail;
                string DBPassword = isSuccess.UserPassword;


                if (DBEmail == UserEmail  &&  DBPassword == UserPassword)
                {
                    Session["Email"] = UserEmail;
                    Session["IsLoggedIn"] = true;
                    return RedirectToAction("Index", "Home");
                }

                ViewBag.ErrorMessage = "Invalid credentials";
                return RedirectToAction("Login");
            }
        }


       
        public ActionResult logout()
        {
            Session["Email"] = null;
            Session["IsLoggedIn"] = false;
            return RedirectToAction("Login");
        }
    }
}