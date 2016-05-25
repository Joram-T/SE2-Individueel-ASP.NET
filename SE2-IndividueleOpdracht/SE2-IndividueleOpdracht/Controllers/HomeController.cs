using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SE2_IndividueleOpdracht.Controllers
{
    public class HomeController : Controller
    {

        public SE2_IndividueleOpdracht.Models.Spotifoe spotifoe = new SE2_IndividueleOpdracht.Models.Spotifoe();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Test()
        {
            ViewBag.Message = "Test jwz.";

            return View();
        }

        public ActionResult Afspeellijsten()
        {
            ViewBag.Data = spotifoe;

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}