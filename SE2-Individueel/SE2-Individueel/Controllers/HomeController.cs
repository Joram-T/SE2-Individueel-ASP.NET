using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SE2_Individueel.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            if ((Session["Gebruiker"] == null))
            {
                return RedirectToAction("Index", "Login", new { area = "" });
            }
            return View();
        }
    }
}