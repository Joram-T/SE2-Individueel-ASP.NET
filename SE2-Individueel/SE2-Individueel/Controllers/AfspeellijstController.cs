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
            if((Session["Gebruiker"] == null))
            {
                return RedirectToAction("Index", "Login", new { area = "" });
            }
            ViewBag.Data = spotify;

            return View();
        }

        public ActionResult Afspeellijst(int id)
        {
            if ((Session["Gebruiker"] == null))
            {
                return RedirectToAction("Index", "Login", new { area = "" });
            }
            ViewBag.ID = id;
            ViewBag.Data = spotify;
            foreach(Models.Afspeellijst afspeellijst in spotify.database.Afspeellijsten((Session["Gebruiker"] as Models.Gebruiker).Gebruikersnaam ))
            {
                if(afspeellijst.ID == id)
                {
                    ViewBag.Afspeellijst = afspeellijst;
                }
            }

            return View();
        }

        public ActionResult NieuweAfspeellijst(string afspeellijstnaam)
        {
            if ((Session["Gebruiker"] == null))
            {
                return RedirectToAction("Index", "Login", new { area = "" });
            }
            ViewBag.Data = spotify;
            if (!string.IsNullOrEmpty(afspeellijstnaam))
            {
                spotify.database.AfspeellijstMaken(afspeellijstnaam, (Session["Gebruiker"] as Models.Gebruiker).Gebruikersnaam);
                return RedirectToAction("Index");
            }

            return View();
        }
    }
}