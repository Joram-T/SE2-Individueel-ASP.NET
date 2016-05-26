using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SE2_Individueel.Controllers
{
    

    public class AfspeellijstController : Controller
    {
        public SE2_Individueel.Models.Spotify spotify = new SE2_Individueel.Models.Spotify();

        // GET: Afspeellijst
        public ActionResult Index()
        {
            ViewBag.Data = spotify;

            return View();
        }

        public ActionResult Afspeellijst()
        {
            ViewBag.Data = spotify;
            return View();
        }

        public ActionResult NieuweAfspeellijst(string afspeellijstnaam)
        {
            //if (true)
            //{
            //    spotify.newAfspeellijst("Test");
            //    return RedirectToAction("Index");
            //}

            ViewBag.Data = spotify;

            return View();
        }
    }
}