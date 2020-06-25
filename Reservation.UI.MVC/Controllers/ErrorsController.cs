using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using Reservation.UI.MVC.Exceptions;

namespace Reservation.UI.MVC.Controllers
{
    public class ErrorsController : Controller
    {
        // GET: Errors
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CustomError()
        {
            return View();
        }
    }
}