using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SE2_IndividueleOpdracht.Models
{
    public class Afspeellijst
    {
        public int ID { get; set; }
        public string Naam { get; set; }
        public TimeSpan Lengte { get; set; }

        public Afspeellijst(int id, string naam)
        {
            ID = id;
            Naam = naam;
        }
    }

}