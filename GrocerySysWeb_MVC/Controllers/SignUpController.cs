using GrocerySysWeb_MVC.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace GrocerySysWeb_MVC.Controllers
{
    public class SignUpController : Controller
    {
        string apiURL = "https://localhost:44390/api";
        public ActionResult SignUp()
        {

            return View();
        }

        public int randomNumber()
        {
            Random random = new Random();
            int randomNumber = random.Next(100000, 999999);
            return randomNumber;
        }

        public void SendEmail(string to, string subject, string body)
        {
            // Replace these values with your own SMTP server details
            string smtpServer = "smtp.gmail.com";
            int smtpPort = 587;
            string smtpUsername = "mahajansiddant@gmail.com"; // Replace with your Gmail email address
            string smtpPassword = "zxxk ietb yghy wjrp"; // Replace with your Gmail password

            // Create a MailMessage
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("mahajansiddant@gmail.com");
            mail.To.Add(new MailAddress(to));
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = false; // Change to true if your body contains HTML

            // Create a SmtpClient
            SmtpClient smtpClient = new SmtpClient(smtpServer, smtpPort);
            smtpClient.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
            smtpClient.EnableSsl = true;

            
            smtpClient.Send(mail);
            
        }

        

        [HttpPost]
        public ActionResult SendOtp(string UserEmail)
        {
            MailMessage mail = new MailMessage();
            int otp = randomNumber();
            string subject = "Your One-Time Password (OTP) for Secure Forget About IT(FAIT)";
            string body = $"Dear {UserEmail},\r\n\r\nThank you for choosing Forget About IT for your online grocery needs. To ensure the security of your account, we have generated a one-time password (OTP) for you.\r\n\r\nYour OTP is: {otp}\r\n\r\nPlease use this OTP to complete your login process and enjoy a seamless shopping experience on our website.\r\n\r\nThank you for trusting Forget About IT. We look forward to serving you.\r\n\r\nBest regards,\r\nForget About IT Team";
            SendEmail(UserEmail, subject, body);
            TempData["UserEmail"] = UserEmail;
            TempData["OTP"] = otp;
            return RedirectToAction("SignUp");
        }

        [HttpPost]
        public async Task<ActionResult> addUserAsync(string UserName , string UserMobile, string UserPassword, string UserAddress, int enteredOTP)
        {
            int otp1 = (int)TempData["OTP"];
            if (otp1 == enteredOTP)
            {
                UserModel user = new UserModel();
                user.UserName = UserName;
                user.UserPassword = UserPassword;
                user.UserAddress = UserAddress;
                user.UserMobile = UserMobile;
                user.UserEmail = TempData["UserEmail"] as string;

                using (HttpClient client = new HttpClient())
                {
                    var json = JsonConvert.SerializeObject(user);
                    var data = new StringContent(json, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync($"{apiURL}/UserApi", data);
                }
                return RedirectToAction("Login" , "Login");
            }
            return RedirectToAction("SignUp");
        }
    }
}