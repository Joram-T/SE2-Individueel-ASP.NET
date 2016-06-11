using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SE2_Individueel.Models
{
    public class Gebruiker
    {
        public string Gebruikersnaam { get; }
        public string Voornaam { get; }
        public string Tussenvoegsel { get; }
        public string Achternaam { get; }
        public string Email { get; }
        public DateTime Geboortedatum { get; }
        public string Land { get; }
        public string Postcode { get; }
        public string Geslacht { get; }
        public string Telefoonnnummer { get; }
        public string Soort { get; }
        public List<Models.Afspeellijst> afspeellijsten;

        public Gebruiker(string gebruikersnaam, string voornaam, string tussenvoegsel, string achternaam, string email, DateTime geboortedatum, string land, string postcode, string geslacht, string telefoonnummer, string soort)
        {
            this.Gebruikersnaam = gebruikersnaam;
            this.Voornaam = voornaam;
            this.Tussenvoegsel = tussenvoegsel;
            this.Achternaam = achternaam;
            this.Email = email;
            this.Geboortedatum = geboortedatum;
            this.Land = land;
            this.Postcode = postcode;
            this.Geslacht = geslacht;
            this.Telefoonnnummer = telefoonnummer;
            this.Soort = soort;
            afspeellijsten = new List<Afspeellijst>();
        }

    }
}