using System;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;
using Reservation.UI.MVC.Models;

namespace Reservation.UI.MVC.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult About()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Rooms()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Restuarant()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact(ContactViewModel cvm)
        {
            if (!ModelState.IsValid)
            {
                return View(cvm);
            }
            #region Send Email Functionality
            string message = $"You have received an email from {cvm.Name} with the subject {cvm.Subject}. Please respond to {cvm.Email} with your response to the following message: <br><cite>{cvm.Message}</cite>";

            MailMessage msg = new MailMessage("admin@christopherahinkle.com", "christopherahinkle@outlook.com", cvm.Subject, message);
            msg.IsBodyHtml = true;

            SmtpClient client = new SmtpClient("mail.christopherahinkle.com");
            client.Credentials = new NetworkCredential("admin@christopherahinkle.com", "Tyson120!");

            try
            {
                client.Send(msg);
            }
            catch (Exception ex)
            {

                ViewBag.ErrorMessage = $"Sorry, something went wrong. Please try again later or review the stacktrace <br>{ex.StackTrace}";
                return View(cvm);
            }

            return View("EmailConfirmation", cvm);
            #endregion
        }
    }
}
