using GrocerySysWeb_MVC.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace GrocerySysWeb_MVC.Controllers
{
    public class ProductController : Controller
    {
        string apiURL = "https://localhost:44390/api";
        string apiURL2 = "https://localhost:44327/api";
        public ActionResult Products()
        {
            string type = Session["type"] as  string;
            using (HttpClient client = new HttpClient())
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"{apiURL}/ProductTypeApi?ProductType={type}");
                HttpResponseMessage response = client.SendAsync(request).Result;
                JavaScriptSerializer serializer = new JavaScriptSerializer();

                string jsonResponse = response.Content.ReadAsStringAsync().Result;

                ViewBag.ProductList = JsonConvert.DeserializeObject<List<InventoryModel>>(jsonResponse);

                return View();
            }
        }

        [HttpPost]
        public ActionResult addToCart(string ProductID)
        {
            string CustomerID = Session["Email"] as string;
            using (HttpClient client = new HttpClient())
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, $"{apiURL2}/CartApi?CustomerID={CustomerID}&ProductID={ProductID}");
                HttpResponseMessage response = client.SendAsync(request).Result;
            }
            return RedirectToAction("Products");
        }
    }
}