using GrocerySysWeb_MVC.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace GrocerySysWeb_MVC.Controllers
{
    public class HomeController : Controller
    {
        string apiURL = "https://localhost:44327/api";
        string apiURL2 = "https://localhost:44390/api";
        public ActionResult Index()
        {
            List<ImageModel> ImageList = new List<ImageModel>();
            using (HttpClient client = new HttpClient())
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"{apiURL}/ImageApi");
                HttpResponseMessage response = client.SendAsync(request).Result;


                if (response.IsSuccessStatusCode)
                {
                    // Ensure successful response before attempting deserialization
                    string jsonResponse = response.Content.ReadAsStringAsync().Result;

                    // Use Newtonsoft.Json for deserialization (Json.NET)
                    ImageList = JsonConvert.DeserializeObject<List<ImageModel>>(jsonResponse);
                }

            }
            ViewBag.ImageList = ImageList;


            /***************************************************************************************************/

            using (HttpClient client = new HttpClient())
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"{apiURL2}/ProductTypeApi");
                HttpResponseMessage response = client.SendAsync(request).Result;


                if (response.IsSuccessStatusCode)
                {
                    // Ensure successful response before attempting deserialization
                    string jsonResponse = response.Content.ReadAsStringAsync().Result;

                    // Use Newtonsoft.Json for deserialization (Json.NET)
                    ViewBag.TypeList = JsonConvert.DeserializeObject<List<String>>(jsonResponse);
                }

            }


            /*************************************************************************************************************/


            return View();
        }

        public ActionResult AddTypeToSession(string type)
        {
            // Check if the session variable exists, create it if not
           

            // Get the existing values from the session variable
            Session["Type"] = type;

            // Redirect to a different action or view
            return RedirectToAction("Products", "Product"); // Change "Index" to the desired action or view
        }

    }
}