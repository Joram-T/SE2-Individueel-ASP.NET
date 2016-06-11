using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SE2_Individueel.Models
{
    public class LoginController : Controller
    {
        Models.Database database = new Database();
        // GET: Login
        public ActionResult Index(string gebruikersnaam, string wachtwoord)
        {
            if (string.IsNullOrEmpty(gebruikersnaam) && string.IsNullOrEmpty(wachtwoord))
            {
                ViewBag.foutmelding = "";
            }

            else if (database.Inloggen(gebruikersnaam, wachtwoord).Gebruikersnaam.ToLower() == gebruikersnaam.ToLower())
            {
                Session["Gebruiker"] = database.Inloggen(gebruikersnaam, wachtwoord);
                
                return RedirectToAction("Index", "Home", new { area = "" });
            }

            else
            {
                ViewBag.foutmelding = "Gebruikersnaam of Wachtwoord is incorrect";
            }

            return View();
        }

        public ActionResult Registratie()
        {
            return View();
        }
    }
}